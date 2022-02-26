using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnSite_Kiosk.UI.Staff
{
    public sealed partial class Staff_SignOutOther : ContentDialog
    {
        public String Reason { get
            {
                return txt_reason.Text;
            } }
        public Staff_SignOutOther()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void txt_reason_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.IsPrimaryButtonEnabled = txt_reason.Text.Length > 0;
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            txt_reason.Focus(FocusState.Programmatic);
            InputPane.GetForCurrentView().TryShow();
        }
    }
}
