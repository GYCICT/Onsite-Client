using OnSite_Kiosk.UI.Staff;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OnSite_Kiosk.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public MainPage()
        {
            this.InitializeComponent();
        }

        

        private void page_home_Loaded(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            // disbale the navigation stack on the root frame (we will use a subframe)
            rootFrame.IsNavigationStackEnabled = false;

            // initialize the home page and stick it into the content frame
            ContentFrame.Navigate(typeof(Home));

            // load the app settings
            if (!localSettings.Values.ContainsKey("SiteID"))
            {
                // initialize the home page and stick it into the content frame
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            
            
        }


        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (ContentFrame.CanGoBack)
                ContentFrame.GoBack();
        }



        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            btn_Back.IsEnabled = ContentFrame.CanGoBack;

            object page = this.ContentFrame.Content;

            Type t = this.ContentFrame.Content.GetType();
            var property = t.GetProperty("PageTitle");


            TitleFadeOut.Completed += (object zsender, object ze) => {
                if (property != null)
                {
                    PageTitleLabel.Text = property.GetValue(page) as String;
                }
                else
                {
                    PageTitleLabel.Text = "";
                }
                TitleFadeIn.Begin();
            };

            // start the transition
            TitleFadeOut.Begin();
            
        }

        private void Logo_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (ContentFrame.Content.GetType() != typeof(SettingsPage)){
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            
        }
    }
}
