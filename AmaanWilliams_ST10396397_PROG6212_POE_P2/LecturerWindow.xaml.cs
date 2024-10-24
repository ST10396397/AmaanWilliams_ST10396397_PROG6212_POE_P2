﻿using System;
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
    /// Interaction logic for LecturerWindow.xaml
    /// </summary>
    public partial class LecturerWindow : Window
    {
        private string connectionString = "Data Source=labG9AEB3\\SQLEXPRESS;Initial Catalog=PROG6212POE;Integrated Security=True;Trust Server Certificate=True";
        public LecturerWindow()
        {
            InitializeComponent();
            LoadClaimStatus();
        }

        private void SubmitClaim_Click(object sender, RoutedEventArgs e)
        {
            SubmitClaim submitClaim = new SubmitClaim();
            submitClaim.Show();
        }

        private void ViewClaims_Click(object sender, RoutedEventArgs e)
        {    
                ViewClaims viewClaims = new ViewClaims();
                viewClaims.Show();
                LoadClaimStatus(); // Load claims status when the button is clicked
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SupportingDocs Docs = new SupportingDocs();
            Docs.Show();
  
        }

        // Method to load claim status into the ListView
        private void LoadClaimStatus()
        {
            string query = "SELECT ClassTaught, TotalAmount, ClaimStatus FROM Claims"; // Adjust the query as necessary

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                ClaimStatusListView.ItemsSource = dataTable.DefaultView; // Set the data source for the ListView
            }
        }
    }
}
