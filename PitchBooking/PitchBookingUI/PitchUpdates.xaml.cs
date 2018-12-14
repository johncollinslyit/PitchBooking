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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class PitchUpdates : Page
    {

        //Connection String for SQL Server Database configured on a VMware Workstation
        //VM running Windows Server 2016 and SQL Server on IP Address 192.168.117.135

        PitchDBEntities db = new PitchDBEntities("metadata=res://*/PitchModel.csdl|res://*/PitchModel.ssdl|res://*/PitchModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.117.135;initial catalog=PitchDB;persist security info=True;user id=john;password=Worldcup1;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");
        List<Pitch> pitches = new List<Pitch>();
        List<Contact> contacts = new List<Contact>();
        List<Booking> bookings = new List<Booking>();
        Pitch selectedPitch = new Pitch();
        PitchType selectedType = new PitchType();
        Booking selectedBooking = new Booking();
        Contact selectedContact = new Contact();


        public PitchUpdates()
        {
            InitializeComponent();
         }

        public List<Contact> name { get; set; }
      

        //Method to initialise the named lists
        private void Page_Loaded(object sender, RoutedEventArgs e)
        { 
            RefreshPitchList();
            RefreshContactList();
            RefreshBookingList();
        }

        //This Method gathers the details of the selected record on the List
        private void lstPitchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPitchList.SelectedIndex > 0)

            {

                selectedPitch = pitches.ElementAt(lstPitchList.SelectedIndex);
                if (selectedPitch.PitchID > 0)
                {
                    tbxPitchLocation.Text = selectedPitch.PitchLocation;
                    tbxPitchName.Text = selectedPitch.PitchName;
                    tbxDay.Text = selectedPitch.Day;
                    cboPitchType.SelectedIndex = selectedPitch.TypeID;
                    cboContact.SelectedIndex = selectedPitch.ContactID;
                    cboClub.SelectedIndex = selectedPitch.BookingID;
                    
                }

            }

        }
        //Method to Add and Modify a record to the DB
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Pitch pitch = new Pitch();       
            
            pitch.PitchLocation = tbxPitchLocation.Text.Trim();
            pitch.PitchName = tbxPitchName.Text.Trim();
            pitch.Day = tbxDay.Text.Trim();
            pitch.TypeID = cboPitchType.SelectedIndex;
            pitch.ContactID = cboContact.SelectedIndex; 
            pitch.BookingID = cboClub.SelectedIndex;
            int saveSucess = SavePitch(pitch);
            if (saveSucess == 1)

            {
                MessageBox.Show("New Record saved successfully.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshPitchList();
                ClearPitchDetails();
                RefreshContactList();
                RefreshBookingList();
            }
            else
            {
                MessageBox.Show("Problem Saving New Record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Method to Perform the Write to the DataBase
        public int SavePitch(Pitch pitch)
        {
      
                db.Entry(pitch).State = System.Data.Entity.EntityState.Added;
                int saveSucess = db.SaveChanges();
                return saveSucess;
        }

        //Method to refrest list after write or delete
        private void RefreshPitchList()

        {
            lstPitchList.ItemsSource = pitches;
            pitches.Clear();
            foreach (var pitch in db.Pitches)
            {
                pitches.Add(pitch);
            }
            lstPitchList.Items.Refresh();
        }

        //Method to refresh list after write or delete
        private void RefreshContactList()

        {
            lstContactList.ItemsSource = pitches;
            contacts.Clear();
            foreach (var contact in db.Contacts)
            {
                contacts.Add(contact);
            }
            lstContactList.Items.Refresh();
        }

        //Method to refresh list after write or delete
        private void RefreshBookingList()

        {
            lstBookingList.ItemsSource = pitches;
            bookings.Clear();
            foreach (var booking in db.Bookings)
            {
                bookings.Add(booking);
            }
            lstBookingList.Items.Refresh();
        }

        //This Method sets the content of the Stack Panel to blank
        private void ClearPitchDetails()
        {
            tbxPitchName.Text = "";
            tbxPitchLocation.Text = "";       
            tbxDay.Text = "";
            cboPitchType.SelectedIndex = 0;
            cboContact.SelectedIndex = 0;
            cboClub.SelectedIndex = 0;



        }

        //Method to Delete a record from the DB
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            db.Pitches.RemoveRange(db.Pitches.Where(t => t.PitchID == selectedPitch.PitchID));
            int saveSucess = db.SaveChanges();
            if (saveSucess == 1)
            {
                MessageBox.Show("Pitch deleted successfully.", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshPitchList();
                ClearPitchDetails();
                RefreshContactList();
                RefreshBookingList();
            }
            else

            {
                MessageBox.Show("Problem deleting Pitch Record.", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        //This Method gathers the details of the selected record on the List
        private void lstContactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lstContactList.SelectedIndex > 0)
            {

               selectedContact = contacts.ElementAt(lstContactList.SelectedIndex);
            if (selectedContact.ContactId > 0)
            {            

            }
            }

        }

        //This Method gathers the details of the selected record on the List
        private void lstBookingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {

            if (lstBookingList.SelectedIndex > 0)
            {

                     selectedBooking = bookings.ElementAt(lstContactList.SelectedIndex);
                    if (selectedBooking.BookingID > 0)

                {
                        

                  }

            }
        }

        //This Method runs on the Cancel Click Event to clear the contents of the Stack Panel, calls method RefreshUserList() and ClearUserDetails()
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            RefreshPitchList();
            ClearPitchDetails();
            RefreshContactList();
            RefreshBookingList();
        }

        //This Method perfors a DB write when the update button is clicked
        private void btnUpdate1_Click(object sender, RoutedEventArgs e)
        {
            
            {
                foreach (var pitch in db.Pitches.Where(t => t.PitchID == selectedPitch.PitchID))

                {
                    pitch.PitchLocation = tbxPitchLocation.Text.Trim();
                    pitch.PitchName = tbxPitchName.Text.Trim();
                    pitch.Day = tbxDay.Text.Trim();
                    pitch.TypeID = cboPitchType.SelectedIndex;
                    pitch.ContactID = cboContact.SelectedIndex;
                    pitch.BookingID = cboClub.SelectedIndex;
                }
                int saveSucess = db.SaveChanges();
                if (saveSucess == 1)
                {
                    MessageBox.Show("Update modified successfully.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshPitchList();
                    ClearPitchDetails();
                    RefreshContactList();
                    RefreshBookingList();
                }

            }

        }
    }


}
