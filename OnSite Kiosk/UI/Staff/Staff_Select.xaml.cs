using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using OnSite_Kiosk.BusinessLogic;
using Windows.UI.Popups;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnSite_Kiosk.UI.Staff
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class Staff_Select : Page
    {
        public String PageTitle { get { return "Staff Kiosk"; } }

        private Person selectedPerson;
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Staff_Select()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (selectedPerson == null){
                txt_search.Focus(FocusState.Programmatic);
                InputPane.GetForCurrentView().TryShow();
            }
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Person)
            {
                Person person = (Person)e.Parameter;
                txt_search.Text = person.ToString();
                action_panel.Visibility = Visibility.Visible;
                lst_results.Visibility = Visibility.Collapsed;
                selectedPerson = person;

            }
            base.OnNavigatedTo(e);
        }



        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (txt_search.Text.Length == 0)
            {
                // clear the search results
                lst_results.Visibility = Visibility.Collapsed;
            }
        }



        private void lst_results_Tapped(object sender, TappedRoutedEventArgs e)
        {
            txt_search.Text = lst_results.SelectedItem.ToString();
            lst_results.Visibility = Visibility.Collapsed;

            // User has been selected
            action_panel.Visibility = Visibility.Visible;

            selectedPerson = (Person) lst_results.SelectedItem;
        }

        private async void txt_search_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != Windows.System.VirtualKey.Enter)
            {
                action_panel.Visibility = Visibility.Collapsed;
                if (txt_search.Text.Length > 0)
                {
                    // start to search
                    try
                    {
                        var t = await new APIClient().StaffSearch(txt_search.Text);
                        Console.WriteLine(t);
                        lst_results.Items.Clear();
                        foreach (Person person in t)
                        {
                            lst_results.Items.Add(person);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Exception!");
                    }
                    
                    

                    lst_results.Visibility = Visibility.Visible;
                }
            }
            
        }

        async private void Signin_Click(object sender, RoutedEventArgs e)
        {
            object tmp;
            String siteid = "";

            if (localSettings.Values.TryGetValue("SiteID", out tmp))
            {
                siteid = (String)tmp;
            }

            if (await new APIClient().StaffSignIn(selectedPerson, siteid, "OnSite Kiosk"))
            {
                this.Frame.Navigate(typeof(SignInOutComplete), "You have successfully signed in.");

            }
            else
            {
                MessageDialog m = new MessageDialog("Could not sign you in");
                await m.ShowAsync();
            }
        }

        private void Signout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Staff_SignOut),selectedPerson);
        }

    }
}
