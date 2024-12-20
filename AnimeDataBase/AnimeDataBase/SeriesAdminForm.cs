using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AnimeDataBase
{
    public partial class SeriesAdminForm : Form
    {
        private SqlConnection _connection;
        private int _selectedSeriesId; // Stores the ID of the currently selected series
        public int aid;
        public string AnimeId;

        public SeriesAdminForm(string _animeId, string AnimeId, string login)
        {
            InitializeComponent();
            aid = Convert.ToInt32(this.AnimeId = AnimeId);
            //label4.Text = AnimeId + " log " + login;

            _connection = GetSqlConnection(); // Establish database connection
                                              // Load anime information (consider using a separate method for better organization)
            LoadAnimeDetails();

            // Populate season dropdown
            PopulateSeasonComboBox();

            // Initial load of series data (consider using a separate method for better organization)
            LoadSeriesData(aid, null); // Load all series initially
            dataGridView1.ClearSelection();
        }

        private SqlConnection GetSqlConnection()
        {
            // Replace "your_connection_string" with your actual connection string
            string connectionString = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = animedb; Integrated Security = True; TrustServerCertificate=True"; // Replace with your actual connection string
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        private void LoadAnimeDetails()
        {
            // Fetch anime details using the provided animeId
            string sql = "SELECT name, numSeasons FROM anime WHERE id = @animeId";
            using (SqlCommand command = new SqlCommand(sql, _connection))
            {
                command.Parameters.AddWithValue("@animeId", aid);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Update form elements with retrieved data
                        this.Text = reader.GetString(0); // Set form title to anime name
                    }
                }
            }
        }
        private void PopulateSeasonComboBox()
        {
            seasonNumber_combobox.Items.Clear();

            // Fetch available seasons for the current anime
            string sql = "SELECT id, seasonNumber FROM seasons WHERE anime_id = @animeId";
            using (SqlCommand command = new SqlCommand(sql, _connection))
            {
                command.Parameters.AddWithValue("@animeId", aid);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int seasonId = reader.GetInt32(0); // Get the actual season_id
                        int seasonNumber = reader.GetInt32(1); // Get the displayed season number

                        // Store the actual season_id (key) and season number (value) in a KeyValuePair
                        KeyValuePair<int, int> seasonPair = new KeyValuePair<int, int>(seasonId, seasonNumber);

                        // Add the KeyValuePair to the combo box
                        seasonNumber_combobox.Items.Add(seasonPair);
                    }
                }
            }

            if (seasonNumber_combobox.Items.Count > 0)
            {
                seasonNumber_combobox.SelectedIndex = 0; // Select the first season by default
            }
        }

        private void LoadSeriesData(int animeId, int? seasonId)
        {
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("seriesNumber", "Номер серии");
                dataGridView1.Columns.Add("title", "Название");
                dataGridView1.Columns.Add("releaseDate", "Дата релиза");
                dataGridView1.Columns.Add("link", "Ссылка");

                // Add hidden column for series.id
                dataGridView1.Columns.Add("seriesId", "seriesId");
                dataGridView1.Columns["seriesId"].Visible = false;
            }
            dataGridView1.Rows.Clear();

            // Construct query based on animeId and optional seasonNumber
            string sql = "SELECT s.seriesNumber, COALESCE(s.title, ''), s.releaseDate, s.link, s.id AS seriesId " +
                          "FROM series s " +
                          "INNER JOIN seasons seas ON s.season_id = seas.id " +
                          "WHERE seas.anime_id = @animeId ";

            if (!seasonId.HasValue)  // Если сезон не выбран
            {
                sql += "AND seas.id = (SELECT TOP 1 id FROM seasons WHERE anime_id = @animeId)";
            }
            else
            {
                sql += "AND seas.id = @seasonId";  // Если сезон выбран
            }

            using (SqlCommand command = new SqlCommand(sql, _connection))
            {
                // Add the @seasonId parameter to the command
                command.Parameters.AddWithValue("@animeId", animeId);
                if (seasonId.HasValue)
                {
                    command.Parameters.AddWithValue("@seasonId", seasonId.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int seriesNumber = reader.GetInt32(0);
                        string title = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        DateTime releaseDate = reader.GetDateTime(2);
                        string link = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        int seriesId = reader.GetInt32(4); // Get the seriesId from the result set
                        
                        dataGridView1.Rows.Add(seriesNumber, title, releaseDate.ToString("yyyy-MM-dd"), link, seriesId);
                       
                    }
                }
            }
        }
        public int seriesId;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Check for valid row click
            {
                // Get the selected row data
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Retrieve series details from the selected row
                int seriesNumber = Convert.ToInt32(selectedRow.Cells[0].Value);
                string title = selectedRow.Cells[1].Value.ToString();
                DateTime releaseDate = Convert.ToDateTime(selectedRow.Cells[2].Value);
                string link = selectedRow.Cells[3].Value.ToString();
                int seriesId = Convert.ToInt32(selectedRow.Cells["seriesId"].Value); // Assuming "seriesId" is the column name


                // Display series details in the textBoxes
                seriesNumberTextBox.Text = seriesNumber.ToString();
                titleTextBox.Text = title;
                dateTextBox.Text = releaseDate.ToString("yyyy-MM-dd");
                linkBox.Text = link;
            }
        }

        private void seasonNumber_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seasonNumber_combobox.SelectedIndex != -1)
            {
                try
                {
                    // Get the actual season_id from the selected item
                    KeyValuePair<int, int> selectedItem = (KeyValuePair<int, int>)seasonNumber_combobox.SelectedItem;
                    int selectedSeasonId = selectedItem.Key;

                    // Call LoadSeriesData with the retrieved season_id
                    LoadSeriesData(aid, selectedSeasonId);
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    MessageBox.Show($"Error loading series: {ex.Message}");
                }
            }
        }

        private void saveDataButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int? selectedSeasonId = null;
                if (seasonNumber_combobox.SelectedIndex != -1)
                {
                    KeyValuePair<int, int> selectedItem = (KeyValuePair<int, int>)seasonNumber_combobox.SelectedItem;
                    selectedSeasonId = selectedItem.Key;
                }

                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int seriesId = Convert.ToInt32(selectedRow.Cells["seriesId"].Value);
                int seasonId = Convert.ToInt32(selectedRow.Cells["seriesId"].Value);  // Assuming this is also "seriesId"?

                string connectionString = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = animedb; Integrated Security = True; TrustServerCertificate=True"; // Replace with your actual connection string

                // Establish database connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // --- Update Series Data ---
                    // Prepare SQL UPDATE statement (similar to your existing code)
                    string updateSeriesSql = "UPDATE series " +
                                              "SET seriesNumber = @seriesNumber, " +
                                              "title = @title, " +
                                              "releaseDate = @releaseDate, " +
                                              "link = @link " +
                                              "WHERE id = @seriesId";

                    // Create SqlCommand object for series update
                    using (SqlCommand updateSeriesCommand = new SqlCommand(updateSeriesSql, connection))
                    {
                        updateSeriesCommand.Parameters.AddWithValue("@seriesNumber", Convert.ToInt32(seriesNumberTextBox.Text));
                        updateSeriesCommand.Parameters.AddWithValue("@title", titleTextBox.Text);
                        updateSeriesCommand.Parameters.AddWithValue("@releaseDate", Convert.ToDateTime(dateTextBox.Text));
                        updateSeriesCommand.Parameters.AddWithValue("@link", linkBox.Text);
                        updateSeriesCommand.Parameters.AddWithValue("@seriesId", seriesId);

                        // Execute update for series and check for rows affected
                        int rowsAffectedSeries = updateSeriesCommand.ExecuteNonQuery();

                        if (rowsAffectedSeries > 0)
                        {
                           
                            UpdateAnimeDates(aid);
                        }
                        else
                        {
                            // Error message (consider handling series update failure separately)
                            MessageBox.Show("Error updating series data. No rows affected.");
                        }
                    }
                }
            }
            else
            {
                // No row selected
                MessageBox.Show("Пожалуйста, выберите серию для удаления.");
            }
        }
        private void UpdateAnimeDates(int animeId)
        {
            string connectionString = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = animedb; Integrated Security = True; TrustServerCertificate=True"; // Replace with your actual connection string

            int firstEpisodeReleaseYear = GetFirstEpisodeReleaseYear(animeId);
            int lastEpisodeReleaseYear = GetLastEpisodeReleaseYear(animeId);

            if (firstEpisodeReleaseYear != -1 && lastEpisodeReleaseYear != -1)
            {
                // Update anime table with retrieved years
                string updateAnimeSql = "UPDATE anime " +
                                        "SET startDate = @firstEpisodeReleaseYear, " +
                                        "endDate = @lastEpisodeReleaseYear " +
                                        "WHERE id = @animeId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand updateAnimeCommand = new SqlCommand(updateAnimeSql, connection))
                    {
                        updateAnimeCommand.Parameters.AddWithValue("@firstEpisodeReleaseYear", firstEpisodeReleaseYear);
                        updateAnimeCommand.Parameters.AddWithValue("@lastEpisodeReleaseYear", lastEpisodeReleaseYear);
                        updateAnimeCommand.Parameters.AddWithValue("@animeId", animeId);

                        int rowsAffectedAnime = updateAnimeCommand.ExecuteNonQuery();

                        if (rowsAffectedAnime > 0)
                        {
                            // Success message
                            MessageBox.Show("Series data and anime dates updated successfully!");
                            KeyValuePair<int, int> selectedItem = (KeyValuePair<int, int>)seasonNumber_combobox.SelectedItem;
                            int selectedSeasonId = selectedItem.Key;
                            LoadSeriesData(aid, selectedSeasonId);
                        }
                        else
                        {
                            // Error message (anime update failure)
                            MessageBox.Show("Error updating anime dates.");
                        }
                    }
                }
            }
            else
            {
                // Handle error if either release year could not be retrieved
                MessageBox.Show("Error retrieving episode release years for anime.");
            }
        }
        private int GetFirstEpisodeReleaseYear(int animeId)
        {
            string connectionString = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = animedb; Integrated Security = True; TrustServerCertificate=True"; // Replace with your actual connection string

            string query = "SELECT YEAR(MIN(s.releaseDate)) AS firstEpisodeReleaseYear " +
                           "FROM anime a " +
                           "INNER JOIN seasons se ON a.id = se.anime_id " +
                           "INNER JOIN series s ON se.id = s.season_id " +
                           "WHERE a.id = @animeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@animeId", animeId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);  // Assuming first column is "firstEpisodeReleaseYear"
                        }
                        else
                        {
                            // Handle error if no result is found
                            return -1;
                        }
                    }
                }
            }
        }
        private int GetLastEpisodeReleaseYear(int animeId)
        {
            string connectionString = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = animedb; Integrated Security = True; TrustServerCertificate=True"; // Replace with your actual connection string

            string query = "SELECT YEAR(MAX(s.releaseDate)) AS lastEpisodeReleaseYear " +
                           "FROM anime a " +
                           "INNER JOIN seasons se ON a.id = se.anime_id " +
                           "INNER JOIN series s ON se.id = s.season_id " +
                           "WHERE a.id = @animeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@animeId", animeId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);  // Assuming first column is "lastEpisodeReleaseYear"
                        }
                        else
                        {
                            // Handle error if no result is found
                            return -1;
                        }
                    }
                }
            }
        }
        private void delButton_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the data grid
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the series ID from the selected row
                int seriesId = Convert.ToInt32(selectedRow.Cells["seriesId"].Value);
                int seriesN = Convert.ToInt32(selectedRow.Cells["seriesNumber"].Value);

                // Confirm deletion with the user
                DialogResult confirmation = MessageBox.Show($"Удалить эту серию ({seriesN})?", "Удаление серии", MessageBoxButtons.YesNo);
                if (confirmation == DialogResult.Yes)
                {
                    // Delete the series using the series ID
                    string deleteSql = "DELETE FROM series WHERE id = @seriesId";
                    using (SqlCommand deleteCommand = new SqlCommand(deleteSql, _connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@seriesId", seriesId);
                        deleteCommand.ExecuteNonQuery();
                    }

                    // Reload series data to reflect the deletion
                    LoadSeriesData(aid, null); // Reload all series since the season may have changed
                }
            }
            else
            {
                // No row selected
                MessageBox.Show("Пожалуйста, выберите серию для удаления.");
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {

            // Get the selected season ID (if any)
            int? selectedSeasonId = null;
            if (seasonNumber_combobox.SelectedIndex != -1)
            {
                KeyValuePair<int, int> selectedItem = (KeyValuePair<int, int>)seasonNumber_combobox.SelectedItem;
                selectedSeasonId = selectedItem.Key;
            }

            // Get the maximum series number for the selected season
            int nextSeriesNumber = 1;
            if (selectedSeasonId.HasValue)
            {
                string maxNumberSql = "SELECT ISNULL(MAX(seriesNumber), 0) + 1 FROM series WHERE season_id = @seasonId";
                using (SqlCommand maxNumberCommand = new SqlCommand(maxNumberSql, _connection))
                {
                    maxNumberCommand.Parameters.AddWithValue("@seasonId", selectedSeasonId.Value);
                    nextSeriesNumber = (int)maxNumberCommand.ExecuteScalar();
                }
            }

            // Insert a new row with the next series number
            try
            {
                string insertSql = "INSERT INTO series (season_id, seriesNumber, releaseDate, title, link) VALUES (@seasonId, @seriesNumber, @releaseDate, @title, @link)";
                using (SqlCommand insertCommand = new SqlCommand(insertSql, _connection))
                {
                    insertCommand.Parameters.AddWithValue("@title", "Пусто");
                    insertCommand.Parameters.AddWithValue("@link", "Пусто");
                    insertCommand.Parameters.AddWithValue("@seasonId", selectedSeasonId);
                    insertCommand.Parameters.AddWithValue("@seriesNumber", nextSeriesNumber);
                    insertCommand.Parameters.AddWithValue("@releaseDate", new DateTime(2000, 1, 1)); // Set default date
                    insertCommand.ExecuteNonQuery();
                }
            }
            catch { MessageBox.Show("Сначала добавьте сезон!"); }

            // Reload series data to reflect the new row
            LoadSeriesData(aid, selectedSeasonId);

            // Optionally, clear any data in the series detail text boxes
            seriesNumberTextBox.Text = "";
            titleTextBox.Text = "";
            dateTextBox.Text = "";
            linkBox.Text = "";
        }

        private void SeriesAdminForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void addSeasonButton_Click(object sender, EventArgs e)
        {
            // Get the maximum season number for the current anime
            int maxSeasonNumber = 0;
            string maxNumberSql = "SELECT ISNULL(MAX(seasonNumber), 0) FROM seasons WHERE anime_id = @animeId";
            using (SqlCommand maxNumberCommand = new SqlCommand(maxNumberSql, _connection))
            {
                maxNumberCommand.Parameters.AddWithValue("@animeId", aid);
                maxSeasonNumber = (int)maxNumberCommand.ExecuteScalar();
            }

            // Insert a new season with the next season number
            int newSeasonNumber = maxSeasonNumber + 1;
            string insertSql = "INSERT INTO seasons (anime_id, seasonNumber) VALUES (@animeId, @seasonNumber)";
            using (SqlCommand insertCommand = new SqlCommand(insertSql, _connection))
            {
                insertCommand.Parameters.AddWithValue("@animeId", aid);
                insertCommand.Parameters.AddWithValue("@seasonNumber", newSeasonNumber);
                insertCommand.ExecuteNonQuery();
            }

            // Reload the season dropdown to reflect the new season
            PopulateSeasonComboBox();

            // Consider selecting the newly added season in the dropdown (optional)
            if (seasonNumber_combobox.Items.Count > 0)
            {
                seasonNumber_combobox.SelectedIndex = seasonNumber_combobox.Items.Count - 1;
            }
        }

        private void delSeasonButton_Click(object sender, EventArgs e)
        {
            // Confirmation dialog to prevent accidental deletion
            if (MessageBox.Show("Удалить этот сезон?", "Удаление сезона", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Check if a season is selected in the dropdown
                if (seasonNumber_combobox.SelectedIndex != -1)
                {
                    // Get the selected season's ID (key) from the KeyValuePair object
                    KeyValuePair<int, int> selectedItem = (KeyValuePair<int, int>)seasonNumber_combobox.SelectedItem;
                    int seasonId = selectedItem.Key;

                    // Delete episodes associated with the season
                    DeleteSeasonEpisodes(seasonId);

                    // Delete the season itself
                    DeleteSeason(seasonId);

                    // Reload the season dropdown and series data (consider clearing selection)
                    PopulateSeasonComboBox();
                    LoadSeriesData(aid, null); // Reload all series since the season may have changed
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите сезон для удаления.");
                }
            }
        }
        private void DeleteSeasonEpisodes(int seasonId)
        {
            string deleteEpisodeSql = "DELETE FROM series WHERE season_id = @seasonId";
            using (SqlCommand deleteEpisodeCommand = new SqlCommand(deleteEpisodeSql, _connection))
            {
                deleteEpisodeCommand.Parameters.AddWithValue("@seasonId", seasonId);
                deleteEpisodeCommand.ExecuteNonQuery();
            }
        }

        private void DeleteSeason(int seasonId)
        {
            string deleteSeasonSql = "DELETE FROM seasons WHERE id = @seasonId";
            using (SqlCommand deleteSeasonCommand = new SqlCommand(deleteSeasonSql, _connection))
            {
                deleteSeasonCommand.Parameters.AddWithValue("@seasonId", seasonId);
                deleteSeasonCommand.ExecuteNonQuery();
            }
        }
    }  
}
