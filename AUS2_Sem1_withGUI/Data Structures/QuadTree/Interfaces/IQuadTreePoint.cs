namespace AUS2_Sem1_withGUI.Data_Structures.QuadTree.Interfaces
{
    public interface IQuadTreePoint<T> where T : IComparable
    {
        T X { get; set; }
        T Y { get; set; }
    }
}
