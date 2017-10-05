using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdater
{
    public class CommandLineArgument
    {
        protected String _Name;

        public String Name { get { return _Name; } }

        public CommandLineArgument(String name)
        {
            _Name = name;
        }

        virtual public String Validate(String value)
        {
            if (String.IsNullOrEmpty(value))
                return GetMessage(Constants.MessageText.RequiredParameter);
            else
                return String.Empty;
        }

        protected String GetMessage(String txt) {
            return String.Format("{0} - {1}", _Name,txt);
        }

    }
}
