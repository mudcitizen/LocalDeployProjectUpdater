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

        public static class ProjectItemMetadata
        {
            public const String CopyToOutputDirectoryName = "CopyToOutputDirectory";
            public const String CopyToOutputDirectoryValue = "PreserveNewest";
        }

        public static class ProjectProperties {
            public static readonly IEnumerable<String> Versions = new List<String>() { "ApplicationVersion","MinimumRequiredVersion" };
            public static readonly IEnumerable<String> ProductNames = new List<String>() { "ProductName", "SuiteName" };
        }
        public static class WTF
        {
            public static readonly String NonList = "Not a list";
        }

    }
}
