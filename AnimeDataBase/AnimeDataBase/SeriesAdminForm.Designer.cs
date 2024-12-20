namespace AnimeDataBase
{
    partial class SeriesAdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeriesAdminForm));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.seasonNumber_combobox = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.saveDataButton = new System.Windows.Forms.Button();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.seriesNumberTextBox = new System.Windows.Forms.TextBox();
            this.linkBox = new System.Windows.Forms.TextBox();
            this.dateTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.delButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.addSeasonButton = new System.Windows.Forms.Button();
            this.delSeasonButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Серии сезона: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Сезон: ";
            // 
            // seasonNumber_combobox
            // 
            this.seasonNumber_combobox.FormattingEnabled = true;
            this.seasonNumber_combobox.Location = new System.Drawing.Point(66, 12);
            this.seasonNumber_combobox.Name = "seasonNumber_combobox";
            this.seasonNumber_combobox.Size = new System.Drawing.Size(89, 21);
            this.seasonNumber_combobox.TabIndex = 10;
            this.seasonNumber_combobox.SelectedIndexChanged += new System.EventHandler(this.seasonNumber_combobox_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 111);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(742, 275);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // saveDataButton
            // 
            this.saveDataButton.Location = new System.Drawing.Point(595, 70);
            this.saveDataButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveDataButton.Name = "saveDataButton";
            this.saveDataButton.Size = new System.Drawing.Size(163, 23);
            this.saveDataButton.TabIndex = 16;
            this.saveDataButton.Text = "Сохранить данные";
            this.saveDataButton.UseVisualStyleBackColor = true;
            this.saveDataButton.Click += new System.EventHandler(this.saveDataButton_Click);
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(161, 72);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(139, 20);
            this.titleTextBox.TabIndex = 18;
            // 
            // seriesNumberTextBox
            // 
            this.seriesNumberTextBox.Location = new System.Drawing.Point(16, 72);
            this.seriesNumberTextBox.Name = "seriesNumberTextBox";
            this.seriesNumberTextBox.Size = new System.Drawing.Size(139, 20);
            this.seriesNumberTextBox.TabIndex = 19;
            // 
            // linkBox
            // 
            this.linkBox.Location = new System.Drawing.Point(451, 72);
            this.linkBox.Name = "linkBox";
            this.linkBox.Size = new System.Drawing.Size(139, 20);
            this.linkBox.TabIndex = 20;
            // 
            // dateTextBox
            // 
            this.dateTextBox.Location = new System.Drawing.Point(306, 72);
            this.dateTextBox.Name = "dateTextBox";
            this.dateTextBox.Size = new System.Drawing.Size(139, 20);
            this.dateTextBox.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Номер серии:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Название серии:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(303, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Дата релиза серии:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(448, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Ссылка на серию:";
            // 
            // delButton
            // 
            this.delButton.Location = new System.Drawing.Point(595, 41);
            this.delButton.Name = "delButton";
            this.delButton.Size = new System.Drawing.Size(163, 23);
            this.delButton.TabIndex = 26;
            this.delButton.Text = "Удалить серию";
            this.delButton.UseVisualStyleBackColor = true;
            this.delButton.Click += new System.EventHandler(this.delButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(595, 12);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(163, 23);
            this.addButton.TabIndex = 27;
            this.addButton.Text = "Добавить серию";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // addSeasonButton
            // 
            this.addSeasonButton.Location = new System.Drawing.Point(161, 12);
            this.addSeasonButton.Name = "addSeasonButton";
            this.addSeasonButton.Size = new System.Drawing.Size(109, 21);
            this.addSeasonButton.TabIndex = 28;
            this.addSeasonButton.Text = "Добавить сезон";
            this.addSeasonButton.UseVisualStyleBackColor = true;
            this.addSeasonButton.Click += new System.EventHandler(this.addSeasonButton_Click);
            // 
            // delSeasonButton
            // 
            this.delSeasonButton.Location = new System.Drawing.Point(276, 12);
            this.delSeasonButton.Name = "delSeasonButton";
            this.delSeasonButton.Size = new System.Drawing.Size(109, 21);
            this.delSeasonButton.TabIndex = 29;
            this.delSeasonButton.Text = "Удалить сезон";
            this.delSeasonButton.UseVisualStyleBackColor = true;
            this.delSeasonButton.Click += new System.EventHandler(this.delSeasonButton_Click);
            // 
            // SeriesAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 401);
            this.Controls.Add(this.delSeasonButton);
            this.Controls.Add(this.addSeasonButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.delButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTextBox);
            this.Controls.Add(this.linkBox);
            this.Controls.Add(this.seriesNumberTextBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.saveDataButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.seasonNumber_combobox);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SeriesAdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование данных серий";
            this.Load += new System.EventHandler(this.SeriesAdminForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox seasonNumber_combobox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button saveDataButton;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox seriesNumberTextBox;
        private System.Windows.Forms.TextBox linkBox;
        private System.Windows.Forms.TextBox dateTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button delButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button addSeasonButton;
        private System.Windows.Forms.Button delSeasonButton;
    }
}