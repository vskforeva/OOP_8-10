namespace lab9_cs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            crudZone = new GroupBox();
            updateButton = new Button();
            delButton = new Button();
            addButton = new Button();
            specBox = new ComboBox();
            bookBox = new TextBox();
            nameBox = new TextBox();
            panel2 = new Panel();
            fileZone = new GroupBox();
            themeButton = new Button();
            exportFileButton = new Button();
            importFileButton = new Button();
            fileTypeBox = new ComboBox();
            panel3 = new Panel();
            mainListView = new ListView();
            id = new ColumnHeader();
            columnName = new ColumnHeader();
            columnBook = new ColumnHeader();
            columnSpec = new ColumnHeader();
            panel1 = new Panel();
            crudZone.SuspendLayout();
            fileZone.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // crudZone
            // 
            crudZone.Controls.Add(updateButton);
            crudZone.Controls.Add(delButton);
            crudZone.Controls.Add(addButton);
            crudZone.Controls.Add(specBox);
            crudZone.Controls.Add(bookBox);
            crudZone.Controls.Add(nameBox);
            crudZone.Controls.Add(panel2);
            crudZone.Font = new Font("Montserrat Medium", 9.749999F);
            crudZone.Location = new Point(4, 3);
            crudZone.Name = "crudZone";
            crudZone.Size = new Size(193, 220);
            crudZone.TabIndex = 1;
            crudZone.TabStop = false;
            crudZone.Text = "Контроль таблицы";
            // 
            // updateButton
            // 
            updateButton.FlatStyle = FlatStyle.Flat;
            updateButton.Location = new Point(7, 147);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(181, 29);
            updateButton.TabIndex = 5;
            updateButton.Text = "Обновить";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += updateButton_Click;
            // 
            // delButton
            // 
            delButton.FlatStyle = FlatStyle.Flat;
            delButton.Location = new Point(7, 182);
            delButton.Name = "delButton";
            delButton.Size = new Size(181, 29);
            delButton.TabIndex = 4;
            delButton.Text = "Удалить";
            delButton.UseVisualStyleBackColor = true;
            delButton.Click += delButton_Click;
            // 
            // addButton
            // 
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Location = new Point(7, 112);
            addButton.Name = "addButton";
            addButton.Size = new Size(181, 29);
            addButton.TabIndex = 3;
            addButton.Text = "Добавить";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // specBox
            // 
            specBox.FlatStyle = FlatStyle.Flat;
            specBox.FormattingEnabled = true;
            specBox.Location = new Point(6, 80);
            specBox.Name = "specBox";
            specBox.Size = new Size(181, 26);
            specBox.TabIndex = 2;
            specBox.Text = "Информатика";
            // 
            // bookBox
            // 
            bookBox.Location = new Point(6, 51);
            bookBox.Name = "bookBox";
            bookBox.Size = new Size(181, 23);
            bookBox.TabIndex = 1;
            bookBox.TextChanged += bookBox_TextChanged;
            // 
            // nameBox
            // 
            nameBox.Location = new Point(6, 22);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(181, 23);
            nameBox.TabIndex = 0;
            nameBox.TextChanged += nameBox_TextChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Location = new Point(5, 79);
            panel2.Name = "panel2";
            panel2.Size = new Size(183, 28);
            panel2.TabIndex = 6;
            // 
            // fileZone
            // 
            fileZone.Controls.Add(themeButton);
            fileZone.Controls.Add(exportFileButton);
            fileZone.Controls.Add(importFileButton);
            fileZone.Controls.Add(fileTypeBox);
            fileZone.Controls.Add(panel3);
            fileZone.Font = new Font("Montserrat Medium", 9.749999F);
            fileZone.ForeColor = Color.Black;
            fileZone.Location = new Point(4, 229);
            fileZone.Name = "fileZone";
            fileZone.Size = new Size(193, 218);
            fileZone.TabIndex = 2;
            fileZone.TabStop = false;
            fileZone.Text = "Контроль файлов";
            // 
            // themeButton
            // 
            themeButton.Image = (Image)resources.GetObject("themeButton.Image");
            themeButton.Location = new Point(159, 184);
            themeButton.Name = "themeButton";
            themeButton.Size = new Size(25, 25);
            themeButton.TabIndex = 3;
            themeButton.UseVisualStyleBackColor = true;
            themeButton.Click += themeButton_Click;
            // 
            // exportFileButton
            // 
            exportFileButton.FlatStyle = FlatStyle.Flat;
            exportFileButton.Location = new Point(99, 54);
            exportFileButton.Name = "exportFileButton";
            exportFileButton.Size = new Size(88, 29);
            exportFileButton.TabIndex = 2;
            exportFileButton.Text = "Экспорт";
            exportFileButton.UseVisualStyleBackColor = true;
            exportFileButton.Click += exportFileButton_Click;
            // 
            // importFileButton
            // 
            importFileButton.FlatStyle = FlatStyle.Flat;
            importFileButton.Location = new Point(6, 54);
            importFileButton.Name = "importFileButton";
            importFileButton.Size = new Size(88, 29);
            importFileButton.TabIndex = 1;
            importFileButton.Text = "Импорт";
            importFileButton.TextImageRelation = TextImageRelation.ImageAboveText;
            importFileButton.UseVisualStyleBackColor = true;
            importFileButton.Click += importFileButton_Click;
            // 
            // fileTypeBox
            // 
            fileTypeBox.FlatStyle = FlatStyle.Flat;
            fileTypeBox.FormattingEnabled = true;
            fileTypeBox.Items.AddRange(new object[] { ".json", ".xml", ".csv" });
            fileTypeBox.Location = new Point(6, 22);
            fileTypeBox.Name = "fileTypeBox";
            fileTypeBox.Size = new Size(181, 26);
            fileTypeBox.TabIndex = 0;
            fileTypeBox.Text = ".json";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Black;
            panel3.Location = new Point(5, 21);
            panel3.Name = "panel3";
            panel3.Size = new Size(183, 28);
            panel3.TabIndex = 4;
            // 
            // mainListView
            // 
            mainListView.Columns.AddRange(new ColumnHeader[] { id, columnName, columnBook, columnSpec });
            mainListView.Dock = DockStyle.Fill;
            mainListView.Font = new Font("Montserrat Medium", 9.749999F, FontStyle.Regular, GraphicsUnit.Point, 204);
            mainListView.FullRowSelect = true;
            mainListView.GridLines = true;
            mainListView.Location = new Point(0, 0);
            mainListView.Name = "mainListView";
            mainListView.Size = new Size(800, 450);
            mainListView.TabIndex = 3;
            mainListView.UseCompatibleStateImageBehavior = false;
            mainListView.View = View.Details;
            mainListView.SelectedIndexChanged += mainListView_SelectedIndexChanged;
            // 
            // id
            // 
            id.Text = "id";
            id.Width = 0;
            // 
            // columnName
            // 
            columnName.Text = "ФИО";
            columnName.Width = 200;
            // 
            // columnBook
            // 
            columnBook.Text = "Номер зачетки";
            columnBook.Width = 200;
            // 
            // columnSpec
            // 
            columnSpec.Text = "Спецификация";
            columnSpec.Width = 200;
            // 
            // panel1
            // 
            panel1.Controls.Add(crudZone);
            panel1.Controls.Add(fileZone);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(600, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 450);
            panel1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(mainListView);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Контроль студентов";
            Load += Form1_Load;
            crudZone.ResumeLayout(false);
            crudZone.PerformLayout();
            fileZone.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private GroupBox crudZone;
        private Button updateButton;
        private Button delButton;
        private Button addButton;
        private ComboBox specBox;
        private TextBox bookBox;
        private TextBox nameBox;
        private GroupBox fileZone;
        private Button exportFileButton;
        private Button importFileButton;
        private ComboBox fileTypeBox;
        private ListView mainListView;
        private ColumnHeader id;
        private ColumnHeader columnName;
        private ColumnHeader columnBook;
        private ColumnHeader columnSpec;
        private Panel panel1;
        private Button themeButton;
        private Panel panel2;
        private Panel panel3;
    }
}
