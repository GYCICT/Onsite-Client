using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;

namespace OnSite_Kiosk.BusinessLogic
{
    class Person
    {
        public enum UserType
        {
            Staff,
            Student
        }

        public String ID { get; set; }
        public String Given1 { get; set; }
        public String Given2 { get; set; }
        public String Surname { get; set; }
        public String ExternalID { get; set; }
        public String Barcode { get; set; }
        public String Mobile { get; set; }
        public String WWVP { get; set; }
        public String Company { get; set; }


        public UserType userType { get; set; }

        public override String ToString()
        {
            return this.Surname + ", " + this.Given1;
        }
    }
}
