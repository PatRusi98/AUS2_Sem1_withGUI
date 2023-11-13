using AUS2_Sem1_withGUI.Data_Structures.QuadTree.Interfaces;
using System.Collections.Concurrent;

namespace AUS2_Sem1_withGUI.Data_Structures.QuadTree.Logic
{
    public class QuadTreeOptimalization<T> : QuadTree<T> where T : IComparable
    {
        public QuadTreeOptimalization(QuadTreeRectangle<T> boundary, int maxRegionsPerNode)
            : base(boundary, maxRegionsPerNode)
        {
        }

        public new void Insert(IQuadTreeData<T> region)
        {
            var regionRectangle = new QuadTreeRectangle<T>(region.X, region.Y, region.Width, region.Height);

            if (!Root.Boundary.IntersectsWith(regionRectangle))
                return;

            var parallelBag = new ConcurrentBag<QuadTreeNode<T>>
            {
                Root
            };

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
