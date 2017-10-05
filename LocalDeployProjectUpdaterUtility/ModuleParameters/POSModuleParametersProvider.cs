using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public class POSModuleParametersProvider : IModuleParametersProvider
    {
        public ModuleParameters GetModuleParameters()
        {
            ModuleParameters parms = new ModuleParameters() { ContentSubFolder = "P" };
            parms.ProductName = "POS";
            return parms;
        }
    }
}
