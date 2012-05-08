using System;
using System.Collections.Generic;

namespace Cirrious.Conference.Core.Models.Raw
{
    public class Slot
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public List<string> SessionIds { get; set; }
        public bool HasSessions { get; set; }

        public DateTime Start()
        {
            try
            {
                string startTime;
                string endTime;
                SplitNameIntoTimes(out startTime, out endTime);
                int minutes;
                int hours;
                SplitTimeString(startTime, out minutes, out hours);
                return HackyConstants.BaseDateTimeLocal.AddHours(hours).AddMinutes(minutes);
            }
            catch (Exception)
            {
                // mask the error
                return HackyConstants.BaseDateTimeLocal;
            }
        }

        public DateTime End()
        {
            try
            {
                string startTime;
                string endTime;
                SplitNameIntoTimes(out startTime, out endTime);
                int minutes;
                int hours;
                SplitTimeString(endTime, out minutes, out hours);
                return HackyConstants.BaseDateTimeLocal.AddHours(hours).AddMinutes(minutes);
            }
            catch (Exception)
            {
                // mask the error
                return HackyConstants.BaseDateTimeLocal;
            }
        }

        private void SplitNameIntoTimes(out string startTime, out string endTime)
        {
			var startEndSplit = Name.Split(' '); //, new [] {/*'-'*/ (char)0x2d, ' '}, StringSplitOptions.RemoveEmptyEntries);
            startTime = startEndSplit[0].Trim();
            endTime = startEndSplit[2].Trim();
        }

        private static void SplitTimeString(string endTime, out int minutes, out int hours)
        {
            var hourMinuteSplit = endTime.Split(':');
            hours = int.Parse(hourMinuteSplit[0]);
            minutes = int.Parse(hourMinuteSplit[1]);
        }
    }
}