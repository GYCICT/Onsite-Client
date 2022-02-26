using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnSite_Kiosk.BusinessLogic
{
    public class GuestPass
    {
        public Guid GUID { get; set; }
        public Guid GuestID { get; set; }
        public Timestamp TimestampIn { get; set; }
        public String SiteName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String DisplayName { get { return LastName + ", " + FirstName; } }
        public String Company { get; set; }
        public String Mobile { get; set; }
        public String WWVP { get; set; }
        public String WWVPVerifiedBy { get; set; }
        public String WifiToken { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
