using AUS2_Sem1_withGUI.Utils;

namespace AUS2_Sem1_withGUI.GeoProject
{
    public class Estate : GeoObject
    {
        private List<Parcel> parcels = new();

        public Estate(int id, string desc, GeoType type,
            (double lat, double lon, char latPos, char lonPos) topLeft, 
            (double lat, double lon, char latPos, char lonPos) topRight) 
            : base(id, desc, type, topLeft, topRight)
        {
            IdNumberByUser = id;
            Description = desc;
            TopLeft = new GPSPosition(topLeft.lat, topLeft.lon, topLeft.latPos, topLeft.lonPos);
            BottomRight = new GPSPosition(topRight.lat, topRight.lon, topRight.latPos, topRight.lonPos);
            X = TopLeft.X;
            Y = TopLeft.Y;
            Height = Math.Abs(BottomRight.X - TopLeft.X);
            Width = Math.Abs(BottomRight.Y - TopLeft.Y);
            Type = type;
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
