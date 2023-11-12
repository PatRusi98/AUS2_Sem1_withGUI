using AUS2_Sem1_withGUI.Data_Structures.QuadTree.Interfaces;
using AUS2_Sem1_withGUI.Utils;

namespace AUS2_Sem1_withGUI.GeoProject
{
    public class GPSPosition : IQuadTreePoint<double>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Position XPosition { get; set; }
        public Position YPosition { get; set; }

        public GPSPosition(double latitude, double longitude, char latPosition, char longPosition)
        {
            X = latitude;
            Y = longitude;
            XPosition = latPosition.CharToPosition();
            YPosition = longPosition.CharToPosition();
        }

        public int CompareTo(IQuadTreePoint<double> other)
        {
            int xComparison = X.CompareTo(other.X);
            if (xComparison != 0)
            {
                return xComparison;
            }
            return Y.CompareTo(other.Y);
        }

        public string ToString()
        {
            return $"{X} {XPosition}, {Y} {YPosition}";
        }
    }
}
