namespace AnimeDataBase
{
    partial class SeriesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeriesForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.seasonNumber_combobox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.watchedSeriesLabel = new System.Windows.Forms.Label();
            this.playButton = new System.Windows.Forms.Button();
            this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            this.browserPanel = new System.Windows.Forms.Panel();
            this.chromiumHostControl1 = new CefSharp.WinForms.Host.ChromiumHostControl();
            this.animedbDataSet1 = new AnimeDataBase.animedbDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.browserPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.animedbDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 157);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(260, 290);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(188, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Весь экран";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // seasonNumber_combobox
            // 
            this.seasonNumber_combobox.FormattingEnabled = true;
            this.seasonNumber_combobox.Location = new System.Drawing.Point(60, 3);
            this.seasonNumber_combobox.Name = "seasonNumber_combobox";
            this.seasonNumber_combobox.Size = new System.Drawing.Size(121, 21);
            this.seasonNumber_combobox.TabIndex = 2;
            this.seasonNumber_combobox.SelectedIndexChanged += new System.EventHandler(this.seasonNumber_combobox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Сезон: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Серии сезона: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Просмотрено серий: ";
            // 
            // watchedSeriesLabel
            // 
            this.watchedSeriesLabel.AutoSize = true;
            this.watchedSeriesLabel.Location = new System.Drawing.Point(132, 27);
            this.watchedSeriesLabel.Name = "watchedSeriesLabel";
            this.watchedSeriesLabel.Size = new System.Drawing.Size(35, 13);
            this.watchedSeriesLabel.TabIndex = 6;
            this.watchedSeriesLabel.Text = "label4";
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(6, 115);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(106, 23);
            this.playButton.TabIndex = 9;
            this.playButton.Text = "Воспроизвести";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // chromiumWebBrowser1
            // 
            this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser1.Location = new System.Drawing.Point(269, 3);
            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(804, 450);
            this.chromiumWebBrowser1.TabIndex = 0;
            // 
            // browserPanel
            // 
            this.browserPanel.Controls.Add(this.chromiumWebBrowser1);
            this.browserPanel.Controls.Add(this.dataGridView1);
            this.browserPanel.Controls.Add(this.seasonNumber_combobox);
            this.browserPanel.Controls.Add(this.playButton);
            this.browserPanel.Controls.Add(this.label1);
            this.browserPanel.Controls.Add(this.watchedSeriesLabel);
            this.browserPanel.Controls.Add(this.button1);
            this.browserPanel.Controls.Add(this.label2);
            this.browserPanel.Controls.Add(this.label3);
            this.browserPanel.Controls.Add(this.chromiumHostControl1);
            this.browserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserPanel.Location = new System.Drawing.Point(0, 0);
            this.browserPanel.Name = "browserPanel";
            this.browserPanel.Size = new System.Drawing.Size(1076, 450);
            this.browserPanel.TabIndex = 11;
            // 
            // chromiumHostControl1
            // 
            this.chromiumHostControl1.Location = new System.Drawing.Point(0, 0);
            this.chromiumHostControl1.Name = "chromiumHostControl1";
            this.chromiumHostControl1.Size = new System.Drawing.Size(272, 450);
            this.chromiumHostControl1.TabIndex = 12;
            // 
            // animedbDataSet1
            // 
            this.animedbDataSet1.DataSetName = "animedbDataSet";
            this.animedbDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SeriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 450);
            this.Controls.Add(this.browserPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SeriesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SeriesForm";
            this.Load += new System.EventHandler(this.SeriesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.browserPanel.ResumeLayout(false);
            this.browserPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.animedbDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox seasonNumber_combobox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label watchedSeriesLabel;
        private System.Windows.Forms.Button playButton;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        private System.Windows.Forms.Panel browserPanel;
        private CefSharp.WinForms.Host.ChromiumHostControl chromiumHostControl1;
        private animedbDataSet animedbDataSet1;
    }
}