using AUS2_Sem1_withGUI.Data_Structures.QuadTree;
using AUS2_Sem1_withGUI.Data_Structures.QuadTree.Logic;
using AUS2_Sem1_withGUI.GeoProject;
using AUS2_Sem1_withGUI.Utils;
using System.Collections.Concurrent;
using static System.Windows.Forms.AxHost;

namespace AUS2_Sem1.GeoProject
{
    public class Controller
    {
        private QuadTreeOptimalization<double> quadTree;

        public Controller(QuadTreeRectangle<double> boundary, int maxRegionsPerNode)
        {
            quadTree = new QuadTreeOptimalization<double>(boundary, maxRegionsPerNode);
        }

        public void AddEstate(int userId, string desc,
            (double lat, double lon, char latPos, char lonPos) topLeft,
            (double lat, double lon, char latPos, char lonPos) topRight)
        {
            var estate = new Estate(userId, desc, GeoType.Estate, topLeft, topRight);
            quadTree.Insert(estate);
            Console.WriteLine($"Estate with CustomId {estate.Id} added.");
            //ConcurrentBag<Parcel> parcelsToUpdate = new ConcurrentBag<Parcel>(
            //    quadTree.FindByRectangle(new QuadTreeRectangle<double>(estate.TopLeft.X, estate.TopLeft.Y, estate.Width, estate.Height))
            //    .OfType<Parcel>()
            //);

            //Parallel.ForEach(parcelsToUpdate, parcel =>
            //{
            //    parcel.AddEstate(estate);
            //});
        }


        public void AddParcel(int userId, string desc,
            (double lat, double lon, char latPos, char lonPos) topLeft,
            (double lat, double lon, char latPos, char lonPos) topRight)
        {
            var parcel = new Parcel(userId, desc, GeoType.Parcel, topLeft, topRight);
            quadTree.Insert(parcel);
            Console.WriteLine($"Parcel with CustomId {parcel.Id} added.");
            //ConcurrentBag<Estate> estatesToUpdate = new ConcurrentBag<Estate>(
            //    quadTree.FindByRectangle(new QuadTreeRectangle<double>(parcel.TopLeft.X, parcel.TopLeft.Y, parcel.Width, parcel.Height))
            //    .OfType<Estate>()
            //);

            //Parallel.ForEach(estatesToUpdate, estate =>
            //{
            //    estate.AddParcel(parcel);
            //});
        }


        public void DeleteEstate(int id, double x, double y)
        {
            var toDelete = FindEstateByPosition(x, y).Find(obj => obj.Id == id);
            if (toDelete != null)
            {
                ConcurrentBag<Parcel> parcelsToRemove = new ConcurrentBag<Parcel>(toDelete.GetParcels());

                Parallel.ForEach(parcelsToRemove, parcel =>
                {
                    parcel.GetEstates().Remove(toDelete);
                });

                quadTree.Delete(toDelete);
            }
            else
            {
                Console.WriteLine($"Estate with CustomId {id} not found.");
            }
        }

        public void DeleteParcel(int id, double x, double y)
        {
            var toDelete = FindParcelByPosition(x, y).Find(obj => obj.Id == id);
            if (toDelete != null)
            {
                ConcurrentBag<Estate> estatesToRemove = new ConcurrentBag<Estate>(toDelete.GetEstates());

                Parallel.ForEach(estatesToRemove, estate =>
                {
                    estate.GetParcels().Remove(toDelete);
                });

                quadTree.Delete(toDelete);
            }
            else
            {
                Console.WriteLine($"Parcel with CustomId {id} not found.");
            }
        }

        public void EditEstate(int id, string desc, int userId, double x, double y)
        {
            var estate = FindEstateByPosition(x, y).Find(obj => obj.Id == id);
            if (estate != null)
            {
                estate.IdNumberByUser = userId;
                estate.Description = desc;
            }
            else
            {
                Console.WriteLine($"Estate with CustomId {id} not found.");
            }
        }

        public void EditParcel(int id, string desc, int userId, double x, double y)
        {
            var parcel = FindParcelByPosition(x, y).Find(obj => obj.Id == id);
            if (parcel != null)
            {
                parcel.IdNumberByUser = userId;
                parcel.Description = desc;
            }
            else
            {
                Console.WriteLine($"Parcel with CustomId {id} not found.");
            }
        }

        public List<Parcel> FindParcelByPosition(double lat, double lon)
        {
            var unfiltered = FindByPosition(lat, lon);
            var result = unfiltered.OfType<Parcel>();
            return result.ToList();
        }

        public List<Estate> FindEstateByPosition(double lat, double lon)
        {
            var unfiltered = FindByPosition(lat, lon);
            var result = unfiltered.OfType<Estate>();
            return result.ToList();
        }

        public List<GeoObject> FindGeoObjectByRegion(double lat1, double lon1, double lat2, double lon2)
        {
            var unfiltered = FindByRegion(lat1, lon1, lat2, lon2);
            var result = unfiltered.OfType<GeoObject>();
            return result.ToList();
        }

        #region Save/Load
        public void SaveData(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("GeoPR_AUS2_SEM1");

                ConcurrentBag<QuadTreeNode<double>> concurrentBag = new ConcurrentBag<QuadTreeNode<double>>
                {
                    quadTree.Root
                };

                Parallel.ForEach(concurrentBag, currentNode =>
                {
                    foreach (var region in currentNode.Regions)
                    {
                        if (region is Parcel parcel)
                        {
                            writer.Write("PARCEL,-,");
                            writer.Write($"{parcel.IdNumberByUser},-,{parcel.Description},-,{parcel.TopLeft.X},-,{parcel.TopLeft.Y},-,{parcel.TopLeft.XPosition.PositionToChar()},-,{parcel.TopLeft.YPosition.PositionToChar()}");
                            writer.WriteLine();
                        }
                        else if (region is Estate estate)
                        {
                            writer.Write("ESTATE,-,");
                            writer.Write($"{estate.IdNumberByUser},-,{estate.Description},-,{estate.TopLeft.X},-,{estate.TopLeft.Y},-,{estate.TopLeft.XPosition.PositionToChar()},-,{estate.TopLeft.YPosition.PositionToChar()}");
                            writer.WriteLine();
                        }
                    }

                    if (!currentNode.IsLeaf)
                    {
                        Parallel.ForEach(currentNode.Children, child =>
                        {
                            concurrentBag.Add(child);
                        });
                    }
                });
            }
        }

        public void LoadData(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();
                if (line == "GeoPR_AUS2_SEM1")
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        try
                        {
                            string[] data = line.Split(',');
                            if (data[0] == "PARCEL")
                            {
                                AddParcel(int.Parse(data[1]), data[2], (double.Parse(data[3]), double.Parse(data[5]), data[7][0], data[9][0]), (double.Parse(data[4]), double.Parse(data[6]), data[8][0], data[10][0]));
                            }
                            else if (data[0] == "ESTATE")
                            {
                                AddEstate(int.Parse(data[1]), data[2], (double.Parse(data[3]), double.Parse(data[5]), data[7][0], data[9][0]), (double.Parse(data[4]), double.Parse(data[6]), data[8][0], data[10][0]));
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Wrong file format.");
                            return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Wrong file format.");
                }
            }
        }   
        #endregion Save/Load

        #region Private

        private List<IQuadTreeData<double>> FindByPosition(double lat, double lon)
        {
            return quadTree.FindByPoint(new GPSPosition(lat, lon, 'N', 'E'));
        }

        private List<IQuadTreeData<double>> FindByRegion(double lat1, double lon1, double lat2, double lon2)
        {
            var topLeft = new GPSPosition(lat1, lon1, 'N', 'E');
            var bottomRight = new GPSPosition(lat2, lon2, 'N', 'E');
            var rectangle = new QuadTreeRectangle<double>(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
            return quadTree.FindByRectangle(rectangle);
        }
        #endregion
    }
}
