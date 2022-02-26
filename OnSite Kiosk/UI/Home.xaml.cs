using OnSite_Kiosk.UI.Staff;
using OnSite_Kiosk.UI.Student;
using OnSite_Kiosk.BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using OnSite_Kiosk.UI.Visitor;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnSite_Kiosk.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        public String PageTitle { get { return "Welcome"; } }

        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        // we accept barcode input
        private BarcodeScan bs = new BarcodeScan();
        

        public Home()
        {
            this.InitializeComponent();
            bs.OnBarcodeScan = OnBarcode;
        }

        async public void OnBarcode(String barcode)
        {
            prg_loading.IsActive = true;
            // we have a barcode. is it our QR code?
            if (barcode.StartsWith("onsite://guestsignin/"))
            {
                String visitorguid = barcode.Substring(21);
                GuestPass pass = await new APIClient().GuestGetSignIn(visitorguid);
                prg_loading.IsActive = false;
                if (pass.GUID != Guid.Empty){
                    this.Frame.Navigate(typeof(Visitor_ConfirmSignOut), pass);
                    return;
                }
                await new MessageDialog("Visitor sign in not found. Perhaps you're already signed out?").ShowAsync();
                return;
            }

            // is it a staff member or student?
            Person person = null;
            try
            {
                person = await new APIClient().GetUserByBarcode(barcode);
            }catch
            {
                prg_loading.IsActive = false;
                var m = new MessageDialog("An error occurred while resolving user barcode.");
                await m.ShowAsync();
                return;
            }
            
            prg_loading.IsActive = false;

            if (person == null)
            {
                var m = new MessageDialog("Unknown user barcode");
                await m.ShowAsync();
                return;
            }
            if (person.userType == Person.UserType.Staff && (bool)localSettings.Values["mod_staff"])
            {
                ((Frame)this.Parent).Navigate(typeof(Staff_Select), person);
                return;
            }
            if (person.userType == Person.UserType.Student && (bool)localSettings.Values["mod_student"])
            {
                ((Frame)this.Parent).Navigate(typeof(Student_Select), person);
                return;
            }

            await new MessageDialog("Sorry, the kiosk is currently unable to process your request. Please see reception.").ShowAsync();
            
        }


        private void btn_staff_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)this.Parent).Navigate(typeof(Staff_Select));
            
        }

        private void btn_student_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)this.Parent).Navigate(typeof(Student_Select));
        }

        private void btn_visitor_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)this.Parent).Navigate(typeof(Visitor_SelectInOut));
        }


        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            bs.Stop();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //enabled modules
            if (!(bool)localSettings.Values["mod_student"])
            {
                main_menu.ColumnDefinitions.Remove(col_student);
                btn_student.Visibility = Visibility.Collapsed;
                btn_visitor.SetValue(Grid.ColumnProperty, (int)btn_visitor.GetValue(Grid.ColumnProperty) - 1);
            }
            if (!(bool)localSettings.Values["mod_staff"])
            {
                main_menu.ColumnDefinitions.Remove(col_staff);
                btn_staff.Visibility = Visibility.Collapsed;
                btn_visitor.SetValue(Grid.ColumnProperty, (int)btn_visitor.GetValue(Grid.ColumnProperty) - 1);
                btn_student.SetValue(Grid.ColumnProperty, (int)btn_student.GetValue(Grid.ColumnProperty) - 1);
            }
            if (!(bool)localSettings.Values["mod_visitor"])
            {
                main_menu.ColumnDefinitions.Remove(col_visitor);
                btn_visitor.Visibility = Visibility.Collapsed;
            }

            // start accepting barcode scanner input
            bs.Start();
        }

    }
}
