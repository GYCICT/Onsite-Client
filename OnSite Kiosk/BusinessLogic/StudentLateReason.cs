﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnSite_Kiosk.BusinessLogic
{
    class StudentLateReason
    {
        public int ID { get; set; }
        public String Description { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
