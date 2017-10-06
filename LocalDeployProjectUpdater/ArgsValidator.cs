using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdater
{
    public class ArgsValidator
    {
        public String Validate(String[] args) {

            const int parmCount = 3;

            IList<CommandLineArgument> cmdLineArgs = new List<CommandLineArgument>()
            {
                new FileNameCommandLineArgument(Constants.ArgumentNames.CsProjectFileName),
                new DirectoryCommandLineArgument(Constants.ArgumentNames.VfpDirectoryName),
                new FileNameCommandLineArgument(Constants.ArgumentNames.ModuleParameterFileName)
            };

            StringBuilder sb;
            if (args.Length != parmCount)
            {
                sb = new StringBuilder();
                sb.AppendLine(Constants.MessageText.WrongParmCount);
                foreach (CommandLineArgument cla in cmdLineArgs)
                    sb.AppendLine(String.Format(" - {0}", cla.Name));
                return sb.ToString();
            }

            sb = new StringBuilder();
            for (int i = 0; i < parmCount; i++)
            {
                String msg = cmdLineArgs[i].Validate(args[i]);
                if (!String.IsNullOrEmpty(msg))
                    sb.AppendLine(msg);
            }

            return sb.ToString();

        }
    }
}
