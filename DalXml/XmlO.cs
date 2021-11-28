using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
namespace DL
{
    class XmlInputOutput
    {
      //Allow reading concurrently for multiple processes at once
     //Credit: William Xifaras https://social.msdn.microsoft.com/Forums/Windows/en-US/8c659fc2-4904-4e6e-9043-69b5695e8303/systemioioexception-the-process-cannot-access-the-file-crdsrdstxt-because-it-is-being-used?forum=csharpgeneral
        public static void SaveListToXml<T>(List<T> list, string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(list.GetType());
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                xmlSerializer.Serialize(fs, list);
                fs.Close();
            }
        }

        public static List<T> LoadListFromXml<T>(string path)
        {
            List<T> list;
            XmlSerializer x = new XmlSerializer(typeof(List<T>));

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                list = (List<T>)x.Deserialize(fs);
                fs.Close();
            };
            return list;
        }

    }
}
