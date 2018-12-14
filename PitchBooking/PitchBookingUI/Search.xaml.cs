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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Page
    {

     PitchDBEntities db = new PitchDBEntities("metadata=res://*/PitchModel.csdl|res://*/PitchModel.ssdl|res://*/PitchModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.117.135;initial catalog=PitchDB;persist security info=True;user id=john;password=Worldcup1;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");
        List<Pitch> pitches = new List<Pitch>();
        public Search()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPitchList();
            
        }


        private void lstUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

           

        }

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

    }







}



