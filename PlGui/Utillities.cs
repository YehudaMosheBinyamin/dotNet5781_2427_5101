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
    {   public static PO.Station StationBoPoAdapter(BO.Station boStation)
        {
            PO.Station poStation = new PO.Station();
            poStation.Code = boStation.Code;
            poStation.Latitude = boStation.Latitude;
            poStation.LineStationsOfStation = Convert((from ls in boStation.LineStationsOfStation select LineStationBoPoAdapter(ls)).ToList());
            poStation.Longtitude = boStation.Longtitude;
            poStation.Name = boStation.Name;
            return poStation;

        }
        public static BO.Station StationPoBoAdapter(PO.Station poStation)
        {
            BO.Station boStation = new BO.Station();
            boStation.Code = poStation.Code;
            boStation.Latitude = poStation.Latitude;
            boStation.LineStationsOfStation = Convert((from ls in poStation.LineStationsOfStation select LineStationPoBoAdapter(ls)).ToList());
            boStation.Longtitude = poStation.Longtitude;
            boStation.Name = poStation.Name;
            return boStation;

        }
        public static PO.AdjacentStations AdjacentStationsBoPoAdapter(BO.AdjacentStations boAdjStat)
        {
            PO.AdjacentStations poAdjacentStations = new PO.AdjacentStations();
            poAdjacentStations.Distance = boAdjStat.Distance;
            poAdjacentStations.Station1 = boAdjStat.Station1;
            poAdjacentStations.Station2 = boAdjStat.Station2;
            poAdjacentStations.Time = boAdjStat.Time;
            poAdjacentStations.InService = boAdjStat.InService;
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
            poLineStation.Name = boLineStation.Name;
            poLineStation.LastStationName = boLineStation.LastStationName;
            return poLineStation;

        }
        public static BO.LineStation LineStationPoBoAdapter(PO.LineStation poLineStation)
        {
            BO.LineStation boLineStation = new BO.LineStation();
            boLineStation.DistanceFromPreviousStation = poLineStation.DistanceFromPreviousStation;
            boLineStation.InService = poLineStation.InService;
            boLineStation.LineId = poLineStation.LineId;
            boLineStation.LineStationIndex = poLineStation.LineStationIndex;
            boLineStation.NextStation = poLineStation.NextStation;
            boLineStation.PrevStation = poLineStation.PrevStation;
            boLineStation.Station = poLineStation.Station;
            boLineStation.Name = poLineStation.Name;
            boLineStation.LastStationName = poLineStation.LastStationName;
            boLineStation.TimeFromPreviousStation = poLineStation.TimeFromPreviousStation;
            return boLineStation;

        }
        public static ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
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
        public static BO.Line LinePoBoAdapter(PO.Line poLine)
        {
            BO.Line boLine = new BO.Line();
            boLine.Area = (BO.Areas)poLine.Area;
            boLine.Code = poLine.Code;
            boLine.Id = poLine.Id;
            boLine.InService = poLine.InService;
            boLine.LastStationName = poLine.LastStationName;
            boLine.stationsInLine = from ls in poLine.stationsInLine select LineStationPoBoAdapter(ls);
            return boLine;
        }
    }
}
