using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace DO
{
    /// <summary>
    /// Represents an exit of a line
    /// </summary>
    public class LineTrip
    {
        public int Id { get; set; }
        public int LineId { get; set; } 
        [XmlIgnore]
        public TimeSpan StartAt { get; set; }
        //Credit to Rory McLeod:https://stackoverflow.com/questions/637933/how-to-serialize-a-timespan-to-xml
        // XmlSerializer does not support TimeSpan, so use this property for 
        // serialization instead.
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "StartsAt")]
        public string StartsAtString
        {
            get
            {
                return XmlConvert.ToString(StartAt);
            }
            set
            {
                StartAt = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
        public bool InService { get; set; }
    }
}
