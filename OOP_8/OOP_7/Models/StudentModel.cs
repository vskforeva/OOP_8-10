using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_8.Models
{
    public class StudentModel
    {
        private string connectionString = "Data Source=DESKTOP-G8PE845\\MONIN_SQL;Initial Catalog = OOP_8; Integrated Security = True; TrustServerCertificate=True";

        public void CreateStudent(string name, string record_book_number, string group_number, string institute, string major, DateTime enrollment_date)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO students(name, record_book_number, group_number, institute, major, enrollment_date) " +
                    "VALUES (@Name, @Record_book_number, @Group_number, @Institute, @Major, @Enrollment_date)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Record_book_number", record_book_number);
                cmd.Parameters.AddWithValue("@Group_number", group_number);
                cmd.Parameters.AddWithValue("@Institute", institute);
                cmd.Parameters.AddWithValue("@Major", major);
                cmd.Parameters.AddWithValue("@Enrollment_date", enrollment_date);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable ReadStudents(string name = null, string recordBookNumber = null,
                               string groupNumber = null, string institute = null,
                               string major = null, DateTime? startDate = null,
                               DateTime? endDate = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var queryBuilder = new StringBuilder("SELECT * FROM students WHERE 1=1");

                if (!string.IsNullOrEmpty(name))
                    queryBuilder.Append(" AND name LIKE @Name");

                if (!string.IsNullOrEmpty(recordBookNumber))
                    queryBuilder.Append(" AND record_book_number LIKE @RecordBookNumber");

                if (!string.IsNullOrEmpty(groupNumber))
                    queryBuilder.Append(" AND group_number LIKE @GroupNumber");

                if (!string.IsNullOrEmpty(institute))
                    queryBuilder.Append(" AND institute LIKE @Institute");

                if (!string.IsNullOrEmpty(major))
                    queryBuilder.Append(" AND major LIKE @Major");

                if (startDate.HasValue)
                    queryBuilder.Append(" AND enrollment_date >= @StartDate");

                if (endDate.HasValue)
                    queryBuilder.Append(" AND enrollment_date <= @EndDate");

                SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);

                if (!string.IsNullOrEmpty(name))
                    cmd.Parameters.AddWithValue("@Name", "%" + name + "%");

                if (!string.IsNullOrEmpty(recordBookNumber))
                    cmd.Parameters.AddWithValue("@RecordBookNumber", "%" + recordBookNumber + "%");

                if (!string.IsNullOrEmpty(groupNumber))
                    cmd.Parameters.AddWithValue("@GroupNumber", "%" + groupNumber + "%");

                if (!string.IsNullOrEmpty(institute))
                    cmd.Parameters.AddWithValue("@Institute", institute);

                if (!string.IsNullOrEmpty(major))
                    cmd.Parameters.AddWithValue("@Major", major);

                if (startDate.HasValue)
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Value);

                if (endDate.HasValue)
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                return dataTable;
            }
        }

        public void UpdateStudent(int studentId, string name, string record_book_number, string group_number, string institute, string major, DateTime enrollment_date)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE students SET name = @Name, record_book_number = @Record_book_number, group_number = @group_number, " +
                    "institute = @Institute, major = @Major, enrollment_date = @Enrollment_date WHERE student_id = @Student_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Record_book_number", record_book_number);
                cmd.Parameters.AddWithValue("@Group_number", group_number);
                cmd.Parameters.AddWithValue("@Institute", institute);
                cmd.Parameters.AddWithValue("@Major", major);
                cmd.Parameters.AddWithValue("@Enrollment_date", enrollment_date);
                cmd.Parameters.AddWithValue("@Student_id", studentId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM students WHERE student_id = @Student_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Student_id", studentId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
