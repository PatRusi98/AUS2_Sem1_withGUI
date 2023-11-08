namespace AUS2_Sem1_withGUI.GeoProject
{
    public class Estate : GeoObject
    {
        private List<Parcel> parcels = new();

        public Estate(double x, double y, int id, string desc, 
            (double lat, double lon, char latPos, char lonPos) topLeft, 
            (double lat, double lon, char latPos, char lonPos) topRight) 
            : base(x, y, id, desc, topLeft, topRight)
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

        public void AddParcel(Parcel parcel)
        {
            parcels.Add(parcel);
        }

        public List<Parcel> GetParcels()
        {
            return parcels;
        }
    }
}
