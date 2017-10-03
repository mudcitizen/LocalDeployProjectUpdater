using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{

    //[Serializable]
    public class ModuleParameters 
    {
        public string ContentSubFolder = String.Empty;
        public List<string> ExcludedFiles = new List<String>();
    }

}
