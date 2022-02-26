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
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnSite_Kiosk.UI.Visitor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Visitor_ConfirmSignOut : Page
    {

        public GuestPass guestPass = new GuestPass();

        public String PageTitle { get { return "Visitor Sign Out"; } }

        public Visitor_ConfirmSignOut()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType() == typeof(GuestPass))
            {
                guestPass = (GuestPass)e.Parameter;
            }
            base.OnNavigatedTo(e);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_name.Text = guestPass.DisplayName;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // sign the visitor out
            String siteid = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SiteID"] as String;

            if (await new APIClient().GuestSignOut(siteid, guestPass))
            {
                // show the complete page
                this.Frame.Navigate(typeof(SignInOutComplete), "Thank you. You have successfully signed out.");
                return;
            }
            // show an error
            await new MessageDialog("An error occurred signing you out. Please try again.").ShowAsync();
        }
    }
}
