using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AnimeDataBase.InfoForm;

namespace AnimeDataBase
{
    public partial class InfoForm : Form
    {
        public string login;
        public string AnimeId;
        public string _animeId;
        public string combotext;



        public int aid;

        private MainForm mainForm;


        private string con = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = animedb; Integrated Security = True; TrustServerCertificate=True";
        //
        //
        //
        public InfoForm(string _animeId, string AnimeId, string combotext, MainForm MainForm, string login)
        {
            InitializeComponent();

            mainForm = MainForm;

            this.AnimeId = AnimeId;
            this.login = login;
            //label11.Text = AnimeId + " log: " + login;

            this.aid = Convert.ToInt32(this.AnimeId = AnimeId);
            //label101.Text = aid.ToString();

            string _animeId2 = _animeId;

            var retriever = new ImageRetriever(con);
            retriever.Retrieve(posterBox1, aid);
            LoadInfo(con, aid);
        }

        //
        // ЗАГРУЗИТЬ ПОСТЕР АНИМЕ
        //
        class ImageRetriever
        {
            private readonly string _connectionString;

            public ImageRetriever(string connectionString)
            {
                _connectionString = connectionString;
            }

            public void Retrieve(PictureBox posterBox1, int aid)
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT poster FROM anime WHERE id = @id";
                    command.Parameters.AddWithValue("@id", aid);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        try
                        {
                            if (reader.Read())
                            {
                                var imageData = (byte[])reader["poster"];
                                if (imageData != null)
                                {
                                    using (var memoryStream = new MemoryStream(imageData))
                                    {
                                        posterBox1.Image = Image.FromStream(memoryStream);
                                    }
                                }
                                else
                                {
                                    // Optional: Handle the case where the poster is null in the database
                                    // You could display a message or set a default image
                                    Console.WriteLine("No poster found for anime with id: {0}", aid);
                                }
                            }
                            else
                            {
                                // Optional: Handle the case where no record is found
                                // You could display a message or set a default image
                                Console.WriteLine("No anime found with id: {0}", aid);
                            }
                        }
                        catch (InvalidCastException ex)
                        {
                            // Handle the InvalidCastException and display the lostposter.png image
                            Console.WriteLine("Error retrieving poster: {0}", ex.Message);
                            posterBox1.Image = Image.FromFile(@"C:\Users\Nekit\source\repos\AnimeDataBase\AnimeDataBase\images\lostposter.png");
                        }
                    }
                }
            }
        }
        //
        // ЗАГРУЗИТЬ ДАННЫЕ АНИМЕ
        //
        public void LoadInfo(string connectionString, int aid)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                try
                {
                    connection.Open();

                    string sql = @"SELECT a.id, a.name,
                    a.origName, a.description, a.agerating, a.numSeries,
                    a.numSeasons, a.startDate, a.endDate, a.rating,

                    (SELECT STUFF((SELECT ', ' + c.country FROM animeCountry ac
                    JOIN countries c ON ac.country_id = c.id WHERE ac.anime_id = a.id
                    FOR XML PATH('')), 1, 2, '')) AS country,

                    (SELECT STUFF((SELECT ', ' + g.genre FROM animeGenre ag
                    JOIN genres g ON ag.genre_id = g.id WHERE ag.anime_id = a.id
                    FOR XML PATH('')), 1, 2, '')) AS genre,

                    (SELECT STUFF((SELECT ', ' + au.author + ' ' FROM animeAuthor aa
                    JOIN authors au ON aa.author_id = au.id WHERE aa.anime_id = a.id
                    FOR XML PATH('')), 1, 2, '')) AS author

                    FROM anime a

                    LEFT JOIN animeCountry ac ON ac.anime_id = a.id
                    LEFT JOIN countries c ON ac.country_id = c.id
                    LEFT JOIN animeGenre ag ON ag.anime_id = a.id
                    LEFT JOIN genres g ON ag.genre_id = g.id
                    LEFT JOIN animeAuthor aa ON aa.anime_id = a.id
                    LEFT JOIN authors au ON aa.author_id = au.id

                    WHERE a.id = @AnimeId
                    GROUP BY a.id, a.name, a.origName, a.description, a.agerating,
                    a.numSeries, a.numSeasons, a.startDate, a.endDate, a.rating;";

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@animeId", aid);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        nameLabel.Text = reader["name"].ToString();
                        orinNameLabel.Text = reader["origname"].ToString();
                        infoRichTextBox.Text = reader["description"].ToString();
                        ageRatingLabel.Text = reader["agerating"].ToString();
                        numSeriesLabel.Text = reader["numseries"].ToString();

                        SeasonCounter seasonCounter = new SeasonCounter(con);
                        int seasonCount = seasonCounter.GetSeasonCount(aid);
                        numSeasonsLabel.Text = seasonCount.ToString();

                        SeriesCounter seriesCounter = new SeriesCounter(con);
                        int seriesCount = seriesCounter.GetTotalEpisodeCount(aid);
                        numSeriesLabel.Text = seriesCount.ToString();

                        ratingLabel.Text = reader["rating"].ToString();

                        AnimeInfoRetriever retriever = new AnimeInfoRetriever(con);
                        string firstEpisodeReleaseDate = retriever.GetFirstEpisodeReleaseDate(aid);
                        string startDate = firstEpisodeReleaseDate.ToString();

                        AnimeInfoRetriever2 retriever2 = new AnimeInfoRetriever2(con);
                        string firstEpisodeReleaseDate2 = retriever2.GetLastReleaseDate(aid);
                        string endDate = firstEpisodeReleaseDate2.ToString();

                        if (endDate == "")
                        {
                            endDate = "Не закончен";
                        }

                        dateLabel.Text = startDate + " - " + endDate;


                        countryLabel.Text = reader["country"].ToString();
                        genreBox.Text = reader["genre"].ToString();
                        authorLabel.Text = reader["author"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Аниме с таким ID не найдено.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public class SeasonCounter
        {
            private readonly string _connectionString;

            public SeasonCounter(string connectionString)
            {
                _connectionString = connectionString;
            }

            public int GetSeasonCount(int aid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();

                        string sql = @"SELECT COUNT(*) AS seasonCount
                                FROM seasons
                                WHERE anime_id = @animeId";

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@animeId", aid);

                        int seasonCount = (int)command.ExecuteScalar();

                        return seasonCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error counting seasons: " + ex.Message);
                        return 0;
                    }
                    finally
                    {
                        connection.Close();
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
        public class AnimeInfoRetriever
        {
            private readonly string _connectionString;

            public AnimeInfoRetriever(string connectionString)
            {
                _connectionString = connectionString;
            }

            public string GetFirstEpisodeReleaseDate(int aid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();

                        string sql = @"SELECT YEAR(MIN(s.releaseDate)) AS firstEpisodeReleaseYear
                        FROM anime a
                        INNER JOIN seasons se ON a.id = se.anime_id
                        INNER JOIN series s ON se.id = s.season_id
                        WHERE a.id = @animeId;
                        ";

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@animeId", aid);

                        string firstEpisodeReleaseDate = command.ExecuteScalar().ToString();

                        return firstEpisodeReleaseDate;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error retrieving first episode release date: " + ex.Message);
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public class AnimeInfoRetriever2
        {
            private readonly string _connectionString;

            public AnimeInfoRetriever2(string connectionString)
            {
                _connectionString = connectionString;
            }

            public string GetLastReleaseDate(int aid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();

                        string sql = @"SELECT YEAR(MAX(s.releaseDate)) AS lastEpisodeReleaseYear
                        FROM anime a
                        INNER JOIN seasons se ON a.id = se.anime_id
                        INNER JOIN series s ON se.id = s.season_id
                        WHERE a.id = @AnimeId;
                        ";

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@animeId", aid);

                        string firstEpisodeReleaseDate = command.ExecuteScalar().ToString();

                        return firstEpisodeReleaseDate;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error retrieving first episode release date: " + ex.Message);
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void ShowSeriesForm()
        {
            SeriesForm f2 = new SeriesForm(_animeId, AnimeId, login); // Pass ID in constructor
            f2.login = login;
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowSeriesForm();
        }

        private void InfoForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "animedbDataSet.status". При необходимости она может быть перемещена или удалена.
            this.statusTableAdapter.Fill(this.animedbDataSet.status);
            this.comboBox1.Text = combotext.ToString();
        }
        //
        // Обновить статус просмотра
        //
        private void button2_Click(object sender, EventArgs e)
        {
            int selectedStatusId = (int)comboBox1.SelectedIndex + 1;

            using (SqlConnection connection = new SqlConnection(con))
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
                                   // MainForm.LoadData();
                                    MessageBox.Show("Статус обновлен!.");
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
                                    MessageBox.Show("Статус обновлен!.");
                                }
                                else
                                {
                                    MessageBox.Show("Не удалось создать новую запись.");
                                }
                            }
                        }
                    }
                    mainForm.LoadData();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
