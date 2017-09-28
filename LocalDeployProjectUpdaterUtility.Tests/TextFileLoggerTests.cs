using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalDeployProjectUpdaterUtility.Tests
{
    [TestClass]
    public class TextFileLoggerTests
    {
        [TestMethod()]
        public void LogTest()
        {
            const string fileName = @"C:\Temp\TestLogger.txt";

            IList<String> expectedLines = new List<String>() { "The rain in spain", "Falls mainly in the Plain" };
            ILogger logger = new TextFileLogger(fileName);
            foreach (String s in expectedLines)
                logger.Log(s);

            IList<String> actualLines = File.ReadAllLines(fileName).ToList();

            Assert.AreEqual(expectedLines.Count, actualLines.Count);

            for (int i = 0; 1 < actualLines.Count - 1; i++)
            {
                String actualLine = actualLines[i];
                String expectedLine = expectedLines[i];
                Assert.AreEqual<String>(expectedLine, actualLine);
            }

        }

        [TestMethod]
        public void CreateFolderTest()
        {
            const String fileName = @"C:\Fung\Log.txt";
            if (File.Exists(fileName))
                File.Delete(fileName);

            String folder = Path.GetDirectoryName(fileName);
            if (Directory.Exists(folder))
                Directory.Delete(folder);

            ILogger logger = new TextFileLogger(fileName);
            Assert.IsTrue(Directory.Exists(folder));

            logger.Log("Stuff");
            Assert.IsTrue(File.Exists(fileName));

            File.Delete(fileName);
            Directory.Delete(folder);
        }

    }
}
