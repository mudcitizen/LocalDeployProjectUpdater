using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;
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


            updater.Update(_CsProjectFileName, Constants.VfpProjectFolderName, moduleParmsFileName);
            
            Assert.IsTrue(File.Exists(moduleParmsFileName));

        }

        [TestMethod]
        public void SpewProjectProperties()
        {
            Project proj = new Project(_CsProjectFileName);
            ILogger logger = new TextFileLogger(@"c:\temp\ProjectProperties.txt");
            foreach (ProjectProperty pp in proj.Properties.OrderBy(pp => pp.Name)) {
                String s = String.Format("Name - {0} ; Value = {1}", pp.Name, pp.EvaluatedValue);
                logger.Log(s);
                Debug.WriteLine(s);
            }
        }

    }
}
