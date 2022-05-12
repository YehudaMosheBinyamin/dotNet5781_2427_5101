using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DO
{
    /*class to represent adjacent stations in a route of a bus line*/
    public class AdjacentStations
    {   
        public int Station1 {get; set;} 
        public int Station2 { get; set; }
        public float Distance{get; set;}
        [XmlIgnore]
        public TimeSpan Time { get; set; }
        //Credit to Rory McLeod:https://stackoverflow.com/questions/637933/how-to-serialize-a-timespan-to-xml
        // XmlSerializer does not support TimeSpan, so use this property for 
        // serialization instead.
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "Time")]
        public string StartsAtString
        {
            get
            {
                return XmlConvert.ToString(Time);
            }
            set
            {
                Time = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
        public bool InService { get; set; }
    }
}

