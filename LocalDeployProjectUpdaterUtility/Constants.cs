using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public static class Constants
    {
        public const String DefaultProjectItemType = "Content";
        public const String AppSettingsProejctItemTypeKey = "ProjectItemType";

        public static class ArgumentNames
        {
            public const String CsProjectFileName = ".NET Project File Name";
            public const String VfpDirectoryName = "VFP Directory Name";
            public const String ModuleParameterFileName = "Module Parameters File Name";
        }

        public static class ProjectItemMetadata
        {
            public const String CopyToOutputDirectoryName = "CopyToOutputDirectory";
            public const String CopyToOutputDirectoryValue = "PreserveNewest";
        }

        public static class ProjectProperties {
            public static readonly IEnumerable<String> Versions = new List<String>() { "ApplicationVersion","MinimumRequiredVersion" };
            public static readonly IEnumerable<String> ProductNames = new List<String>() { "ProductName", "SuiteName" };
        }

        public static class MessageText
        {
            public const String WrongParmCount = "3 parameters are required:";
            public const String RequiredParameter = "Required parameter not supplied";
            public const String FileNotFound = "File Not found";
            public const String DirectoryNotFound = "Directory not Found";
            public const String NotAValidProjectFile = "Not a valid .NET project file";
            public const String NotAValidModuleParametersFile = "Not a valid ModuleParameters file";
            public const String RequiredFileTypesNotPresent = "Required file types are not present";
        }

    }
}
