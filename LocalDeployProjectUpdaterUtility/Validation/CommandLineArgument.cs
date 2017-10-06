using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public class CommandLineArgument : IValidator
    {
        protected String _Name;
        protected IValidator _Validator;

        public String Name { get { return _Name; } }

        public CommandLineArgument(String name) : this(name, null) { }

        public CommandLineArgument(String name,IValidator validator)
        {
            _Name = name;
            _Validator = validator;
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
