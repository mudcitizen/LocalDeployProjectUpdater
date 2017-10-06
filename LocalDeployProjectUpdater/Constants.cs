using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdater
{
    public static class Constants
    {
        public static class ArgumentNames
        {
            public const String CsProjectFileName = ".NET Project File Name";
            public const String VfpDirectoryName = "VFP Directory Name";
            public const String ModuleParameterFileName = "Module Parameters File Name";
        }

        public static class MessageText
        {
            public const String WrongParmCount = "3 parameters are required:";
            public const String RequiredParameter = "Required Parameter";
            public const String FileNotFound = "File Not Found";
            public const String DirectoryNotFound = "Directory Not Found";
            public const String NotAValidProjectFile = "Not a valid .NET project file";
            public const String NotAValidModuleParametersFile = "Not a valid Module Parameters file";
        }
    }
}
