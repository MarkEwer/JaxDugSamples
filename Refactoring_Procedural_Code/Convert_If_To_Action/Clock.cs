using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_If_To_Action
{
    public class Clock
    {
        public Clock():this(DateTime.Now) { }
        public Clock(DateTime dateTime) { this.SetCurrentDateTime(dateTime); }
        private TimeSpan Offset { get; set; }
        public void SetCurrentDateTime(DateTime current)
        {
            this.Offset = current.Subtract(DateTime.Now);
        }
        public DateTime GetCurrentDate()
        {
            return DateTime.Now.Add(Offset).Date;
        }
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now.Add(Offset);
        }
    }
}
