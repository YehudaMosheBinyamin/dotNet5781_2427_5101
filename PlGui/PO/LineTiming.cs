using System;
using System.ComponentModel;
namespace PO
{
    public class LineTiming : INotifyPropertyChanged
    {
        private int lineId;
        public int LineId
        {
            get { return lineId; }
            set
            {
                lineId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LineId"));
            }
        }
        private int lineCode;
        public int LineCode
        {
            get { return lineCode; }
            set
            {
                lineCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LineCode"));
            }
        }
        private TimeSpan startTime;
        public TimeSpan StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StartTime"));
            }
        }

        private string lastStationName;
        public string LastStationName
        {
            set
            {
                lastStationName = value;
            }
            get 
            { 
                return lastStationName; 
            }
        }
        private TimeSpan waitingTime;
        public TimeSpan WaitingTime
        {
            set
            {
                waitingTime = value;
                new PropertyChangedEventArgs("WaitingTime"); 
             }
            get 
            { 
                return waitingTime; 
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
