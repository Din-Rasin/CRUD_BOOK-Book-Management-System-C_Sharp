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

namespace Add_book
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=DESKTOP-UEQEMOD;Database=LibraryDB;Trusted_Connection=True;";
            string title = textBoxTitle.Text;
            string author = textBoxAuthor.Text;
            int year;

            if (!int.TryParse(textBoxYear.Text, out year))
            {
                MessageBox.Show("Please enter a valid year.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Books (Title, Author, PublishYear) VALUES (@Title, @Author, @Year)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@Year", year);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book added successfully! USE LibraryDB SELECT * FROM Books !!!!!!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=DESKTOP-UEQEMOD;Database=LibraryDB;Trusted_Connection=True;";
            int bookId;

            if (!int.TryParse(textBoxBookID.Text, out bookId))
            {
                MessageBox.Show("Please enter a valid Book ID.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Author, PublishYear FROM Books WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", bookId);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        textBoxTitle.Text = reader["Title"].ToString();
                        textBoxAuthor.Text = reader["Author"].ToString();
                        textBoxYear.Text = reader["PublishYear"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Book not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=DESKTOP-UEQEMOD;Database=LibraryDB;Trusted_Connection=True;";
            int bookId;
            int year;

            if (!int.TryParse(textBoxBookID.Text, out bookId))
            {
                MessageBox.Show("Please enter a valid Book ID.");
                return;
            }

            if (!int.TryParse(textBoxYear.Text, out year))
            {
                MessageBox.Show("Please enter a valid year.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Books SET Title = @Title, Author = @Author, PublishYear = @Year WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", textBoxTitle.Text);
                cmd.Parameters.AddWithValue("@Author", textBoxAuthor.Text);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Id", bookId);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Book updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Book not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=DESKTOP-UEQEMOD;Database=LibraryDB;Trusted_Connection=True;";
            int bookId;

            if (!int.TryParse(textBoxBookID.Text, out bookId))
            {
                MessageBox.Show("Please enter a valid Book ID.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Books WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", bookId);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Book deleted successfully!");
                        ClearTextFields();
                    }
                    else
                    {
                        MessageBox.Show("Book not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void ClearTextFields()
        {
            textBoxBookID.Clear();
            textBoxTitle.Clear();
            textBoxAuthor.Clear();
            textBoxYear.Clear();
        }
    }
}
