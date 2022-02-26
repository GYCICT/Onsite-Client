using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnSite_Kiosk.BusinessLogic
{
    class SignOutReason
    {
        public int ReasonID { get; set; }
        public String Description { get; set; }
        public int DefaultTimeBlocks { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
