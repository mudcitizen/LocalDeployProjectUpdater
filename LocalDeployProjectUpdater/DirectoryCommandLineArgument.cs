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
        public DirectoryCommandLineArgument(string name) : base(name)
        {
        }

        public override string Validate(string value)
        {
            String msg = base.Validate(value);
            if (String.IsNullOrEmpty(msg))
                if (!Directory.Exists(value))
                    msg = GetMessage(Constants.MessageText.DirectoryNotFound);
            return msg;
        }
    }
}
