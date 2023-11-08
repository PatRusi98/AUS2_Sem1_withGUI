namespace AUS2_Sem1_withGUI.GeoProject
{
    public class Parcel : GeoObject
    {
        private List<Estate> estates = new();

        public Parcel(double x, double y, int id, string desc, (double lat, double lon, char latPos, char lonPos) topLeft, (double lat, double lon, char latPos, char lonPos) topRight) : base(x, y, id, desc, topLeft, topRight)
        {
            X = x;
            Y = y;
            IdNumber = id;
            Description = desc;
            TopLeft = new GPSPosition(topLeft.lat, topLeft.lon, topLeft.latPos, topLeft.lonPos);
            BottomRight = new GPSPosition(topRight.lat, topRight.lon, topRight.latPos, topRight.lonPos);
            Height = Math.Abs(BottomRight.X - TopLeft.X);
            Width = Math.Abs(BottomRight.Y - TopLeft.Y);
        }

        public void AddEstate(Estate estate)
        {
            estates.Add(estate);
        }

        public List<Estate> GetEstates()
        {
            return estates;
        }
    }
}
