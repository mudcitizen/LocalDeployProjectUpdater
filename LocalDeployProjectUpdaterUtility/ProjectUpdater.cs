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
        IMismatchFinder _MismatchFinder;
        readonly String _FileItemType;

        public ProjectUpdater() : this(new MismatchFinder()) { }

        public ProjectUpdater(IMismatchFinder mismatchFindfer) {
            _MismatchFinder = mismatchFindfer;
            _FileItemType = ConfigurationManager.AppSettings[Constants.DefaultFileItemType];
            if (_FileItemType == null)
                _FileItemType = Constants.DefaultFileItemType;
        }

        public ProjectDifferences Update(String csProjFileName, String vfpDirectoryName, String moduleParametersFileName) {

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

            IEnumerable<String> vfpProjFiles = Directory.GetFiles(vfpDirectoryName);
            /*
             * IList<ProjectItem> contentItems = project.Items.Where(pi => pi.ItemType == "Content" && pi.EvaluatedInclude.StartsWith(@"h\")).OrderBy(pi => pi.EvaluatedInclude).Select(pi => pi).ToList();
            IEnumerable<String> fileNames = contentItems.Select(ci => Path.GetFileName(ci.EvaluatedInclude)).ToList();
             */
            Project project = new Project(csProjFileName);
            IEnumerable<String> csProjFiles = GetProjectContentFiles(project, moduleParms);

            ProjectDifferences projDiffs = new ProjectDifferencesProvider(_MismatchFinder).GetDifferences(csProjFiles, vfpProjFiles);

            //TODO get rid of these constants 
            KeyValuePair<String, String> kvp = new KeyValuePair<string, string>("CopyToOutputDirectory", "PreserveNewest");
            IEnumerable<KeyValuePair<String, String>> metadata = new List<KeyValuePair<String, String>>() { kvp };
            //TODO Filter out custom forms etc

            // Make sure the H folders 
            String contentFolderName = Path.GetDirectoryName(csProjFileName) + Path.DirectorySeparatorChar + moduleParms.ContentSubFolder;
            if (!Directory.Exists(contentFolderName))
                Directory.CreateDirectory(contentFolderName);

            foreach (String fileName in projDiffs.Additions) {
                String csProjItemName = (moduleParms.ContentSubFolder + Path.DirectorySeparatorChar + fileName).ToUpper();
                String copyToFileName = contentFolderName + Path.DirectorySeparatorChar + fileName;
                String copyFromFileName = vfpDirectoryName + Path.DirectorySeparatorChar + fileName;
                File.Copy(copyFromFileName, copyToFileName);
                project.AddItem(_FileItemType, moduleParms.ContentSubFolder + Path.DirectorySeparatorChar + fileName, metadata);
            }

            project.Save();

            return projDiffs;
        }

        public IEnumerable<String> GetProjectContentFiles(Project project, ModuleParameters moduleParms) {
            return project.Items
                .Where(pi => (pi.ItemType == _FileItemType) && (pi.EvaluatedInclude.StartsWith(moduleParms.ContentSubFolder + Path.DirectorySeparatorChar)))
                .OrderBy(pi => pi.EvaluatedInclude)
                .Select(pi => Path.GetFileName(pi.EvaluatedInclude))
                .ToList();
        }

    }
}
