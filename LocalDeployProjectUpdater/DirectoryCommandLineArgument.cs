using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdater
{
    public class DirectoryCommandLineArgument : CommandLineArgument
    {
        public DirectoryCommandLineArgument(string name) : base(name) {}
        public DirectoryCommandLineArgument(string name,IValidator validator) : base(name, validator) { }

        public override string Validate(string value)
        {
            String message = base.Validate(value);
            if (String.IsNullOrEmpty(message))
                if (!Directory.Exists(value))
                    message = GetMessage(Constants.MessageText.DirectoryNotFound);

            if ((String.IsNullOrEmpty(message)) && (_Validator != null))
                message = _Validator.Validate(value);

            return message;
        }
    }
}
