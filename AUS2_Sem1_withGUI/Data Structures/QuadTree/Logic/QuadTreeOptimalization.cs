using AUS2_Sem1_withGUI.Data_Structures.QuadTree.Interfaces;
using System.Collections.Concurrent;

namespace AUS2_Sem1_withGUI.Data_Structures.QuadTree.Logic
{
    public class QuadTreeOptimalization<T> : QuadTree<T> where T : IComparable
    {
        public QuadTreeOptimalization(QuadTreeRectangle<T> boundary, int maxRegionsPerNode, int maxHeight)
            : base(boundary, maxRegionsPerNode, maxHeight)
        {
        }

        public new void Insert(IQuadTreeData<T> region)
        {
            var regionRectangle = new QuadTreeRectangle<T>(region.X, region.Y, region.Width, region.Height);

            if (!Root.Boundary.IntersectsWith(regionRectangle))
                return;

            var concurrentBag = new ConcurrentBag<QuadTreeNode<T>>();
            concurrentBag.Add(Root);

            while (concurrentBag.TryTake(out var currentNode))
            {
                if (currentNode.Boundary.IntersectsWith(regionRectangle))
                {
                    if (currentNode.IsLeaf)
                    {
                        currentNode.Regions.Add(region);

                        if (currentNode.Regions.Count > MaxRegionsPerNode)
                        {
                            Subdivide(currentNode);

                            if (currentNode.Height >= MaxHeight)
                            {
                                DistributeRegionsAmongParents(currentNode);
                            }
                            else
                            {
                                DistributeRegions(currentNode);
                            }
                        }
                    }
                    else
                    {
                        Parallel.ForEach(currentNode.Children, child =>
                        {
                            if (child.Boundary.IntersectsWith(regionRectangle))
                            {
                                concurrentBag.Add(child);
                            }
                        });
                    }
                }
            }
        }

        protected new void DistributeRegionsAmongParents(QuadTreeNode<T> node)
        {
            var current = node;

            while (current != null)
            {
                var overlappingRegions = new List<IQuadTreeData<T>>(current.Regions);
                current.Regions.Clear();

                ConcurrentBag<IQuadTreeData<T>> regionsToDistribute = new ConcurrentBag<IQuadTreeData<T>>(overlappingRegions);

                Parallel.ForEach(GetParents(current), ancestor =>
                {
                    foreach (var existingRegion in regionsToDistribute)
                    {
                        var existingRegionRectangle = new QuadTreeRectangle<T>(existingRegion.X, existingRegion.Y, existingRegion.Width, existingRegion.Height);

                        if (ancestor.Boundary.IntersectsWith(existingRegionRectangle))
                        {
                            ancestor.Regions.Add(existingRegion);
                        }
                    }
                });

                current = current.Parent;
            }
        }

        protected new void DistributeRegions(QuadTreeNode<T> node)
        {
            var overlappingRegions = new List<IQuadTreeData<T>>(node.Regions);
            node.Regions.Clear();

            ConcurrentBag<IQuadTreeData<T>> regionsToDistribute = new ConcurrentBag<IQuadTreeData<T>>(overlappingRegions);

            Parallel.ForEach(node.Children, child =>
            {
                foreach (var existingRegion in regionsToDistribute)
                {
                    var existingRegionRectangle = new QuadTreeRectangle<T>(existingRegion.X, existingRegion.Y, existingRegion.Width, existingRegion.Height);

                    if (child.Boundary.IntersectsWith(existingRegionRectangle))
                    {
                        child.Regions.Add(existingRegion);
                    }
                }
            });
        }


        public new void Delete(IQuadTreeData<T> region)
        {
            var regionRectangle = new QuadTreeRectangle<T>(region.X, region.Y, region.Width, region.Height);

            if (!Root.Boundary.IntersectsWith(regionRectangle))
                return;

            var concurrentBag = new ConcurrentBag<QuadTreeNode<T>>
            {
                Root
            };

            while (concurrentBag.TryTake(out var currentNode))
            {
                if (currentNode.Boundary.IntersectsWith(regionRectangle))
                {
                    if (currentNode.IsLeaf)
                    {
                        currentNode.Regions.Remove(region);
                    }
                    else
                    {
                        Parallel.ForEach(currentNode.Children, child =>
                        {
                            if (child.Boundary.IntersectsWith(regionRectangle))
                            {
                                concurrentBag.Add(child);
                            }
                        });
                    }
                }
            }
        }

        public new List<IQuadTreeData<T>> FindByPoint(IQuadTreePoint<T> point)
        {
            var result = new ConcurrentBag<IQuadTreeData<T>>();
            var concurrentBag = new ConcurrentBag<QuadTreeNode<T>>
            {
                Root
            };

            while (concurrentBag.TryTake(out var currentNode))
            {
                if (currentNode.Boundary.ContainsPoint(point))
                {
                    if (currentNode.IsLeaf)
                    {
                        Parallel.ForEach(currentNode.Regions, region =>
                        {
                            var regionRectangle = new QuadTreeRectangle<T>(region.X, region.Y, region.Width, region.Height);

                            if (regionRectangle.ContainsPoint(point))
                            {
                                result.Add(region);
                            }
                        });
                    }
                    else
                    {
                        Parallel.ForEach(currentNode.Children, child =>
                        {
                            concurrentBag.Add(child);
                        });
                    }
                }
            }

            return result.ToList();
        }

        public new List<IQuadTreeData<T>> FindByRectangle(QuadTreeRectangle<T> rectangle)
        {
            var result = new ConcurrentBag<IQuadTreeData<T>>();
            var concurrentBag = new ConcurrentBag<QuadTreeNode<T>>();
            concurrentBag.Add(Root);

            while (concurrentBag.TryTake(out var currentNode))
            {
                if (currentNode.Boundary.IntersectsWith(rectangle))
                {
                    if (currentNode.IsLeaf)
                    {
                        Parallel.ForEach(currentNode.Regions, region =>
                        {
                            var regionRectangle = new QuadTreeRectangle<T>(region.X, region.Y, region.Width, region.Height);
                            if (regionRectangle.IntersectsWith(rectangle))
                            {
                                result.Add(region);
                            }
                        });
                    }
                    else
                    {
                        Parallel.ForEach(currentNode.Children, child =>
                        {
                            concurrentBag.Add(child);
                        });
                    }
                }
            }

            return result.ToList();
        }

    }
}
