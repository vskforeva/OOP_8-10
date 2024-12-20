using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using System.Data.SqlClient; // Assuming SQL Server database




namespace AnimeDataBase
{
    public partial class SeriesForm : Form
    {
        public string login;
        public string _animeId;
        public int aid;
        public string videolink;
        public SqlConnection _connection;

        public SeriesForm(string _animeId, string AnimeId, string login)
        {
           InitializeComponent();

            this.login = login;
            this.aid = Convert.ToInt32(AnimeId);
            //label4.Text = aid.ToString() + " Log: " + login + " vid: " + videolink;
            _connection = GetSqlConnection(); // Establish database connection

            // Load anime information (consider using a separate method for better organization)
            LoadAnimeDetails();

            // Populate season dropdown
            PopulateSeasonComboBox();

            // Initial load of series data (consider using a separate method for better organization)
            LoadSeriesData(aid, null); // Load all series initially
            dataGridView1.ClearSelection();


        }
        public string con = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = animedb; Integrated Security = True; TrustServerCertificate=True";

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
                dataGridView1.Columns.Add("link", "link");
                dataGridView1.Columns.Add("seriesId", "seriesId");
                dataGridView1.Columns["seriesId"].Visible = false;
                dataGridView1.Columns["link"].Visible = false;

                DataGridViewCheckBoxColumn watchedColumn = new DataGridViewCheckBoxColumn();
                watchedColumn.HeaderText = "Просмотрено";
                watchedColumn.Width = 60;
                watchedColumn.ReadOnly = false; // Allow users to check/uncheck
                dataGridView1.Columns.Add(watchedColumn);
            }

            dataGridView1.Rows.Clear();

            // Construct query based on animeId and optional seasonNumber
            string sql = @"
                SELECT s.seriesNumber, COALESCE(s.title, ''), s.releaseDate, s.link, s.id AS seriesId, 
                CASE WHEN w.id IS NOT NULL THEN 1 ELSE 0 END AS watched
                FROM series s
                INNER JOIN seasons seas ON s.season_id = seas.id
                LEFT JOIN watchedEpisode w ON w.user_login = @login AND w.series_id = s.id
                WHERE seas.anime_id = @animeId ";

            if (!seasonId.HasValue) // Check if seasonId is null (meaning first load)
            {
                sql += "AND seas.id = (SELECT TOP 1 id FROM seasons WHERE anime_id = @animeId)"; // Get the first season ID
            }
            else
            {
                sql += "AND seas.id = @seasonId"; // Use provided seasonId if not null
            }


            using (SqlCommand command = new SqlCommand(sql, _connection))
            {
                command.Parameters.AddWithValue("@animeId", animeId);
                if (seasonId.HasValue)
                {
                    command.Parameters.AddWithValue("@seasonId", seasonId.Value);
                }
                command.Parameters.AddWithValue("@login", login); // Add the user_login parameter


                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        int seriesNumber = reader.GetInt32(0);
                        string title = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        DateTime releaseDate = reader.GetDateTime(2);
                        string link = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        int seriesId = reader.GetInt32(4); // Get the seriesId from the result set

                        SeriesCounter seriesCounter = new SeriesCounter(con);
                        int allSeriesCount = seriesCounter.GetTotalEpisodeCount(aid);

                        SeriesCounter2 seriesCounter2 = new SeriesCounter2(con);
                        int watchSeriesCount = seriesCounter2.GetTotalWatchedEpisodeCount(aid, login);
                       
                        watchedSeriesLabel.Text = watchSeriesCount + " из " + allSeriesCount;

                        // Check if the "watched" value is null and handle accordingly
                        bool watched;
                        if (reader.IsDBNull(5))
                        {
                            watched = false; // Set a default value (e.g., not watched)
                        }
                        else
                        {
                            int watchedFlag = reader.GetInt32(5); // Get the watched flag as an integer
                            watched = watchedFlag == 1;
                        }

                        dataGridView1.Rows.Add(seriesNumber, title, releaseDate.ToString("yyyy-MM-dd"), link, seriesId, watched);

                        // Set the checkbox value based on the watched flag
                        dataGridView1[5, dataGridView1.Rows.Count - 1].Value = watched;
                    }
                }
            }
        }
        public class SeriesCounter
        {
            private readonly string _connectionString;

            public SeriesCounter(string connectionString)
            {
                _connectionString = connectionString;
            }
            public int GetTotalEpisodeCount(int aid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();

                        string sql = @"SELECT
                        (SELECT SUM(episodeCount) FROM (
                            SELECT COUNT(*) AS episodeCount
                            FROM anime a
                            INNER JOIN seasons se ON a.id = se.anime_id
                            INNER JOIN series s ON se.id = s.season_id
                            WHERE a.id = @animeId
                        ) AS subquery) AS totalEpisodeCount";

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@animeId", aid);

                        int totalEpisodeCount = (int)command.ExecuteScalar();

                        return totalEpisodeCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error counting episodes: " + ex.Message);
                        return 0;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public class SeriesCounter2
        {
            private readonly string _connectionString;

            public SeriesCounter2(string connectionString)
            {
                _connectionString = connectionString;
            }
            public int GetTotalWatchedEpisodeCount(int aid, string login)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();

                        string sql = @"WITH watched_episodes AS (
                          SELECT se.id AS season_id, s.id AS series_id
                          FROM watchedEpisode we
                          INNER JOIN series s ON we.series_id = s.id
                          INNER JOIN seasons se ON s.season_id = se.id
                          WHERE we.user_login = @login
                        )
                        SELECT
                          (SELECT SUM(episodeCount) FROM (
                            SELECT COUNT(*) AS episodeCount
                            FROM anime a
                            INNER JOIN seasons se ON a.id = se.anime_id
                            INNER JOIN series s ON se.id = s.season_id
                            WHERE a.id = @animeId
                            AND s.id IN (SELECT series_id FROM watched_episodes)
                          ) AS subquery) AS totalWatchedEpisodeCount;";

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@animeId", aid);
                        command.Parameters.AddWithValue("@login", login);

                        int totalEpisodeCount = (int)command.ExecuteScalar();

                        return totalEpisodeCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error counting episodes: " + ex.Message);
                        return 0;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }



        private void playButton_Click(object sender, EventArgs e)
        {

            if (videolink != null && videolink != "") // Check if videolink has a value
            {
                chromiumWebBrowser1.Load(videolink);
                chromiumWebBrowser1.Focus();
                SendKeys.SendWait("{f}");
                MessageBox.Show($"Playing anime: {videolink}");
            }
            else
            {
                MessageBox.Show("Выберите серию аниме");
            }
        }

        //
        //ВЫБРАТЬ АНИМЕ
        //
      
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Check for valid row click
            {
                SeriesCounter seriesCounter = new SeriesCounter(con);
                int allSeriesCount = seriesCounter.GetTotalEpisodeCount(aid);

                SeriesCounter2 seriesCounter2 = new SeriesCounter2(con);
                int watchSeriesCount = seriesCounter2.GetTotalWatchedEpisodeCount(aid, login);

                watchedSeriesLabel.Text = watchSeriesCount + " из " + allSeriesCount;

                //label4.Text = aid.ToString() + " Log: " + login + " vid: " + videolink;
                // Assuming "link" is in the 4th column (index 3)
                string link = (string)dataGridView1.Rows[e.RowIndex].Cells[3].Value;

                if (!string.IsNullOrEmpty(link)) // Check if link is not null or empty
                {
                    videolink = link;
                    //MessageBox.Show($"Link retrieved: {videolink}");
                    //label4.Text = aid.ToString() + " Log: " + login + " vid: " + videolink;
                   
                }
                else
                {
                    MessageBox.Show("Ссылка для этой серии недоступна.");
                }
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0) // Check if it's the "Watched" checkbox column and a valid row
            {

                bool isChecked = (bool)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                int seriesId = (int)dataGridView1[4, e.RowIndex].Value; // Get the seriesId from the adjacent cell

                UpdateWatchedEpisode(seriesId, isChecked);
            }
        }

        private void UpdateWatchedEpisode(int seriesId, bool isChecked)
        {
            string connectionString = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = animedb; Integrated Security = True; TrustServerCertificate=True"; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@seriesId", seriesId);

                    string sql;
                    if (isChecked)
                    {
                        sql = @"
                    INSERT INTO watchedEpisode (user_login, series_id)
                    SELECT @login, @seriesId
                    FROM series s
                    WHERE s.id = @seriesId
                    AND NOT EXISTS (
                        SELECT 1 FROM watchedEpisode w
                        WHERE w.user_login = @login AND w.series_id = @seriesId
                    )";
                    }
                    else
                    {
                        sql = @"DELETE FROM watchedEpisode
                          WHERE user_login = @login AND series_id = @seriesId";
                    }

                    command.CommandText = sql;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Ошибка обновления статуса: {ex.Message}");
                    }
                }
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
        private void SeriesForm_Load(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Focus();
            SendKeys.SendWait("{f}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Focus();
            SendKeys.SendWait("{f}");
        }
    }
    
}
