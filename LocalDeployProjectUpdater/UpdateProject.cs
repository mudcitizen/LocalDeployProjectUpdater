using System;
using System.Collections.Generic;
using LocalDeployProjectUpdaterUtility;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdater
{
    public class UpdateProject
    {
        static void Main(string[] args)
        {

            String messages = ValidateArgs(args);
            if (String.IsNullOrEmpty(messages))
            {
                WriteLines("Processing");
                new ProjectUpdater().Update(args[0], args[1], args[2]);
                WriteLinesAndPressAnyKey("Process complete");
            }
            else
            {
                HandleInvalidParameters(messages);
            }

        }

  
        static void HandleInvalidParameters(String txt)
        {
            WriteLinesAndPressAnyKey(txt);
        }

        static void WriteLinesAndPressAnyKey(String text)
        {
            WriteLines(text);
            PressAnyKey();
        }

        static void WriteLines(String text)
        {
            foreach (String line in StringToLines(text))
                Console.WriteLine(line);
            PressAnyKey();
        }

        static IEnumerable<String> StringToLines(String text)
        {
            return text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        static void PressAnyKey()
        {
            Console.WriteLine("Press any key");
            Console.ReadLine();
        }

        public static String ValidateArgs(String[] args)
        {
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
