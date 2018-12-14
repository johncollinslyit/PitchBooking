using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBLibrary;

namespace PitchBookingUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //SQL DB Attach String which contains SQL server IP Address as well as user name and password
        PitchDBEntities db = new PitchDBEntities("metadata=res://*/PitchModel.csdl|res://*/PitchModel.ssdl|res://*/PitchModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.117.135;initial catalog=PitchDB;persist security info=True;user id=john;password=Worldcup1;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            // Takes in UserName and Password from Login Screen and connects to SQL DB to validate
            
            // Tells user if they not validated or if validated continues to the Main Dashboard
           
            // Using 'Try - Catch' to throw a message if we cannot connect to the SQL Server 
           

            User validatedUser = new User();
            bool login = false;
            try
            {
                string currentUser = tbxUsername.Text;
                string currentPassword = tbxPassword.Password;
                foreach (var user in db.Users)
                {
                    if (user.Username == currentUser && user.Password == currentPassword)
                    {
                        login = true;
                        validatedUser = user;
                    }
                }
            }
            catch (EntityException)
            {
                MessageBox.Show("Cannot Conect to SQL Server. Application Closing.", "SQL Server Connect", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                Environment.Exit(0);
            }

            if (login)
            {
                CreateLogEntry("Login", "User Login OK", validatedUser.UserID, validatedUser.Username);
                this.Hide();
                Dashboard dashboard = new Dashboard();
                dashboard.user = validatedUser;
                dashboard.Owner = this;
                dashboard.ShowDialog();
                
            }
            MessageBox.Show("Please Check your username and password");
        }

        // Validated User Details Name, Time, etc written to the log file
        private void CreateLogEntry(string category, string description, int userID, string userName)
        {
            string comment = $"{description} user credentials  = {userName}";
            Log log = new Log();
            log.UserID = userID;
            log.Category = category;
            log.Description = comment;
            log.Date = DateTime.Now;
            SaveLog(log);
        }

        //Method to Perform the Write to the DataBase
        private void SaveLog(Log log)
        {
            db.Entry(log).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

    }
}
