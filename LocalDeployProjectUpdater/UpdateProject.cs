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

            /*
             * C:\ClickOnceSolution\vh\vh.csproj C:\temp\VfpProject C:\temp\HostModuleParms.xml
             */

            String messages = new ArgsValidator().Validate(args);

            if (String.IsNullOrEmpty(messages))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Processing");
                sb.AppendLine();
                sb.AppendLine(String.Format("{0} - {1}", Constants.ArgumentNames.CsProjectFileName, args[Constants.ArgumentIndexes.CsProjectFileNameIndex]));
                sb.AppendLine(String.Format("{0} - {1}", Constants.ArgumentNames.VfpDirectoryName, args[Constants.ArgumentIndexes.VfpDirectoryNameIndex]));
                sb.AppendLine(String.Format("{0} - {1}", Constants.ArgumentNames.ModuleParameterFileName, args[Constants.ArgumentIndexes.ModuleParameterFileNameIndex]));
                sb.AppendLine();

                WriteLines(sb.ToString());
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

    }
}
