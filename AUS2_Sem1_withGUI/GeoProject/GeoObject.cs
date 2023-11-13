using AUS2_Sem1_withGUI.Data_Structures.QuadTree;
using AUS2_Sem1_withGUI.Utils;

namespace AUS2_Sem1_withGUI.GeoProject
{
    public class GeoObject : IQuadTreeData<double>
    {
        private static int LastId = 0;

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int IdNumberByUser { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public GPSPosition TopLeft { get; set; }
        public GPSPosition BottomRight { get; set; }
        public string TopLeftString { get; set; }
        public string BottomRightString { get; set; }
        public GeoType Type { get; set; }

        public GeoObject(int id, string desc, GeoType type,
            (double lat, double lon, char latPos, char lonPos) topLeft, 
            (double lat, double lon, char latPos, char lonPos) topRight)
        {
            IdNumberByUser = id;
            Id = ++LastId;
            Description = desc;
            TopLeft = new GPSPosition(topLeft.lat, topLeft.lon, topLeft.latPos, topLeft.lonPos);
            BottomRight = new GPSPosition(topRight.lat, topRight.lon, topRight.latPos, topRight.lonPos);
            X = TopLeft.X;
            Y = TopLeft.Y;
            Width = Math.Abs(BottomRight.X - TopLeft.X);
            Height = Math.Abs(BottomRight.Y - TopLeft.Y);
            TopLeftString = TopLeft.ToString();
            BottomRightString = BottomRight.ToString();
            Type = type;
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
