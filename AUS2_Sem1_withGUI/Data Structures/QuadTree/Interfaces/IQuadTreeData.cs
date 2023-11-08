namespace AUS2_Sem1_withGUI.Data_Structures.QuadTree
{
    public interface IQuadTreeData<T> where T : IComparable
    {
        T X { get; set; }
        T Y { get; set; }
        T Width { get; set; }
        T Height { get; set; }
    }
}