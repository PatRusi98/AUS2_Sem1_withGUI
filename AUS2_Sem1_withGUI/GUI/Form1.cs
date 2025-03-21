using AUS2_Sem1.GeoProject;
using AUS2_Sem1_withGUI.Data_Structures.QuadTree.Logic;
using AUS2_Sem1_withGUI.GeoProject;
using AUS2_Sem1_withGUI.Utils;
using System.Runtime.CompilerServices;

namespace AUS2_Sem1_withGUI
{
    public partial class Form1 : Form
    {
        private Controller GeoSystem;
        private Random GeoRandom = new Random();
        private double maxRange;
        private static bool QuadTreeExists { get; set; }

        public Form1()
        {
            InitializeComponent();
            QuadTreeExists = false;
        }

        #region Menu File

        #region Load
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;

                using (StreamReader reader = new StreamReader(path))
                {
                    string line = reader.ReadLine();
                    if (line == "GeoPR_AUS2_SEM1")
                    {
                        if ((line = reader.ReadLine()) != null)
                        {
                            try
                            {
                                string[] qtData = line.Split(';');
                                if (qtData[0] == "QUADTREE")
                                {
                                    var newBoundary = new QuadTreeRectangle<double>(double.Parse(qtData[1]), double.Parse(qtData[2]), double.Parse(qtData[3]), double.Parse(qtData[4]));
                                    GeoSystem = new Controller(newBoundary, int.Parse(qtData[5]), int.Parse(qtData[6]));
                                }
                                else
                                {
                                    Console.WriteLine("Wrong file format.");
                                    return;
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Wrong file format.");
                                return;
                            }

                            while ((line = reader.ReadLine()) != null)
                            {
                                try
                                {
                                    string[] data = line.Split(';');
                                    if (data[0] == "PARCEL")
                                    {
                                        GeoSystem.AddParcel(int.Parse(data[1]), data[2], (double.Parse(data[3]), double.Parse(data[4]), data[5][0], data[6][0]), (double.Parse(data[7]), double.Parse(data[8]), data[9][0], data[10][0]));
                                    }
                                    else if (data[0] == "ESTATE")
                                    {
                                        GeoSystem.AddEstate(int.Parse(data[1]), data[2], (double.Parse(data[3]), double.Parse(data[4]), data[5][0], data[6][0]), (double.Parse(data[7]), double.Parse(data[8]), data[9][0], data[10][0]));
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Wrong file format.");
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong file format.");
                    }
                }
            }

            QuadTreeExists = true;
        }
        #endregion

        #region Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = saveFileDialog1.FileName;
                GeoSystem.SaveData(path);
            }
        }
        #endregion

        #region Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion Exit

        #endregion Menu File

        #region Menu Parcel

        #region Add
        private void addParcelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var id = "";
            var desc = "";
            var lat1 = 0.0;
            var lon1 = 0.0;
            var lat2 = 0.0;
            var lon2 = 0.0;

            if (AddObject(title: "Add Parcel", ref id, ref desc, ref lat1, ref lon1, ref lat2, ref lon2) == DialogResult.OK)
            {
                GeoSystem.AddParcel(int.Parse(id), desc, (lat1, lon1, 'N', 'E'), (lat2, lon2, 'S', 'W'));
            }
            else
            {
                return;
            }
        }
        #endregion Add

        #region Find
        private void findParcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = 0.0;
            var y = 0.0;

            if (FindObject(title: "Find Parcel", ref x, ref y) == DialogResult.OK)
            {
                dataGridView1.DataSource = GeoSystem.FindParcelByPosition(x, y);
            }
            else
            {
                return;
            }
        }
        #endregion Find

        #endregion Menu Parcel

        #region Menu Estate

        #region Add
        private void addEstateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = "";
            var desc = "";
            var lat1 = 0.0;
            var lon1 = 0.0;
            var lat2 = 0.0;
            var lon2 = 0.0;

            if (AddObject(title: "Add Estate", ref id, ref desc, ref lat1, ref lon1, ref lat2, ref lon2) == DialogResult.OK)
            {
                GeoSystem.AddEstate(int.Parse(id), desc, (lat1, lon1, 'N', 'E'), (lat2, lon2, 'S', 'W'));
            }
            else
            {
                return;
            }
        }

        private void addEstateToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var id = "";
            var desc = "";
            var lat1 = 0.0;
            var lon1 = 0.0;
            var lat2 = 0.0;
            var lon2 = 0.0;

            if (AddObject(title: "Add Estate", ref id, ref desc, ref lat1, ref lon1, ref lat2, ref lon2) == DialogResult.OK)
            {
                GeoSystem.AddEstate(int.Parse(id), desc, (lat1, lon1, 'N', 'E'), (lat2, lon2, 'S', 'W'));
            }
            else
            {
                return;
            }
        }
        #endregion Add

        #region Find
        private void findEstateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var x = 0.0;
            var y = 0.0;

            if (FindObject(title: "Find Estate", ref x, ref y) == DialogResult.OK)
            {
                dataGridView1.DataSource = GeoSystem.FindEstateByPosition(x, y);
            }
            else
            {
                return;
            }
        }
        #endregion Find

        #endregion Menu Estate

        #region Menu All Objects
        private void findAllInRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x1 = 0.0;
            var y1 = 0.0;
            var x2 = 0.0;
            var y2 = 0.0;

            if (FindObjectInRange(title: "Find Parcel", ref x1, ref y1, ref x2, ref y2) == DialogResult.OK)
            {
                dataGridView1.DataSource = GeoSystem.FindGeoObjectByRegion(x1, y1, x2, y2);
            }
            else
            {
                return;
            }
        }
        #endregion Menu All Objects

        #region Menu Generator

        #region Add
        private void parcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = 0;

            if (RandomOperations(title: "Add Random Parcels", ref x) == DialogResult.OK)
            {
                for (int i = 0; i < x; i++)
                {
                    CreateRandomParcel(GeoRandom);
                }
            }
            else
            {
                return;
            }
        }

        private void estatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = 0;

            if (RandomOperations(title: "Add Random Estates", ref x) == DialogResult.OK)
            {
                for (int i = 0; i < x; i++)
                {
                    CreateRandomEstate(GeoRandom);
                }
            }
            else
            {
                return;
            }
        }

        private void randomObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = 0;

            if (RandomOperations(title: "Add Random Objects", ref x) == DialogResult.OK)
            {
                for (int i = 0; i < x; i++)
                {
                    if (GeoRandom.Next(2) == 0)
                    {
                        CreateRandomParcel(GeoRandom);
                    }
                    else
                    {
                        CreateRandomEstate(GeoRandom);
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void CreateRandomEstate(Random random)
        {
            double x = random.NextDouble() * maxRange * 0.9869;
            double y = random.NextDouble() * maxRange * 0.9869;
            int id = random.Next(1, int.MaxValue);
            GeoSystem.AddEstate(id, "randomDesc", (x, y, 'N', 'E'), (x + 1, y + 1, 'S', 'W'));
            string description = $"Estate {id}";
            Console.WriteLine($"Estate created: {x}, {y}, id: {id}, {description}");
        }

        private void CreateRandomParcel(Random random)
        {
            double x = random.NextDouble() * maxRange * 0.9869;
            double y = random.NextDouble() * maxRange * 0.9869;
            int id = random.Next(1, int.MaxValue);
            GeoSystem.AddParcel(id, "randomDesc", (x, y, 'N', 'E'), (x + 1, y + 1, 'S', 'W'));
            string description = $"Parcel {id}";
            Console.WriteLine($"Parcel created: {x}, {y}, id: {id}, {description}");
        }
        #endregion Add

        #region Random Operations
        private void randomOperationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = 0;

            if (RandomOperations(title: "Random Operations", ref x) == DialogResult.OK)
            {
                for (int i = 0; i < x; i++)
                {
                    int operation = GeoRandom.Next(3);
                    switch (operation)
                    {
                        case 0:
                            if (GeoRandom.Next(2) == 0)
                            {
                                CreateRandomParcel(GeoRandom);
                            }
                            else
                            {
                                CreateRandomEstate(GeoRandom);
                            }
                            break;
                        case 1:
                            dataGridView1.DataSource = FindRandomObject(GeoRandom);
                            break;
                        case 2:
                            DeleteRandomObject(GeoRandom);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                return;
            }
        }

        private List<GeoObject> FindRandomObject(Random random)
        {
            var x = random.NextDouble();
            var y = random.NextDouble();

            if (random.Next(2) == 0)
            {
                return GeoSystem.FindParcelByPosition(x, y).ToList<GeoObject>();
            }
            else
            {
                return GeoSystem.FindEstateByPosition(x, y).ToList<GeoObject>();
            }
        }

        private void DeleteRandomObject(Random random)
        {
            var obj = FindRandomObject(random).FirstOrDefault();

            if (obj != null && obj.Type == GeoType.Estate)
            {
                GeoSystem.DeleteEstate(obj.Id, obj.X, obj.Y);
            }
            else if (obj != null && obj.Type == GeoType.Parcel)
            {
                GeoSystem.DeleteParcel(obj.Id, obj.X, obj.Y);
            }
        }
        #endregion Random Operations

        #endregion Menu Generator

        #region Popup

        #region GPS Position
        private static DialogResult FindObject(string title, ref double x, ref double y)
        {
            Form form = new Form();
            //form.AutoSize = true;
            form.Height = 600;
            form.Width = 600;
            form.Text = title;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            Label labelX = new Label() { Left = 50, Top = 140, Text = "Latitude:" };
            labelX.AutoSize = true;
            TextBox boxX = new TextBox() { Left = 50, Top = 170, Width = 200 };
            Label labelY = new Label() { Left = 50, Top = 200, Text = "Longitude:" };
            labelY.AutoSize = true;
            TextBox boxY = new TextBox() { Left = 50, Top = 230, Width = 200 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 260 };
            confirmation.AutoSize = true;
            Button cancel = new Button() { Text = "Cancel", Left = 250, Width = 100, Top = 260 };
            cancel.AutoSize = true;
            confirmation.Click += (sender, e) => { form.DialogResult = DialogResult.OK; };
            cancel.Click += (sender, e) => { form.Close(); };
            form.Controls.Add(labelX);
            form.Controls.Add(boxX);
            form.Controls.Add(labelY);
            form.Controls.Add(boxY);
            form.Controls.Add(confirmation);
            form.Controls.Add(cancel);
            form.AcceptButton = confirmation;
            form.CancelButton = cancel;

            if (!QuadTreeExists)
            {
                form.Close();
                return DialogResult.Cancel;
            }

            form.ShowDialog();

            try
            {
                x = double.Parse(boxX.Text);
                y = double.Parse(boxY.Text);
                DialogResult result = DialogResult.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return DialogResult.Cancel;
            }

            return form.DialogResult;
        }
        #endregion GPS Position

        #region New
        private static DialogResult NewProject(string title, ref double x, ref double y, ref int height)
        {
            Form form = new Form();
            //form.AutoSize = true;
            form.Height = 600;
            form.Width = 600;
            form.Text = title;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            Label labelX = new Label() { Left = 50, Top = 20, Text = "From:" };
            labelX.AutoSize = true;
            TextBox boxX = new TextBox() { Left = 50, Top = 60, Width = 200 };
            Label labelY = new Label() { Left = 50, Top = 100, Text = "To:" };
            labelY.AutoSize = true;
            TextBox boxY = new TextBox() { Left = 50, Top = 140, Width = 200 };
            Label labelHeight = new Label() { Left = 50, Top = 180, Text = "Height:" };
            labelHeight.AutoSize = true;
            TextBox boxHeight = new TextBox() { Left = 50, Top = 220, Width = 200 };
            Button confirmation = new Button() { Left = 350, Width = 100, Top = 260, Text = "Ok" };
            confirmation.AutoSize = true;
            Button cancel = new Button() { Left = 250, Width = 100, Top = 260, Text = "Cancel" };
            cancel.AutoSize = true;
            confirmation.Click += (sender, e) => { form.DialogResult = DialogResult.OK; };
            cancel.Click += (sender, e) => { form.Close(); };
            form.Controls.Add(labelX);
            form.Controls.Add(boxX);
            form.Controls.Add(labelY);
            form.Controls.Add(boxY);
            form.Controls.Add(labelHeight);
            form.Controls.Add(boxHeight);
            form.Controls.Add(confirmation);
            form.Controls.Add(cancel);
            form.AcceptButton = confirmation;
            form.CancelButton = cancel;

            form.ShowDialog();

            try
            {
                x = double.Parse(boxX.Text);
                y = double.Parse(boxY.Text);
                height = int.Parse(boxHeight.Text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return DialogResult.Cancel;
            }

            return form.DialogResult;
        }
        #endregion New

        #region Range
        private static DialogResult FindObjectInRange(string title, ref double x1, ref double y1, ref double x2, ref double y2)
        {
            Form form = new Form();
            //form.AutoSize = true;
            form.Height = 600;
            form.Width = 600;
            form.Text = title;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            Label labelX1 = new Label() { Left = 50, Top = 20, Text = "Latitude 1:" };
            labelX1.AutoSize = true;
            TextBox boxX1 = new TextBox() { Left = 50, Top = 50, Width = 200 };
            Label labelY1 = new Label() { Left = 50, Top = 80, Text = "Longitude 1:" };
            labelY1.AutoSize = true;
            TextBox boxY1 = new TextBox() { Left = 50, Top = 110, Width = 200 };
            Label labelX2 = new Label() { Left = 50, Top = 140, Text = "Latitude 2:" };
            labelX2.AutoSize = true;
            TextBox boxX2 = new TextBox() { Left = 50, Top = 170, Width = 200 };
            Label labelY2 = new Label() { Left = 50, Top = 200, Text = "Longitude 2:" };
            labelY2.AutoSize = true;
            TextBox boxY2 = new TextBox() { Left = 50, Top = 230, Width = 200 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 260 };
            confirmation.AutoSize = true;
            Button cancel = new Button() { Text = "Cancel", Left = 250, Width = 100, Top = 260 };
            cancel.AutoSize = true;
            confirmation.Click += (sender, e) => { form.DialogResult = DialogResult.OK; };
            cancel.Click += (sender, e) => { form.Close(); };
            form.Controls.Add(labelX1);
            form.Controls.Add(boxX1);
            form.Controls.Add(labelY1);
            form.Controls.Add(boxY1);
            form.Controls.Add(labelX2);
            form.Controls.Add(boxX2);
            form.Controls.Add(labelY2);
            form.Controls.Add(boxY2);
            form.Controls.Add(confirmation);
            form.Controls.Add(cancel);
            form.AcceptButton = confirmation;
            form.CancelButton = cancel;

            if (!QuadTreeExists)
            {
                form.Close();
                return DialogResult.Cancel;
            }

            form.ShowDialog();

            try
            {
                x1 = double.Parse(boxX1.Text);
                y2 = double.Parse(boxY1.Text);
                x2 = double.Parse(boxX2.Text);
                y2 = double.Parse(boxY2.Text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return DialogResult.Cancel;
            }

            return form.DialogResult;
        }
        #endregion Range

        #region Create
        private static DialogResult AddObject(string title, ref string id, ref string desc, ref double lat1, ref double lon1, ref double lat2, ref double lon2)
        {
            Form form = new Form();
            //form.AutoSize = true;
            form.Height = 600;
            form.Width = 600;
            form.Text = title;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            Label labelId = new Label() { Left = 50, Top = 20, Text = "ID:" };
            labelId.AutoSize = true;
            TextBox boxId = new TextBox() { Left = 50, Top = 45, Width = 400 };
            Label labelDesc = new Label() { Left = 50, Top = 70, Text = "Description:" };
            labelDesc.AutoSize = true;
            TextBox boxDesc = new TextBox() { Left = 50, Top = 95, Width = 400 };
            Label labelLat1 = new Label() { Left = 50, Top = 120, Text = "Latitude 1:" };
            labelLat1.AutoSize = true;
            TextBox boxLat1 = new TextBox() { Left = 50, Top = 145, Width = 400 };
            Label labelLon1 = new Label() { Left = 50, Top = 170, Text = "Longitude 1:" };
            labelLon1.AutoSize = true;
            TextBox boxLon1 = new TextBox() { Left = 50, Top = 195, Width = 400 };
            Label labelLat2 = new Label() { Left = 50, Top = 220, Text = "Latitude 2:" };
            labelLat2.AutoSize = true;
            TextBox boxLat2 = new TextBox() { Left = 50, Top = 245, Width = 400 };
            Label labelLon2 = new Label() { Left = 50, Top = 270, Text = "Longitude 2:" };
            labelLon2.AutoSize = true;
            TextBox boxLon2 = new TextBox() { Left = 50, Top = 295, Width = 400 };
            Button confirmation = new Button() { Text = "Add", Left = 350, Width = 100, Top = 320 };
            confirmation.AutoSize = true;
            Button cancel = new Button() { Text = "Cancel", Left = 250, Width = 100, Top = 320 };
            cancel.AutoSize = true;
            confirmation.Click += (sender, e) => { form.DialogResult = DialogResult.OK; };
            cancel.Click += (sender, e) => { form.Close(); };
            form.Controls.Add(labelId);
            form.Controls.Add(boxId);
            form.Controls.Add(labelDesc);
            form.Controls.Add(boxDesc);
            form.Controls.Add(labelLat1);
            form.Controls.Add(boxLat1);
            form.Controls.Add(labelLon1);
            form.Controls.Add(boxLon1);
            form.Controls.Add(labelLat2);
            form.Controls.Add(boxLat2);
            form.Controls.Add(labelLon2);
            form.Controls.Add(boxLon2);
            form.Controls.Add(confirmation);
            form.Controls.Add(cancel);
            form.AcceptButton = confirmation;
            form.CancelButton = cancel;

            if (!QuadTreeExists)
            {
                form.Close();
                return DialogResult.Cancel;
            }

            form.ShowDialog();

            try
            {
                id = boxId.Text;
                desc = boxDesc.Text;
                lat1 = double.Parse(boxLat1.Text);
                lat2 = double.Parse(boxLat2.Text);
                lon1 = double.Parse(boxLon1.Text);
                lon2 = double.Parse(boxLon2.Text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return DialogResult.Cancel;
            }

            return form.DialogResult;
        }
        #endregion Create

        #region Edit
        private static DialogResult EditObject(string title, ref int id, ref string desc, ref double lat1, ref double lon1, ref double lat2, ref double lon2)
        {
            Form form = new Form();
            //form.AutoSize = true;
            form.Height = 600;
            form.Width = 600;
            form.Text = title;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            Label labelId = new Label() { Left = 50, Top = 20, Text = "ID:" };
            labelId.AutoSize = true;
            TextBox boxId = new TextBox() { Left = 50, Top = 45, Width = 400 };
            boxId.Text = id.ToString();
            Label labelDesc = new Label() { Left = 50, Top = 70, Text = "Description:" };
            labelDesc.AutoSize = true;
            TextBox boxDesc = new TextBox() { Left = 50, Top = 95, Width = 400 };
            boxDesc.Text = desc;
            Label labelLat1 = new Label() { Left = 50, Top = 120, Text = "Latitude 1:" };
            labelLat1.AutoSize = true;
            TextBox boxLat1 = new TextBox() { Left = 50, Top = 145, Width = 400 };
            boxLat1.Text = lat1.ToString();
            Label labelLon1 = new Label() { Left = 50, Top = 170, Text = "Longitude 1:" };
            labelLon1.AutoSize = true;
            TextBox boxLon1 = new TextBox() { Left = 50, Top = 195, Width = 400 };
            boxLon1.Text = lon1.ToString();
            Label labelLat2 = new Label() { Left = 50, Top = 220, Text = "Latitude 2:" };
            labelLat2.AutoSize = true;
            TextBox boxLat2 = new TextBox() { Left = 50, Top = 245, Width = 400 };
            boxLat2.Text = lat2.ToString();
            Label labelLon2 = new Label() { Left = 50, Top = 270, Text = "Longitude 2:" };
            labelLon2.AutoSize = true;
            TextBox boxLon2 = new TextBox() { Left = 50, Top = 295, Width = 400 };
            boxLon2.Text = lon2.ToString();
            Button confirmation = new Button() { Text = "Edit", Left = 350, Width = 100, Top = 320 };
            confirmation.AutoSize = true;
            Button cancel = new Button() { Text = "Cancel", Left = 250, Width = 100, Top = 320 };
            cancel.AutoSize = true;
            confirmation.Click += (sender, e) => { form.DialogResult = DialogResult.Yes; };
            cancel.Click += (sender, e) => { form.Close(); };
            form.Controls.Add(labelId);
            form.Controls.Add(boxId);
            form.Controls.Add(labelDesc);
            form.Controls.Add(boxDesc);
            form.Controls.Add(labelLat1);
            form.Controls.Add(boxLat1);
            form.Controls.Add(labelLon1);
            form.Controls.Add(boxLon1);
            form.Controls.Add(labelLat2);
            form.Controls.Add(boxLat2);
            form.Controls.Add(labelLon2);
            form.Controls.Add(boxLon2);
            form.Controls.Add(confirmation);
            form.Controls.Add(cancel);
            form.AcceptButton = confirmation;
            form.CancelButton = cancel;

            if (!QuadTreeExists)
            {
                form.Close();
                return DialogResult.Cancel;
            }

            form.ShowDialog();

            try
            {
                id = int.Parse(boxId.Text.ToString());
                desc = boxDesc.Text;
                lat1 = double.Parse(boxLat1.Text);
                lat2 = double.Parse(boxLat2.Text);
                lon1 = double.Parse(boxLon1.Text);
                lon2 = double.Parse(boxLon2.Text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return DialogResult.Cancel;
            }

            return form.DialogResult;
        }

        private static DialogResult EditOrDelete(string title, string detail)
        {
            Form form = new Form();
            //form.AutoSize = true;
            form.Height = 600;
            form.Width = 600;
            form.Text = title;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            Label labelDetail = new Label() { Left = 50, Top = 20, Text = detail };
            labelDetail.AutoSize = true;
            Button edit = new Button() { Text = "Edit", Left = 250, Width = 100, Top = 200 };
            edit.AutoSize = true;
            Button delete = new Button() { Text = "Delete", Left = 350, Width = 100, Top = 200 };
            delete.AutoSize = true;
            Button cancel = new Button() { Text = "Cancel", Left = 150, Width = 100, Top = 200 };
            cancel.AutoSize = true;
            edit.Click += (sender, e) => { form.DialogResult = DialogResult.No; };
            delete.Click += (sender, e) => { form.DialogResult = DialogResult.Yes; };
            cancel.Click += (sender, e) => { form.Close(); };
            form.Controls.Add(labelDetail);
            form.Controls.Add(delete);
            form.Controls.Add(edit);
            form.Controls.Add(cancel);
            form.CancelButton = cancel;

            if (!QuadTreeExists)
            {
                form.Close();
                return DialogResult.Cancel;
            }

            form.ShowDialog();

            return form.DialogResult;
        }
        #endregion Edit

        #region Delete
        private static DialogResult ConfirmDelete(string title)
        {
            Form form = new Form();
            //form.AutoSize = true;
            form.Height = 600;
            form.Width = 600;
            form.Text = title;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            Label labelCount = new Label() { Left = 50, Top = 140, Text = "Are u sure?" };
            labelCount.AutoSize = true;
            Button confirmation = new Button() { Text = "Delete", Left = 350, Width = 100, Top = 260 };
            confirmation.AutoSize = true;
            Button cancel = new Button() { Text = "Cancel", Left = 250, Width = 100, Top = 260 };
            cancel.AutoSize = true;
            confirmation.Click += (sender, e) => { form.DialogResult = DialogResult.OK; };
            cancel.Click += (sender, e) => { form.Close(); };
            form.Controls.Add(labelCount);
            form.Controls.Add(confirmation);
            form.Controls.Add(cancel);
            form.AcceptButton = confirmation;
            form.CancelButton = cancel;

            if (!QuadTreeExists)
            {
                form.Close();
                return DialogResult.Cancel;
            }

            form.ShowDialog();

            return form.DialogResult;
        }
        #endregion Delete

        #region Random Operations
        private static DialogResult RandomOperations(string title, ref int count)
        {
            Form form = new Form();
            //form.AutoSize = true;
            form.Height = 600;
            form.Width = 600;
            form.Text = title;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            Label labelCount = new Label() { Left = 50, Top = 140, Text = "Count:" };
            labelCount.AutoSize = true;
            TextBox boxCount = new TextBox() { Left = 50, Top = 170, Width = 200 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 260 };
            confirmation.AutoSize = true;
            Button cancel = new Button() { Text = "Cancel", Left = 250, Width = 100, Top = 260 };
            cancel.AutoSize = true;
            confirmation.Click += (sender, e) => { form.DialogResult = DialogResult.OK; };
            cancel.Click += (sender, e) => { form.Close(); };
            form.Controls.Add(labelCount);
            form.Controls.Add(boxCount);
            form.Controls.Add(confirmation);
            form.Controls.Add(cancel);
            form.AcceptButton = confirmation;
            form.CancelButton = cancel;

            if (!QuadTreeExists)
            {
                form.Close();
                return DialogResult.Cancel;
            }

            form.ShowDialog();

            try
            {
                count = int.Parse(boxCount.Text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return DialogResult.Cancel;
            }

            return form.DialogResult;
        }
        #endregion Random Operations

        #endregion Popup

        #region Table
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];

                var id = int.Parse(selectedRow.Cells["idSystem"].Value.ToString());
                var userId = int.Parse(selectedRow.Cells["idNumberByUserDataGridViewTextBoxColumn"].Value.ToString());
                var x = double.Parse(selectedRow.Cells["xValue"].Value.ToString());
                var y = double.Parse(selectedRow.Cells["yValue"].Value.ToString());
                var lat1 = double.Parse(selectedRow.Cells["xValue"].Value.ToString());
                var lon1 = double.Parse(selectedRow.Cells["yValue"].Value.ToString());
                var height = double.Parse(selectedRow.Cells["heightValue"].Value.ToString());
                var width = double.Parse(selectedRow.Cells["widthValue"].Value.ToString());
                var lat2 = lat1 + width;
                var lon2 = lon1 + height;
                var type = selectedRow.Cells["typeDataGridViewTextBoxColumn"].Value.ToString();

                var detail = "";
                if (type == "Parcel")
                {
                    detail = GeoSystem.FindParcelById(id, x, y).GetEstatesString();
                }
                else
                {
                    detail = GeoSystem.FindEstateById(id, x, y).GetParcelsString();
                }

                var operation = EditOrDelete(title: "Edit or Delete", detail);

                if (operation == DialogResult.No)
                {
                    var desc = selectedRow.Cells["descriptionDataGridViewTextBoxColumn"].Value.ToString();
                    var edit = EditObject(title: "Edit", ref userId, ref desc, ref lat1, ref lon1, ref lat2, ref lon2);
                    if (edit == DialogResult.Yes)
                    {
                        if (type == "Parcel")
                        {
                            GeoSystem.DeleteParcel(id, x, y);
                            GeoSystem.AddParcel(userId, desc, (lat1, lon1, 'N', 'E'), (lat2, lon2, 'S', 'W'));
                        }
                        else
                        {
                            GeoSystem.DeleteEstate(id, x, y);
                            GeoSystem.AddEstate(userId, desc, (lat1, lon1, 'N', 'E'), (lat2, lon2, 'S', 'W'));
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (operation == DialogResult.Yes)
                {
                    if (ConfirmDelete(title: "Confirm Delete") == DialogResult.OK)
                    {
                        if (type == "Parcel")
                        {
                            GeoSystem.DeleteParcel(id, x, y);
                            dataGridView1.Refresh();
                        }
                        else
                        {
                            GeoSystem.DeleteEstate(id, x, y);
                            dataGridView1.Refresh();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        #endregion Table

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = 0.0;
            var y = 0.0;
            maxRange = y;
            var maxHeight = 0;

            if (NewProject(title: "New project", ref x, ref y, ref maxHeight) == DialogResult.OK)
            {
                QuadTreeExists = true;
                var boundary = new QuadTreeRectangle<double>(x, x, y, y);
                GeoSystem = new Controller(boundary, 4, maxHeight);
            }
            else
            {
                return;
            }
        }

        private void findAllObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (QuadTreeExists)
            {
                dataGridView1.DataSource = GeoSystem.FindAllObjects();
            }
        }
    }
}