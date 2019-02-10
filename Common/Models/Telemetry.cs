namespace Common
{
    public class Telemetry
    {
        public Telemetry(double longitude, double latitude, float altitude)
        {
            Longitude = longitude;
            Latitude = latitude;
            Altitude = altitude;
        }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public float Altitude { get; set; }
    }
}
