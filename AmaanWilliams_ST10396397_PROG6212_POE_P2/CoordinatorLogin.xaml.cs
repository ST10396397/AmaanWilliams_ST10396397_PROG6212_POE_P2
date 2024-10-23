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
    /// Interaction logic for CoordinatorLogin.xaml
    /// </summary>
    public partial class CoordinatorLogin : Window
    {
        public CoordinatorLogin()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            // Get email and password from the form
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();  // In a real app, you should hash the password.

            // Validate input
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }

            try
            {
                // Connection string to the database
                string connectionString = "Data Source=hp820g4\\SQLEXPRESS;Initial Catalog=POE;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open connection
                    connection.Open();

                    // SQL query to check if the user exists and is a Programme Coordinator
                    string query = "SELECT COUNT(*) FROM AccountUser WHERE Email = @Email AND PasswordHash = @PasswordHash AND AccountType = 'Programme Coordinator/Academic Manager'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters for the email and password
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PasswordHash", password);  // Password should be hashed in a real app

                        // Execute the query
                        int count = (int)command.ExecuteScalar();

                        // If the credentials match a Programme Coordinator, redirect to the dashboard
                        if (count > 0)
                        {
                            MessageBox.Show("Programme Coordinator logged in successfully.");

                            // Redirect to ProgrammeCoordinatorDashboard window and close the login window
                            CoordinatorWindow coordinatorWindow = new CoordinatorWindow();
                            coordinatorWindow.Show();
                            this.Close();  // Close the login window
                        }
                        else
                        {
                            MessageBox.Show("Invalid email or password.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
