using System;
using System.IO;
using System.Diagnostics;
using LocalDeployProjectUpdater;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalDeployProjectUpdater.Tests
{
    [TestClass]
    public class ValidateTests
    {
        const String _CsProjFile = @"C:\temp\csProjFile.txt";
        const String _VfpDirectory = @"C:\temp\vpfTestFolder";
        const String _ModuleParmsFile = @"C:\temp\moduleParms.txt";

        ArgsValidator _Validator;

        [TestInitialize]
        public void Setup()
        {
            if (!File.Exists(_CsProjFile))
                File.Create(_CsProjFile);

            if (!Directory.Exists(_VfpDirectory))
                Directory.CreateDirectory(_VfpDirectory);

            if (!File.Exists(_ModuleParmsFile))
                File.Create(_ModuleParmsFile);

            _Validator = new ArgsValidator();
        }

        [TestMethod]
        public void TestTooFewParms()
        {
            String[] parms = new[] { "" };
            String msg = _Validator.Validate(parms);
            Debug.WriteLine(msg);
            Assert.IsTrue(msg.IndexOf(Constants.MessageText.WrongParmCount) >= 0);
        }

        [TestMethod]
        public void TestTooManyParms()
        {
            String[] parms = new[] { "","","","" };
            String msg = _Validator.Validate(parms);
            Debug.WriteLine(msg);
            Assert.IsTrue(msg.IndexOf(Constants.MessageText.WrongParmCount) >= 0);
        }
        [TestMethod]
        public void TestCsProjFileNullOrEmpty()
        {
            String[] parms = new[] { "",_VfpDirectory, _ModuleParmsFile };
            String msg = _Validator.Validate(parms);
            Debug.WriteLine(msg);
            Assert.IsTrue(msg.IndexOf(Constants.MessageText.RequiredParameter) >= 0);
            Assert.IsTrue(msg.IndexOf(Constants.ArgumentNames.CsProjectFileName) >= 0);
        }
        [TestMethod]
        public void TestVfpDirectoryNullOrEmpty()
        {
            String[] parms = new[] { _CsProjFile, "", _ModuleParmsFile };
            String msg = _Validator.Validate(parms);
            Debug.WriteLine(msg);
            Assert.IsTrue(msg.IndexOf(Constants.MessageText.RequiredParameter) >= 0);
            Assert.IsTrue(msg.IndexOf(Constants.ArgumentNames.VfpDirectoryName) >= 0);
        }

        [TestMethod]
        public void TestModuleParmsFileNameNullOrEmpty()
        {
            String[] parms = new[] { _CsProjFile, _VfpDirectory,""};
            String msg = _Validator.Validate(parms);
            Debug.WriteLine(msg);
            Assert.IsTrue(msg.IndexOf(Constants.MessageText.RequiredParameter) >= 0);
            Assert.IsTrue(msg.IndexOf(Constants.ArgumentNames.ModuleParameterFileName) >= 0);
        }
        [TestMethod]
        public void TestAllValidParms()
        {
            String[] parms = new[] { _CsProjFile, _VfpDirectory, _ModuleParmsFile};
            String msg = _Validator.Validate(parms);
            Debug.WriteLine(msg);
            Assert.IsTrue(String.IsNullOrEmpty(msg));
        }
        [TestMethod]
        public void TestCsProjFileDoesNotExist()
        {
            File.Delete(_CsProjFile);
            String[] parms = new[] { _CsProjFile, _VfpDirectory, _ModuleParmsFile };
            String msg = _Validator.Validate(parms);
            Debug.WriteLine(msg);
            Assert.IsTrue(msg.IndexOf(Constants.MessageText.FileNotFound) >= 0);
            Assert.IsTrue(msg.IndexOf(Constants.ArgumentNames.CsProjectFileName) >= 0);
        }

        [TestMethod]
        public void TestModuleParmsFileDoesNotExist()
        {
            File.Delete(_ModuleParmsFile);
            String[] parms = new[] { _CsProjFile, _VfpDirectory, _ModuleParmsFile };
            String msg = _Validator.Validate(parms);
            Debug.WriteLine(msg);
            Assert.IsTrue(msg.IndexOf(Constants.MessageText.FileNotFound) >= 0);
            Assert.IsTrue(msg.IndexOf(Constants.ArgumentNames.ModuleParameterFileName) >= 0);
        }

        [TestMethod]
        public void TestVfpDirectoryDoesNotExist()
        {
            Directory.Delete(_VfpDirectory);
            String[] parms = new[] { _CsProjFile, _VfpDirectory, _ModuleParmsFile };
            String msg = _Validator.Validate(parms);
            Debug.WriteLine(msg);
            Assert.IsTrue(msg.IndexOf(Constants.MessageText.DirectoryNotFound) >= 0);
            Assert.IsTrue(msg.IndexOf(Constants.ArgumentNames.VfpDirectoryName) >= 0);
        }
    }
}
