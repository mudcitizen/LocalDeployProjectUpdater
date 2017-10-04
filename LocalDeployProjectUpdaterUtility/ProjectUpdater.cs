using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.Build.Evaluation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public class ProjectUpdater
    {
        readonly String _ProjectItemType;

        public ProjectUpdater()
        {
            _ProjectItemType = ConfigurationManager.AppSettings[Constants.DefaultProjectItemType];
            if (_ProjectItemType == null)
                _ProjectItemType = Constants.DefaultProjectItemType;
        }

        public void Update(String csProjFileName, String vfpDirectoryName, String moduleParametersFileName)
        {

            ModuleParameters moduleParms;
            ModuleParametersPersistor persistor = new ModuleParametersPersistor();
            if (File.Exists(moduleParametersFileName))
            {
                moduleParms = persistor.Read(moduleParametersFileName);
            }
            else
            {
                moduleParms = new HostModuleParametersProvider().GetModuleParameters();
                persistor.Write(moduleParametersFileName, moduleParms);
            }

            Project project = new Project(csProjFileName);
            String projectFolderName = Path.GetDirectoryName(csProjFileName);

            // Make sure the H folder exists 
            String contentFolderName = Path.Combine(projectFolderName, moduleParms.ContentSubFolder);

            if (!Directory.Exists(contentFolderName))
                Directory.CreateDirectory(contentFolderName);

            // Remove current content from Project
            IEnumerable<ProjectItem> currentContent = project.Items
                .Where(pi => (pi.ItemType == _ProjectItemType) && (pi.EvaluatedInclude.StartsWith(moduleParms.ContentSubFolder + Path.DirectorySeparatorChar)))
                .OrderBy(pi => pi.EvaluatedInclude);
            project.RemoveItems(currentContent);


            // Remove current content from Directory
            foreach (String fileName in Directory.GetFiles(contentFolderName))
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }


            //TODO get rid of these constants 
            IEnumerable<KeyValuePair<String, String>> metadata = new List<KeyValuePair<String, String>>() { new KeyValuePair<string, string>("CopyToOutputDirectory", "PreserveNewest") };
            //TODO Filter out custom forms etc


            foreach (String fileName in Directory.GetFiles(vfpDirectoryName))
            {
                String justFileName = Path.GetFileName(fileName).ToUpper();
                String csProjItemName = Path.Combine(moduleParms.ContentSubFolder, justFileName);
                String copyToFileName = Path.Combine(contentFolderName, justFileName);
                File.Copy(fileName, copyToFileName);
                project.AddItem(_ProjectItemType, Path.Combine(moduleParms.ContentSubFolder, justFileName), metadata);
            }

            project.Save();

        }
    }
}
