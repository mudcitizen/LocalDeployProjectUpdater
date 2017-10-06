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

            String messages = new ArgsValidator().Validate(args);

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

    }
}
