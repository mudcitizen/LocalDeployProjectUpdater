using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public class FileNameCommandLineArgument : CommandLineArgument
    {
        public FileNameCommandLineArgument(string name) : base(name) {}
        public FileNameCommandLineArgument(string name,IValidator validator) : base(name, validator) { }

        public override string Validate(string value)
        {
            String message = base.Validate(value);
            if (String.IsNullOrEmpty(message)) {
                if (!File.Exists(value))
                    message = GetMessage(value,Constants.MessageText.FileNotFound);
            }

            if ((String.IsNullOrEmpty(message)) && (_Validator != null))
                message = _Validator.Validate(value);

            return message;
        }
    }
}
