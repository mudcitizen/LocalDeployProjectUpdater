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
            return new ModuleParameters() { ContentSubFolder = "P" };
        }
    }
}
