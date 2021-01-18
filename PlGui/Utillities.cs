using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PO;
namespace PlGui
{
    public static class Utillities
    {
        public static PO.AdjacentStations AdjacentStationsBoPoAdapter(BO.AdjacentStations boAdjStat)
        {
            PO.AdjacentStations poAdjacentStations = new PO.AdjacentStations();
            poAdjacentStations.Distance = boAdjStat.Distance;
            poAdjacentStations.Station1 = boAdjStat.Station1;
            poAdjacentStations.Station2 = boAdjStat.Station2;
            poAdjacentStations.Time = boAdjStat.Time;
            return poAdjacentStations;
        }
        public static  PO.LineStation LineStationBoPoAdapter(BO.LineStation boLineStation)
        {
            PO.LineStation poLineStation = new PO.LineStation();
            poLineStation.DistanceFromPreviousStation = boLineStation.DistanceFromPreviousStation;
            poLineStation.InService = boLineStation.InService;
            poLineStation.LineId = boLineStation.LineId;
            poLineStation.LineStationIndex = boLineStation.LineStationIndex;
            poLineStation.NextStation = boLineStation.NextStation;
            poLineStation.PrevStation = boLineStation.PrevStation;
            poLineStation.Station = boLineStation.Station;
            poLineStation.TimeFromPreviousStation = boLineStation.TimeFromPreviousStation;
            return poLineStation;

        }
        public static ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        public static PO.Station StationBoPoAdapter(BO.Station boStation)
        {
            PO.Station poStation = new PO.Station();
            poStation.Code = boStation.Code;
            poStation.Latitude = boStation.Latitude;
            poStation.lineStationsOfStation= Convert(from lineStation in boStation.lineStationsOfStation select LineStationBoPoAdapter(lineStation));
            poStation.Longtitude = boStation.Longtitude;
            poStation.Name = boStation.Name;
            return poStation;
        }
        public static PO.Line LineBoPoAdapter(BO.Line boLine)
        {
            PO.Line poLine = new PO.Line();
            poLine.Area = (PO.Areas)boLine.Area;
            poLine.Code = boLine.Code;
            poLine.Id = boLine.Id;
            poLine.InService = boLine.InService;
            poLine.LastStationName = boLine.LastStationName;
            poLine.stationsInLine = Convert(from ls in boLine.stationsInLine select LineStationBoPoAdapter(ls));
            return poLine;
        }
    }
}
