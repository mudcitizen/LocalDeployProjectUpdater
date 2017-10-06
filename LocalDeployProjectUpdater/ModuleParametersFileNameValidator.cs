using System;
using LocalDeployProjectUpdaterUtility;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdater
{
    public class ModuleParametersFileNameValidator : IValidator
    {
        public string Validate(string value)
        {
            String message;
            message = String.Empty;

            try
            {
                ModuleParametersPersistor persistor = new ModuleParametersPersistor();
                ModuleParameters parms = persistor.Read(value);
            }
            catch (Exception ex)
            {
                message = Constants.MessageText.NotAValidModuleParametersFile + " - " + value + " " + ex.ToString();
            }
            return message;
        }
    }
}
