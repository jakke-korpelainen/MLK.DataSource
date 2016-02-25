using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLK.DataSource
{
    public class ScheduleItem
    {
        public ScheduleType ScheduleType { get; set; }

        public string Field { get; set; }

        public string Time { get; set; }

        public string ReservationUrl { get; set; }
    }
}