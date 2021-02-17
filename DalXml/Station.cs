using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
namespace DL
{
class Station
    {
        public static void SaveStationsToXml<T>(List<T> list,string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(path, FileMode.Create);
            xmlSerializer.Serialize(fs, list);   
        }


        public static List<DO.Station> LoadListFromXml(string path)
        {
            List<DO.Station> list;
            XmlSerializer x = new XmlSerializer(typeof(List<DO.Station>));
            FileStream fs = new FileStream(path, FileMode.Open);
            list = (List<DO.Station>)x.Deserialize(fs);
            return list;
        }

    }
}
