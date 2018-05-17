using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Class for a Time
    /// </summary>
    public class Time
    {
        private uint hour;
        private uint minutes;
        private bool isAM;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_hour">The hour</param>
        /// <param name="_minute">The minutes</param>
        /// <param name="_AM">Is the time AM</param>
        public Time(uint _hour, uint _minute, bool _AM)
        {
            hour = _hour;
            minutes = _minute;
            isAM = _AM;
        }

        /// <summary>
        /// Copy Constructor: creates a new Time from an existing one
        /// </summary>
        /// <param name="_time">The Time to copy</param>
        public Time(Time _time)
        {
            this.hour = _time.hour;
            this.minutes = _time.minutes;
            this.isAM = _time.isAM;
        }

        /// <summary>
        /// Converts the hour into 24-hour time
        /// </summary>
        /// <returns>The converted 24-hour time</returns>
        private uint To24Hour()
        {
            if (isAM)
            {
                if (hour == 12)
                    return 0;

                return hour;
            }
            else
            {
                if (hour == 12)
                    return 12;

                return hour + 12;
            }
        }

        /// <summary>
        /// Convert the time into minutes
        /// </summary>
        /// <returns>The time in minutes</returns>
        public uint ToMinutes()
        {
            return To24Hour() * 60 + minutes;
        }

        /// <summary>
        /// Compares two times
        /// </summary>
        /// <param name="_other">The other time to compare to</param>
        /// <returns>
        ///    -1: this time comes before other
        ///     0: the two times are equal
        ///     1: this time comes after other
        /// </returns>
        public int CompareTime(Time _other) => (int)this.ToMinutes() - (int)_other.ToMinutes();

        /// <summary>
        /// Creates a string of the time
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            //create am/pm string
            string amS;
            if (isAM)
                amS = "am";
            else
                amS = "pm";

            //create hour string
            string hourS = hour + "";
            if (hour == 0)
                hourS += "0";

            //create minute string
            string minutesS = minutes + "";
            if (minutes == 0)
                minutesS += "0";

            //concatenate and return
            return hourS + ":" + minutesS + amS;
        }
    }
}
