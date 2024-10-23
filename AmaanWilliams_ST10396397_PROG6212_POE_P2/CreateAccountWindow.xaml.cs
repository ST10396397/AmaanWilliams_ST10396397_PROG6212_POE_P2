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
    /// Interaction logic for CreateAccountWindow.xaml
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        public CreateAccountWindow()
        {
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the values from the form
            string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string firstName = FirstNameTextBox.Text.Trim();
            string lastName = LastNameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();  // This should be hashed in a real system!

            // Validate form input
            if (string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Please fill out all fields.");
                return;
            }

            try
            {
                // Address of SQL server and database 
                string connectionString = "";

                // Establish connection
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Open Connection
                    con.Open();

                    // Hash the password (this is just a placeholder, implement a real hashing algorithm)
                    string passwordHash = password; // Replace this with a real hash function for security

                    // SQL Query with all required fields, including email
                    string query = "INSERT INTO AccountUser (FirstName, LastName, Email, PhoneNumber, PasswordHash, AccountType) " +
                                   "VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @PasswordHash, @AccountType)";

                    // Execute query with parameters
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);  // Add the email parameter
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        cmd.Parameters.AddWithValue("@AccountType", role);

                        cmd.ExecuteNonQuery();
                    }

                    // Close Connection
                    con.Close();
                }

                MessageBox.Show("Account successfully created.");
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
