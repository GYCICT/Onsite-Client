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

namespace OnSite_Kiosk.UI.Visitor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Visitor_SelectInOut : Page
    {
        public String PageTitle { get { return "Visitor Kiosk"; } }

        public Visitor_SelectInOut()
        {
            this.InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Visitor_SignIn));
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Visitor_Select));
        }
    }
}
