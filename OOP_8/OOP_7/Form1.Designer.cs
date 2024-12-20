namespace OOP_8
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
            button1 = new Button();
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            textBox1 = new TextBox();
            comboBox2 = new ComboBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            button2 = new Button();
            button3 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            sortByRecordBook = new Button();
            label7 = new Label();
            dtpEndDate = new DateTimePicker();
            dtpStartDate = new DateTimePicker();
            cmbMajor = new ComboBox();
            txtRecordBookNumber = new TextBox();
            cmbInstitute = new ComboBox();
            txtGroupNumber = new TextBox();
            txtName = new TextBox();
            clearFilter = new Button();
            splitter1 = new Splitter();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(543, 48);
            button1.Margin = new Padding(2, 3, 2, 3);
            button1.Name = "button1";
            button1.Size = new Size(326, 29);
            button1.TabIndex = 0;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(221, 49);
            comboBox1.Margin = new Padding(2, 3, 2, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(150, 28);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox1.KeyPress += comboBox1_KeyPress;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(102, 15);
            dataGridView1.Margin = new Padding(2, 3, 2, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(926, 463);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(62, 49);
            textBox1.Margin = new Padding(2, 3, 2, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 27);
            textBox1.TabIndex = 3;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(221, 116);
            comboBox2.Margin = new Padding(2, 3, 2, 3);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(150, 28);
            comboBox2.TabIndex = 4;
            comboBox2.KeyPress += comboBox2_KeyPress;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(62, 116);
            textBox2.Margin = new Padding(2, 3, 2, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(150, 27);
            textBox2.TabIndex = 5;
            textBox2.KeyPress += textBox2_KeyPress;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(379, 49);
            textBox3.Margin = new Padding(2, 3, 2, 3);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(156, 27);
            textBox3.TabIndex = 6;
            textBox3.KeyPress += textBox3_KeyPress;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(379, 116);
            dateTimePicker1.Margin = new Padding(2, 3, 2, 3);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(156, 27);
            dateTimePicker1.TabIndex = 7;
            dateTimePicker1.KeyPress += dateTimePicker1_KeyPress;
            // 
            // button2
            // 
            button2.Location = new Point(543, 117);
            button2.Margin = new Padding(2, 3, 2, 3);
            button2.Name = "button2";
            button2.Size = new Size(326, 29);
            button2.TabIndex = 8;
            button2.Text = "Удалить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(543, 83);
            button3.Margin = new Padding(2, 3, 2, 3);
            button3.Name = "button3";
            button3.Size = new Size(326, 29);
            button3.TabIndex = 9;
            button3.Text = "Изменить";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(62, 25);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(102, 20);
            label1.TabIndex = 10;
            label1.Text = "Имя студента";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(62, 92);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(114, 20);
            label2.TabIndex = 11;
            label2.Text = "Номер зачетки";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(379, 25);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(112, 20);
            label3.TabIndex = 12;
            label3.Text = "Номер группы";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(379, 92);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(94, 20);
            label4.TabIndex = 13;
            label4.Text = "Дата записи";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(221, 25);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(70, 20);
            label5.TabIndex = 14;
            label5.Text = "Институт";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(221, 92);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(104, 20);
            label6.TabIndex = 15;
            label6.Text = "Направление";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom;
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Location = new Point(102, 484);
            groupBox1.Margin = new Padding(2, 4, 2, 4);
            groupBox1.MaximumSize = new Size(926, 308);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 4, 2, 4);
            groupBox1.Size = new Size(926, 308);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            groupBox2.BackColor = SystemColors.ControlLightLight;
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(sortByRecordBook);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(dtpEndDate);
            groupBox2.Controls.Add(dtpStartDate);
            groupBox2.Controls.Add(cmbMajor);
            groupBox2.Controls.Add(txtRecordBookNumber);
            groupBox2.Controls.Add(cmbInstitute);
            groupBox2.Controls.Add(txtGroupNumber);
            groupBox2.Controls.Add(txtName);
            groupBox2.Controls.Add(clearFilter);
            groupBox2.Location = new Point(6, 153);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(914, 147);
            groupBox2.TabIndex = 27;
            groupBox2.TabStop = false;
            groupBox2.Text = "Фильтры";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(519, 83);
            label13.Name = "label13";
            label13.Size = new Size(58, 20);
            label13.TabIndex = 32;
            label13.Text = "Группа";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(384, 79);
            label12.Name = "label12";
            label12.Size = new Size(104, 20);
            label12.TabIndex = 31;
            label12.Text = "Направление";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(249, 83);
            label11.Name = "label11";
            label11.Size = new Size(70, 20);
            label11.TabIndex = 30;
            label11.Text = "Институт";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(125, 83);
            label10.Name = "label10";
            label10.Size = new Size(127, 20);
            label10.TabIndex = 29;
            label10.Text = "Зачетная книжка";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(7, 79);
            label9.Name = "label9";
            label9.Size = new Size(102, 20);
            label9.TabIndex = 28;
            label9.Text = "Имя студента";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(642, 75);
            label8.Name = "label8";
            label8.Size = new Size(119, 20);
            label8.TabIndex = 27;
            label8.Text = "Начальная дата";
            // 
            // sortByRecordBook
            // 
            sortByRecordBook.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            sortByRecordBook.Location = new Point(129, 28);
            sortByRecordBook.Margin = new Padding(3, 4, 3, 4);
            sortByRecordBook.Name = "sortByRecordBook";
            sortByRecordBook.Size = new Size(114, 31);
            sortByRecordBook.TabIndex = 26;
            sortByRecordBook.Text = "⮃";
            sortByRecordBook.UseVisualStyleBackColor = true;
            sortByRecordBook.Click += sortByRecordBook_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(642, 115);
            label7.Name = "label7";
            label7.Size = new Size(111, 20);
            label7.TabIndex = 26;
            label7.Text = "Конечная дата";
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(767, 107);
            dtpEndDate.Margin = new Padding(3, 4, 3, 4);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(140, 27);
            dtpEndDate.TabIndex = 24;
            dtpEndDate.ValueChanged += dtpEndDate_ValueChanged;
            // 
            // dtpStartDate
            // 
            dtpStartDate.Location = new Point(767, 68);
            dtpStartDate.Margin = new Padding(3, 4, 3, 4);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(140, 27);
            dtpStartDate.TabIndex = 23;
            dtpStartDate.Value = new DateTime(1999, 1, 1, 0, 0, 0, 0);
            dtpStartDate.ValueChanged += dtpStartDate_ValueChanged;
            // 
            // cmbMajor
            // 
            cmbMajor.FormattingEnabled = true;
            cmbMajor.Location = new Point(384, 107);
            cmbMajor.Margin = new Padding(3, 4, 3, 4);
            cmbMajor.Name = "cmbMajor";
            cmbMajor.Size = new Size(127, 28);
            cmbMajor.TabIndex = 19;
            cmbMajor.SelectedIndexChanged += cmbMajor_SelectedIndexChanged;
            // 
            // txtRecordBookNumber
            // 
            txtRecordBookNumber.Location = new Point(128, 107);
            txtRecordBookNumber.Margin = new Padding(3, 4, 3, 4);
            txtRecordBookNumber.Name = "txtRecordBookNumber";
            txtRecordBookNumber.Size = new Size(114, 27);
            txtRecordBookNumber.TabIndex = 21;
            txtRecordBookNumber.TextChanged += txtRecordBookNumber_TextChanged;
            // 
            // cmbInstitute
            // 
            cmbInstitute.FormattingEnabled = true;
            cmbInstitute.Location = new Point(249, 107);
            cmbInstitute.Margin = new Padding(3, 4, 3, 4);
            cmbInstitute.Name = "cmbInstitute";
            cmbInstitute.Size = new Size(127, 28);
            cmbInstitute.TabIndex = 18;
            cmbInstitute.SelectedIndexChanged += cmbInstitute_SelectedIndexChanged_1;
            // 
            // txtGroupNumber
            // 
            txtGroupNumber.Location = new Point(519, 107);
            txtGroupNumber.Margin = new Padding(3, 4, 3, 4);
            txtGroupNumber.Name = "txtGroupNumber";
            txtGroupNumber.Size = new Size(114, 27);
            txtGroupNumber.TabIndex = 22;
            txtGroupNumber.TextChanged += txtGroupNumber_TextChanged;
            // 
            // txtName
            // 
            txtName.Location = new Point(7, 107);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(114, 27);
            txtName.TabIndex = 20;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // clearFilter
            // 
            clearFilter.Location = new Point(767, 29);
            clearFilter.Margin = new Padding(3, 4, 3, 4);
            clearFilter.Name = "clearFilter";
            clearFilter.Size = new Size(141, 31);
            clearFilter.TabIndex = 25;
            clearFilter.Text = "Сбросить Фильтр";
            clearFilter.UseVisualStyleBackColor = true;
            clearFilter.Click += clearFilter_Click;
            // 
            // splitter1
            // 
            splitter1.Location = new Point(0, 0);
            splitter1.Margin = new Padding(2, 4, 2, 4);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(2, 808);
            splitter1.TabIndex = 17;
            splitter1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1126, 808);
            Controls.Add(splitter1);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2, 3, 2, 3);
            MinimumSize = new Size(964, 607);
            Name = "Form1";
            Text = "Учет студентов";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private ComboBox comboBox1;
        private DataGridView dataGridView1;
        private TextBox textBox1;
        private ComboBox comboBox2;
        private TextBox textBox2;
        private TextBox textBox3;
        private DateTimePicker dateTimePicker1;
        private Button button2;
        private Button button3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private GroupBox groupBox1;
        private Splitter splitter1;
        private ComboBox cmbInstitute;
        private ComboBox cmbMajor;
        private TextBox txtName;
        private TextBox txtRecordBookNumber;
        private TextBox txtGroupNumber;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button clearFilter;
        private Button sortByRecordBook;
        private GroupBox groupBox2;
        private Label label8;
        private Label label7;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
    }
}
