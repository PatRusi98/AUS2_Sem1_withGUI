using AUS2_Sem1_withGUI.Data_Structures.QuadTree;

namespace AUS2_Sem1_withGUI.GeoProject
{
    public class GeoObject : IQuadTreeData<double>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int IdNumber { get; set; }
        public string Description { get; set; }
        public GPSPosition TopLeft { get; set; }
        public GPSPosition BottomRight { get; set; }

        public GeoObject(double x, double y, int id, string desc,
            (double lat, double lon, char latPos, char lonPos) topLeft, 
            (double lat, double lon, char latPos, char lonPos) topRight)
        {
            IdNumber = id;
            Description = desc;
            TopLeft = new GPSPosition(topLeft.lat, topLeft.lon, topLeft.latPos, topLeft.lonPos);
            BottomRight = new GPSPosition(topRight.lat, topRight.lon, topRight.latPos, topRight.lonPos);
            X = TopLeft.X;
            Y = TopLeft.Y;
            Width = Math.Abs(BottomRight.X - TopLeft.X);
            Height = Math.Abs(BottomRight.Y - TopLeft.Y);
        }

        public GeoObject getById(int id)
        {
            //TODO
            
            return null;
        }

        public List<GeoObject> getByPosition(GPSPosition position)
        {
            //TODO
            return new List<GeoObject> { this};
        }

        public List<GeoObject> getAll()
        {
            //TODO
            return new List<GeoObject> { this };
        }

        public bool add(GeoObject data)
        {
            //TODO
            return false;
        }

        public bool removeOnPositon(GPSPosition position)
        {
            //TODO
            return false;
        }

        public bool removeByRange(GPSPosition position)
        {
            //TODO
            return false;
        }

        public bool edit(GeoObject data)
        {
            //TODO
            return false;
        }
    }
}
