using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace OnSite_Kiosk.BusinessLogic
{
    public class ZeroOrGreaterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int ivalue = (int)value;
            return ivalue > -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
