﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{

    //[Serializable]
    public class ModuleParameters
    {
        public string ContentSubFolder { get; set; }
        public List<string> ExcludedFiles { get; set; }

        public ModuleParameters()
        {
            ContentSubFolder = String.Empty;
            ExcludedFiles = new List<String>();
        }
    }

}
