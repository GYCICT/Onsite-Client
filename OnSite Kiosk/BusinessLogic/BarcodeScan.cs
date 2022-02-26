using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace OnSite_Kiosk.BusinessLogic
{
    class BarcodeScan
    {
        private static readonly int MaxBarcodeLength = 1024;
        private String _barcode = "";

        public BarcodeScanDelegate OnBarcodeScan;

        public BarcodeScan()
        {

        }

        public delegate void BarcodeScanDelegate(String barcode);


        public void Start()
        {
            // register for keyboard events so that we can handle barcode scanning. 
            CoreWindow.GetForCurrentThread().CharacterReceived += Key_Handler;
            _barcode = "";
        }

        public void Stop()
        {
            // un-register for keyboard events so that we can handle barcode scanning. 
            CoreWindow.GetForCurrentThread().CharacterReceived -= Key_Handler;
            _barcode = "";
        }

        private void Key_Handler(object sender, CharacterReceivedEventArgs e)
        {

            Console.WriteLine(e.KeyCode);
            if (e.KeyCode == 13)
            {
                // do something with the barcode
                String __barcode = this._barcode;
                this._barcode = "";
                OnBarcodeScan(__barcode);
                return;
            }
            if (e.KeyCode >= 32 && e.KeyCode <= 126)
            {
                // printable ascii charachters
                this._barcode += (char)e.KeyCode;
            }
            // maximum barcode length
            if (this._barcode.Length > MaxBarcodeLength)
            {
                this._barcode = this._barcode.Substring(this._barcode.Length - MaxBarcodeLength, MaxBarcodeLength);
            }


        }

    }
}
