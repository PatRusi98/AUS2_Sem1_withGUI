namespace AUS2_Sem1_withGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            parcelToolStripMenuItem = new ToolStripMenuItem();
            addParcelToolStripMenuItem1 = new ToolStripMenuItem();
            findParcelToolStripMenuItem = new ToolStripMenuItem();
            estateToolStripMenuItem = new ToolStripMenuItem();
            addEstateToolStripMenuItem = new ToolStripMenuItem();
            findEstateToolStripMenuItem1 = new ToolStripMenuItem();
            allObjectsToolStripMenuItem = new ToolStripMenuItem();
            findAllInRangeToolStripMenuItem = new ToolStripMenuItem();
            generatorToolStripMenuItem = new ToolStripMenuItem();
            addToolStripMenuItem = new ToolStripMenuItem();
            parcelsToolStripMenuItem = new ToolStripMenuItem();
            estatesToolStripMenuItem = new ToolStripMenuItem();
            randomObjectsToolStripMenuItem = new ToolStripMenuItem();
            randomOperationsToolStripMenuItem = new ToolStripMenuItem();
            dataGridView1 = new DataGridView();
            geoObjectBindingSource3 = new BindingSource(components);
            geoObjectBindingSource2 = new BindingSource(components);
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            geoObjectBindingSource = new BindingSource(components);
            geoObjectBindingSource1 = new BindingSource(components);
            typeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            xValue = new DataGridViewTextBoxColumn();
            yValue = new DataGridViewTextBoxColumn();
            idNumberByUserDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            topLeftDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bottomRightDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idSystem = new DataGridViewTextBoxColumn();
            widthValue = new DataGridViewTextBoxColumn();
            heightValue = new DataGridViewTextBoxColumn();
            menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)geoObjectBindingSource3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)geoObjectBindingSource2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)geoObjectBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)geoObjectBindingSource1).BeginInit();
            SuspendLayout();
            // 
            // menu
            // 
            menu.ImageScalingSize = new Size(24, 24);
            menu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, parcelToolStripMenuItem, estateToolStripMenuItem, allObjectsToolStripMenuItem, generatorToolStripMenuItem });
            menu.Location = new Point(0, 0);
            menu.Name = "menu";
            menu.Padding = new Padding(9, 3, 0, 3);
            menu.Size = new Size(1513, 35);
            menu.TabIndex = 0;
            menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, loadToolStripMenuItem, saveToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(181, 34);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(181, 34);
            loadToolStripMenuItem.Text = "Load file";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(181, 34);
            saveToolStripMenuItem.Text = "Save file";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(181, 34);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // parcelToolStripMenuItem
            // 
            parcelToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addParcelToolStripMenuItem1, findParcelToolStripMenuItem });
            parcelToolStripMenuItem.Name = "parcelToolStripMenuItem";
            parcelToolStripMenuItem.Size = new Size(73, 29);
            parcelToolStripMenuItem.Text = "Parcel";
            // 
            // addParcelToolStripMenuItem1
            // 
            addParcelToolStripMenuItem1.Name = "addParcelToolStripMenuItem1";
            addParcelToolStripMenuItem1.Size = new Size(148, 34);
            addParcelToolStripMenuItem1.Text = "Add";
            addParcelToolStripMenuItem1.Click += addParcelToolStripMenuItem1_Click;
            // 
            // findParcelToolStripMenuItem
            // 
            findParcelToolStripMenuItem.Name = "findParcelToolStripMenuItem";
            findParcelToolStripMenuItem.Size = new Size(148, 34);
            findParcelToolStripMenuItem.Text = "Find";
            findParcelToolStripMenuItem.Click += findParcelToolStripMenuItem_Click;
            // 
            // estateToolStripMenuItem
            // 
            estateToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addEstateToolStripMenuItem, findEstateToolStripMenuItem1 });
            estateToolStripMenuItem.Name = "estateToolStripMenuItem";
            estateToolStripMenuItem.Size = new Size(75, 29);
            estateToolStripMenuItem.Text = "Estate";
            // 
            // addEstateToolStripMenuItem
            // 
            addEstateToolStripMenuItem.Name = "addEstateToolStripMenuItem";
            addEstateToolStripMenuItem.Size = new Size(148, 34);
            addEstateToolStripMenuItem.Text = "Add";
            addEstateToolStripMenuItem.Click += addEstateToolStripMenuItem_Click_1;
            // 
            // findEstateToolStripMenuItem1
            // 
            findEstateToolStripMenuItem1.Name = "findEstateToolStripMenuItem1";
            findEstateToolStripMenuItem1.Size = new Size(148, 34);
            findEstateToolStripMenuItem1.Text = "Find";
            findEstateToolStripMenuItem1.Click += findEstateToolStripMenuItem1_Click;
            // 
            // allObjectsToolStripMenuItem
            // 
            allObjectsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { findAllInRangeToolStripMenuItem });
            allObjectsToolStripMenuItem.Name = "allObjectsToolStripMenuItem";
            allObjectsToolStripMenuItem.Size = new Size(110, 29);
            allObjectsToolStripMenuItem.Text = "All objects";
            // 
            // findAllInRangeToolStripMenuItem
            // 
            findAllInRangeToolStripMenuItem.Name = "findAllInRangeToolStripMenuItem";
            findAllInRangeToolStripMenuItem.Size = new Size(251, 34);
            findAllInRangeToolStripMenuItem.Text = "Find all in range...";
            findAllInRangeToolStripMenuItem.Click += findAllInRangeToolStripMenuItem_Click;
            // 
            // generatorToolStripMenuItem
            // 
            generatorToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addToolStripMenuItem, randomOperationsToolStripMenuItem });
            generatorToolStripMenuItem.Name = "generatorToolStripMenuItem";
            generatorToolStripMenuItem.Size = new Size(106, 29);
            generatorToolStripMenuItem.Text = "Generator";
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { parcelsToolStripMenuItem, estatesToolStripMenuItem, randomObjectsToolStripMenuItem });
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(272, 34);
            addToolStripMenuItem.Text = "Add...";
            // 
            // parcelsToolStripMenuItem
            // 
            parcelsToolStripMenuItem.Name = "parcelsToolStripMenuItem";
            parcelsToolStripMenuItem.Size = new Size(244, 34);
            parcelsToolStripMenuItem.Text = "Parcels";
            parcelsToolStripMenuItem.Click += parcelsToolStripMenuItem_Click;
            // 
            // estatesToolStripMenuItem
            // 
            estatesToolStripMenuItem.Name = "estatesToolStripMenuItem";
            estatesToolStripMenuItem.Size = new Size(244, 34);
            estatesToolStripMenuItem.Text = "Estates";
            estatesToolStripMenuItem.Click += estatesToolStripMenuItem_Click;
            // 
            // randomObjectsToolStripMenuItem
            // 
            randomObjectsToolStripMenuItem.Name = "randomObjectsToolStripMenuItem";
            randomObjectsToolStripMenuItem.Size = new Size(244, 34);
            randomObjectsToolStripMenuItem.Text = "Random objects";
            randomObjectsToolStripMenuItem.Click += randomObjectsToolStripMenuItem_Click;
            // 
            // randomOperationsToolStripMenuItem
            // 
            randomOperationsToolStripMenuItem.Name = "randomOperationsToolStripMenuItem";
            randomOperationsToolStripMenuItem.Size = new Size(272, 34);
            randomOperationsToolStripMenuItem.Text = "Random operations";
            randomOperationsToolStripMenuItem.Click += randomOperationsToolStripMenuItem_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { typeDataGridViewTextBoxColumn, xValue, yValue, idNumberByUserDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn, topLeftDataGridViewTextBoxColumn, bottomRightDataGridViewTextBoxColumn, idSystem, widthValue, heightValue });
            dataGridView1.DataSource = geoObjectBindingSource3;
            dataGridView1.Location = new Point(13, 40);
            dataGridView1.Margin = new Padding(4, 5, 4, 5);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(1487, 826);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // geoObjectBindingSource3
            // 
            geoObjectBindingSource3.DataSource = typeof(GeoProject.GeoObject);
            // 
            // geoObjectBindingSource2
            // 
            geoObjectBindingSource2.DataSource = typeof(GeoProject.GeoObject);
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // geoObjectBindingSource
            // 
            geoObjectBindingSource.DataSource = typeof(GeoProject.GeoObject);
            // 
            // geoObjectBindingSource1
            // 
            geoObjectBindingSource1.DataSource = typeof(GeoProject.GeoObject);
            // 
            // typeDataGridViewTextBoxColumn
            // 
            typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            typeDataGridViewTextBoxColumn.HeaderText = "Type";
            typeDataGridViewTextBoxColumn.MinimumWidth = 8;
            typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            typeDataGridViewTextBoxColumn.ReadOnly = true;
            typeDataGridViewTextBoxColumn.Width = 150;
            // 
            // xValue
            // 
            xValue.DataPropertyName = "X";
            xValue.HeaderText = "xValue";
            xValue.MinimumWidth = 8;
            xValue.Name = "xValue";
            xValue.ReadOnly = true;
            xValue.Visible = false;
            xValue.Width = 150;
            // 
            // yValue
            // 
            yValue.DataPropertyName = "Y";
            yValue.HeaderText = "yValue";
            yValue.MinimumWidth = 8;
            yValue.Name = "yValue";
            yValue.ReadOnly = true;
            yValue.Visible = false;
            yValue.Width = 150;
            // 
            // idNumberByUserDataGridViewTextBoxColumn
            // 
            idNumberByUserDataGridViewTextBoxColumn.DataPropertyName = "IdNumberByUser";
            idNumberByUserDataGridViewTextBoxColumn.HeaderText = "ID";
            idNumberByUserDataGridViewTextBoxColumn.MinimumWidth = 8;
            idNumberByUserDataGridViewTextBoxColumn.Name = "idNumberByUserDataGridViewTextBoxColumn";
            idNumberByUserDataGridViewTextBoxColumn.ReadOnly = true;
            idNumberByUserDataGridViewTextBoxColumn.Width = 150;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            descriptionDataGridViewTextBoxColumn.MinimumWidth = 8;
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            descriptionDataGridViewTextBoxColumn.Width = 150;
            // 
            // topLeftDataGridViewTextBoxColumn
            // 
            topLeftDataGridViewTextBoxColumn.DataPropertyName = "TopLeftString";
            topLeftDataGridViewTextBoxColumn.HeaderText = "GPS 1";
            topLeftDataGridViewTextBoxColumn.MinimumWidth = 8;
            topLeftDataGridViewTextBoxColumn.Name = "topLeftDataGridViewTextBoxColumn";
            topLeftDataGridViewTextBoxColumn.ReadOnly = true;
            topLeftDataGridViewTextBoxColumn.Width = 150;
            // 
            // bottomRightDataGridViewTextBoxColumn
            // 
            bottomRightDataGridViewTextBoxColumn.DataPropertyName = "BottomRightString";
            bottomRightDataGridViewTextBoxColumn.HeaderText = "GPS 2";
            bottomRightDataGridViewTextBoxColumn.MinimumWidth = 8;
            bottomRightDataGridViewTextBoxColumn.Name = "bottomRightDataGridViewTextBoxColumn";
            bottomRightDataGridViewTextBoxColumn.ReadOnly = true;
            bottomRightDataGridViewTextBoxColumn.Width = 150;
            // 
            // idSystem
            // 
            idSystem.DataPropertyName = "Id";
            idSystem.HeaderText = "SystemID";
            idSystem.MinimumWidth = 8;
            idSystem.Name = "idSystem";
            idSystem.ReadOnly = true;
            idSystem.Visible = false;
            idSystem.Width = 150;
            // 
            // widthValue
            // 
            widthValue.DataPropertyName = "Width";
            widthValue.HeaderText = "Width";
            widthValue.MinimumWidth = 8;
            widthValue.Name = "widthValue";
            widthValue.ReadOnly = true;
            widthValue.Visible = false;
            widthValue.Width = 150;
            // 
            // heightValue
            // 
            heightValue.DataPropertyName = "Height";
            heightValue.HeaderText = "Height";
            heightValue.MinimumWidth = 8;
            heightValue.Name = "heightValue";
            heightValue.ReadOnly = true;
            heightValue.Visible = false;
            heightValue.Width = 150;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1513, 885);
            Controls.Add(dataGridView1);
            Controls.Add(menu);
            MainMenuStrip = menu;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "Form1";
            Text = "GeoObject";
            Load += Form1_Load;
            menu.ResumeLayout(false);
            menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)geoObjectBindingSource3).EndInit();
            ((System.ComponentModel.ISupportInitialize)geoObjectBindingSource2).EndInit();
            ((System.ComponentModel.ISupportInitialize)geoObjectBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)geoObjectBindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem parcelToolStripMenuItem;
        private ToolStripMenuItem findParcelToolStripMenuItem;
        private ToolStripMenuItem estateToolStripMenuItem;
        private ToolStripMenuItem addEstateToolStripMenuItem;
        private ToolStripMenuItem addParcelToolStripMenuItem1;
        private ToolStripMenuItem findEstateToolStripMenuItem1;
        private ToolStripMenuItem allObjectsToolStripMenuItem;
        private ToolStripMenuItem findAllInRangeToolStripMenuItem;
        private ToolStripMenuItem generatorToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem parcelsToolStripMenuItem;
        private ToolStripMenuItem estatesToolStripMenuItem;
        private ToolStripMenuItem randomObjectsToolStripMenuItem;
        private ToolStripMenuItem randomOperationsToolStripMenuItem;
        private DataGridView dataGridView1;
        //private BindingSource geoObjectBindingSource;
        //private BindingSource geoObjectBindingSource1;
        private BindingSource geoObjectBindingSource2;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private BindingSource geoObjectBindingSource;
        private BindingSource geoObjectBindingSource1;
        private BindingSource geoObjectBindingSource3;
        private ToolStripMenuItem newToolStripMenuItem;
        private DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn xValue;
        private DataGridViewTextBoxColumn yValue;
        private DataGridViewTextBoxColumn idNumberByUserDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn topLeftDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn bottomRightDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idSystem;
        private DataGridViewTextBoxColumn widthValue;
        private DataGridViewTextBoxColumn heightValue;
    }
}