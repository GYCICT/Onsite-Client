using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.Codecrete.QrCodeGenerator;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using Windows.Storage;
using Newtonsoft.Json;

namespace Onsite_Kiosk.BusinessLogic
{
    public class PrintedPass
    {
        
        public enum PassType
        {
            Visitor,
            StudentLeave,
            StudentLate
        }

        public PrintedPass()
        {
            Printing();
        }


        private float printLine(String line, Font printFont, PrintPageEventArgs ev, float y, StringAlignment alignment = StringAlignment.Near)
        {
            int pageWidth = ev.PageBounds.Width;
            int marginWidth = ev.MarginBounds.Width;
            int marginBottom = ev.MarginBounds.Height;

            float x = 0;

            SizeF lineSize = ev.Graphics.MeasureString(line, printFont, marginWidth);
            switch (alignment)
            {
                case StringAlignment.Near:
                    x = ev.MarginBounds.X;
                    break;
                case StringAlignment.Center:
                    x = pageWidth / 2 - lineSize.Width / 2;
                    break;
                case StringAlignment.Far:
                    x = (ev.MarginBounds.X + ev.MarginBounds.Width) - lineSize.Width;
                    break;

            }
            Rectangle r = new Rectangle((int)x, (int)y, marginWidth, marginBottom - (int)y);
            ev.Graphics.DrawString(line, printFont, Brushes.Black, r, new StringFormat());
            y = lineSize.Height + 5;
            return y;
        }

        private void pd_VisitorPass(object sender, PrintPageEventArgs ev)
        {

            int pageWidth = ev.PageBounds.Width;

            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            Font printFont = new Font("Arial", 10);
            Font printFontLarge = new Font("Arial", 12);
            Font printFontSmall = new Font("Arial", 6, FontStyle.Italic);

            float yPos = topMargin;

            var passDataString = ApplicationData.Current.LocalSettings.Values["PassData"] as String;

            Dictionary<String, object> passData = JsonConvert.DeserializeObject<Dictionary<String, object>>(passDataString);


            var qr = QrCode.EncodeText("onsite://guestsignin/" + passData["GUID"] as String, QrCode.Ecc.Medium);
            Bitmap qrbmp = qr.ToBitmap(6, 0);

            Newtonsoft.Json.Linq.JObject timestampObject = passData["TimestampIn"] as Newtonsoft.Json.Linq.JObject;
            DateTime eventTimestamp = (DateTime)timestampObject.First.ToObject(typeof(DateTime));

            // todo: make this customizable
            
            Image logo = Image.FromFile(Windows.ApplicationModel.Package.Current.InstalledLocation.Path + "/Assets/passlogo.png");
            ev.Graphics.DrawImage(logo, pageWidth / 2 - 24, (int)yPos, 48, 66);
            yPos += 66;

            yPos += printLine("Guilford Young College", printFont, ev, yPos, StringAlignment.Center);
            yPos += printLine(passData["SiteName"] as String, printFont, ev, yPos, StringAlignment.Center);
            yPos += printFont.Height;
            yPos += printLine("VISITOR", printFontLarge, ev, yPos, StringAlignment.Center);
            yPos += printFont.Height;
            yPos += printLine(passData["LastName"] as String + ", " + passData["FirstName"] as String, printFontLarge, ev, yPos, StringAlignment.Center);
            yPos += printLine(passData["Company"] as String, printFont, ev, yPos, StringAlignment.Center);
            yPos += printLine("Date: " + eventTimestamp.ToString("dd/MM/yyyy"), printFont, ev, yPos, StringAlignment.Near);
            yPos += printLine("Time In: " + eventTimestamp.ToString("h:mm tt"), printFont, ev, yPos, StringAlignment.Near);
            yPos += printFontLarge.Height;
            if (passData["WifiToken"] != null)
            {
                yPos += printLine("Wifi: GYC-GUEST", printFont, ev, yPos, StringAlignment.Center);
                yPos += printLine("Token: " + passData["WifiToken"] as String, printFont, ev, yPos, StringAlignment.Center);
            }
            yPos += printFontLarge.Height;
            ev.Graphics.DrawImage(qrbmp, pageWidth / 2 - 50, (int)yPos, 100, 100);
            yPos += 120;
            yPos += printLine("Scan this pass on the kiosk to sign out", printFontSmall, ev, yPos, StringAlignment.Center);


            // If more lines exist, print another page.
            ev.HasMorePages = false;
        }

        private void pd_StudentLeavePass(object sender, PrintPageEventArgs ev)
        {

            int pageWidth = ev.PageBounds.Width;

            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            Font printFont = new Font("Arial", 10);
            Font printFontLarge = new Font("Arial", 12);
            Font printFontSmall = new Font("Arial", 6, FontStyle.Italic);

            float yPos = topMargin;

            var passDataString = ApplicationData.Current.LocalSettings.Values["PassData"] as String;

            Dictionary<String, object> passData = JsonConvert.DeserializeObject<Dictionary<String, object>>(passDataString);
            DateTime eventTimestamp = (DateTime)passData["TimestampOut"];
            DateTime? returning = (DateTime?)passData["Returning"];

            // todo: make this customizable

            Image logo = Image.FromFile(Windows.ApplicationModel.Package.Current.InstalledLocation.Path + "/Assets/passlogo.png");
            ev.Graphics.DrawImage(logo, pageWidth / 2 - 24, (int)yPos, 48, 66);
            yPos += 66;

            yPos += printLine("Guilford Young College", printFont, ev, yPos, StringAlignment.Center);
            yPos += printFont.Height;
            yPos += printLine("LEAVE PASS", printFontLarge, ev, yPos, StringAlignment.Center);
            yPos += printFont.Height;
            yPos += printLine(passData["LastName"] as String + ", " + passData["FirstName"] as String, printFontLarge, ev, yPos, StringAlignment.Center);
            yPos += printLine("Date: " + eventTimestamp.ToString("dd/MM/yyyy"), printFont, ev, yPos, StringAlignment.Near);
            yPos += printLine("Time Out: " + eventTimestamp.ToString("h:mm tt"), printFont, ev, yPos, StringAlignment.Near);
            //if (returning.HasValue)
            //{
            //    yPos += printLine("Expected return: " + returning.Value.ToString("h:mm tt"), printFont, ev, yPos, StringAlignment.Near);
            //}
            //else
            //{
            //    yPos += printFont.Height;
            //    yPos += printLine("NOT RETURNING", printFont, ev, yPos, StringAlignment.Center);
            //}


            // If more lines exist, print another page.
            ev.HasMorePages = false;
        }

        private void pd_StudentLatePass(object sender, PrintPageEventArgs ev)
        {

            int pageWidth = ev.PageBounds.Width;

            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            Font printFont = new Font("Arial", 10);
            Font printFontLarge = new Font("Arial", 12);
            Font printFontSmall = new Font("Arial", 6, FontStyle.Italic);

            float yPos = topMargin;

            var passDataString = ApplicationData.Current.LocalSettings.Values["PassData"] as String;

            Dictionary<String, object> passData = JsonConvert.DeserializeObject<Dictionary<String, object>>(passDataString);
            DateTime eventTimestamp = (DateTime)passData["TimestampIn"];

            // todo: make this customizable

            Image logo = Image.FromFile(Windows.ApplicationModel.Package.Current.InstalledLocation.Path + "/Assets/passlogo.png");
            ev.Graphics.DrawImage(logo, pageWidth / 2 - 24, (int)yPos, 48, 66);
            yPos += 66;

            yPos += printLine("Guilford Young College", printFont, ev, yPos, StringAlignment.Center);
            yPos += printFont.Height;
            yPos += printLine("LATE PASS", printFontLarge, ev, yPos, StringAlignment.Center);
            yPos += printFont.Height;
            yPos += printLine(passData["LastName"] as String + ", " + passData["FirstName"] as String, printFontLarge, ev, yPos, StringAlignment.Center);
            yPos += printLine("Date: " + eventTimestamp.ToString("dd/MM/yyyy"), printFont, ev, yPos, StringAlignment.Near);
            yPos += printLine("Time In: " + eventTimestamp.ToString("h:mm tt"), printFont, ev, yPos, StringAlignment.Near);


            // If more lines exist, print another page.
            ev.HasMorePages = false;
        }

        // Print the file.
        public void Printing()
        {

            var passTypeString = ApplicationData.Current.LocalSettings.Values["PassType"] as String;

            PassType passType = (PassType)Enum.Parse(typeof(PassType),passTypeString);

            //try
            //{
            PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = new PaperSize("Thermal Print", 197, 787);
                pd.DefaultPageSettings.Margins = new Margins(8, 8, 8, 8);
            switch (passType)
            {
                case PassType.Visitor:
                    pd.PrintPage += new PrintPageEventHandler(pd_VisitorPass);
                    break;
                case PassType.StudentLeave:
                    pd.PrintPage += new PrintPageEventHandler(pd_StudentLeavePass);
                    break;
                case PassType.StudentLate:
                    pd.PrintPage += new PrintPageEventHandler(pd_StudentLatePass);
                    break;
            }
                
                // Print the document.
                pd.Print();
                

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    throw ex;
            //}
        }
    }

    class PassPrint
    {
        static void Main(string[] args)
        {
            new PrintedPass();
        }
    }
}
