using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ViewClaims.xaml
    /// </summary>
    public partial class ViewClaims : Window
    {

        private string connectionString = "";
        public ViewClaims()
        {
            InitializeComponent();
            LoadClaims();
        }

        private void DeleteClaim_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimsListView.SelectedItem != null)
            {
                DataRowView selectedClaim = (DataRowView)ClaimsListView.SelectedItem;
                int claimID = Convert.ToInt32(selectedClaim["ClaimID"]);

                // Confirm deletion
                if (MessageBox.Show("Are you sure you want to delete this claim?", "Delete Claim", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DeleteClaim(claimID);
                    LoadClaims(); // Reload claims after deletion
                }
            }
            else
            {
                MessageBox.Show("Please select a claim to delete.");
            }
        }

        private void LoadClaims()
        {
            string query = "SELECT ClaimID, ClassTaught, TotalAmount, ClaimStatus FROM Claims"; // Modify if necessary

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                ClaimsListView.ItemsSource = dataTable.DefaultView; // Set the data source for the ListView
            }
        }

        private void DeleteClaim(int claimID)
        {
            string query = "DELETE FROM Claims WHERE ClaimID = @ClaimID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClaimID", claimID);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Claim deleted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
