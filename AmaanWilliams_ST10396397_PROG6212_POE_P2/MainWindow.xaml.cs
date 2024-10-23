using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmaanWilliams_ST10396397_PROG6212_POE_P2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountWindow createAccountWindow = new CreateAccountWindow();
            createAccountWindow.Show();
        }

        private void LectuerLogin_Click(object sender, RoutedEventArgs e)
        {
            LecturerLogin lecturerLogin = new LecturerLogin();
            lecturerLogin.Show();
        }

        private void CoordinatorLogin_Click(object sender, RoutedEventArgs e)
        {
            LecturerLogin lecturerLogin = new LecturerLogin();
            lecturerLogin.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            this.Close();
        }
    }
}