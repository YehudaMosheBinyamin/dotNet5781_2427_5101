using dotNet5781_02_2427_5101;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{
    class BusStop
    {
        static List<int> busKeyList;
        int busStationKey;
        float latitude;
        float longtitude;
        public int BusStationKey
        {
            get(){return busStationKey;}
}
public float Latitude { get { return latitude; } }
public float Longtitude { get { return longtitude; } }
public BusStop()
{
    Random rand = new Random();
    int randomCode = rand.Next(0, 999999);
    if (busKeyList.Contains(randomCode))
    {
        randomCode = rand.Next(0, 999999);
    }
    busStationKey = randomCode;
    double randomLatitude = rand.NextDouble(31, 33.3);
    latitude = randomLatitude;
    double random Longtitude = rand.NextDouble(34.3, 35.5);
    longtitude = randomLongtitude;
        public override string ToString()
{
    return String.Format("Bus station code: {0} {1} {2} ", BusStationKey, Latitude, Longtitude);
}

    }
        public enum Area { General, North, South, Center, Jerusalem };
public class BusLine
{
    Area operatingArea;
    public Area OperatingArea { get { return operatingArea; } }
    int busLine;
    public int BusLine { get { return BusLine; } }
    int firstStation;
    public int FirstStation { get { return firstStation; } }
    int lastStation;
    public int LastStation { get { return lastStation; } }
    List<BusStop> listStations = new List<BusStop>();
    public List<BusStop> ListStations { get { return listStations; } }
    public override string ToString()
    {
        return String.Format("Bus number: {0} Area: {1} Stations: {2}", BusLine, OperatingArea, ListStations);
    }
    public void addStation
    {
    }
    public bool inList(BusStop busStop)
    {
        if listStations.Contains(busStop)
               {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void removeBusStop(BusStop bs)
    {
        if (inList(bs)) { listStations.Remove(bs); }
    }
    public float distance(BusStop bs1, BusStop bs2)
    {
    }
    //bus line from two bus stops
    public BusLine(BusStop stop1, BusStop stop2)
    {
        BusLine newLine = new BusStop() { firstStation = stop1, lastStation = stop2 };
        return newLine;


    }



}









class Program
{
    static void Main(string[] args)
    {
    }
}
}