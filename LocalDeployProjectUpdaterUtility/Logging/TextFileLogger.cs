using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility.Logging
{
    public class TextFileLogger
    {
        private String _FileName;

        public TextFileLogger() : this(@"BuildLab.Log") { }


        public TextFileLogger(String fileName)
        {
            _FileName = fileName;
            if (File.Exists(_FileName))
                File.Delete(_FileName);
        }

        public void Log(string logText)
        {
            using (StreamWriter writer = new StreamWriter(_FileName, true))
            {
                writer.AutoFlush = true;
                writer.WriteLine(logText);
            }
        }
    }
}
