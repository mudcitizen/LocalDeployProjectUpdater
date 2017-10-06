using System;
using LocalDeployProjectUpdaterUtility;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public class ArgsValidator
    {
        public String Validate(String[] args)
        {

            const int parmCount = 3;
            const int csProjFileNameIndex = 0;
            const int vfpDirectoryNameIndex = 1;
            const int moduleParameterFileNameIndex = 2;


            IList<CommandLineArgument> cmdLineArgs = new List<CommandLineArgument>()
            {

                new FileNameCommandLineArgument(Constants.ArgumentNames.CsProjectFileName,new ProjectFileValidator()),
                //new FileNameCommandLineArgument(Constants.ArgumentNames.CsProjectFileName),
                new DirectoryCommandLineArgument(Constants.ArgumentNames.VfpDirectoryName),
                new FileNameCommandLineArgument(Constants.ArgumentNames.ModuleParameterFileName)
            };

            StringBuilder sb;
            // Wrong number of args?
            if (args.Length != parmCount)
            {
                sb = new StringBuilder();
                sb.AppendLine(Constants.MessageText.WrongParmCount);
                foreach (CommandLineArgument cla in cmdLineArgs)
                    sb.AppendLine(String.Format(" - {0}", cla.Name));
                return sb.ToString();
            }

            // Right number of args ; basic validation on each arg
            sb = new StringBuilder();
            for (int i = 0; i < parmCount; i++)
            {
                String msg = cmdLineArgs[i].Validate(args[i]);
                if (!String.IsNullOrEmpty(msg))
                    sb.AppendLine(msg);
            }

            if (String.IsNullOrEmpty(sb.ToString()))
            {
                // If the ModuleParameters has a RequiredFileTypes list see if the directory fills the bill
                ModuleParameters parms = new ModuleParametersPersistor().Read(args[moduleParameterFileNameIndex]);
                if ((parms.RequiredFileTypes != null) && (parms.RequiredFileTypes.Count > 0))
                {
                    IValidator v = new RequiredFileTypesDirectoryValidator(parms.RequiredFileTypes);
                    String msg = v.Validate(args[vfpDirectoryNameIndex]);
                    if (!String.IsNullOrEmpty(msg))
                        sb.AppendLine(msg);
                }

            }

            return sb.ToString();

        }
    }
}
