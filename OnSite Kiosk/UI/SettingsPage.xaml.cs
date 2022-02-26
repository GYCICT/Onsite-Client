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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnSite_Kiosk.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public String PageTitle { get { return "Settings"; } }

        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            localSettings.Values["SiteID"] = txt_SiteID.Text;
            localSettings.Values["APIBase"] = txt_APIBase.Text;
            localSettings.Values["EndOfDay"] = txt_EndOfDay.Text;

            localSettings.Values["Mod_Staff"] = sw_mod_staff.IsOn;
            localSettings.Values["Mod_Student"] = sw_mod_student.IsOn;
            localSettings.Values["Mod_Visitor"] = sw_mod_visitor.IsOn;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            object tmp;

            if (localSettings.Values.TryGetValue("SiteID", out tmp))
            {
                txt_SiteID.Text = tmp.ToString();
            }
            if (localSettings.Values.TryGetValue("APIBase", out tmp))
            {
                txt_APIBase.Text = tmp.ToString();
            }
            if (localSettings.Values.TryGetValue("EndOfDay", out tmp))
            {
                txt_EndOfDay.Text = tmp.ToString();
            }
            if (localSettings.Values.TryGetValue("mod_student", out tmp))
            {
                sw_mod_student.IsOn = (bool)tmp;
            }
            if (localSettings.Values.TryGetValue("mod_staff", out tmp))
            {
                sw_mod_staff.IsOn = (bool)tmp;
            }
            if (localSettings.Values.TryGetValue("mod_visitor", out tmp))
            {
                sw_mod_visitor.IsOn = (bool)tmp;
            }



        }
    }
}
