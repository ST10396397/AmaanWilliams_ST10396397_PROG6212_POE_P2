using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace AmaanWilliams_ST10396397_PROG6212_POE_P2
{
    /// <summary>
    /// Interaction logic for LecturerLogin.xaml
    /// </summary>
    public partial class LecturerLogin : Window
    {
        public LecturerLogin()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the user's input from the form fields
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            // Validate that both fields are filled out
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }
            // SQL connection

            try
            {
                // SQL connection string to your database
                string connectionString = "Data Source=labG9AEB3\\SQLEXPRESS;Initial Catalog=PROG6212POE;Integrated Security=True;Trust Server Certificate=True";
                // Establish a SQL connection using the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to check if the email and password hash match an entry in the AccountUser table
                    string query = "SELECT COUNT(*) FROM AccountUser WHERE Email = @Email AND PasswordHash = @PasswordHash AND AccountType = 'Lecturer'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Use SQL parameters to prevent SQL injection attacks
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PasswordHash", password);  // Assuming password is stored as plain text (it should be hashed)

                        // Execute the query and get the number of matching entries
                        int count = (int)command.ExecuteScalar();

                        // Check if any entries were found
                        if (count > 0)
                        {
                            MessageBox.Show("Lecturer logged in successfully.");

                            // Open the Lecturer Dashboard window and close the login window
                            LecturerWindow lecturerWindow = new LecturerWindow();
                            lecturerWindow.Show();
                            this.Close();  // Close the login window
                        }
                        else
                        {
                            // No matching entries were found
                            MessageBox.Show("Invalid email or password.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
    }
}
