using System;
using System.IO;
using LocalDeployProjectUpdaterUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalDeployProjectUpdaterUtility.Tests
{
    [TestClass]
    public class ProjectUpdaterTest
    {
        const String _CsProjectFileName = @"C:\ClickOnceSolution\vh\vh.csproj";

        [TestInitialize]
        public void Setup() { }

        [TestCleanup]
        public void TearDown() { }

        [TestMethod]
        public void TestUpdateVerifyHostParmsXmlFileCreated()
        {
            ProjectUpdater updater = new ProjectUpdater();

            const string moduleParmsFileName = @"C:\temp\HostModuleParms.xml";
            if (File.Exists(moduleParmsFileName))
                File.Delete(moduleParmsFileName);


            ProjectDifferences diffs = updater.Update(_CsProjectFileName, Constants.VfpProjectFolderName, moduleParmsFileName);
            
            Assert.IsTrue(File.Exists(moduleParmsFileName));

        }
    }
}
