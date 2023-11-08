using AUS2_Sem1_withGUI.Data_Structures.QuadTree.Interfaces;

namespace AUS2_Sem1_withGUI.Data_Structures.QuadTree.Logic
{
    public class QuadTreeRectangle<T> where T : IComparable
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Width { get; set; }
        public T Height { get; set; }

        public QuadTreeRectangle(T x, T y, T width, T height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool IntersectsWith(QuadTreeRectangle<T> other)
        {
            double right = Convert.ToDouble(X) + Convert.ToDouble(Width);
            double otherRight = Convert.ToDouble(other.X) + Convert.ToDouble(other.Width);
            double bottom = Convert.ToDouble(Y) + Convert.ToDouble(Height);
            double otherBottom = Convert.ToDouble(other.Y) + Convert.ToDouble(other.Height);

            return right > Convert.ToDouble(other.X) &&
                   Convert.ToDouble(X) < otherRight &&
                   bottom > Convert.ToDouble(other.Y) &&
                   Convert.ToDouble(Y) < otherBottom;
        }

        public bool ContainsPoint(IQuadTreePoint<T> point)
        {
            var x = Convert.ToDouble(point.X);
            var y = Convert.ToDouble(point.Y);

            return Convert.ToDouble(X) <= x && x <= Convert.ToDouble(X) + Convert.ToDouble(Width) &&
                   Convert.ToDouble(Y) <= y && y <= Convert.ToDouble(Y) + Convert.ToDouble(Height);
        }
    }
}
