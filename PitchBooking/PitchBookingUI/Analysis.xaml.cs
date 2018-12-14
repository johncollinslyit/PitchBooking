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
    /// Interaction logic for Analysis.xaml
    /// </summary>
    public partial class Analysis : Page
    {
        List<Contact> contactList = new List<Contact>();
        List<User> userList = new List<User>();
        List<Log> logList = new List<Log>();
        List<Pitch> pitchList = new List<Pitch>();

        //Connection String for SQL Server Database configured on a VMware Workstation
        //VM running Windows Server 2016 and SQL Server on IP Address 192.168.117.135

        PitchDBEntities db = new PitchDBEntities("metadata=res://*/PitchModel.csdl|res://*/PitchModel.ssdl|res://*/PitchModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.117.135;initial catalog=PitchDB;persist security info=True;user id=john;password=Worldcup1;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        //enum sets which mode the DB change is in  - Summary, Count or Statistics (Also  for Which Table is selected
        enum AnalysisType
        {
            Summary,
            Count,
            Statistics
        }

        private AnalysisType analysisType = new AnalysisType();

        enum TableSelected
        {
            Pitch,
            User,
            Log       

        }

        private TableSelected tableSelected = new TableSelected();

        public Analysis()
        {
            InitializeComponent();
        }

        //Method to set the state of the ComboBox Selection -  for Analysis Type
        private void cboAnalysisType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //Check that an option has been selected
            //the selected index will be 0 if an option has been selected
            //if (cboAnalysisType.SelectedIndex == 1)
            {
                if (cboAnalysisType.SelectedIndex > 0)

                {

                    if (cboAnalysisType.SelectedIndex == 1)
                    {

                        analysisType = AnalysisType.Summary;
                    }

                    if (cboAnalysisType.SelectedIndex == 2)
                    {

                        analysisType = AnalysisType.Count;
                    }

                    if (cboAnalysisType.SelectedIndex == 3)
                    {
                        analysisType = AnalysisType.Statistics;
                    }

                }
            }
        }

        //Method to set the state of the ComboBox Selection - For Selected Table
        private void cboChooseTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Check that an option has been selected
            //the selected index will be 0 if an option has been selected
            //if (cboChooseTable.SelectedIndex == 1)
            //{ 
                if (cboChooseTable.SelectedIndex > 0)
                {
                      if (cboChooseTable.SelectedIndex == 1)
                         {
                       tableSelected = TableSelected.Pitch;
                         }
                      if (cboChooseTable.SelectedIndex == 2)
                        {
                      tableSelected = TableSelected.User;
                        }
                      if (cboChooseTable.SelectedIndex == 3)
                        {
                      tableSelected = TableSelected.Log;
                        }
                //}   
                      
            }
        }


        //Method to build the load data from the tables as page is loaded 

        private void Page_Loaded(object sender, RoutedEventArgs e)

        {
            foreach (var pitchRecord in db.Pitches)
            {
                pitchList.Add(pitchRecord);
            }

            foreach (var userRecord in db.Users)
            {
               userList.Add(userRecord);
            }

            foreach (var logRecord in db.Logs)
            {
               logList.Add(logRecord);
            }
        }


        //Method to run the Analyse method ehn the Analyse button is selected (Allowing Analysis type to be used +
        //Summary or Count and output to display area tbkAnalysisOutput)

        private void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            int recordCount = 0;
            string output = "";
            tbkAnalysisOutput.Text = "";

            if (analysisType == AnalysisType.Summary && tableSelected == TableSelected.Pitch)

            {
                foreach (var item in pitchList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} Pitch Name is {item.PitchName}" + Environment.NewLine;
                }
                output = output + Environment.NewLine + $"Total records In Pitch Table = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;
            }

            if (analysisType == AnalysisType.Summary && tableSelected == TableSelected.User)

            {
                foreach (var item in userList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} has the name {item.Forename}" + Environment.NewLine;
                }
                output = output + Environment.NewLine + $"Total records In User Table = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;
            }

            if (analysisType == AnalysisType.Summary && tableSelected == TableSelected.Log)

            {
                foreach (var item in logList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} has a Date and Time Entry {item.Date}" + Environment.NewLine;
                }
                output = output + Environment.NewLine + $"Total records In Log Table = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;
            }

            if (analysisType == AnalysisType.Count && tableSelected == TableSelected.Pitch)

            {
                foreach (var item in pitchList)
                   
                recordCount++;
                output = output + Environment.NewLine + $"Total records In Pitch Table = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;
            }

            if (analysisType == AnalysisType.Count && tableSelected == TableSelected.User)

            {
                foreach (var item in userList)
                  
                recordCount++;
                output = output + Environment.NewLine + $"Total records In User Table = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;
            }

            if (analysisType == AnalysisType.Count && tableSelected == TableSelected.Log)

            {
                foreach (var item in logList)
                    
                recordCount++;
                output = output + Environment.NewLine + $"Total records In Log Table = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;
            }
        }
    }
}