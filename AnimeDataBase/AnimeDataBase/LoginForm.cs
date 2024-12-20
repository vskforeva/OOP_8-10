using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimeDataBase
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        //
        // MD5
        //
        private string CalculateMD5Hash(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public string role;
        public string login;
        private void loginButton_Click(object sender, EventArgs e)
        {
            string connectionString =
                            "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog=animedb;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    if (loginField.Text != "" && passField.Text != "")
                    {
                        string hashedPassword = CalculateMD5Hash(passField.Text);
                        string sql = "select * from users where login = @login and password = @pass";
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = cmd.CommandType;
                        cmd.CommandText = sql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@login", loginField.Text);
                        cmd.Parameters.AddWithValue("@pass", hashedPassword);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader["login"].ToString() == "admin")
                                {
                                    MainForm f2a = new MainForm(reader["login"].ToString());
                                    f2a.role = reader["role"].ToString();
                                    f2a.Text = "Вошел в систему - " + reader["login"].ToString();
                                    f2a.login = "admin";
                                    //f2a.login = reader["login"].ToString();
                                    f2a.Show();

                                }
                                else
                                {
                                    MainForm f2 = new MainForm(reader["login"].ToString());
                                    f2.role = reader["role"].ToString();
                                    f2.Text = "Вошел в систему - " + reader["login"].ToString();
                                    f2.login ="admin";
                                    //f2.login = reader["login"].ToString();
                                    f2.Show();

                                }
                            }

                        }
                        else MessageBox.Show("Данные не найдены :(");
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        //
        // ОТЛАДКА АДМИН
        //
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Set login and password fields based on checkbox state
            if (checkBox1.Checked)
            {
                loginField.Text = "admin";
                passField.Text = "1"; // Consider using a more secure password hashing mechanism
            }
            else
            {
                loginField.Text = "";
                passField.Text = "";
            }
        }
        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //
        // РЕГИСТРАЦИЯ
        //
        public void button1_Click(object sender, EventArgs e)
        {
            string connectionString =
                            "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog=animedb;Integrated Security=True;TrustServerCertificate=True";

            // Получить логин из поля loginBox
            string login = loginField.Text;

            // Проверить, существует ли логин в базе данных
            bool userExists = CheckUserExists(login);

            // Если пользователь не существует
            if (!userExists)
            {
                // Создать диалоговое окно с вопросом
                DialogResult result = MessageBox.Show("Пользователь с таким логином не найден. Создать нового?", "Создание нового пользователя", MessageBoxButtons.YesNo);

                // Если пользователь ответил "Да"
                if (result == DialogResult.Yes)
                {
                    // Создать нового пользователя
                    CreateUser(login);

                    // Отобразить сообщение об успешном создании
                    MessageBox.Show("Пользователь успешно создан!");
                }
            }
            else
            {
                // Отобразить сообщение о том, что пользователь уже существует
                MessageBox.Show("Пользователь с таким логином уже существует.");
            }
            //
            // Функция для проверки существования пользователя
            //
            bool CheckUserExists(string user_login)
            {
                // Подключиться к базе данных MS SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для проверки существования пользователя
                    string checkQuery = @"
                    SELECT COUNT(*) FROM users
                    WHERE login = @login;
                    ";

                    using (SqlCommand command = new SqlCommand(checkQuery, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            //
            // Функция для создания нового пользователя
            //
            void CreateUser(string new_user_login)
            {
                // Подключиться к базе данных MS SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для создания нового пользователя
                    string insertQuery = @"
                    INSERT INTO users (login, password, role)
                    VALUES (@login, @password, @role);
                    ";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Установить пароль (например, хэш пароля)
                        string password = CalculateMD5Hash(passField.Text); // Пример получения хэшированного пароля

                        command.Parameters.AddWithValue("@role", "user");
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);

                        command.ExecuteNonQuery();
                    }
                }
            }

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
    
}
