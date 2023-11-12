using AUS2_Sem1_withGUI.Data_Structures.QuadTree;
using AUS2_Sem1_withGUI.Data_Structures.QuadTree.Logic;
using AUS2_Sem1_withGUI.GeoProject;
using AUS2_Sem1_withGUI.Utils;

namespace AUS2_Sem1.GeoProject
{
    public class Controller
    {
        private QuadTree<double> quadTree;

        public Controller(QuadTreeRectangle<double> boundary, int maxRegionsPerNode)
        {
            quadTree = new QuadTree<double>(boundary, maxRegionsPerNode);
        }

        public void AddEstate(int userId, string desc,
            (double lat, double lon, char latPos, char lonPos) topLeft,
            (double lat, double lon, char latPos, char lonPos) topRight)
        {
            var estate = new Estate(userId, desc, GeoType.Estate, topLeft, topRight);
            quadTree.Insert(estate);

            foreach (var parcel in quadTree.FindByRectangle(new QuadTreeRectangle<double>(estate.TopLeft.X, estate.TopLeft.Y, estate.Width, estate.Height)))
            {
                if (parcel is Parcel)
                {
                    ((Parcel)parcel).AddEstate(estate);
                }
            }
        }

        public void AddParcel(int userId, string desc,
            (double lat, double lon, char latPos, char lonPos) topLeft,
            (double lat, double lon, char latPos, char lonPos) topRight)
        {
            var parcel = new Parcel(userId, desc, GeoType.Parcel, topLeft, topRight);
            quadTree.Insert(parcel);

            foreach (var estate in quadTree.FindByRectangle(new QuadTreeRectangle<double>(parcel.TopLeft.X, parcel.TopLeft.Y, parcel.Width, parcel.Height)))
            {
                if (estate is Estate)
                {
                    ((Estate)estate).AddParcel(parcel);
                }
            }
        }

        public void DeleteEstate(int id, double x, double y)
        {
            //var parcel = quadTree.FindByPoint(new GPSPosition(x, y, 'N', 'E')).Find(obj => obj is Estate && ((Estate)obj).Id == id) as Estate;
            var toDelete = FindEstateByPosition(x, y).Find(obj => obj.Id == id);
            if (toDelete != null)
            {
                foreach (var parcel in toDelete.GetParcels())
                {
                    parcel.GetEstates().Remove(toDelete);
                }

                quadTree.Delete(toDelete);
            }
            else
            {
                Console.WriteLine($"Estate with CustomId {id} not found.");
            }
        }

        public void DeleteParcel(int id, double x, double y)
        {
            //var parcel = quadTree.FindByPoint(new GPSPosition(x, y, 'N', 'E')).Find(obj => obj is Parcel && ((Parcel)obj).Id == id) as Parcel;
            var toDelete = FindParcelByPosition(x, y).Find(obj => obj.Id == id);
            if (toDelete != null)
            {
                foreach (var estate in toDelete.GetEstates())
                {
                    estate.GetParcels().Remove(toDelete);
                }

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
