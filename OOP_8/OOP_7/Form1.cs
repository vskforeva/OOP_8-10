using Microsoft.Data.SqlClient;
using OOP_8.Models;
using OOP_8.Presenters;
using OOP_8.Views;
using System.Data;
using System.Text.RegularExpressions;

namespace OOP_8
{
    public partial class Form1 : Form, IStudentView
    {
        private JsonDataHandler jsonDataHandler = new JsonDataHandler();
        private StudentSorter studentSorter = new StudentSorter();

        private string connectionString = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = OOP_8; Integrated Security = True; TrustServerCertificate=True";


        public event EventHandler AddStudent;
        public event EventHandler UpdateStudent;
        public event EventHandler DeleteStudent;
        public event EventHandler ViewStudents;
        public event EventHandler StudentNameChanged;
        public event EventHandler StudentRecordBookNumberChanged;
        public event EventHandler StudentGroupNumberChanged;
        public event EventHandler StudentInstituteChanged;
        public event EventHandler StudentMajorChanged;
        public event EventHandler StartDateChanged;
        public event EventHandler EndDateChanged;


        public int StudentId { get; set; }
        public string StudentName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string StudentRecordBookNumber
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        public string StudentGroupNumber
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }
        public string StudentInstitute
        {
            get { return comboBox1.Text; }
            set { comboBox1.Text = value; }
        }
        public string StudentMajor
        {
            get { return comboBox2.Text; }
            set { comboBox2.Text = value; }
        }
        public DateTime StudentEnrollmentDate
        {
            get { return dateTimePicker1.Value; }
            set { dateTimePicker1.Value = value; }
        }

        public Form1()
        {
            InitializeComponent();

            // ������� ������ � ������
            StudentModel model = new StudentModel();
            DynamicFilter filter = new DynamicFilter(model, this);
            StudentPresenter presenter = new StudentPresenter(this, model);

            // ������������� �� ������� ��������� ����� � �����������

            txtName.TextChanged += (s, e) => StudentNameChanged?.Invoke(this, e);
            txtRecordBookNumber.TextChanged += (s, e) => StudentRecordBookNumberChanged?.Invoke(this, e);
            txtGroupNumber.TextChanged += (s, e) => StudentGroupNumberChanged?.Invoke(this, e);

            cmbInstitute.SelectedIndexChanged += (s, e) =>
            {
                StudentInstituteChanged?.Invoke(this, e); // �������� ������� ��������� ���������
                if (cmbInstitute.SelectedItem is string selectedInstitute && selectedInstitute != "���")
                {
                    LoadMajors(selectedInstitute); // ��������� ������������� ��� ���������� ���������
                }
                else
                {
                    LoadMajors("���"); // ��������� ��� �������������, ���� ������� "���"
                }
            };

            cmbMajor.SelectedIndexChanged += (s, e) => StudentMajorChanged?.Invoke(this, e);

            dtpStartDate.ValueChanged += (s, e) => StartDateChanged?.Invoke(this, e);
            dtpEndDate.ValueChanged += (s, e) => EndDateChanged?.Invoke(this, e);

            // ������������� ������ � ����������� ���������
            LoadInstitutes(); // ��������� ��������� ��� ����������
            LoadMajors("���"); // ��������� ��� ������������� �� ���������

            initWeekDays(); // ������������� comboBox1
            initSpecDays(); // ������������� comboBox2

            ViewStudents?.Invoke(this, EventArgs.Empty); // ��������� �� null ����� �������
        }
        private void AddEventHandlers()
        {
            // �������� �� ������� ��������� ����� � �����������
            txtName.TextChanged += (s, e) => StudentNameChanged?.Invoke(this, e);
            txtRecordBookNumber.TextChanged += (s, e) => StudentRecordBookNumberChanged?.Invoke(this, e);
            txtGroupNumber.TextChanged += (s, e) => StudentGroupNumberChanged?.Invoke(this, e);

            cmbInstitute.SelectedIndexChanged += (s, e) => StudentInstituteChanged?.Invoke(this, e);

            cmbMajor.SelectedIndexChanged += (s, e) => StudentMajorChanged?.Invoke(this, e);

            dtpStartDate.ValueChanged += (s, e) => StartDateChanged?.Invoke(this, e);

            dtpEndDate.ValueChanged += (s, e) => EndDateChanged?.Invoke(this, e);
        }
        private void InitializeComboBoxes()
        {
            initWeekDays(); // ������������� ������� ����������
            initSpecDays(); // ������������� ������� ����������
        }

        public string FilteredStudentName => txtName.Text;
        public string FilteredStudentRecordBookNumber => txtRecordBookNumber.Text;
        public string FilteredStudentGroupNumber => txtGroupNumber.Text;

        public string FilteredStudentInstitute => cmbInstitute.SelectedItem?.ToString();
        public string FilteredStudentMajor => cmbMajor.SelectedItem?.ToString();

        public DateTime? StartDate => dtpStartDate.Checked ? dtpStartDate.Value : (DateTime?)null;

        public DateTime? EndDate => dtpEndDate.Checked ? dtpEndDate.Value : (DateTime?)null;

        public class WeekDay
        {
            private int _id;
            private string _dayName;
            private string _specName;
            public int Id { get => _id; set => _id = value; }
            public string DayName { get => _dayName; set => _dayName = value; }
            public string SpecName { get => _specName; set => _specName = value; }
        }
        private void initWeekDays()
        {
            List<WeekDay> weekDays = new List<WeekDay> {
                        new WeekDay{ Id = 1, DayName = "�����" },
                        new WeekDay{ Id = 2, DayName = "���" },
                        new WeekDay{ Id = 3, DayName = "���" },
                        new WeekDay{ Id = 4, DayName = "���" },
                        new WeekDay{ Id = 5, DayName = "���" },
                        new WeekDay{ Id = 6, DayName = "���" },
                        new WeekDay{ Id = 7, DayName = "���" },
            };
            comboBox1.DataSource = weekDays;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "DayName";
        }
        private void initSpecDays()
        {
            UpdateSpecComboBox(1);
            List<WeekDay> specDays = new List<WeekDay>
            {
                        new WeekDay{ Id = 1, SpecName = "���������� �����������" },
                        new WeekDay{ Id = 2, SpecName = "�����������" },
                        new WeekDay{ Id = 3, SpecName = "���������� � ������������ �����" },

            };
            comboBox2.DataSource = specDays;
            comboBox2.ValueMember = "Id";
            comboBox2.DisplayMember = "SpecName";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadInstitutes(); // ��������� ��������� ��� ����������
            LoadMajors("���"); // ��������� ������������� ��� ����������

            initWeekDays(); // ������������� comboBox1
            initSpecDays(); // ������������� comboBox2

            ViewStudents?.Invoke(this, EventArgs.Empty); // ��������� �� null ����� �������
        }

        private void CmbInstitute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInstitute.SelectedItem is string selectedInstitute && selectedInstitute != "���")
            {
                LoadMajors(selectedInstitute); // ��������� ������������� ��� ���������� ���������
            }
            else
            {
                LoadMajors("���"); // ���� ������� "���", ��������� ��� �������������
            }

            // �������� ������� ��������� �������
            StudentInstituteChanged?.Invoke(this, EventArgs.Empty);
        }
        private void LoadInstitutes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT institute FROM students";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<string> institutes = new List<string> { "���" }; // ��������� ����� "���"

                while (reader.Read())
                {
                    institutes.Add(reader["institute"].ToString());
                }

                cmbInstitute.DataSource = institutes;
            }
        }
        private void LoadMajors(string institute)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT major FROM students WHERE institute = @Institute";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Institute", institute);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<string> majors = new List<string> { "���" }; // ��������� ����� "���"

                while (reader.Read())
                {
                    majors.Add(reader["major"].ToString());
                }

                cmbMajor.DataSource = majors;
            }
        }
        private void cmbInstitute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInstitute.SelectedItem is string selectedInstitute && selectedInstitute != "���")
            {
                LoadMajors(selectedInstitute); // ��������� ������������� ��� ���������� ���������
            }
            else
            {
                LoadMajors("���"); // ���� ������� "���", ��������� ��� �������������
            }

            // �������� ������� ��������� �������
            StudentInstituteChanged?.Invoke(this, EventArgs.Empty);
        }
        private void LoadDataJson()
        {
            var rowsData = jsonDataHandler.LoadData();

            foreach (var row in rowsData)
            {
                dataGridView1.Rows.Add(
                    row.Id,
                    row.TextField1,
                    row.TextField2,
                    row.TextField3,
                    row.DayName,
                    row.SpecName,
                    row.DateValue);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!dataValidation()) return;

            //int newId = GetNextId(); // ��������� ����� ������ � DataGridView
            //dataGridView1.Rows.Add(
            //    newId,
            //    textBox1.Text,
            //    studentRecordNumber,
            //    currentText,
            //    ((WeekDay)comboBox1.SelectedItem).DayName.ToString(),
            //    ((WeekDay)comboBox2.SelectedItem).SpecName.ToString(),
            //    dateTimePicker1.Value // ��������� �������� �� DateTimePicker � 6-� �������
            //);

            SaveData(); // ��������� ������ � JSON ����� ����������
            AddStudent.Invoke(sender, e);
        }
        private bool dataValidation()
        {
            // ���������, ��������� �� ����������� ����
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("����������, ��������� ��� ��������� ����.");
                return false;
            }

            // �������� ����� ������ �������
            if (textBox2.Text.Length < 8)
            {
                MessageBox.Show("������� ����� ������� ���������");
                return false; // ���������� ���������� ������, ���� ����� ������ 8
            }

            // �������� ������� ��� textBox3
            string currentText = textBox3.Text;
            if (!string.IsNullOrWhiteSpace(currentText))
            {
                Regex regex = new Regex(@"^\d{3,}[�-��-�]?-?[�-��-�]{2,}$");
                if (!regex.IsMatch(currentText))
                {
                    MessageBox.Show("������: ������� ������ �� �������, ��������: 100�-���; 200-��");
                    return false; // ���������� ���������� ������, ���� ������ �� ��������
                }
            }
            else
            {
                MessageBox.Show("������: ���� �� ������ ���� ������.");
                return false; // ���������� ���������� ������, ���� ���� ������
            }

            // �������� �� ������������ ������ ������� (��������������, ��� ��� column2)
            string studentRecordNumber = textBox2.Text; // ������������, ��� ����� ������� �������� � textBox2
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row == dataGridView1.SelectedRows[0]) continue;
                if (!row.IsNewRow && row.Cells[2].Value != null && row.Cells[2].Value.ToString() == studentRecordNumber)
                {
                    MessageBox.Show("������� � ����� ������� ������� ��� ����������!");
                    return false;
                }
            }

            return true;
        }

        private int GetNextId()
        {
            int maxId = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int id))
                {
                    if (id > maxId)
                    {
                        maxId = id;
                    }
                }
            }
            // Find the first missing ID
            for (int i = 1; i <= maxId + 1; i++)
            {
                bool idExists = false;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == i.ToString())
                    {
                        idExists = true;
                        break;
                    }
                }

                if (!idExists)
                {
                    return i; // Return the first missing ID
                }
            }

            return maxId + 1; // If no missing ID found, return the next available ID
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // ���������, ������� �� ������
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // ������� ���������� ������
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow) // ���������, ��� ��� �� ����� ������
                    {
                        StudentId = Convert.ToInt32(row.Cells[0].Value);
                        DeleteStudent.Invoke(sender, e);
                    }
                }

                //// ��������� ID ����� �������� (�����������)
                //UpdateIds();
                SaveData();
            }
            else
            {
                MessageBox.Show("����������, �������� ������ ��� ��������.");
            }
        }

        //private void UpdateIds()
        //{
        //    int currentId = 1;

        //    foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        if (row.Cells[0].Value != null)
        //        {
        //            row.Cells[0].Value = currentId++;
        //        }
        //    }
        //}
        private void SaveData()
        {
            var rowsData = new List<DataRow>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    rowsData.Add(new DataRow
                    {
                        Id = Convert.ToInt32(row.Cells[0].Value),
                        TextField1 = row.Cells[1].Value?.ToString(),
                        TextField2 = row.Cells[2].Value?.ToString(),
                        TextField3 = row.Cells[3].Value?.ToString(),
                        DayName = row.Cells[4].Value?.ToString(),
                        SpecName = row.Cells[5].Value?.ToString(),
                        DateValue = Convert.ToDateTime(row.Cells[6].Value)
                    });
                }
            }

            jsonDataHandler.SaveData(rowsData); // ��������� ������ ����� JsonDataHandler
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // ���������, ��� ������ ������ ������ -1 � �� �������� ����� �������
            if (e.RowIndex >= 0 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                // �������� ��������� ������
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // ��������� ��������� ���� ������� �� ��������� ������
                textBox1.Text = selectedRow.Cells[1].Value?.ToString() ?? ""; // �������� 1 �� ������ ������ �������
                textBox2.Text = selectedRow.Cells[2].Value?.ToString() ?? ""; // �������� 2 �� ������ ������ �������
                textBox3.Text = selectedRow.Cells[3].Value?.ToString() ?? ""; // �������� 3 �� ������ ������ �������

                // �������� �������� ��� � ������������� �� �����
                string dayName = selectedRow.Cells[4].Value?.ToString() ?? ""; // �������� 4 �� ������ ������ ������� ��� DayName
                string specName = selectedRow.Cells[5].Value?.ToString() ?? ""; // �������� 5 �� ������ ������ ������� ��� SpecName

                // ������� ��������������� ID ��� ComboBox1 (����)
                var weekDays = (List<WeekDay>)comboBox1.DataSource;
                var day = weekDays.FirstOrDefault(w => w.DayName == dayName);
                if (day != null)
                {
                    comboBox1.SelectedValue = day.Id; // ������������� ID ���
                }
                else
                {
                    MessageBox.Show("���� �� ������: " + dayName);
                }

                // ������� ��������������� ID ��� ComboBox2 (�������������)
                var specDays = (List<WeekDay>)comboBox2.DataSource;
                var spec = specDays.FirstOrDefault(s => s.SpecName == specName);
                if (spec != null)
                {
                    comboBox2.SelectedValue = spec.Id; // ������������� ID �������������
                }
                else
                {
                    MessageBox.Show("������������� �� �������: " + specName);
                }

                // ������������� ���� � DateTimePicker �� ������ 6
                if (selectedRow.Cells[6].Value is DateTime dateValue) // �������� 6 �� ������ ������ ������� ��� ����
                {
                    dateTimePicker1.Value = dateValue; // ������������� �������� � DateTimePicker
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateSelectedRow(sender, e);
        }

        private void UpdateSpecComboBox(int dayId)
        {
            List<WeekDay> specDays = new List<WeekDay>();

            // ���������� ��������� ������������� �� ������ ���������� ���
            switch (dayId)
            {
                case 1: // �����
                    specDays.Add(new WeekDay { Id = 1, SpecName = "���������� �����������" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "�����������" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "���������� � ������������ �����" });
                    break;
                case 2: // ���
                    specDays.Add(new WeekDay { Id = 4, SpecName = "�������" });
                    specDays.Add(new WeekDay { Id = 5, SpecName = "������� ����" });
                    specDays.Add(new WeekDay { Id = 6, SpecName = "����������" });
                    break;
                case 3: //���
                    specDays.Add(new WeekDay { Id = 1, SpecName = "��������" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "�����" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "��������" });
                    break;
                case 4://���
                    specDays.Add(new WeekDay { Id = 1, SpecName = "���������� ����" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "�������� ����" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "���� ����" });
                    break;
                case 5: //���
                    specDays.Add(new WeekDay { Id = 1, SpecName = "�����" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "������� ������" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "���������" });
                    break;
                case 6: //���
                    specDays.Add(new WeekDay { Id = 1, SpecName = "���������" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "��������" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "������" });
                    break;
                case 7: //���
                    specDays.Add(new WeekDay { Id = 1, SpecName = "����������" });
                    specDays.Add(new WeekDay { Id = 2, SpecName = "����������" });
                    specDays.Add(new WeekDay { Id = 3, SpecName = "���������" });
                    break;
                // �������� �������������� ������ ��� ������ �������� dayId
                default:
                    break;
            }

            // ��������� ComboBox2
            comboBox2.DataSource = specDays;
            comboBox2.ValueMember = "Id";
            comboBox2.DisplayMember = "SpecName";
        }

        private void UpdateSelectedRow(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (!dataValidation()) return;
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];


                // ��������� �������� � ��������� ������
                //selectedRow.Cells[1].Value = textBox1.Text; // ��������� �������� �� textBox1
                //selectedRow.Cells[2].Value = textBox2.Text; // ��������� �������� �� textBox2
                //selectedRow.Cells[3].Value = textBox3.Text; // ��������� �������� �� textBox3

                //selectedRow.Cells[4].Value = ((WeekDay)comboBox1.SelectedItem).DayName; // ��������� �������� ��� DayName
                //selectedRow.Cells[5].Value = ((WeekDay)comboBox2.SelectedItem).SpecName; // ��������� �������� ��� SpecName
                //selectedRow.Cells[6].Value = dateTimePicker1.Value; // ��������� �������� ��� ����

                //SaveData(); // ��������� ������ � JSON ����� ���������


                StudentId = Convert.ToInt32(selectedRow.Cells[0].Value);
                UpdateStudent.Invoke(sender, e);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                UpdateSelectedRow(sender, e);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ���������, �������� �� ��������� ������ ������ ��� �������� �� �� �������� Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // �������� ����, ���� ��� �� �����
            }

            // ��������� ����� ������ � textBox2
            if (textBox2.Text.Length >= 8 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // �������� ����, ���� ����� ������ ��� 8 ��������
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                string currentText = textBox3.Text;
                if (!string.IsNullOrWhiteSpace(currentText))
                {
                    Regex regex = new Regex(@"^\d{3,}[�-��-�]?-?[�-��-�]{2,}$");
                    if (!regex.IsMatch(currentText))
                    {
                        MessageBox.Show("������: ������� ������ �� �������, ��������: 100�-���; 200-��");
                        return;
                    }
                    UpdateSelectedRow(sender, e);
                }
                else
                {
                    MessageBox.Show("������: ���� �� ������ ���� ������.");
                }
            }

        }

        public void DisplayStudents(System.Data.DataTable students)
        {
            dataGridView1.DataSource = students;

            dataGridView1.Columns["student_id"].HeaderText = "ID ��������";
            dataGridView1.Columns["name"].HeaderText = "��� ��������";
            dataGridView1.Columns["record_book_number"].HeaderText = "� �������";
            dataGridView1.Columns["group_number"].HeaderText = "� ������";
            dataGridView1.Columns["institute"].HeaderText = "��������";
            dataGridView1.Columns["major"].HeaderText = "�����������";
            dataGridView1.Columns["enrollment_date"].HeaderText = "���� �����������";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                UpdateSelectedRow(sender, e);
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                UpdateSelectedRow(sender, e);
            }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                UpdateSelectedRow(sender, e);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is WeekDay selectedDay)
            {
                UpdateSpecComboBox(selectedDay.Id);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            // �������� ������� ��������� ����� ��������
            StudentNameChanged?.Invoke(this, EventArgs.Empty);
        }

        private void txtRecordBookNumber_TextChanged(object sender, EventArgs e)
        {
            // �������� ������� ��������� ������ �������� ������
            StudentRecordBookNumberChanged?.Invoke(this, EventArgs.Empty);
        }

        private void cmbInstitute_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // �������� ������� ��������� ���������
            StudentInstituteChanged?.Invoke(this, EventArgs.Empty);
        }

        private void cmbMajor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // �������� ������� ��������� �������������
            StudentMajorChanged?.Invoke(this, EventArgs.Empty);
        }

        private void txtGroupNumber_TextChanged(object sender, EventArgs e)
        {
            // �������� ������� ��������� ������ ������
            StudentGroupNumberChanged?.Invoke(this, EventArgs.Empty);
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            // �������� ������� ��������� ��������� ����
            StartDateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            // �������� ������� ��������� �������� ����
            EndDateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void clearFilter_Click(object sender, EventArgs e)
        {
            // ������� ��������� ����
            txtName.Clear();
            txtRecordBookNumber.Clear();
            txtGroupNumber.Clear();

            // ���������� ����������
            cmbInstitute.SelectedIndex = 0;
            cmbMajor.SelectedIndex = 0;

            // ���������� ���� ������ ����
            dtpStartDate.Value = DateTime.Now;
            dtpStartDate.Checked = false;
            dtpEndDate.Value = DateTime.Now;
            dtpEndDate.Checked = false;

            // ���������� ����������
            studentSorter.ResetSort();

            // �������� ������� ��������� �������, ����� �������� ������ ���������
            ViewStudents?.Invoke(this, EventArgs.Empty);
        }

        private void sortByRecordBook_Click(object sender, EventArgs e)
        {
            // �������� ������� ������ �� DataGridView
            DataTable currentStudents = (DataTable)dataGridView1.DataSource;

            // ��������� ��������� �� ������ �������
            DataTable sortedStudents = studentSorter.SortByRecordBookNumber(currentStudents);

            // ��������� DataGridView � ���������������� �������
            DisplayStudents(sortedStudents);
        }
    }
}
