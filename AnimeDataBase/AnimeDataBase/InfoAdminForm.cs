using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AnimeDataBase.InfoForm;

namespace AnimeDataBase
{
    public partial class InfoAdminForm : Form
    {
        public string AnimeId;
        public string _animeId;
        public string login;
        public int aid;

        private MainForm mainForm;

        private string con = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = animedb; Integrated Security = True; TrustServerCertificate=True";

        public InfoAdminForm(string _animeId, string AnimeId, MainForm MainForm, string login)
        {
            InitializeComponent();
            this.login = login;
            mainForm = MainForm;

            this.AnimeId = AnimeId;
            //label10.Text = AnimeId;
            this.aid = Convert.ToInt32(this.AnimeId = AnimeId);
            string _animeId2 = _animeId;
            var retriever = new ImageRetriever(con);
            retriever.Retrieve(posterBox, aid);
        }
        //
        // СОХРАНИТЬ ПОСТЕР
        //
        public void savePoster_Click(object sender, EventArgs e)
        {
            var uploader = new ImageUploader(con);
            uploader.Upload(posterBox, aid);
            var retriever = new ImageRetriever(con);
            retriever.Retrieve(posterBox, aid);
            return;
        }
        //
        // ЗАГРУЗИТЬ ПОСТЕР
        //
        class ImageUploader
        {
            private readonly string _connectionString;

            public ImageUploader(string connectionString)
            {
                _connectionString = connectionString;

            }

            public void Upload(PictureBox posterBox, int aid)
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE anime SET poster = @image WHERE id = @id";
                    command.Parameters.AddWithValue("@id", aid);
                    try
                    {
                        var image = new Bitmap(posterBox.Image);
                        using (var memoryStream = new MemoryStream())
                        {
                            image.Save(memoryStream, ImageFormat.Jpeg);
                            memoryStream.Position = 0;

                            var sqlParameter = new SqlParameter("@image", SqlDbType.VarBinary, (int)memoryStream.Length)
                            {
                                Value = memoryStream.ToArray()

                            };
                            command.Parameters.Add(sqlParameter);
                            connection.Open();
                            int affectedRows = command.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Постер загружен успешно!");
                            }
                            else
                            {
                                MessageBox.Show("Ошибка: Не удалось обновить постер.");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
        }
        //
        // ПОКАЗАТЬ ПОСТЕР
        //
        class ImageRetriever
        {
            private readonly string _connectionString;

            public ImageRetriever(string connectionString)
            {
                _connectionString = connectionString;
            }

            public void Retrieve(PictureBox posterBox, int aid)
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
                                        posterBox.Image = Image.FromStream(memoryStream);
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
                            posterBox.Image = Image.FromFile(@"C:\Users\Nekit\source\repos\AnimeDataBase\AnimeDataBase\images\lostposter.png");
                        }
                    }
                }
            }
        }
        //
        // ВЫБРАТЬ ПОСТЕР
        //
        private void selectPoster_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "(*.jpg, *.jpeg, *.png) | *.jpg; *jpeg; *.png";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        posterBox.Image = Image.FromFile(openFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not load image file. " + ex.Message);
                    }
                }
            }
        }
        //
        // ЗАГРУЗКА ДАНННЫХ В ТАБЛИЦУ
        //
        private void InfoAdminForm_Load(object sender, EventArgs e)
        {
            LoadInfo(con, aid);
        }

        //
        // ЗАГРУЗИТЬ ДАННЫЕ АНИМЕ
        //
        public void LoadInfo(string con, int aid)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                try
                {
                    connection.Open();

                    // Retrieve all countries
                    List<string> allCountries = GetListOfAllCountries(connection);

                    // Retrieve all genres
                    List<string> allGenres = GetListOfAllGenres(connection);

                    // Retrieve all authors
                    List<string> allAuthors = GetListOfAllAuthors(connection);

                    // Fetch anime data
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
                    command.Parameters.AddWithValue("@AnimeId", aid);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        nameBox.Text = reader["name"].ToString();
                        origNameBox.Text = reader["origname"].ToString();
                        infoRichTextBox.Text = reader["description"].ToString();
                        ageBox.Text = reader["agerating"].ToString();

                        SeasonCounter seasonCounter = new SeasonCounter(con);
                        int seasonCount = seasonCounter.GetSeasonCount(aid);
                        numSeasonsBox.Text = seasonCount.ToString();

                        SeriesCounter seriesCounter = new SeriesCounter(con);
                        int seriesCount = seriesCounter.GetTotalEpisodeCount(aid);
                        numSeriesBox.Text = seriesCount.ToString();

                        rateBox.Text = reader["rating"].ToString();

                        AnimeInfoRetriever retriever = new AnimeInfoRetriever(con);
                        string firstEpisodeReleaseDate = retriever.GetFirstEpisodeReleaseDate(aid);
                        string startDate = firstEpisodeReleaseDate.ToString();

                        AnimeInfoRetriever2 retriever2 = new AnimeInfoRetriever2(con);
                        string firstEpisodeReleaseDate2 = retriever2.GetLastReleaseDate(aid);
                        string endDate = firstEpisodeReleaseDate2.ToString();

                        startDateBox.Text = startDate;
                        endDateBox.Text = endDate;

                        //dateLabel.Text = startDate + " - " + endDate;

                        // Populate CheckBoxes with all options and set checked based on anime data
                        foreach (string country in allCountries)
                        {
                            countryListBox.Items.Add(country, reader["country"].ToString().Contains(country));
                        }

                        foreach (string genre in allGenres)
                        {
                            genreListBox.Items.Add(genre, reader["genre"].ToString().Contains(genre));
                        }

                        foreach (string author in allAuthors)
                        {
                            authorListBox.Items.Add(author, reader["author"].ToString().Contains(author));
                        }
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
        private List<string> GetListOfAllCountries(SqlConnection connection)
        {
            List<string> countries = new List<string>();

            // Replace with your actual query to retrieve all distinct countries
            string sql = @"SELECT DISTINCT country FROM countries";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        countries.Add(reader["country"].ToString());
                    }
                }
            }

            return countries;
        }
        private List<string> GetListOfAllGenres(SqlConnection connection)
        {
            List<string> genres = new List<string>();

            // Replace with your actual query to retrieve all distinct genres
            string sql = @"SELECT DISTINCT genre FROM genres";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        genres.Add(reader["genre"].ToString());
                    }
                }
            }

            return genres;
        }
        private List<string> GetListOfAllAuthors(SqlConnection connection)
        {
            List<string> authors = new List<string>();

            // Replace with your actual query to retrieve all distinct authors
            string sql = @"SELECT DISTINCT author FROM authors";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        authors.Add(reader["author"].ToString());
                    }
                }
            }

            return authors;
        }

        //
        // сохранить данные
        //
        private void saveAll_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                try
                {
                    connection.Open();

                    // Prepare UPDATE statement with parameters for anime data
                    string updateSql = @"
                        UPDATE anime
                        SET name = @name,
                            origName = @origName,
                            description = @description,
                            agerating = @ageRating,
                            rating = @rating
                        WHERE id = @animeId;
                      ";

                    SqlCommand updateCommand = new SqlCommand(updateSql, connection);

                    // Add parameters for text box values
                    updateCommand.Parameters.AddWithValue("@animeId", aid);
                    updateCommand.Parameters.AddWithValue("@name", nameBox.Text);
                    updateCommand.Parameters.AddWithValue("@origName", origNameBox.Text);
                    updateCommand.Parameters.AddWithValue("@description", infoRichTextBox.Text);
                    updateCommand.Parameters.AddWithValue("@ageRating", ageBox.Text);

                    // Handle rating with validation
                    float rating;
                    bool isValidRating = float.TryParse(rateBox.Text, out rating);

                    if (isValidRating)
                    {
                        // Clamp rating between 0.0 and 10.0
                        rating = Math.Min(Math.Max(rating, 0.0f), 10.0f);
                        // Round to one decimal place
                        rating = (float)Math.Round(rating, 1);
                        updateCommand.Parameters.AddWithValue("@rating", rating);
                    }
                    else
                    {
                        MessageBox.Show("Неверный формат рейтинга. Пожалуйста, введите число от 0,0 до 10,0. (Разделяйте числа запятой!)");
                        return; // Exit if rating is invalid
                    }

                    // Execute the UPDATE command for the anime data
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    // Handle updates to country, genre, and author associations

                    if (rowsAffected > 0)
                    {
                        // Get selected items from checkboxes
                        List<string> selectedCountries = countryListBox.CheckedItems.Cast<string>().ToList();
                        List<string> selectedGenres = genreListBox.CheckedItems.Cast<string>().ToList();
                        List<string> selectedAuthors = authorListBox.CheckedItems.Cast<string>().ToList();

                        // **Delete existing associations before inserting new ones**
                        DeleteExistingAssociations(connection, aid);

                        // Update associations in separate loops
                        InsertNewAssociations(connection, aid, selectedCountries, "countries");
                        InsertNewAssociations(connection, aid, selectedGenres, "genres");
                        InsertNewAssociations(connection, aid, selectedAuthors, "authors");

                        MessageBox.Show("Данные успешно обновлены!");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не удалось обновить данные аниме.");
                    }

                    mainForm.LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void DeleteExistingAssociations(SqlConnection connection, int animeId)
        {
            string deleteSql = @"
                DELETE FROM animeCountry WHERE anime_id = @animeId;
                DELETE FROM animeGenre WHERE anime_id = @animeId;
                DELETE FROM animeAuthor WHERE anime_id = @animeId;
              ";

            using (SqlCommand deleteCommand = new SqlCommand(deleteSql, connection))
            {
                deleteCommand.Parameters.AddWithValue("@animeId", animeId);
                deleteCommand.ExecuteNonQuery();
            }
        }
        // Function to insert associations in a loop
        private void InsertNewAssociations(SqlConnection connection, int animeId, List<string> selectedItems, string tableName)
        {
            string associationTable = "";
            string insertSql = "";

            if (tableName.Equals("authors"))
            {
                //associationTable = "animeAuthor";
                insertSql = $"INSERT INTO [animeAuthor] (anime_id, author_id) VALUES (@animeId, @itemId)";
            }
            else if (tableName.Equals("genres")) // Assuming tableName is a dictionary or object
            {
                //associationTable = "animeGenre";
                insertSql = $"INSERT INTO [animeGenre] (anime_id, genre_id) VALUES (@animeId, @itemId)";
            }
            else if (tableName.Equals("countries"))
            {
                //associationTable = "animeCountry";
                insertSql = $"INSERT INTO [animeCountry] (anime_id, country_id) VALUES (@animeId, @itemId)";
            }
            else
            {
                // Handle invalid tableName (optional)
                MessageBox.Show($"Invalid tableName: {tableName}");
                return;
            }

            using (SqlConnection innerConnection = new SqlConnection(con))
            {
                innerConnection.Open();

                foreach (string item in selectedItems)
                {
                    int itemId = GetItemId(connection, tableName, item);

                    if (itemId > 0)
                    {
                        using (SqlCommand insertCommand = new SqlCommand(insertSql, innerConnection))
                        {
                            insertCommand.Parameters.AddWithValue("@animeId", animeId);
                            insertCommand.Parameters.AddWithValue("@itemId", itemId);
                            try
                            {
                                insertCommand.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                Console.WriteLine($"Error inserting association for '{item}' in '{associationTable}' table: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Warning: Item '{0}' not found in '{1}' table.", item, tableName);
                    }
                }
            }
        }
        private int GetItemId(SqlConnection connection, string tableName, string itemName)
        {
            string sql = "";

            if (tableName.Equals("authors"))
            {
                sql = $"SELECT id FROM authors WHERE author = @itemName";
            }
            else if (tableName.Equals("genres"))
            {
                sql = $"SELECT id FROM genres WHERE genre = @itemName";
            }
            else if (tableName.Equals("countries"))
            {
                sql = $"SELECT id FROM countries WHERE country = @itemName";
            }
            else
            {
                // Handle invalid tableName (optional)
                Console.WriteLine($"Invalid tableName: {tableName}");
                return 0;
            }

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@itemName", itemName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0); // Assuming the ID is stored in the first column (index 0)
                    }
                    else
                    {
                        return 0; // Item not found
                    }
                }
            }
        }

        private void ShowAdminSeriesForm()
        {
            SeriesAdminForm f2 = new SeriesAdminForm(_animeId, AnimeId, login); // Pass ID in constructor
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowAdminSeriesForm();
        }

        private void deletePoster_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void addAuthorButton_Click(object sender, EventArgs e)
        {
            // Get the text entered in the authorTextBox
            string newAuthorName = authorTextBox.Text.Trim();

            // Check if the textbox is empty
            if (string.IsNullOrEmpty(newAuthorName))
            {
                MessageBox.Show("Поле имени автора пустое. Пожалуйста, введите имя автора.");
                return;
            }

            // Connect to the database
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                // Check if the author already exists
                int authorId = GetAuthorId(connection, newAuthorName);

                if (authorId > 0)
                {
                    // Author already exists, associate it with the anime
                    InsertAuthorAssociation(connection, aid, authorId);
                    MessageBox.Show($"Автор '{newAuthorName}' уже существует в базе данных. Он(а) добавлен(а) к этому аниме.");
                }
                else
                {
                    // Insert the new author into the 'authors' table
                    authorId = InsertNewAuthor(connection, newAuthorName);

                    if (authorId > 0)
                    {
                        // Associate the new author with the anime
                        InsertAuthorAssociation(connection, aid, authorId);
                        MessageBox.Show($"Новый автор '{newAuthorName}' добавлен(а) в базу данных и связан(а) с этим аниме.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не удалось добавить нового автора.");
                    }
                }
            }
            int GetAuthorId(SqlConnection connection, string authorName)
            {
                string sql = "SELECT id FROM authors WHERE author = @authorName";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@authorName", authorName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            // Helper function to insert a new author
             int InsertNewAuthor(SqlConnection connection, string authorName)
            {
                string sql = "INSERT INTO authors (author) VALUES (@authorName); SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@authorName", authorName);

                    object newId = command.ExecuteScalar();

                    if (newId != null)
                    {
                        return Convert.ToInt32(newId);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            // Helper function to associate author with anime
             void InsertAuthorAssociation(SqlConnection connection, int animeId, int authorId)
            {
                string sql = "INSERT INTO animeAuthor (anime_id, author_id) VALUES (@animeId, @authorId)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@animeId", animeId);
                    command.Parameters.AddWithValue("@authorId", authorId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void addCountryButton_Click(object sender, EventArgs e)
        {
            // Get the text entered in the countryTextBox
            string newCountryName = countryTextBox.Text.Trim();

            // Check if the textbox is empty
            if (string.IsNullOrEmpty(newCountryName))
            {
                MessageBox.Show("Поле названия страны пустое. Пожалуйста, введите название страны.");
                return;
            }

            // Connect to the database
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                // Check if the country already exists
                int countryId = GetCountryId(connection, newCountryName);

                if (countryId > 0)
                {
                    // Country already exists, associate it with the anime
                    InsertCountryAssociation(connection, aid, countryId);
                    MessageBox.Show($"Страна '{newCountryName}' уже существует в базе данных. Она добавлена к этому аниме.");
                }
                else
                {
                    // Insert the new country into the 'countries' table
                    countryId = InsertNewCountry(connection, newCountryName);

                    if (countryId > 0)
                    {
                        // Associate the new country with the anime
                        InsertCountryAssociation(connection, aid, countryId);
                        MessageBox.Show($"Новая страна '{newCountryName}' добавлена в базу данных и связана с этим аниме.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не удалось добавить новую страну.");
                    }
                }
            }
        }
        private int GetCountryId(SqlConnection connection, string countryName)
        {
            string sql = "SELECT id FROM countries WHERE country = @countryName";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@countryName", countryName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        // Helper function to insert a new country
        private int InsertNewCountry(SqlConnection connection, string countryName)
        {
            string sql = "INSERT INTO countries (country) VALUES (@countryName); SELECT SCOPE_IDENTITY();";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@countryName", countryName);

                object newId = command.ExecuteScalar();

                if (newId != null)
                {
                    return Convert.ToInt32(newId);
                }
                else
                {
                    return 0;
                }
            }
        }

        // Helper function to associate country with anime
        private void InsertCountryAssociation(SqlConnection connection, int animeId, int countryId)
        {
            string sql = "INSERT INTO animeCountry (anime_id, country_id) VALUES (@animeId, @countryId)";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@animeId", animeId);
                command.Parameters.AddWithValue("@countryId", countryId);
                command.ExecuteNonQuery();
            }
        }

        private void addGenreButton_Click(object sender, EventArgs e)
        {
            // Get the text entered in the genreTextBox
            string newGenreName = genreTextBox.Text.Trim();

            // Check if the textbox is empty
            if (string.IsNullOrEmpty(newGenreName))
            {
                MessageBox.Show("Поле названия жанра пустое. Пожалуйста, введите название жанра.");
                return;
            }

            // Connect to the database
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                // Check if the genre already exists
                int genreId = GetGenreId(connection, newGenreName);

                if (genreId > 0)
                {
                    // Genre already exists, associate it with the anime
                    InsertGenreAssociation(connection, aid, genreId);
                    MessageBox.Show($"Жанр '{newGenreName}' уже существует в базе данных. Он добавлен к этому аниме.");
                }
                else
                {
                    // Insert the new genre into the 'genres' table
                    genreId = InsertNewGenre(connection, newGenreName);

                    if (genreId > 0)
                    {
                        // Associate the new genre with the anime
                        InsertGenreAssociation(connection, aid, genreId);
                        MessageBox.Show($"Новый жанр '{newGenreName}' добавлен в базу данных и связан с этим аниме.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не удалось добавить новый жанр.");
                    }
                }
            }
        }
        // Helper function to check if genre exists
        private int GetGenreId(SqlConnection connection, string genreName)
        {
            string sql = "SELECT id FROM genres WHERE genre = @genreName";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@genreName", genreName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        // Helper function to insert a new genre
        private int InsertNewGenre(SqlConnection connection, string genreName)
        {
            string sql = "INSERT INTO genres (genre) VALUES (@genreName); SELECT SCOPE_IDENTITY();";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@genreName", genreName);

                object newId = command.ExecuteScalar();

                if (newId != null)
                {
                    return Convert.ToInt32(newId);
                }
                else
                {
                    return 0;
                }
            }
        }

        // Helper function to associate genre with anime
        private void InsertGenreAssociation(SqlConnection connection, int animeId, int genreId)
        {
            string sql = "INSERT INTO animeGenre (anime_id, genre_id) VALUES (@animeId, @genreId)";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@animeId", animeId);
                command.Parameters.AddWithValue("@genreId", genreId);
                command.ExecuteNonQuery();
            }
        }
    }
}
