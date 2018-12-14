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
using DBLibrary;

namespace PitchBookingUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml - This is the base for calling all the Application Screens, Admin, Analysis and Pithces
    /// </summary>
    public partial class Dashboard : Window
    {
        public User user = new User();

        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            frmMain.Navigate(admin);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            frmMain.Navigate(search);
        }

        private void btnPitches_Click(object sender, RoutedEventArgs e)
        {

            PitchUpdates pitchupdates = new PitchUpdates();
            frmMain.Navigate(pitchupdates);

        }

        //Method to Control Access based on User Level - sets the Admin and Analysis button to visible if the user LevelID is 3
        private void CheckUserAccess (User user)
        {
            if (user.LevelID == 3)
            {
                btnAdmin.Visibility = Visibility.Visible;
                btnAnalysis.Visibility = Visibility.Visible;
            }       
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserAccess(user);
       }

        private void btnAnalysis_Click(object sender, RoutedEventArgs e)
        {
            Analysis analysis = new Analysis();
            frmMain.Navigate(analysis);
        }
    }
}
