using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using OnSite_Kiosk.BusinessLogic;
using Windows.UI.Popups;
using System.ServiceModel.Channels;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnSite_Kiosk.UI.Student
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Student_Late : Page
    {

        
        public String PageTitle { get { return "Student Late Pass"; } }

        private Person selectedPerson = null;
        private StudentLateReason selectedReason = null;


        private int _MaxCols = 3;

        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Student_Late()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Person)
            {
                selectedPerson = e.Parameter as Person;
            }
                base.OnNavigatedTo(e);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // this code to be moved to business logic so that we start with a List<SignOutReason> object
            String reasonstring = "[{\"ID\":20,\"Description\":\"Other\"},{\"ID\":21,\"Description\":\"Personal / Family Issue\"},{\"ID\":22,\"Description\":\"Slept in\"},{\"ID\":24,\"Description\":\"Study Leave\"},{\"ID\":23,\"Description\":\"Transport Issues\"},{\"ID\":18,\"Description\":\"Weather\"}]";
            List<StudentLateReason> reasons = JsonConvert.DeserializeObject<List<StudentLateReason>>(reasonstring);


            // set up the grid
            int col = 0;
            int row = 0;
            int count = 0;

            reason_grid.Children.Clear();
            reason_grid.ColumnDefinitions.Clear();
            reason_grid.RowDefinitions.Clear();

            foreach (StudentLateReason reason in reasons)
            {
                count++;
                if (row == 0)
                {
                    // add a column definition
                    reason_grid.ColumnDefinitions.Add(new ColumnDefinition());
                }
                if (col == 0)
                {
                    // add a new row definition
                    reason_grid.RowDefinitions.Add(new RowDefinition());
                }

                if (count == reasons.Count)
                {
                    col = _MaxCols; // always put 'other' in the last column
                }

                ToggleButton b = new ToggleButton();

                b.Unchecked += (object zsender, RoutedEventArgs ze) =>
                {
                    selectedReason = null;
                };
                
                b.Checked += (object zsender, RoutedEventArgs ze) =>
                {
                    foreach(object child in reason_grid.Children)
                    {
                        if (child.GetType() == typeof(ToggleButton) && child != zsender)
                        {
                            ((ToggleButton)child).IsChecked = false;
                        }
                    }

                    selectedReason = reason;

                    
                    
                };
                b.Height = 100;
                
                b.Content = reason;
                b.SetValue(Grid.ColumnProperty, col);
                b.SetValue(Grid.RowProperty, row);
                b.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                b.CornerRadius = new CornerRadius(10);
                reason_grid.Children.Add(b);
                col++;
                if (col == _MaxCols)
                {
                    row++;
                    col = 0;
                }
            }


        }


        private async void btn_Signout_Click(object sender, RoutedEventArgs e)
        {
            
            if (await new APIClient().StudentLate(selectedPerson, selectedReason))
            {
                // print the late pass
                Dictionary<String, object> passData = new Dictionary<String, object> {
                    { "LastName", selectedPerson.Surname },
                    { "FirstName", selectedPerson.Given1 },
                    { "TimestampIn", DateTime.Now }

                };
                ApplicationData.Current.LocalSettings.Values["PassType"] = "StudentLate";
                ApplicationData.Current.LocalSettings.Values["PassData"] = JsonConvert.SerializeObject(passData);

                if (Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
                {
                    await Windows.ApplicationModel.FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
                }
                // show success
                this.Frame.Navigate(typeof(SignInOutComplete), "You have successfully signed in. Please wait while your late pass prints.");
            }
            else
            {
                await new MessageDialog("Sorry, the sign in attempt failed. Please see reception.").ShowAsync();
            }
        }
            
    }
}
