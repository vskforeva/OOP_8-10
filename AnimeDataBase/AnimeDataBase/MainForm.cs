using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AnimeDataBase
{
    public partial class MainForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = animedb; Integrated Security = True; TrustServerCertificate=True"; // Replace with your actual connection string
        public string login; // To store the logged-in user's name
        public string login_save;
        public string role;
        public string role_memory;
        public string AnimeId = "1";
        public string _animeId = "1";
        public int otladka = 0;
        public string combotext;

        public string minData;
        public string maxData;
       

        public delegate void LoadDataHandler();
        public event LoadDataHandler LoadDataEvent;

        public MainForm(string login)
        {
            InitializeComponent();
            this.login = login;
            role_memory = role;
            login_save = login;
            //this.login = login;


        }
        private void testForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "animedbDataSet1.status". При необходимости она может быть перемещена или удалена.
            this.statusTableAdapter.Fill(this.animedbDataSet1.status);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "animedbDataSet.anime". При необходимости она может быть перемещена или удалена.
            this.animeTableAdapter.Fill(this.animedbDataSet.anime);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "animedbDataSet.status". При необходимости она может быть перемещена или удалена.
            this.statusTableAdapter.Fill(this.animedbDataSet.status);
            LoadData();
            combotext = dataGridView1[9, 0].Value.ToString();
            comboBox1.Text = combotext;

            switch (role)
            {
                case "admin":
                    this.checkBox1.Enabled = true;
                    this.checkBox1.Checked = true;
                    this.addButton.Enabled = true;
                    this.deleteButton.Enabled = true;
                    break;
                case "user":
                    this.checkBox1.Enabled = false;
                    this.checkBox1.Checked = false;
                    this.addButton.Enabled = false;
                    this.deleteButton.Enabled = false;
                    break;
                default: break;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.role = "admin";
            }
            else
            {
                this.role = "user";
            }
        }
        public void LoadData()
        {
            GetLastWatchedEpisodeData();
            dataGridView1.DataSource = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = @"
                    SELECT a.id, a.name AS Название, a.origName, a.startDate AS [Дата выхода], a.endDate AS [Дата завершения], a.rating AS Рейтинг,

                    (SELECT STUFF((SELECT ', ' + c.country FROM animeCountry ac
                    JOIN countries c ON ac.country_id = c.id WHERE ac.anime_id = a.id
                    FOR XML PATH('')), 1, 2, '')) AS Страна,

                    (SELECT STUFF((SELECT ', ' + g.genre FROM animeGenre ag
                    JOIN genres g ON ag.genre_id = g.id WHERE ag.anime_id = a.id
                    FOR XML PATH('')), 1, 2, '')) AS Жанры,

                    (SELECT STUFF((SELECT ', ' + au.author + ' ' FROM animeAuthor aa
                    JOIN authors au ON aa.author_id = au.id WHERE aa.anime_id = a.id
                    FOR XML PATH('')), 1, 2, '')) AS Режиссер,

                    ISNULL(status.status, 'Не смотрел') AS [Статус]
                    FROM anime a
                    LEFT JOIN watchStatus ws ON a.id = ws.anime_id AND ws.user_login = @login
                    LEFT JOIN status ON ws.seriesWatchStatus = status.id;";

                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                        dataGridView1.Columns["Id"].Visible = false;
                        dataGridView1.Columns["origName"].Visible = false;
                        dataGridView1.Columns["Рейтинг"].Width = 55;
                        dataGridView1.Columns["Страна"].Width = 55;
                        dataGridView1.Columns["Жанры"].Width = 185;
                        dataGridView1.Columns["Дата выхода"].Width = 75;
                        dataGridView1.Columns["Дата завершения"].Width = 75;
                    }
                }
                dataGridView1.Rows[0].Selected = false;
                dataGridView1.Rows[otladka].Selected = true;
                
            }
        }
        //
        // ПОИСК ПО ЖАНРАМ
        //
        private void genreBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = genreBox.Text.ToLower();
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            try
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    var filteredRows = dataTable.Select($"Жанры LIKE '%{searchText}%'");

                    if (filteredRows.Length > 0)
                    {
                        dataGridView1.DataSource = filteredRows.CopyToDataTable();
                    }
                    else
                    {
                        dataGridView1.DataSource = new DataTable();
                    }
                }
                else
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
            }
        }
        //
        // ПОИСК ПО СТРАНАМ
        //
        private void countryBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = countryBox.Text.ToLower();
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            try
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    var filteredRows = dataTable.Select($"Страна LIKE '%{searchText}%'");

                    if (filteredRows.Length > 0)
                    {
                        dataGridView1.DataSource = filteredRows.CopyToDataTable();
                    }
                    else
                    {
                        dataGridView1.DataSource = new DataTable();
                    }
                }
                else
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
            }
        }
        //
        // ПОИСК ПО РЕЖИССЕРАМ
        //
        private void AuthorBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = AuthorBox.Text.ToLower();
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            try
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    var filteredRows = dataTable.Select($"Режиссер LIKE '%{searchText}%'");

                    if (filteredRows.Length > 0)
                    {
                        dataGridView1.DataSource = filteredRows.CopyToDataTable();
                    }
                    else
                    {
                        dataGridView1.DataSource = new DataTable();
                    }
                }
                else
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
            }
        }


        //
        // ПОИСК ПО НАЗВАНИЮ
        //

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = nameBox.Text.ToLower();
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            try
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    var filteredRows = dataTable.Select($"Название LIKE '%{searchText}%' OR origName LIKE '%{searchText}%'");

                    if (filteredRows.Length > 0)
                    {
                        dataGridView1.DataSource = filteredRows.CopyToDataTable();
                    }
                    else
                    {
                        dataGridView1.DataSource = new DataTable();
                    }
                }
                else
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
            }
        }
        //
        // ФИЛЬТРАЦИЯ ПО ДАТЕ
        //
        private void minDate_TextChanged(object sender, EventArgs e)
        {
            // Try parsing the user input into a DateTime value
            if (int.TryParse(minDate.Text, out var minDateParsed))
            {
                FilterData(minDateParsed, null); // Pass min date and null for max date
            }
            else
            {
                // Display an error message for invalid date format
                MessageBox.Show("Invalid date format. Please enter a valid date.");
            }
        }

        private void maxDate_TextChanged(object sender, EventArgs e)
        {
            // Try parsing the user input into a DateTime value
            if (int.TryParse(maxDate.Text, out var maxDateParsed))
            {
                FilterData(null, maxDateParsed); // Pass null for min date and max date
            }
            else
            {
                // Display an error message for invalid date format
                MessageBox.Show("Invalid date format. Please enter a valid date.");
            }
        }
        //
        // ФИЛЬТРАЦИЯ ПО РЕЙТИНГУ
        //
        private void minRate_TextChanged(object sender, EventArgs e)
        {
            // Try parsing the user input into a double value
            if (double.TryParse(minRate.Text, out var minRateParsed))
            {
                // Convert double to nullable int
                int? minRateInt = (int?)minRateParsed;

                // Filter data using minRateInt and null for max rating
                FilterData(minRateInt, null);
            }
            else
            {
                
            }
        }

        private void maxRate_TextChanged(object sender, EventArgs e)
        {
            // Try parsing the user input into a double value
            if (double.TryParse(maxRate.Text, out var maxRateParsed))
            {
                // Convert double to nullable int
                int? maxRateInt = (int?)maxRateParsed;

                // Filter data using null for min rating and maxRateInt
                FilterData(null, maxRateInt);
            }
            else
            {
               
            }
        }
        private void FilterData(int? minYear, int? maxYear)
        {
            var dataTable = (DataTable)dataGridView1.DataSource;

            // Check if DataTable is empty
            if (dataTable.Rows.Count == 0)
            {
                // Display an empty table (or handle it as per your requirement)
                dataGridView1.DataSource = new DataTable();
                return;
            }

            // Build the filter expression based on provided values
            string filterExpression = "";
            if (minYear.HasValue)
            {
                filterExpression += $"[Дата выхода] >= {minYear.Value}";
            }

            if (maxYear.HasValue)
            {
                if (!string.IsNullOrEmpty(filterExpression))
                {
                    filterExpression += " AND "; // Add AND for combined filtering
                }
                filterExpression += $"[Дата завершения] <= {maxYear.Value}";
            }

            // Apply filter (handle potential exception)
            try
            {
                var filteredRows = dataTable.Select(filterExpression ?? "");
                dataGridView1.DataSource = filteredRows.CopyToDataTable();
            }
            catch (InvalidOperationException ex)
            {
                // Handle the empty DataTable exception gracefully
                if (ex.Message.Contains("Источник не содержит строк DataRow."))
                {
                    
                }
                else
                {
                 
                    throw;
                }
            }
        }
        //
        // ПОДРОБНАЯ ИНФОРМАЦИЯ
        //

        private void ShowInfoForm()
        {
            InfoForm f3 = new InfoForm(_animeId, AnimeId, combotext, this,login); // Pass ID in constructor
            f3.login = this.login_save.ToString();
            f3.combotext = this.combotext;
            f3.Show();
        }
        private void ShowAdminForm()
        {
            InfoAdminForm f3 = new InfoAdminForm(_animeId, AnimeId, this, login); // Pass ID in constructor
            f3.login = this.login_save.ToString();

            f3.Show();
        }
        public void moreInfoButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0) // Check if a row is selected
                {
                    combotext = dataGridView1[9, otladka].Value.ToString();
                    comboBox1.Text = combotext;

                    DataGridViewRow row = dataGridView1.SelectedRows[0]; // Get the first selected row
                    _animeId = row.Cells["Id"]?.Value?.ToString(); // Extract ID safely

                    if (_animeId != null) // Check if ID is retrieved before using it
                    {
                        ShowInfoForm(); // Call a separate method to show the form
                    }
                    else
                    {
                        MessageBox.Show("Could not retrieve ID from selected row.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row in the DataGridView first.");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Выберите аниме");
            }
        }

        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Console.WriteLine("Row clicked!"); // Add this line for verification
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                otladka = e.RowIndex;
                // MessageBox.Show("EROWINDEX" + dataGridView1.Rows[e.RowIndex]);
                combotext = dataGridView1[9, dataGridView1.CurrentRow.Index].Value.ToString();
                comboBox1.Text = combotext;
                //rowIndex = dataGridView1.Rows[e.RowIndex];
                PopulateTextBoxes(row);
            }
        }
        public void PopulateTextBoxes(DataGridViewRow row)
        {
            AnimeId = row.Cells[0]?.Value?.ToString();
            //MessageBox.Show("EROWINDEX" + AnimeId);
            //_animeId = AnimeId;

            //label14.Text = AnimeId.ToString() + " log: "+ login;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (role)
            {
                case "admin":
                    {
                        if (e.RowIndex >= 0)
                        {
                            Console.WriteLine("Row clicked!"); // Add this line for verification
                            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                            //rowIndex = dataGridView1.Rows[e.RowIndex];
                            PopulateTextBoxes(row);
                        }

                        if (dataGridView1.SelectedRows.Count > 0) // Check if a row is selected
                        {
                            DataGridViewRow row = dataGridView1.SelectedRows[0]; // Get the first selected row
                            _animeId = row.Cells["Id"]?.Value?.ToString(); // Extract ID safely

                            if (_animeId != null) // Check if ID is retrieved before using it
                            {
                                ShowAdminForm(); // Call a separate method to show the form
                            }
                            else
                            {
                                MessageBox.Show("Could not retrieve ID from selected row.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select a row in the DataGridView first.");
                        }
                        break;
                    }
                case "user":
                    {
                        if (e.RowIndex >= 0)
                        {
                            Console.WriteLine("Row clicked!"); // Add this line for verification
                            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                            //rowIndex = dataGridView1.Rows[e.RowIndex];
                            PopulateTextBoxes(row);
                        }

                        if (dataGridView1.SelectedRows.Count > 0) // Check if a row is selected
                        {
                            DataGridViewRow row = dataGridView1.SelectedRows[0]; // Get the first selected row
                            _animeId = row.Cells["Id"]?.Value?.ToString(); // Extract ID safely

                            if (_animeId != null) // Check if ID is retrieved before using it
                            {
                                ShowInfoForm(); // Call a separate method to show the form
                            }
                            else
                            {
                                MessageBox.Show("Could not retrieve ID from selected row.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select a row in the DataGridView first.");
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public int aidd;
        public void GetLastWatchedEpisodeData()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Combine queries for better performance (optional)
                string sql = @"
            SELECT anime.id AS AnimeId,
                   anime.name AS AnimeName,
                   series.title AS SeriesName,
                   seasons.seasonNumber AS SeasonNumber,
                   series.seriesNumber AS SeriesNumber
            FROM watchedEpisode
            JOIN series ON watchedEpisode.series_id = series.id
            JOIN seasons ON series.season_id = seasons.id
            JOIN anime ON seasons.anime_id = anime.id
            WHERE user_login = @login
            AND watchedEpisode.id = (
                SELECT MAX(watchedEpisode.id)
                FROM watchedEpisode
                WHERE user_login = @login
            )
            ORDER BY watchedEpisode.id DESC";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@login", login);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nameLabel.Text = reader["AnimeName"].ToString();
                            snameLabel.Text = reader["SeriesName"].ToString();
                            ssLabel.Text = $"({reader["SeasonNumber"]} сезон, {reader["SeriesNumber"]} серия)";
                            aidd = Convert.ToInt32(reader["AnimeId"]);
                        }
                        else
                        {
                            // Handle no episodes found (optional)
                            nameLabel.Text = "Нет просмотренных серий";
                            snameLabel.Text = "";
                            ssLabel.Text = "";
                        }
                    }
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //
        // ОБНОВИТЬ СТАТУС ПРОСМОТРА
        //
        private void statusButton_Click(object sender, EventArgs e)
        {

            int selectedStatusId = (int)comboBox1.SelectedIndex + 1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Проверка существования записи
                    string checkExistsSql = @"
                    SELECT COUNT(*)
                    FROM watchStatus
                    WHERE anime_id = @AnimeId
                    AND user_login = @userLogin;
                    ";

                    using (SqlCommand checkExistsCommand = new SqlCommand(checkExistsSql, connection))
                    {
                        checkExistsCommand.Parameters.AddWithValue("@AnimeId", AnimeId);
                        checkExistsCommand.Parameters.AddWithValue("@userLogin", login);

                        int count = (int)checkExistsCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            // Обновление существующей записи
                            string updateSql = @"
                            UPDATE watchStatus
                            SET seriesWatchStatus = @selectedStatusId
                            WHERE anime_id = @AnimeId
                            AND user_login = @userLogin;
                            ";

                            using (SqlCommand updateCommand = new SqlCommand(updateSql, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@AnimeId", AnimeId);
                                updateCommand.Parameters.AddWithValue("@selectedStatusId", selectedStatusId);
                                updateCommand.Parameters.AddWithValue("@userLogin", login);

                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    LoadData(); // Обновить представление данных
                                }
                                else
                                {
                                    MessageBox.Show("Не удалось обновить запись.");
                                }
                            }
                        }
                        else
                        {
                            // Создание новой записи
                            string insertSql = @"
                            INSERT INTO watchStatus (anime_id, user_login, seriesWatchStatus)
                            VALUES (@AnimeId, @userLogin, @selectedStatusId);
                            ";

                            using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@AnimeId", AnimeId);
                                insertCommand.Parameters.AddWithValue("@userLogin", login);
                                insertCommand.Parameters.AddWithValue("@selectedStatusId", selectedStatusId);

                                int rowsAffected = insertCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    LoadData(); // Обновить представление данных
                                }
                                else
                                {
                                    MessageBox.Show("Не удалось создать новую запись.");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
                finally
                {
                    connection.Close(); // Закрыть соединение
                }
            }
        }

        private void MainForm_Enter(object sender, EventArgs e)
        {
            LoadData();
        }

        private void MainForm_Leave(object sender, EventArgs e)
        {
            LoadData();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            LoadData();
        }

        //
        // ДОБАВИТЬ АНИМЕ
        //
        private void addButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Generate a unique ID for other purposes (not for insertion)
                    string animeId = Guid.NewGuid().ToString(); // For reference only

                    // Insert a new empty anime record without specifying ID
                    string insertAnimeSql = @"
                      INSERT INTO anime (name, origName, description, startDate, endDate, rating, poster)
                      VALUES (@name, @origName, @description, NULL, NULL, NULL, NULL);
                    ";

                    using (SqlCommand insertAnimeCommand = new SqlCommand(insertAnimeSql, connection))
                    {
                        //insertAnimeCommand.Parameters.AddWithValue("@animeId", animeId);
                        insertAnimeCommand.Parameters.AddWithValue("@name", ""); // Empty title
                        insertAnimeCommand.Parameters.AddWithValue("@origName", ""); // Empty title
                        insertAnimeCommand.Parameters.AddWithValue("@description", ""); // Empty description
                        //insertAnimeCommand.Parameters.AddWithValue("startDate", null); // Current date
                        //insertAnimeCommand.Parameters.AddWithValue("@endDate", null); // Current date
                        //insertAnimeCommand.Parameters.AddWithValue("@rating", null); // Current date
                        //insertAnimeCommand.Parameters.AddWithValue("@poster", ""); // Empty image URL
                        insertAnimeCommand.ExecuteNonQuery();
                    }

                    // Refresh the data grid to display the new anime record
                    LoadData();

                    // Display a success message
                    MessageBox.Show("Новое аниме успешно добавлено!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении аниме: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //
        // УДАЛИТЬ АНИМЕ
        //
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Check if a row is selected
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0]; // Get the first selected row
                string animeId = row.Cells["Id"]?.Value?.ToString(); // Extract ID safely
                otladka = 0;

                if (animeId != null) // Check if ID is retrieved before using it
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить данное аниме?", "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();

                                // Transaction to ensure data integrity
                                using (SqlTransaction transaction = connection.BeginTransaction())
                                {
                                    // 1. Delete Watch Status (to avoid FK constraint violation)
                                    string deleteWatchStatusSql = @"
                                    DELETE FROM watchStatus
                                    WHERE anime_id = @animeId;
                                    ";

                                    using (SqlCommand deleteWatchStatusCommand = new SqlCommand(deleteWatchStatusSql, connection, transaction))
                                    {
                                        deleteWatchStatusCommand.Parameters.AddWithValue("@animeId", animeId);
                                        deleteWatchStatusCommand.ExecuteNonQuery();
                                    }

                                    // 2. Delete Watched Episodes (to avoid FK constraint violation)
                                    string deleteWatchedEpisodesSql = @"
                                    DELETE FROM watchedEpisode
                                    WHERE series_id IN (
                                      SELECT id
                                      FROM series
                                      WHERE season_id IN (
                                        SELECT id
                                        FROM seasons
                                        WHERE anime_id = @animeId
                                      )
                                    );
                                    ";

                                    using (SqlCommand deleteWatchedEpisodesCommand = new SqlCommand(deleteWatchedEpisodesSql, connection, transaction))
                                    {
                                        deleteWatchedEpisodesCommand.Parameters.AddWithValue("@animeId", animeId);
                                        deleteWatchedEpisodesCommand.ExecuteNonQuery();
                                    }

                                    // 3. Delete Series (to avoid FK constraint violation)
                                    string deleteSeriesSql = @"
                                    DELETE FROM series
                                    WHERE season_id IN (
                                      SELECT id
                                      FROM seasons
                                      WHERE anime_id = @animeId
                                    );
                                    ";

                                    using (SqlCommand deleteSeriesCommand = new SqlCommand(deleteSeriesSql, connection, transaction))
                                    {
                                        deleteSeriesCommand.Parameters.AddWithValue("@animeId", animeId);
                                        deleteSeriesCommand.ExecuteNonQuery();
                                    }

                                    // 4. Delete Seasons
                                    string deleteSeasonsSql = @"
                                    DELETE FROM seasons
                                    WHERE anime_id = @animeId;
                                    ";

                                    using (SqlCommand deleteSeasonsCommand = new SqlCommand(deleteSeasonsSql, connection, transaction))
                                    {
                                        deleteSeasonsCommand.Parameters.AddWithValue("@animeId", animeId);
                                        deleteSeasonsCommand.ExecuteNonQuery();
                                    }

                                    // 5. Delete Anime Genres
                                    string deleteAnimeGenreSql = @"
                                    DELETE FROM animeGenre
                                    WHERE anime_id = @animeId;
                                    ";

                                    using (SqlCommand deleteAnimeGenreCommand = new SqlCommand(deleteAnimeGenreSql, connection, transaction))
                                    {
                                        deleteAnimeGenreCommand.Parameters.AddWithValue("@animeId", animeId);
                                        deleteAnimeGenreCommand.ExecuteNonQuery();
                                    }

                                    // 6. Delete Anime Countries
                                    string deleteAnimeCountrySql = @"
                                    DELETE FROM animeCountry
                                    WHERE anime_id = @animeId;
                                    ";

                                    using (SqlCommand deleteAnimeCountryCommand = new SqlCommand(deleteAnimeCountrySql, connection, transaction))
                                    {
                                        deleteAnimeCountryCommand.Parameters.AddWithValue("@animeId", animeId);
                                        deleteAnimeCountryCommand.ExecuteNonQuery();
                                    }

                                    // 7. Delete Anime Authors
                                    string deleteAnimeAuthorSql = @"
                                    DELETE FROM animeAuthor
                                    WHERE anime_id = @animeId;
                                    ";

                                    using (SqlCommand deleteAnimeAuthorCommand = new SqlCommand(deleteAnimeAuthorSql, connection, transaction))
                                    {
                                        deleteAnimeAuthorCommand.Parameters.AddWithValue("@animeId", animeId);
                                        deleteAnimeAuthorCommand.ExecuteNonQuery();
                                    }

                                    // 8. Delete Anime
                                    string deleteAnimeSql = @"
                                    DELETE FROM anime
                                    WHERE id = @animeId;
                                    ";

                                    using (SqlCommand deleteAnimeCommand = new SqlCommand(deleteAnimeSql, connection, transaction))
                                    {
                                        deleteAnimeCommand.Parameters.AddWithValue("@animeId", animeId);
                                        deleteAnimeCommand.ExecuteNonQuery();
                                    }

                                    transaction.Commit(); // Commit the transaction if all deletions are successful
                                    MessageBox.Show("Аниме успешно удалено.");
                                    LoadData(); // Refresh the data grid
                                }

                            }
                            catch (Exception ex)
                            {
                                //transaction.Rollback(); // Rollback if any error occurs
                                MessageBox.Show("Ошибка при удалении аниме: " + ex.Message);
                            }
                            finally
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            genreBox.Text = "";
            countryBox.Text = "";
            AuthorBox.Text = "";
            minDate.Text = "";
            maxDate.Text = "";
            minRate.Text = "";
            maxRate.Text = "";
        }
        //
        // открыть последнюю серию
        //
        private void ShowSeriesForm()
        {
            AnimeId = aidd.ToString();
            SeriesForm f2 = new SeriesForm(_animeId, AnimeId, login); // Pass ID in constructor
            f2.login = login;
            f2.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ShowSeriesForm();
        }
    }
}
