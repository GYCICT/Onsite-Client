using OnSite_Kiosk.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class Visitor_Select : Page, INotifyPropertyChanged
    {
        public String PageTitle { get { return "Visitor Sign Out"; } }

        public ObservableCollection<GuestPass> Guests = new ObservableCollection<GuestPass>();

        private String siteid = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SiteID"] as String;

        public Visitor_Select()
        {
            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<GuestPass> guests = await new APIClient().GuestGetSignedIn(siteid);

            Guests = new ObservableCollection<GuestPass>(guests);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Guests)));

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // sign the visitor out
            

            GuestPass guestpass = (GuestPass)lst_visitor.SelectedItem;
            if (await new APIClient().GuestSignOut(siteid, guestpass))
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
