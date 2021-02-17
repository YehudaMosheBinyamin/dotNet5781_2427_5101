using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DL
{
    public static class InitAll
    {
        static InitAll()
        {
            
            Initialize();
           
        }
        public static void Initialize()
        {
            bool truePAth=File.Exists(@"..\bin\Stations.xml");
            if (truePAth)
            {
                throw new Exception("true");
            }
            else
            {
                throw new Exception("false");
            }
            Station.SaveStationsToXml(DS.DataSource.stationsList, @"..\bin\Stations.xml");
        }
    }
}
