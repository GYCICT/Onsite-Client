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
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnSite_Kiosk.UI.Visitor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Visitor_CheckWWVP : Page
    {

        private Dictionary<String, object> guestinformation;
        private BarcodeScan bs = new BarcodeScan();

        public String PageTitle { get { return "Data Check";  } }
        public Visitor_CheckWWVP()
        {
            this.InitializeComponent();
        }

        private async void validate_wwvp()
        {

            prg_wwvp.IsActive = true;

            WWVP wwvp = await new APIClient().VerifyWWVP(guestinformation["lastname"] as String, guestinformation["wwvp"] as String);
            

            FadeOutWait.Completed += (object zsender, object ze) => {
                prg_wwvp.IsActive = false;
                pnl_wait.Visibility = Visibility.Collapsed;

                if (wwvp != null)
                {
                    if (wwvp.Status == WWVPStatus.Registered)
                    {
                        guestinformation["wwvpverifiedby"] = "ONLINE";

                        pnl_success.Opacity = 0;
                        pnl_success.Visibility = Visibility.Visible;
                        FadeInSuccess.Begin();
                        var timer = new System.Timers.Timer(800);
                        timer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
                        {
                            _ = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                this.Frame.Navigate(typeof(Visitor_SiteInfo), guestinformation);
                            });
                            timer.Enabled = false;
                            timer.Dispose();
                        };
                        timer.Enabled = true;

                        return;

                    }   
                }
                pnl_warn.Opacity = 0;
                pnl_warn.Visibility = Visibility.Visible;
                lbl_wwvpinfo.Text = guestinformation["lastname"].ToString() + ", " + guestinformation["firstname"].ToString() + "\n" + guestinformation["wwvp"].ToString();
                FadeInWarn.Begin();
                
                bs.Start();

            };

            FadeOutWait.Begin();

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            bs.Stop();
            if (e.NavigationMode == NavigationMode.New)
            {
                // remove ourself from the backstack if we are navigating forward
                this.Frame.BackStack.RemoveAt(this.Frame.BackStack.Count - 1);
            }
            base.OnNavigatedFrom(e);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType() == typeof(Dictionary<String, object>))
            {
                guestinformation = e.Parameter as Dictionary<String, object>;

                validate_wwvp();

                //String siteid = localSettings.Values["siteid"].ToString();
                //GuestPass guestpass = await new APIClient().GuestSignIn(siteid, txt_firstname.Text, txt_lastname.Text, txt_mobile.Text, txt_company.Text, null, null, null, true);
                // print the guestpass
                //Console.WriteLine(guestpass.FirstName);
            }
            base.OnNavigatedTo(e);
        }

        private async void OnBarcodeScan(String barcode)
        {
            // does the barcode belong to a staff member?
            Person person = await new APIClient().GetUserByBarcode(barcode);
            // if so, we can proceed
            if (person != null && person.userType == Person.UserType.Staff)
            {
                guestinformation["wwvpverifiedby"] = person.ToString();
                this.Frame.Navigate(typeof(Visitor_SiteInfo), guestinformation);
                return;
            }
            
            _ = new MessageDialog("A staff member is required to authorise your sign in.").ShowAsync();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            bs.OnBarcodeScan += OnBarcodeScan;
        }
    }
}
