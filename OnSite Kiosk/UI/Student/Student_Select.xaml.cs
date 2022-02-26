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
using System.Collections.Generic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnSite_Kiosk.UI.Student
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class Student_Select : Page
    {
        public String PageTitle { get { return "Student Kiosk"; } }

        private Person selectedPerson;
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Student_Select()
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
                lst_results.Visibility = Visibility.Collapsed;
                selectedPerson = person;

                show_actions();

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


        private async void show_actions()
        {
            // need to load the available actions
            // todo: animate
            prg_loading.IsActive = true;
            List<String> actions = await new APIClient().StudentActions(selectedPerson);
            prg_loading.IsActive = false;
            if (actions == null)
            {
                return;
            }

            if (actions.Contains("signin"))
            {
                col_signin.Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                col_signin.Width = new GridLength(0);
            }
            if (actions.Contains("signout"))
            {
                col_signout.Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                col_signout.Width = new GridLength(0);
            }
            if (actions.Contains("late"))
            {
                col_late.Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                col_late.Width = new GridLength(0);
            }

            action_panel.Visibility = Visibility.Visible;
        }


        private void lst_results_Tapped(object sender, TappedRoutedEventArgs e)
        {
            txt_search.Text = lst_results.SelectedItem.ToString();
            lst_results.Visibility = Visibility.Collapsed;

            // User has been selected
            selectedPerson = (Person) lst_results.SelectedItem;

            show_actions();
        }

        private async void txt_search_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != Windows.System.VirtualKey.Enter)
            {
                action_panel.Visibility = Visibility.Collapsed;
                if (txt_search.Text.Length > 0)
                {
                    // start to search

                    var t = await new APIClient().StudentSearch(txt_search.Text);
                    Console.WriteLine(t);
                    lst_results.Items.Clear();
                    foreach (Person person in t)
                    {
                        lst_results.Items.Add(person);
                    }

                    lst_results.Visibility = Visibility.Visible;
                }
            }
            
        }

        async private void Signin_Click(object sender, RoutedEventArgs e)
        {

            if (await new APIClient().StudentSignIn(selectedPerson))
            {
                this.Frame.Navigate(typeof(SignInOutComplete), "You have successfully signed in.");

            }
            else
            {
                MessageDialog m = new MessageDialog("Could not sign you in. Please see reception.");
                await m.ShowAsync();
            }
        }

        private void Signout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Student_SignOut),selectedPerson);
        }

        private void Late_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Student_Late), selectedPerson);
        }
    }
}
