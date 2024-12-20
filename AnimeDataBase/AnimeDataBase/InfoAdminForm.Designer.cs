namespace AnimeDataBase
{
    partial class InfoAdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoAdminForm));
            this.posterBox = new System.Windows.Forms.PictureBox();
            this.selectPoster = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.infoRichTextBox = new System.Windows.Forms.RichTextBox();
            this.orinNameLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.endDateBox = new System.Windows.Forms.Label();
            this.startDateBox = new System.Windows.Forms.Label();
            this.rateBox = new System.Windows.Forms.TextBox();
            this.ageBox = new System.Windows.Forms.TextBox();
            this.saveAll = new System.Windows.Forms.Button();
            this.numSeriesBox = new System.Windows.Forms.Label();
            this.numSeasonsBox = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.genreListBox = new System.Windows.Forms.CheckedListBox();
            this.countryListBox = new System.Windows.Forms.CheckedListBox();
            this.authorListBox = new System.Windows.Forms.CheckedListBox();
            this.label21 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.origNameBox = new System.Windows.Forms.TextBox();
            this.savePoster = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.posterPanel = new System.Windows.Forms.Panel();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.authorTextBox = new System.Windows.Forms.TextBox();
            this.countryTextBox = new System.Windows.Forms.TextBox();
            this.genreTextBox = new System.Windows.Forms.TextBox();
            this.addAuthorButton = new System.Windows.Forms.Button();
            this.addCountryButton = new System.Windows.Forms.Button();
            this.addGenreButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.posterBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.posterPanel.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // posterBox
            // 
            this.posterBox.Location = new System.Drawing.Point(5, 5);
            this.posterBox.Margin = new System.Windows.Forms.Padding(2);
            this.posterBox.Name = "posterBox";
            this.posterBox.Size = new System.Drawing.Size(271, 357);
            this.posterBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.posterBox.TabIndex = 0;
            this.posterBox.TabStop = false;
            // 
            // selectPoster
            // 
            this.selectPoster.Location = new System.Drawing.Point(5, 366);
            this.selectPoster.Margin = new System.Windows.Forms.Padding(2);
            this.selectPoster.Name = "selectPoster";
            this.selectPoster.Size = new System.Drawing.Size(271, 35);
            this.selectPoster.TabIndex = 1;
            this.selectPoster.Text = "Выбрать постер";
            this.selectPoster.UseVisualStyleBackColor = true;
            this.selectPoster.Click += new System.EventHandler(this.selectPoster_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(5, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 17);
            this.label5.TabIndex = 26;
            this.label5.Text = "Возрастной рейтинг:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(5, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 25;
            this.label4.Text = "Рейтинг:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(5, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Всего сезонов: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(131, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Всего серий:";
            // 
            // infoRichTextBox
            // 
            this.infoRichTextBox.Location = new System.Drawing.Point(6, 99);
            this.infoRichTextBox.Name = "infoRichTextBox";
            this.infoRichTextBox.Size = new System.Drawing.Size(249, 246);
            this.infoRichTextBox.TabIndex = 40;
            this.infoRichTextBox.Text = resources.GetString("infoRichTextBox.Text");
            // 
            // orinNameLabel
            // 
            this.orinNameLabel.AutoSize = true;
            this.orinNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.orinNameLabel.Location = new System.Drawing.Point(2, 43);
            this.orinNameLabel.Name = "orinNameLabel";
            this.orinNameLabel.Size = new System.Drawing.Size(169, 16);
            this.orinNameLabel.TabIndex = 36;
            this.orinNameLabel.Text = "Оригинальное название";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameLabel.Location = new System.Drawing.Point(3, 4);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(72, 17);
            this.nameLabel.TabIndex = 35;
            this.nameLabel.Text = "Название";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Описание:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(5, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 17);
            this.label8.TabIndex = 33;
            this.label8.Text = "Дата начала: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(673, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 17);
            this.label7.TabIndex = 32;
            this.label7.Text = "Страна: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(804, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 31;
            this.label6.Text = "Жанр:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.endDateBox);
            this.groupBox1.Controls.Add(this.startDateBox);
            this.groupBox1.Controls.Add(this.rateBox);
            this.groupBox1.Controls.Add(this.ageBox);
            this.groupBox1.Controls.Add(this.saveAll);
            this.groupBox1.Controls.Add(this.numSeriesBox);
            this.groupBox1.Controls.Add(this.numSeasonsBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(5, 334);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(249, 152);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            // 
            // endDateBox
            // 
            this.endDateBox.AutoSize = true;
            this.endDateBox.Location = new System.Drawing.Point(112, 42);
            this.endDateBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.endDateBox.Name = "endDateBox";
            this.endDateBox.Size = new System.Drawing.Size(48, 13);
            this.endDateBox.TabIndex = 48;
            this.endDateBox.Text = "endDate";
            // 
            // startDateBox
            // 
            this.startDateBox.AutoSize = true;
            this.startDateBox.Location = new System.Drawing.Point(112, 18);
            this.startDateBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.startDateBox.Name = "startDateBox";
            this.startDateBox.Size = new System.Drawing.Size(50, 13);
            this.startDateBox.TabIndex = 47;
            this.startDateBox.Text = "startDate";
            // 
            // rateBox
            // 
            this.rateBox.Location = new System.Drawing.Point(148, 79);
            this.rateBox.Margin = new System.Windows.Forms.Padding(2);
            this.rateBox.Name = "rateBox";
            this.rateBox.Size = new System.Drawing.Size(98, 20);
            this.rateBox.TabIndex = 42;
            // 
            // ageBox
            // 
            this.ageBox.Location = new System.Drawing.Point(148, 58);
            this.ageBox.Margin = new System.Windows.Forms.Padding(2);
            this.ageBox.Name = "ageBox";
            this.ageBox.Size = new System.Drawing.Size(98, 20);
            this.ageBox.TabIndex = 41;
            // 
            // saveAll
            // 
            this.saveAll.Location = new System.Drawing.Point(5, 119);
            this.saveAll.Margin = new System.Windows.Forms.Padding(2);
            this.saveAll.Name = "saveAll";
            this.saveAll.Size = new System.Drawing.Size(241, 26);
            this.saveAll.TabIndex = 46;
            this.saveAll.Text = "Сохранить изменения";
            this.saveAll.UseVisualStyleBackColor = true;
            this.saveAll.Click += new System.EventHandler(this.saveAll_Click);
            // 
            // numSeriesBox
            // 
            this.numSeriesBox.AutoSize = true;
            this.numSeriesBox.Location = new System.Drawing.Point(205, 101);
            this.numSeriesBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.numSeriesBox.Name = "numSeriesBox";
            this.numSeriesBox.Size = new System.Drawing.Size(41, 13);
            this.numSeriesBox.TabIndex = 36;
            this.numSeriesBox.Text = "label11";
            // 
            // numSeasonsBox
            // 
            this.numSeasonsBox.AutoSize = true;
            this.numSeasonsBox.Location = new System.Drawing.Point(85, 101);
            this.numSeasonsBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.numSeasonsBox.Name = "numSeasonsBox";
            this.numSeasonsBox.Size = new System.Drawing.Size(41, 13);
            this.numSeasonsBox.TabIndex = 35;
            this.numSeasonsBox.Text = "label10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 34;
            this.label1.Text = "Дата конца: ";
            // 
            // genreListBox
            // 
            this.genreListBox.FormattingEnabled = true;
            this.genreListBox.Location = new System.Drawing.Point(807, 22);
            this.genreListBox.Name = "genreListBox";
            this.genreListBox.Size = new System.Drawing.Size(222, 394);
            this.genreListBox.TabIndex = 60;
            // 
            // countryListBox
            // 
            this.countryListBox.FormattingEnabled = true;
            this.countryListBox.Location = new System.Drawing.Point(676, 23);
            this.countryListBox.Name = "countryListBox";
            this.countryListBox.Size = new System.Drawing.Size(125, 394);
            this.countryListBox.TabIndex = 59;
            // 
            // authorListBox
            // 
            this.authorListBox.FormattingEnabled = true;
            this.authorListBox.Location = new System.Drawing.Point(545, 23);
            this.authorListBox.Name = "authorListBox";
            this.authorListBox.Size = new System.Drawing.Size(125, 394);
            this.authorListBox.TabIndex = 58;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(545, 2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(76, 17);
            this.label21.TabIndex = 56;
            this.label21.Text = "Режиссер:";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(6, 22);
            this.nameBox.Margin = new System.Windows.Forms.Padding(2);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(251, 20);
            this.nameBox.TabIndex = 43;
            // 
            // origNameBox
            // 
            this.origNameBox.Location = new System.Drawing.Point(6, 61);
            this.origNameBox.Margin = new System.Windows.Forms.Padding(2);
            this.origNameBox.Name = "origNameBox";
            this.origNameBox.Size = new System.Drawing.Size(251, 20);
            this.origNameBox.TabIndex = 44;
            // 
            // savePoster
            // 
            this.savePoster.Location = new System.Drawing.Point(5, 405);
            this.savePoster.Margin = new System.Windows.Forms.Padding(2);
            this.savePoster.Name = "savePoster";
            this.savePoster.Size = new System.Drawing.Size(271, 35);
            this.savePoster.TabIndex = 45;
            this.savePoster.Text = "Сохранить постер";
            this.savePoster.UseVisualStyleBackColor = true;
            this.savePoster.Click += new System.EventHandler(this.savePoster_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(5, 444);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(271, 35);
            this.button2.TabIndex = 53;
            this.button2.Text = "Перейти к сериям\r\n";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // posterPanel
            // 
            this.posterPanel.Controls.Add(this.posterBox);
            this.posterPanel.Controls.Add(this.selectPoster);
            this.posterPanel.Controls.Add(this.button2);
            this.posterPanel.Controls.Add(this.savePoster);
            this.posterPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.posterPanel.Location = new System.Drawing.Point(0, 0);
            this.posterPanel.Margin = new System.Windows.Forms.Padding(2);
            this.posterPanel.MaximumSize = new System.Drawing.Size(281, 486);
            this.posterPanel.MinimumSize = new System.Drawing.Size(281, 486);
            this.posterPanel.Name = "posterPanel";
            this.posterPanel.Size = new System.Drawing.Size(281, 486);
            this.posterPanel.TabIndex = 55;
            // 
            // infoPanel
            // 
            this.infoPanel.Controls.Add(this.nameLabel);
            this.infoPanel.Controls.Add(this.label9);
            this.infoPanel.Controls.Add(this.orinNameLabel);
            this.infoPanel.Controls.Add(this.infoRichTextBox);
            this.infoPanel.Controls.Add(this.origNameBox);
            this.infoPanel.Controls.Add(this.groupBox1);
            this.infoPanel.Controls.Add(this.nameBox);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(281, 0);
            this.infoPanel.Margin = new System.Windows.Forms.Padding(2);
            this.infoPanel.MaximumSize = new System.Drawing.Size(259, 486);
            this.infoPanel.MinimumSize = new System.Drawing.Size(259, 486);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(259, 486);
            this.infoPanel.TabIndex = 56;
            // 
            // authorTextBox
            // 
            this.authorTextBox.Location = new System.Drawing.Point(545, 423);
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.Size = new System.Drawing.Size(125, 20);
            this.authorTextBox.TabIndex = 61;
            this.authorTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // countryTextBox
            // 
            this.countryTextBox.Location = new System.Drawing.Point(676, 423);
            this.countryTextBox.Name = "countryTextBox";
            this.countryTextBox.Size = new System.Drawing.Size(125, 20);
            this.countryTextBox.TabIndex = 62;
            // 
            // genreTextBox
            // 
            this.genreTextBox.Location = new System.Drawing.Point(807, 423);
            this.genreTextBox.Name = "genreTextBox";
            this.genreTextBox.Size = new System.Drawing.Size(222, 20);
            this.genreTextBox.TabIndex = 63;
            // 
            // addAuthorButton
            // 
            this.addAuthorButton.Location = new System.Drawing.Point(545, 449);
            this.addAuthorButton.Name = "addAuthorButton";
            this.addAuthorButton.Size = new System.Drawing.Size(125, 30);
            this.addAuthorButton.TabIndex = 64;
            this.addAuthorButton.Text = "+ Автор";
            this.addAuthorButton.UseVisualStyleBackColor = true;
            this.addAuthorButton.Click += new System.EventHandler(this.addAuthorButton_Click);
            // 
            // addCountryButton
            // 
            this.addCountryButton.Location = new System.Drawing.Point(676, 449);
            this.addCountryButton.Name = "addCountryButton";
            this.addCountryButton.Size = new System.Drawing.Size(125, 30);
            this.addCountryButton.TabIndex = 65;
            this.addCountryButton.Text = "+ Страна";
            this.addCountryButton.UseVisualStyleBackColor = true;
            this.addCountryButton.Click += new System.EventHandler(this.addCountryButton_Click);
            // 
            // addGenreButton
            // 
            this.addGenreButton.Location = new System.Drawing.Point(807, 449);
            this.addGenreButton.Name = "addGenreButton";
            this.addGenreButton.Size = new System.Drawing.Size(222, 30);
            this.addGenreButton.TabIndex = 66;
            this.addGenreButton.Text = "+ Жанр";
            this.addGenreButton.UseVisualStyleBackColor = true;
            this.addGenreButton.Click += new System.EventHandler(this.addGenreButton_Click);
            // 
            // InfoAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 486);
            this.Controls.Add(this.addGenreButton);
            this.Controls.Add(this.addCountryButton);
            this.Controls.Add(this.addAuthorButton);
            this.Controls.Add(this.genreTextBox);
            this.Controls.Add(this.countryTextBox);
            this.Controls.Add(this.authorTextBox);
            this.Controls.Add(this.genreListBox);
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.countryListBox);
            this.Controls.Add(this.posterPanel);
            this.Controls.Add(this.authorListBox);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InfoAdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование подробной информации";
            this.Load += new System.EventHandler(this.InfoAdminForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.posterBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.posterPanel.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox posterBox;
        private System.Windows.Forms.Button selectPoster;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox infoRichTextBox;
        private System.Windows.Forms.Label orinNameLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rateBox;
        private System.Windows.Forms.TextBox ageBox;
        private System.Windows.Forms.Label numSeriesBox;
        private System.Windows.Forms.Label numSeasonsBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox origNameBox;
        private System.Windows.Forms.Button savePoster;
        private System.Windows.Forms.Button saveAll;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel posterPanel;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.CheckedListBox genreListBox;
        private System.Windows.Forms.CheckedListBox countryListBox;
        private System.Windows.Forms.CheckedListBox authorListBox;
        private System.Windows.Forms.Label endDateBox;
        private System.Windows.Forms.Label startDateBox;
        private System.Windows.Forms.TextBox authorTextBox;
        private System.Windows.Forms.TextBox countryTextBox;
        private System.Windows.Forms.TextBox genreTextBox;
        private System.Windows.Forms.Button addAuthorButton;
        private System.Windows.Forms.Button addCountryButton;
        private System.Windows.Forms.Button addGenreButton;
    }
}