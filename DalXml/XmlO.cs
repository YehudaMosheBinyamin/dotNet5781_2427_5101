using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
namespace DL
{
static class XmlInputOutput
    {
        public static void SaveListToXml<T>(List<T> list,string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            xmlSerializer.Serialize(fs, list);
        }

        public static List<T> LoadListFromXml<T>(string path)
        {
            List<T> list;
            XmlSerializer x = new XmlSerializer(typeof(List<T>));
            FileStream fs = new FileStream(path, FileMode.Open);
            list = (List<T>)x.Deserialize(fs);
            return list;
        }

    }
}
