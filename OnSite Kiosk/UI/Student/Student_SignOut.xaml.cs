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
    public sealed partial class Student_SignOut : Page
    {

        
        public String PageTitle { get { return "Student Sign Out"; } }

        private Person selectedPerson = null;
        private SignOutReason selectedReason = null;
        private DateTime? selectedTime = null;


        private int _MaxCols = 3;

        private DateTime now;
        private int numBlocks = 0;

        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Student_SignOut()
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
            String reasonstring = "[{\"ReasonID\":19,\"Description\":\"Appointment\",\"DefaultTimeBlocks\":2},{\"ReasonID\":1,\"Description\":\"Illness\",\"DefaultTimeBlocks\":-1},{\"ReasonID\":24,\"Description\":\"Study Leave\",\"DefaultTimeBlocks\":-1},{\"ReasonID\":20,\"Description\":\"Other\",\"DefaultTimeBlocks\":-1}]";
            List<SignOutReason> reasons = JsonConvert.DeserializeObject<List<SignOutReason>>(reasonstring);

            // set up the slider values
            DateTime endofday = DateTime.Parse((localSettings.Values["EndOfDay"] as String));
            now = DateTime.Now;
            // Round down to the current timeblock
            int baseMinute = (now.Minute / 15) * 15;
            now = DateTime.Parse(now.ToString("yyyy-MM-dd HH:") + baseMinute.ToString());

            // Calculate the number of 15 minute blocks until end of day
            TimeSpan span = endofday.Subtract(now);
            numBlocks = (int)span.TotalMinutes / 15;

            sld_estimated_return.Maximum = numBlocks;
            sld_estimated_return_ValueChanged(null, null);


            // set up the grid
            int col = 0;
            int row = 0;
            int count = 0;

            reason_grid.Children.Clear();
            reason_grid.ColumnDefinitions.Clear();
            reason_grid.RowDefinitions.Clear();

            foreach (SignOutReason reason in reasons)
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
                    btn_Signout.IsEnabled = false;
                    btn_SignoutNoPass.IsEnabled = false;
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
                    btn_Signout.IsEnabled = true;
                    btn_SignoutNoPass.IsEnabled = true;

                    if (reason.DefaultTimeBlocks >= 0)
                    {
                        sld_estimated_return.Value = reason.DefaultTimeBlocks + 1;
                    }
                    else
                    {
                        // DefaultTimeBlocks is -1, meaning default to not returning
                        sld_estimated_return.Value = sld_estimated_return.Maximum;
                    }
                    
                    
                    
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

        private void sld_estimated_return_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (lbl_return_time == null)
            {
                // in the event that the page hasn't fully loaded yet
                return;
            }
            if (sld_estimated_return.Value == sld_estimated_return.Maximum)
            {
                lbl_return_time.Text = "Not returning";
                selectedTime = null;
                return;
            }
            // calculate the label value
            int minutestoadd = (int)sld_estimated_return.Value * 15;
            selectedTime = now.AddMinutes(minutestoadd);
            this.lbl_return_time.Text = selectedTime.Value.ToString("%h:mm tt");
        }

        private async void Signout_Complete(bool PrintPass)
        {
            String successMessage = "You have successfully signed out.";
            selectedTime = null;

            if (await new APIClient().StudentSignOut(selectedPerson, (String)localSettings.Values["SiteID"], selectedReason, (DateTime?)selectedTime))
            {
                if (PrintPass)
                {
                    // print the leave pass
                    Dictionary<String, object> passData = new Dictionary<String, object> {
                        { "LastName", selectedPerson.Surname },
                        { "FirstName", selectedPerson.Given1 },
                        { "TimestampOut", DateTime.Now },
                        { "Returning", selectedTime }

                    };
                    ApplicationData.Current.LocalSettings.Values["PassType"] = "StudentLeave";
                    ApplicationData.Current.LocalSettings.Values["PassData"] = JsonConvert.SerializeObject(passData);

                    if (Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
                    {
                        await Windows.ApplicationModel.FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
                    }
                    successMessage = "You have successfully signed out. Please wait while your leave pass prints.";
                }
                
                // show success
                this.Frame.Navigate(typeof(SignInOutComplete), successMessage);
            }
            else
            {
                await new MessageDialog("Sorry, the sign out attempt failed. Please try again.").ShowAsync();
            }
        }

        private void btn_Signout_Click(object sender, RoutedEventArgs e)
        {
            Signout_Complete(true);
        }

        private void btn_SignoutNoPass_Click(object sender, RoutedEventArgs e)
        {
            Signout_Complete(false);
        }
    }
}
