using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalDeployProjectUpdaterUtility;
using LocalDeployProjectUpdaterUtility;

namespace LocalDeployProjectUpdater.Experiments
{
    [TestClass]
    public class TestSerializer
    {
        [TestMethod]
        public void TestWrite()
        {
            const String fileName = @"C:\temp\HostParms.xml";

            if (File.Exists(fileName))
                File.Delete(fileName);
            ModuleParameters parms = new HostModuleParametersProvider().GetModuleParameters();

            //XmlSerializer s = new XmlSerializer(typeof(ModuleParameters));
            //using (TextWriter w = new StreamWriter(fileName)) {
            //    s.Serialize(w, parms);
            //}
            ModuleParametersPersistor p = new ModuleParametersPersistor();
            p.Write(fileName, parms);

            Assert.IsTrue(File.Exists(fileName));

            ModuleParameters readParms = p.Read(fileName);
            Assert.AreEqual(parms.ContentSubFolder, readParms.ContentSubFolder);
            Assert.AreEqual(parms.ExcludedFiles.Count,readParms.ExcludedFiles.Count);
            foreach (String fn in parms.ExcludedFiles)
                Assert.IsTrue(readParms.ExcludedFiles.Contains(fn));
        }
    }
}
