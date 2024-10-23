using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    /// Interaction logic for CoordinatorWindow.xaml
    /// </summary>
    public partial class CoordinatorWindow : Window
    {
        public CoordinatorWindow()
        {
            InitializeComponent();
            LoadClaims();
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimsListView.SelectedItem is Claim selectedClaim)
            {
                UpdateClaimStatus(selectedClaim.ClaimID, "Approved");
            }
            else
            {
                MessageBox.Show("Please select a claim to approve.");
            }
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimsListView.SelectedItem is Claim selectedClaim)
            {
                UpdateClaimStatus(selectedClaim.ClaimID, "Rejected");
            }
            else
            {
                MessageBox.Show("Please select a claim to reject.");
            }
        }

        private void PendingButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClaimsListView.SelectedItem is Claim selectedClaim)
            {
                UpdateClaimStatus(selectedClaim.ClaimID, "Pending");
            }
            else
            {
                MessageBox.Show("Please select a claim to set as pending.");
            }
        }

        private void LoadClaims()
        {
            List<Claim> claims = GetClaimsFromDatabase();
            ClaimsListView.ItemsSource = claims;
        }

        private List<Claim> GetClaimsFromDatabase()
        {
            List<Claim> claims = new List<Claim>();
            string connectionString = "";
            string query = "SELECT ClaimID, ClassTaught, TotalAmount, ClaimStatus FROM Claims";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        claims.Add(new Claim
                        {
                            ClaimID = reader.GetInt32(0),
                            ClassTaught = reader.GetString(1),
                            TotalAmount = reader.GetDecimal(2),
                            ClaimStatus = reader.GetString(3)
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return claims;
        }

        private void UpdateClaimStatus(int claimID, string newStatus)
        {
            string connectionString = "";
            string query = "UPDATE Claims SET ClaimStatus = @ClaimStatus WHERE ClaimID = @ClaimID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClaimStatus", newStatus);
                command.Parameters.AddWithValue("@ClaimID", claimID);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Claim status updated successfully!");
                    LoadClaims(); // Reload claims after updating
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
    public class Claim
    {
        public int ClaimID { get; set; }
        public string ClassTaught { get; set; }
        public decimal TotalAmount { get; set; }
        public string ClaimStatus { get; set; }
    }
}
