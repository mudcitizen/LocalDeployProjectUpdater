using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public class TextFileLogger : ILogger
    {
        private String _FileName;

        public TextFileLogger() : this(@"BuildLab.Log") { }


        public TextFileLogger(String fileName)
        {
            _FileName = fileName;
            if (File.Exists(_FileName))
            {
                File.Delete(_FileName);
            }
            else
            {
                String path = Path.GetDirectoryName(fileName);
                if ((!String.IsNullOrEmpty(path)) && (!Directory.Exists(path)))
                    Directory.CreateDirectory(path);
            }
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
