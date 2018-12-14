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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBLibrary;

namespace PitchBookingUI
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {

        //Connection String for SQL Server Database configured on a VMware Workstation
        //VM running Windows Server 2016 and SQL Server on IP Address 192.168.117.135

        PitchDBEntities db = new PitchDBEntities("metadata=res://*/PitchModel.csdl|res://*/PitchModel.ssdl|res://*/PitchModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.117.135;initial catalog=PitchDB;persist security info=True;user id=john;password=Worldcup1;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");
        List<User> users = new List<User>();
        List<Log> logs = new List<Log>();
        User selectedUser = new User();

        //enum sets which mode the DB change is in  - Add, Modify or Delete
        enum DBOperation
        {
            Add,
            Modify,
            Delete
        }

        //DBOperation dbOperation = new DBOperation();

        public Admin()
        {
            InitializeComponent();
        }

        //Method to reload list after update 
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUserList();

            lstlogsList.ItemsSource = logs;
            foreach (var log in db.Logs)
            {
                logs.Add(log);

            }

        }

        //Methods below to Add and Modify a user to the DB set in the enum
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            //if (dbOperation == DBOperation.Add)

            {
                User user = new User();
                user.Username = tbxUsername.Text.Trim();
                user.Password = tbxPassword.Text.Trim();
                user.Surname = tbxSurname.Text.Trim();
                user.Forename = tbxForename.Text.Trim();
                user.LevelID = cboAccessLevel.SelectedIndex;
                int saveSucess = SaveUser(user);
                if (saveSucess == 1)
                {
                    MessageBox.Show("User saved successfully.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                }
                else
                {
                    MessageBox.Show("Problem Saving User Record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }

        }

        //This Method saves the changes to the DB 
        public int SaveUser(User user)

        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSucess = db.SaveChanges();
            return saveSucess;
        }

        //This Method is called after an update or delete to refresh the Userlist
        private void RefreshUserList()

        {
            lstUserList.ItemsSource = users;
            users.Clear();
            foreach (var user in db.Users)
            {
                users.Add(user);
            }
            lstUserList.Items.Refresh();
        }

        //This Method sets the content of the Stack Panel to blank
        private void ClearUserDetails()
        {
            tbxUsername.Text = "";
            tbxPassword.Text = "";
            tbxForename.Text = "";
            tbxSurname.Text = "";
            cboAccessLevel.SelectedIndex = 0;
        }

        //This Method updates the displayed userlist

        private void lstUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUserList.SelectedIndex >0)

            {

                selectedUser = users.ElementAt(lstUserList.SelectedIndex);
                if (selectedUser.UserID > 0)
                {
                    tbxUsername.Text = selectedUser.Username;
                    tbxPassword.Text = selectedUser.Password;
                    tbxForename.Text = selectedUser.Forename;
                    tbxSurname.Text = selectedUser.Surname;
                    cboAccessLevel.SelectedIndex = selectedUser.LevelID;
                }
            }         
        
        }

        //This Method selets the enum event to modify
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            //dbOperation = DBOperation.Modify;

            {
                foreach (var user in db.Users.Where(t => t.UserID == selectedUser.UserID))

                {
                    user.Forename = tbxForename.Text.Trim();
                    user.Surname = tbxSurname.Text.Trim();
                    user.Password = tbxPassword.Text.Trim();
                    user.Username = tbxUsername.Text.Trim();
                    user.LevelID = cboAccessLevel.SelectedIndex;
                }
                int saveSucess = db.SaveChanges();
                if (saveSucess == 1)
                {
                    MessageBox.Show("User modified successfully.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                }

            }

        }

            //This Method deletes a selected record from the DB, is is callled Delete Click
            private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            db.Users.RemoveRange(db.Users.Where(t => t.UserID == selectedUser.UserID));
            int saveSucess = db.SaveChanges();
            if (saveSucess == 1 )
            {
                MessageBox.Show("User deleted successfully.", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshUserList();
                ClearUserDetails();
            }
            else

            {
                MessageBox.Show("Problem deleting User Record.", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        //This Method runs on the Cancel Click Event to clear the contents of the Stack Panel, calls method RefreshUserList() and ClearUserDetails();
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            RefreshUserList();
            ClearUserDetails();
        }
    }
}
