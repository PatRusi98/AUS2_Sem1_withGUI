using System.Collections.Concurrent;

namespace AUS2_Sem1_withGUI.Data_Structures.QuadTree.Logic
{
    public class QuadTreeOptimization<T> : QuadTree<T> where T : IComparable
    {
        public QuadTreeOptimization(QuadTreeRectangle<T> boundary, int maxRegionsPerNode)
            : base(boundary, maxRegionsPerNode)
        {
        }

        public new void Insert(IQuadTreeData<T> region)
        {
            var regionRectangle = new QuadTreeRectangle<T>(region.X, region.Y, region.Width, region.Height);

            if (!Root.Boundary.IntersectsWith(regionRectangle))
                return;

            var parallelBag = new ConcurrentBag<QuadTreeNode<T>>();
            parallelBag.Add(Root);

            Parallel.ForEach(parallelBag, currentNode =>
            {
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

                            Parallel.ForEach(overlappingRegions, existingRegion =>
                            {
                                var existingRegionRectangle = new QuadTreeRectangle<T>(existingRegion.X, existingRegion.Y, existingRegion.Width, existingRegion.Height);
                                Parallel.ForEach(currentNode.Children, child =>
                                {
                                    if (child.Boundary.IntersectsWith(existingRegionRectangle))
                                    {
                                        parallelBag.Add(child);
                                    }
                                });
                            });
                        }
                    }
                    else
                    {
                        Parallel.ForEach(currentNode.Children, child =>
                        {
                            if (child.Boundary.IntersectsWith(regionRectangle))
                            {
                                parallelBag.Add(child);
                            }
                        });
                    }
                }
            });
        }
    }
}
