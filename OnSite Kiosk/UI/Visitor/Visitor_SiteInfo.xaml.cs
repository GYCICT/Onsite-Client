using OnSite_Kiosk.BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnSite_Kiosk.UI.Visitor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Visitor_SiteInfo : Page
    {
        private Dictionary<String, object> guestinfo;
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public String PageTitle { get { return "Site Information"; } }
        public Visitor_SiteInfo()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter.GetType() == typeof(Dictionary<String, object>))
            {
                guestinfo = e.Parameter as Dictionary<String, object>;
            }
            base.OnNavigatedTo(e);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // display the loading indicator
            LoadingView lv = new LoadingView();
            _=lv.ShowAsync();
            // do the sign in
            String siteid = localSettings.Values["SiteID"].ToString();
            GuestPass guestpass = await new APIClient().GuestSignIn(siteid, 
                guestinfo["firstname"] as String, 
                guestinfo["lastname"] as String, 
                guestinfo["mobile"] as String, 
                guestinfo["company"] as String,
                guestinfo["wwvp"] as String,
                guestinfo["wwvpverifiedby"] as String,
                guestinfo["staffcontact"] as Person,
                guestinfo["internet"].ToString() == "True");

            // hide the loading indicator
            lv.Hide();

            // did it succeed?
            if (guestpass != null)
            {
                // print the visitor pass
                ApplicationData.Current.LocalSettings.Values["PassType"] = "Visitor";
                ApplicationData.Current.LocalSettings.Values["PassData"] = guestpass.ToString();
                
                if (Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
                {
                    await Windows.ApplicationModel.FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
                }

                // go to the success page
                this.Frame.Navigate(typeof(SignInOutComplete), "You have successfully signed in. Your visitor pass will now print.");
            }
        }
    }
}
