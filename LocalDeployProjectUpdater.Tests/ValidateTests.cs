using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using LocalDeployProjectUpdater;
using LocalDeployProjectUpdaterUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalDeployProjectUpdater.Tests
{
    [TestClass]
    public class ValidateTests
    {
        const String _CsProjFile = @"C:\temp\csProjFile.txt";
        const String _VfpDirectory = @"C:\temp\vpfTestFolder";
        const String _ModuleParmsFile = @"C:\temp\moduleParms.txt";

        const String _ValidModuleParmsFile = @"C:\temp\HostParms.xml";
        const String _ValidVfpDirectory = @"C:\temp\VfpProject";
        const String _ValidCsProjFile = @"C:\ClickOnceSolution\vh\vh.csproj";

        ArgsValidator _Validator;

        [TestInitialize]
        public void Setup()
        {
            if (!File.Exists(_CsProjFile))
                File.Create(_CsProjFile).Close();

            if (!Directory.Exists(_VfpDirectory))
                Directory.CreateDirectory(_VfpDirectory);

            if (!File.Exists(_ModuleParmsFile))
                File.Create(_ModuleParmsFile).Close();

            _Validator = new ArgsValidator();

            if (!File.Exists(_ValidModuleParmsFile))
            {
                ModuleParameters parms = new HostModuleParametersProvider().GetModuleParameters();
                new ModuleParametersPersistor().Write(_ValidModuleParmsFile, parms);
            }

            Assert.IsTrue(File.Exists(_ValidCsProjFile));
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

        [TestMethod]
        public void TestInvalidProjectFileValidator()
        {
            const string fileName = @"C:\temp\TestInvalidProjectFileValidator.csproj";
            if (File.Exists(fileName))
                File.Delete(fileName);
            File.Create(fileName).Close();
            IValidator v = new FileNameCommandLineArgument("WTF", new ProjectFileValidator());
            String msg = v.Validate(fileName);
            try
            {
                File.Delete(fileName);
            }
            catch (Exception)
            {
            }
            Debug.WriteLine(msg);
            Assert.IsTrue(msg.IndexOf(Constants.MessageText.NotAValidProjectFile) >= 0);
        }
        [TestMethod]
        public void TestValidProjectFileValidator()
        {
            IValidator v = new ProjectFileValidator();
            String msg = v.Validate(_ValidCsProjFile);
            Assert.IsTrue(String.IsNullOrEmpty(msg));
        }

        [TestMethod]
        public void TestInvalidModuleParmsFileValidator()
        {
            const string fileName = @"C:\temp\TestInvalidModuleParmsFileValidator.xml";
            if (File.Exists(fileName))
                File.Delete(fileName);
            File.Create(fileName).Close();
            //IValidator v = new FileNameCommandLineArgument("WTF", new ModuleParametersFileNameValidator());
            IValidator v = new ModuleParametersFileNameValidator();
            String msg = v.Validate(fileName);
            try
            {
                File.Delete(fileName);
            }
            catch (Exception)
            {
            }
            Debug.WriteLine(msg);
            Assert.IsTrue(msg.IndexOf(Constants.MessageText.NotAValidModuleParametersFile) >= 0);
        }

        [TestMethod]
        public void TestValidModuleParmsFileValidator()
        {
            IValidator v = new CommandLineArgument("WTF", new ModuleParametersFileNameValidator());
            String msg = v.Validate(_ValidModuleParmsFile);
            Assert.IsTrue(String.IsNullOrEmpty(msg));
        }

        [TestMethod]
        public void TestRequiredFileTypesDirectoryValidator()
        {
            IList<String> reqFileTypes = new List<String>() { "fxp", "exe" };
      
            IValidator v = new RequiredFileTypesDirectoryValidator(reqFileTypes);
            String msg = GetRequiredFileTypesDirectoryValidatorResult(_ValidVfpDirectory, reqFileTypes);
            Assert.IsTrue(String.IsNullOrEmpty(msg));

            reqFileTypes.Add(Guid.NewGuid().ToString());
            msg = GetRequiredFileTypesDirectoryValidatorResult(_ValidVfpDirectory, reqFileTypes);
            Debug.WriteLine(msg);
            Assert.IsTrue(msg.IndexOf(Constants.MessageText.RequiredFileTypesNotPresent)>=0);


        }

        String GetRequiredFileTypesDirectoryValidatorResult(String directoryName, IEnumerable<String> reqFileTypes) {
            IValidator v = new RequiredFileTypesDirectoryValidator(reqFileTypes);
            return v.Validate(directoryName);
        }


    }
}
