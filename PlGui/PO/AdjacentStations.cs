using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PO
{
 public class AdjacentStations
  {
            public int Station1 { get; set; }
            public int Station2 { get; set; }
            public float Distance { get; set; }
            public TimeSpan Time { get; set; }
            public bool InService;
            public override string ToString()
            {
                return String.Format($"Station1:{Station1} Station2:{Station2} Distance:{Distance} Time:{Time} InService{InService}");
            }
  } 
}
