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
            ModuleParameters parms = new HostModuleParametersProvider().GetParameters();

            XmlSerializer s = new XmlSerializer(typeof(ModuleParameters));
            using (TextWriter w = new StreamWriter(fileName)) {
                s.Serialize(w, parms);
            }
            //ModuleParametersPersistor p = new ModuleParametersPersistor();
            //p.Write(fileName, parms);

            Assert.IsTrue(File.Exists(fileName));
        }
    }
}
