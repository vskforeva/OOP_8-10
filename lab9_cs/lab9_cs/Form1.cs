using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Formatting = Newtonsoft.Json.Formatting;

namespace lab9_cs
{
    public partial class Form1 : Form
    {
        private List<Student> students = new List<Student>();
        private const string filePath = "students.json";
        private readonly Regex namePattern = new Regex(@"^[�-ߨ][�-��]+\s[�-ߨ][�-��]+\s[�-ߨ][�-��]+$");
        private readonly Regex bookPattern = new Regex(@"^\d{8}$");
        private int nextId;
        private bool isDarkTheme = false;

        public Form1()
        {
            InitializeComponent();
            LoadData();
            specBox.Items.AddRange(new string[] { "�����������", "���������", "����������" });
      
            if (specBox.Items.Count > 0)
            {
                specBox.SelectedIndex = 0;
            }

        }

        private void LoadData()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                students = JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
                nextId = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1; // ��������� nextId
                UpdateListView();
            }
        }
        private void UpdateListView()
        {
            mainListView.Items.Clear();
            for (int i = 0; i < students.Count; i++)
            {
                var student = students[i];

                ListViewItem item = new ListViewItem(student.Id.ToString());
                item.SubItems.Add(student.FullName);
                item.SubItems.Add(student.StudentId);
                item.SubItems.Add(student.Major);

                // THEME //
                if (isDarkTheme)
                {
                    item.BackColor = (i % 2 == 0) ? System.Drawing.Color.FromArgb(50, 50, 50)
                        : System.Drawing.Color.FromArgb(70, 70, 70); 
                    item.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    item.BackColor = (i % 2 == 0) ? System.Drawing.Color.WhiteSmoke
                        : System.Drawing.Color.White; 
                    item.ForeColor = System.Drawing.Color.Black;
                }

                mainListView.Items.Add(item);
            }
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            if (!namePattern.IsMatch(nameBox.Text))
            {
                nameBox.BackColor = System.Drawing.Color.LightCoral;
            }
            else
            {
                nameBox.BackColor = System.Drawing.Color.White;
            }
        }

        private void bookBox_TextChanged(object sender, EventArgs e)
        {
            if (!bookPattern.IsMatch(bookBox.Text))
            {
                bookBox.BackColor = System.Drawing.Color.LightCoral;
            }
            else
            {
                bookBox.BackColor = System.Drawing.Color.White; 
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!namePattern.IsMatch(nameBox.Text))
            {
                MessageBox.Show("������� ��� � ������� '������� ��� ��������'.",
                    "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!bookPattern.IsMatch(bookBox.Text))
            {
                MessageBox.Show("������� ����� ������� � ������� '00000000'.",
                    "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (students.Any(s => s.StudentId == bookBox.Text))
            {
                bookBox.BackColor = System.Drawing.Color.LightCoral;
                MessageBox.Show("������� � ����� ������� ������� ��� ����������.",
                    "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bookBox.BackColor = System.Drawing.Color.White;

            var student = new Student
            {
                Id = nextId++, // ����������� ���������� ID � ����������� nextId
                FullName = nameBox.Text,
                StudentId = bookBox.Text,
                Major = specBox.SelectedItem.ToString()
            };

            students.Add(student);
            SaveData();
            UpdateListView();
            ClearFields();
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (mainListView.SelectedItems.Count > 0)
            {
                var selectedItem = mainListView.SelectedItems[0];
                int studentId;

                if (int.TryParse(selectedItem.Text, out studentId))
                {
                    var studentToRemove = students.FirstOrDefault(s => s.Id == studentId);

                    if (studentToRemove != null)
                    {
                        students.Remove(studentToRemove);
                        SaveData();
                        UpdateListView();
                        ClearFields();
                    }
                }
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (mainListView.SelectedItems.Count > 0)
            {
                var selectedItem = mainListView.SelectedItems[0];
                int studentId;

                if (int.TryParse(selectedItem.Text, out studentId))
                {
                    var studentToUpdate = students.FirstOrDefault(s => s.Id == studentId);

                    if (bookBox.Text != studentToUpdate.StudentId && students.Any(s => s.StudentId == bookBox.Text))
                    {
                        bookBox.BackColor = System.Drawing.Color.LightCoral;
                        MessageBox.Show("������� � ����� ������� ������� ��� ����������.", "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bookBox.BackColor = System.Drawing.Color.White;

                    if (studentToUpdate != null)
                    {
                        studentToUpdate.FullName = nameBox.Text;
                        studentToUpdate.StudentId = bookBox.Text;
                        studentToUpdate.Major = specBox.SelectedItem.ToString();

                        SaveData();
                        UpdateListView();
                        ClearFields();
                    }
                }
            }
        }

        private void mainListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainListView.SelectedItems.Count > 0)
            {
                var selectedItem = mainListView.SelectedItems[0];

                nameBox.Text = selectedItem.SubItems[1].Text;
                bookBox.Text = selectedItem.SubItems[2].Text;
                specBox.SelectedItem = selectedItem.SubItems[3].Text;

                nameBox.BackColor = System.Drawing.Color.White;
                bookBox.BackColor = System.Drawing.Color.White;
            }
        }

        private void SaveData()
        {
            var json = JsonConvert.SerializeObject(students, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void ClearFields()
        {
            nameBox.Clear();
            bookBox.Clear();
            specBox.SelectedIndex = -1;

            nameBox.BackColor = System.Drawing.Color.White;
            bookBox.BackColor = System.Drawing.Color.White;
        }

        private void importFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON, XML and CSV Files (*.json;*.xml;*.csv)|*.json;*.xml;*.csv";
                openFileDialog.Title = "������ �����";

                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var filePath in openFileDialog.FileNames)
                    {
                        string fileExtension = Path.GetExtension(filePath).ToLower();

                        switch (fileExtension)
                        {
                            case ".json":
                                ImportJson(filePath);
                                break;
                            case ".xml":
                                ImportXml(filePath);
                                break;
                            case ".csv":
                                ImportCsv(filePath);
                                break;
                            default:
                                MessageBox.Show("���������������� ������ �����: " + fileExtension, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                    }
                }
            }
        }

        /// I ///
        /// M ///
        /// P ///
        /// O ///
        /// R ///  
        /// T ///

        private void ImportJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            students = JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
            nextId = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;
            UpdateListView();
        }
        private void ImportXml(string filePath)
        {
            var doc = XDocument.Load(filePath);
            students = doc.Descendants("Student")
                           .Select(s => new Student
                           {
                               Id = (int)s.Element("Id"),
                               FullName = (string)s.Element("FullName"),
                               StudentId = (string)s.Element("StudentId"),
                               Major = (string)s.Element("Major")
                           }).ToList();
            nextId = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;
            UpdateListView();
        }
        private void ImportCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            students.Clear();

            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                if (parts.Length == 4 && int.TryParse(parts[0], out int id))
                {
                    students.Add(new Student
                    {
                        Id = id,
                        FullName = parts[1],
                        StudentId = parts[2],
                        Major = parts[3]
                    });
                }
            }

            nextId = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;
            UpdateListView();
        }

        /// E ///
        /// X ///
        /// P ///
        /// O ///
        /// R ///  
        /// T ///

        private void exportFileButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml|CSV Files (*.csv)|*.csv";

                // ��������� ���������� ���� ����� �� ���������
                switch (fileTypeBox.SelectedItem.ToString())
                {
                    case ".json":
                        saveFileDialog.FilterIndex = 1;
                        break;
                    case ".xml":
                        saveFileDialog.FilterIndex = 2;
                        break;
                    case ".csv":
                        saveFileDialog.FilterIndex = 3;
                        break;
                }

                saveFileDialog.Title = "������� �����";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    string fileExtension = Path.GetExtension(filePath).ToLower();

                    switch (fileExtension)
                    {
                        case ".json":
                            ExportJson(filePath);
                            break;
                        case ".xml":
                            ExportXml(filePath);
                            break;
                        case ".csv":
                            ExportCsv(filePath);
                            break;
                        default:
                            MessageBox.Show("���������������� ������ �����.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }
        private void ExportJson(string filePath)
        {
            var json = JsonConvert.SerializeObject(students, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        private void ExportXml(string filePath)
        {
            var doc = new XDocument(new XElement("Students",
                students.Select(s => new XElement("Student",
                    new XElement("Id", s.Id),
                    new XElement("FullName", s.FullName),
                    new XElement("StudentId", s.StudentId),
                    new XElement("Major", s.Major)
                ))));

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(stream, new UTF8Encoding(false)))
                {
                    doc.Save(writer);
                }
            }
        }
        private void ExportCsv(string filePath)
        {
            var lines = new List<string>
    {
        "Id,FullName,StudentId,Major"
    };

            lines.AddRange(students.Select(s => $"{s.Id},{s.FullName},{s.StudentId},{s.Major}"));

            int attempts = 0;
            bool fileOpened = false;

            while (attempts < 5 && !fileOpened)
            {
                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    using (var writer = new StreamWriter(stream, new UTF8Encoding(true)))
                    {
                        foreach (var line in lines)
                        {
                            writer.WriteLine(line);
                        }
                    }
                    fileOpened = true;
                }
                catch (IOException ex)
                {
                    attempts++;
                    Thread.Sleep(100);

                    if (attempts >= 5)
                    {
                        MessageBox.Show($"�� ������� ������� ���� ����� {attempts} �������. ������: {ex.Message}",
                                        "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// T ///
        /// H ///
        /// E ///
        /// M ///
        /// E ///

        private void themeButton_Click(object sender, EventArgs e)
        {
            // ����������� ����
            isDarkTheme = !isDarkTheme;

            
            if (isDarkTheme)
            {
                this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30); // ������ ��� �����
                mainListView.BackColor = System.Drawing.Color.FromArgb(45, 45, 48); // ������ ��� ListView
                mainListView.ForeColor = System.Drawing.Color.LightGray; // ����� �����

                ChangeControlColors(fileZone.Controls, System.Drawing.Color.FromArgb(50, 50, 50), System.Drawing.Color.Gray);
                ChangeControlColors(crudZone.Controls, System.Drawing.Color.FromArgb(50, 50, 50), System.Drawing.Color.Gray);

                fileZone.ForeColor = System.Drawing.Color.Gray; // ��������� fileZone �����
                crudZone.ForeColor = System.Drawing.Color.Gray; // ��������� crudZone �����
            }
            else
            {
                this.BackColor = System.Drawing.Color.White; // ������� ��� �����
                mainListView.BackColor = System.Drawing.Color.White; // ������� ��� ListView
                mainListView.ForeColor = System.Drawing.Color.Black; // ������ �����

                ChangeControlColors(fileZone.Controls, System.Drawing.Color.LightGray, System.Drawing.Color.Black);
                ChangeControlColors(crudZone.Controls, System.Drawing.Color.LightGray, System.Drawing.Color.Black);

                fileZone.ForeColor = System.Drawing.Color.Black; // ��������� fileZone ������
                crudZone.ForeColor = System.Drawing.Color.Black; // ��������� crudZone ������

                ChangeTextBoxColors(fileZone.Controls, System.Drawing.Color.White);
                ChangeTextBoxColors(crudZone.Controls, System.Drawing.Color.White);
            }
            UpdateListView();
        }
        private void ChangeControlColors(Control.ControlCollection controls, System.Drawing.Color backColor, System.Drawing.Color foreColor)
        {
            foreach (Control control in controls)
            {
                if (control is System.Windows.Forms.Button)
                {
                    control.BackColor = backColor;
                    control.ForeColor = foreColor;
                }
                else if (control is System.Windows.Forms.TextBox || control is System.Windows.Forms.ComboBox)
                {
                    control.BackColor = backColor;
                    control.ForeColor = foreColor;
                }
            }
        }
        private void ChangeTextBoxColors(Control.ControlCollection controls, System.Drawing.Color backColor)
        {
            foreach (Control control in controls)
            {
                if (control is System.Windows.Forms.TextBox || control is System.Windows.Forms.ComboBox)
                {
                    control.BackColor = backColor;
                    control.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

    }
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string StudentId { get; set; }
        public string Major { get; set; }
    }
}

