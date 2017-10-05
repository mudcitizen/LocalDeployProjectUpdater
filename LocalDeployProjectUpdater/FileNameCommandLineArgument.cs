using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdater
{
    public class FileNameCommandLineArgument : CommandLineArgument
    {
        public FileNameCommandLineArgument(string name) : base(name) {}

        public override string Validate(string value)
        {
            String message = base.Validate(value);
            if (String.IsNullOrEmpty(message)) {
                if (!File.Exists(value))
                    message = GetMessage(Constants.MessageText.FileNotFound);
            }
            return message;
        }
    }
}
