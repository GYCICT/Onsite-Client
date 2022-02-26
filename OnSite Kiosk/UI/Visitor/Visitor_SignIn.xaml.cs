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
using OnSite_Kiosk.BusinessLogic;
using Windows.UI.ViewManagement;
using System.ComponentModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnSite_Kiosk.UI.Visitor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Visitor_SignIn : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;


        public String PageTitle { get { return "Visitor Sign In"; } }

        public String firstname { get; set; }

        public Visitor_SignIn()
        {
            this.InitializeComponent();
            DataContext = this;
        }
        
        private async void txt_search_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != Windows.System.VirtualKey.Enter)
            {
                //action_panel.Visibility = Visibility.Collapsed;
                if (txt_search.Text.Length > 0)
                {
                    // start to search

                    var t = await new APIClient().StaffSearch(txt_search.Text);
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
            //action_panel.Visibility = Visibility.Visible;

            //selectedPerson = (Person)lst_results.SelectedItem;
        }




        private void txt_firstname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_firstname.Text.Length > 0)
            {
                img_firstname_ok.Opacity = 1;
                img_firstname_warn.Opacity = 0;

                lookupguest();
            }
            else
            {
                img_firstname_ok.Opacity = 0;
                img_firstname_warn.Opacity = 1;
            }
        }

        private void txt_lastname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_lastname.Text.Length > 0)
            {
                img_lastname_ok.Opacity = 1;
                img_lastname_warn.Opacity = 0;

                lookupguest();
            }
            else
            {
                img_lastname_ok.Opacity = 0;
                img_lastname_warn.Opacity = 1;
            }
        }

        private void txt_mobile_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_mobile.Text.Length == 10 && txt_mobile.Text.StartsWith("04"))
            {
                img_mobile_ok.Opacity = 1;
                img_mobile_warn.Opacity = 0;

                lookupguest();
            }
            else
            {
                img_mobile_ok.Opacity = 0;
                img_mobile_warn.Opacity = 1;
            }
        }

        private async void lookupguest()
        {
            if (txt_firstname.Text.Length > 0 &&
                txt_lastname.Text.Length > 0 &&
                txt_mobile.Text.Length == 10 && txt_mobile.Text.StartsWith("04"))
            {
                // validation passed - try to locate an existing guest record
                // show a loading indicator
                var a = new LoadingView();
                _ = a.ShowAsync();

                Person person = await new APIClient().GuestSearch(txt_firstname.Text, txt_lastname.Text, txt_mobile.Text);
                // hide a loading indicator
                a.Hide();
                if (person != null)
                {
                    if (txt_company.Text.Length == 0)
                        txt_company.Text = person.Company;
                    if (txt_wwvp.Text.Length == 0)
                       txt_wwvp.Text = person.WWVP;

                }
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txt_firstname.Focus(FocusState.Programmatic);
            InputPane.GetForCurrentView().TryShow();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<String, object> responses = new Dictionary<String, object> {
                {"firstname", txt_firstname.Text },
                {"lastname", txt_lastname.Text },
                {"mobile", txt_mobile.Text },
                {"company", txt_company.Text },
                {"wwvp", txt_wwvp.Text },
                {"staffcontact", lst_results.SelectedItem },
                {"internet", chk_internet.IsChecked }
            };
            this.Frame.Navigate(typeof(Visitor_CheckWWVP), responses);
            
        }
    }
}
