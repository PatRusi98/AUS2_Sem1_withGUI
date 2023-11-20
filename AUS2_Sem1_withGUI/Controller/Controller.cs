using AUS2_Sem1_withGUI.Data_Structures.QuadTree;
using AUS2_Sem1_withGUI.Data_Structures.QuadTree.Logic;
using AUS2_Sem1_withGUI.GeoProject;
using AUS2_Sem1_withGUI.Utils;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace AUS2_Sem1.GeoProject
{
    public class Controller
    {
        private QuadTree<double> quadTree;
        private QuadTreeRectangle<double> _boundary;
        private int regionsPerNode;
        private int height;

        public Controller(QuadTreeRectangle<double> boundary, int maxRegionsPerNode, int maxHeight)
        {
            _boundary = boundary;
            quadTree = new QuadTree<double>(boundary, maxRegionsPerNode, maxHeight);
            regionsPerNode = maxRegionsPerNode;
            height = maxHeight;
        }

        public void AddEstate(int userId, string desc,
            (double lat, double lon, char latPos, char lonPos) topLeft,
            (double lat, double lon, char latPos, char lonPos) topRight)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var estate = new Estate(userId, desc, GeoType.Estate, topLeft, topRight);
            quadTree.Insert(estate);
            Console.WriteLine($"Estate with CustomId {estate.Id} added.");
            ConcurrentBag<Parcel> parcelsToUpdate = new ConcurrentBag<Parcel>(
                quadTree.FindByRectangle(new QuadTreeRectangle<double>(estate.TopLeft.X, estate.TopLeft.Y, estate.Width, estate.Height))
                .OfType<Parcel>()
            );

            Parallel.ForEach(parcelsToUpdate, parcel =>
            {
                parcel.AddEstate(estate);
            });

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }


        public void AddParcel(int userId, string desc,
            (double lat, double lon, char latPos, char lonPos) topLeft,
            (double lat, double lon, char latPos, char lonPos) topRight)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var parcel = new Parcel(userId, desc, GeoType.Parcel, topLeft, topRight);
            quadTree.Insert(parcel);
            Console.WriteLine($"Parcel with CustomId {parcel.Id} added.");
            ConcurrentBag<Estate> estatesToUpdate = new ConcurrentBag<Estate>(
                quadTree.FindByRectangle(new QuadTreeRectangle<double>(parcel.TopLeft.X, parcel.TopLeft.Y, parcel.Width, parcel.Height))
                .OfType<Estate>()
            );

            Parallel.ForEach(estatesToUpdate, estate =>
            {
                estate.AddParcel(parcel);
            });

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }


        public void DeleteEstate(int id, double x, double y)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var toDelete = FindEstateByPosition(x, y).Find(obj => obj.Id == id);
            if (toDelete != null)
            {
                ConcurrentBag<Parcel> parcelsToRemove = new ConcurrentBag<Parcel>(toDelete.GetParcels());

                Parallel.ForEach(parcelsToRemove, parcel =>
                {
                    try
                    {
                        parcel.GetEstates().Remove(toDelete);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });

                quadTree.Delete(toDelete);
            }
            else
            {
                Console.WriteLine($"Estate with CustomId {id} not found.");
            }

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        public void DeleteParcel(int id, double x, double y)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var toDelete = FindParcelByPosition(x, y).Find(obj => obj.Id == id);
            if (toDelete != null)
            {
                ConcurrentBag<Estate> estatesToRemove = new ConcurrentBag<Estate>(toDelete.GetEstates());

                Parallel.ForEach(estatesToRemove, estate =>
                {
                    try
                    {
                        estate.GetParcels().Remove(toDelete);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });

                quadTree.Delete(toDelete);
            }
            else
            {
                Console.WriteLine($"Parcel with CustomId {id} not found.");
            }

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        public void EditEstate(int id, string desc, int userId, double x, double y)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

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

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        public void EditParcel(int id, string desc, int userId, double x, double y)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

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

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        public Parcel FindParcelById(int id, double lat, double lon)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = FindParcelByPosition(lat, lon).Find(obj => obj.Id == id);

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            return result;
        }

        public Estate FindEstateById(int id, double lat, double lon)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = FindEstateByPosition(lat, lon).Find(obj => obj.Id == id);

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            return result;
        }

        public List<Parcel> FindParcelByPosition(double lat, double lon)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var unfiltered = FindByPosition(lat, lon);
            var result = unfiltered.OfType<Parcel>();

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            return result.ToList();
        }

        public List<Estate> FindEstateByPosition(double lat, double lon)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var unfiltered = FindByPosition(lat, lon);
            var result = unfiltered.OfType<Estate>();

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            return result.ToList();
        }

        public List<GeoObject> FindGeoObjectByRegion(double lat1, double lon1, double lat2, double lon2)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var unfiltered = FindByRegion(lat1, lon1, lat2, lon2);
            var result = unfiltered.OfType<GeoObject>();

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            return result.ToList();
        }

        public List<GeoObject> FindAllObjects()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var unfiltered = FindByRegion(_boundary.X, _boundary.Y, _boundary.X + _boundary.Width, _boundary.Y + _boundary.Height);
            var result = unfiltered.OfType<GeoObject>();

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            return result.ToList();
        }

        #region Save/Load
        public void SaveData(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("GeoPR_AUS2_SEM1");
                writer.WriteLine($"QUADTREE;{_boundary.X};{_boundary.Y};{_boundary.Width};{_boundary.Height};{regionsPerNode};{height}");

                var objects = FindAllObjects();

                foreach (var item in objects)
                {
                    if (item is Parcel parcel)
                    {
                        writer.Write("PARCEL;");
                        writer.Write($"{parcel.IdNumberByUser};{parcel.Description};{parcel.TopLeft.X};{parcel.TopLeft.Y};{parcel.TopLeft.XPosition.PositionToChar()};{parcel.TopLeft.YPosition.PositionToChar()};{parcel.BottomRight.X};{parcel.BottomRight.Y};{parcel.BottomRight.XPosition.PositionToChar()};{parcel.BottomRight.YPosition.PositionToChar()}");
                        writer.WriteLine();
                    }
                    else if (item is Estate estate)
                    {
                        writer.Write("ESTATE;");
                        writer.Write($"{estate.IdNumberByUser};{estate.Description};{estate.TopLeft.X};{estate.TopLeft.Y};{estate.TopLeft.XPosition.PositionToChar()};{estate.TopLeft.YPosition.PositionToChar()};{estate.BottomRight.X};{estate.BottomRight.Y};{estate.BottomRight.XPosition.PositionToChar()};{estate.BottomRight.YPosition.PositionToChar()}");
                        writer.WriteLine();
                    }
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
