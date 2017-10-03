using LocalDeployProjectUpdaterUtility;
using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LocalDeployProjectUpdaterUtility
{
    public class ModuleParametersPersistor
    {
        public ModuleParameters Read(String fileName)
        {
            ModuleParameters parms;
            using (FileStream writer = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer ser = GetSerializer();
                parms = (ModuleParameters)ser.Deserialize(writer);
                writer.Close();
            }
            return parms;
        }

        public void Write(String fileName, ModuleParameters moduleParameters)
        {
            using (TextWriter writer = new StreamWriter(fileName))
            {
                XmlSerializer ser = GetSerializer();
                ser.Serialize(writer, moduleParameters);
                writer.Close();
            }
        }

        private XmlSerializer GetSerializer()
        {
            return new XmlSerializer(typeof(ModuleParameters));
        }
    }
}
