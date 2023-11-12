using AUS2_Sem1_withGUI.Data_Structures.QuadTree.Interfaces;
using AUS2_Sem1_withGUI.Utils;

namespace AUS2_Sem1_withGUI.Data_Structures.QuadTree.Logic
{
    public class QuadTree<T> where T : IComparable
    {
        public QuadTreeNode<T> Root;
        protected int MaxRegionsPerNode;
        private IMathOperations<T> MathOperations;

        public QuadTree(QuadTreeRectangle<T> boundary, int maxRegionsPerNode)
        {
            Root = new QuadTreeNode<T>(boundary);
            MaxRegionsPerNode = maxRegionsPerNode;
            MathOperations = (IMathOperations<T>)new MathOperationsDouble();
        }

        #region Insert
        public void Insert(IQuadTreeData<T> region)
        {
            var regionRectangle = new QuadTreeRectangle<T>(region.X, region.Y, region.Width, region.Height);

            if (!Root.Boundary.IntersectsWith(regionRectangle))
                return;

            var stack = new Stack<QuadTreeNode<T>>();
            stack.Push(Root);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();

                if (currentNode.Boundary.IntersectsWith(regionRectangle))
                {
                    if (currentNode.IsLeaf)
                    {
                        currentNode.Regions.Add(region);

                        if (currentNode.Regions.Count > MaxRegionsPerNode)
                        {
                            Subdivide(currentNode);
                            var overlappingRegions = new List<IQuadTreeData<T>>(currentNode.Regions);
                            currentNode.Regions.Clear();

                            foreach (var existingRegion in overlappingRegions)
                            {
                                var existingRegionRectangle = new QuadTreeRectangle<T>(existingRegion.X, existingRegion.Y, existingRegion.Width, existingRegion.Height);
                                foreach (var child in currentNode.Children)
                                {
                                    if (child.Boundary.IntersectsWith(existingRegionRectangle))
                                    {
                                        stack.Push(child);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var child in currentNode.Children)
                        {
                            if (child.Boundary.IntersectsWith(regionRectangle))
                            {
                                stack.Push(child);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Delete
        public void Delete(IQuadTreeData<T> region)
        {
            var regionRectangle = new QuadTreeRectangle<T>(region.X, region.Y, region.Width, region.Height);

            if (!Root.Boundary.IntersectsWith(regionRectangle))
                return;

            var stack = new Stack<QuadTreeNode<T>>();
            stack.Push(Root);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();

                if (currentNode.Boundary.IntersectsWith(regionRectangle))
                {
                    if (currentNode.IsLeaf)
                    {
                        currentNode.Regions.Remove(region);
                    }
                    else
                    {
                        foreach (var child in currentNode.Children)
                        {
                            if (child.Boundary.IntersectsWith(regionRectangle))
                            {
                                stack.Push(child);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Find
        public List<IQuadTreeData<T>> FindByPoint(IQuadTreePoint<T> point)
        {
            var result = new List<IQuadTreeData<T>>();
            var stack = new Stack<QuadTreeNode<T>>();
            stack.Push(Root);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();

                if (currentNode.Boundary.ContainsPoint(point))
                {
                    if (currentNode.IsLeaf)
                    {
                        foreach (var region in currentNode.Regions)
                        {
                            var regionRectangle = new QuadTreeRectangle<T>(region.X, region.Y, region.Width, region.Height);

                            if (regionRectangle.ContainsPoint(point))
                            {
                                result.Add(region);
                            }
                        }
                    }
                    else
                    {
                        foreach (var child in currentNode.Children)
                        {
                            stack.Push(child);
                        }
                    }
                }
            }

            return result;
        }

        public List<IQuadTreeData<T>> FindByRectangle(QuadTreeRectangle<T> rectangle)
        {
            var result = new List<IQuadTreeData<T>>();
            var stack = new Stack<QuadTreeNode<T>>();
            stack.Push(Root);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();

                if (currentNode.Boundary.IntersectsWith(rectangle))
                {
                    if (currentNode.IsLeaf)
                    {
                        foreach (var region in currentNode.Regions)
                        {
                            var regionRectangle = new QuadTreeRectangle<T>(region.X, region.Y, region.Width, region.Height);
                            if (regionRectangle.IntersectsWith(rectangle))
                            {
                                result.Add(region);
                            }
                        }
                    }
                    else
                    {
                        foreach (var child in currentNode.Children)
                        {
                            stack.Push(child);
                        }
                    }
                }
            }

            return result;
        }
        #endregion

        #region Private methods
        protected void Subdivide(QuadTreeNode<T> node)
        {
            T subWidth = MathOperations.Divide(node.Boundary.Width, (dynamic) 2);
            T subHeight = MathOperations.Divide(node.Boundary.Height, (dynamic) 2);
            T x = node.Boundary.X;
            T y = node.Boundary.Y;

            node.Children = new QuadTreeNode<T>[4];
            node.Children[0] = new QuadTreeNode<T>(new QuadTreeRectangle<T>(x, y, subWidth, subHeight));
            node.Children[1] = new QuadTreeNode<T>(new QuadTreeRectangle<T>(MathOperations.Add(x, subWidth), y, subWidth, subHeight));
            node.Children[2] = new QuadTreeNode<T>(new QuadTreeRectangle<T>(x, MathOperations.Add(y, subHeight), subWidth, subHeight));
            node.Children[3] = new QuadTreeNode<T>(new QuadTreeRectangle<T>(MathOperations.Add(x, subWidth), MathOperations.Add(y, subHeight), subWidth, subHeight));
        }


        #endregion
    }
}
