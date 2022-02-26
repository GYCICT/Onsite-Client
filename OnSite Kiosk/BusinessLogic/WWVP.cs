using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnSite_Kiosk.BusinessLogic
{
    enum WWVPStatus
    {
        Unknown,
        Registered,
        Expired
    }

    class WWVP
    {
        public String RegistrationNumber { get; set; }
        public WWVPStatus Status { get; set; } = WWVPStatus.Unknown;

        public DateTime Expiry { get; set; }
    }
}
