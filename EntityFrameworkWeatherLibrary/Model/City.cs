using System;

namespace WeatherDomainLibrary.Model
{
    public class Coordinate
    {
        public float Lon { get; set; }
        public float Lat { get; set; }

    }
    public class City
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Coordinate Coord { get; set; }
    }
}
