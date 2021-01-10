using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Bus
    {
        public string LicenseNum { get; set; }
        public DateTime FromDate { get; set; }
        public double TotalTrip { get; set; }
        public double FuelRemain { get; set; }
        public BusStatus Status { get; set; }
        public bool InService;
        public bool IsDangerous { get; set; }
        public DateTime LastTreated { get; set; }
        public int KmSinceTreated { get; set; }
    }
    public enum BusStatus
    {
        Ready, Transit, Refilling, Treatment
    }
}
