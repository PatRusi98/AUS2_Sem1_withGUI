namespace AUS2_Sem1_withGUI.Data_Structures.QuadTree.Logic
{
    public class QuadTreeNode<T> where T : IComparable
    {
        public QuadTreeRectangle<T> Boundary { get; set; }
        public List<IQuadTreeData<T>> Regions { get; set; }
        public QuadTreeNode<T>[] Children { get; set; }
        public bool IsLeaf => Children == null;

        public QuadTreeNode(QuadTreeRectangle<T> boundary)
        {
            Boundary = boundary;
            Regions = new List<IQuadTreeData<T>>();
            Children = null;
        }
    }
}
