using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalDeployProjectUpdaterUtility.Tests
{
    [TestClass]
    public class ModuleParametersPersistorTests
    {
        [TestMethod]
        public void TestWriteRead()
        {
            const String fileName = @"C:\Temp\HostParms.Xml";
            if (File.Exists(fileName))
                File.Delete(fileName);
            Assert.IsTrue(!File.Exists(fileName));

            ModuleParameters exp = new HostModuleParametersProvider().GetModuleParameters();

            Assert.IsFalse(String.IsNullOrEmpty(exp.ContentSubFolder));
            Assert.IsFalse(exp.ExcludedFiles.Count == 0);
            ModuleParametersPersistor persistor = new ModuleParametersPersistor();

            persistor.Write(fileName, exp);
            Assert.IsTrue(File.Exists(fileName));

            ModuleParameters actual = persistor.Read(fileName);
            Assert.AreEqual(exp.ContentSubFolder, actual.ContentSubFolder);
            Assert.AreEqual(exp.ExcludedFiles.Count, actual.ExcludedFiles.Count);
            foreach (String fn in exp.ExcludedFiles)
                actual.ExcludedFiles.Contains(fn);

        }

        [TestMethod]
        public void TestNonStock()
        {
            const String fileName = @"C:\Temp\HostParmsNonStock.Xml";

            ModuleParameters stock, expected, actual;
            stock = new HostModuleParametersProvider().GetModuleParameters();
            expected = new HostModuleParametersProvider().GetModuleParameters();


            expected.ContentSubFolder = stock.ContentSubFolder + Guid.NewGuid().ToString();
            String extraExcludedFile = Guid.NewGuid().ToString() + ".txt";
            expected.ExcludedFiles.Add(extraExcludedFile);

            ModuleParametersPersistor persistor = new ModuleParametersPersistor();
            persistor.Write(fileName, expected);
            actual = persistor.Read(fileName);

            Assert.AreNotEqual(stock.ContentSubFolder, expected.ContentSubFolder);
            Assert.AreEqual(actual.ContentSubFolder, expected.ContentSubFolder);
            Assert.AreEqual(stock.ExcludedFiles.Count + 1, actual.ExcludedFiles.Count);
            Assert.IsFalse(stock.ExcludedFiles.Contains(extraExcludedFile));
            Assert.IsTrue(actual.ExcludedFiles.Contains(extraExcludedFile));







        }

    }
}
