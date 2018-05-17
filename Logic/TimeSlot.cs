using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class TimeSlot
    {
        public enum Day { MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY, NONE = -1 };
        private static readonly string[] dayStrings = { "MON", "TUES", "WED", "THUR", "FRI" };

        private Time startTime;
        private Time endTime;
        private Day day;
        private string classType;
        private string location;

        public TimeSlot(Time _startTime, Time _endTime, Day _day, string _classType, string _location)
        {
            startTime = _startTime;
            endTime = _endTime;
            day = _day;
            classType = _classType;
            location = _location;
        }

        public bool HasConflict(TimeSlot _other)
        {
            //check if same day
            if (this.day != _other.day)
                return false;

            /*
            * Check time conflicts
            * Case 1: If other starts after this starts but before this ends or
            * Case 2: If other ends after this starts but before this ends or
            * Case 3: If other starts before this starts and ends after this ends
            */
            return (_other.startTime.CompareTime(this.startTime) >= 0 && _other.startTime.CompareTime(this.endTime) < 0 ||
                    _other.endTime.CompareTime(this.startTime) > 0 && _other.endTime.CompareTime(this.endTime) <= 0 ||
                    _other.startTime.CompareTime(this.startTime) <= 0 && _other.endTime.CompareTime(this.endTime) >= 0);
        }

        public static Day CreateDay(string _day)
        {
            //make input case-insensitive
            _day = _day.ToLower();

            for (int i = 0; i < dayStrings.Length; i++)
            {
                //if day matches a day string, set the day
                if (dayStrings[i].ToLower() == _day)
                    return (Day)i;
            }

            return Day.NONE;
        }

        public void SetDay(Day _day) { day = _day; }
        public bool SetDay(string _day)
        {
            Day newDay = CreateDay(_day);
            if (newDay == Day.NONE)
                return false;

            day = newDay;
            return true;
        }

        public string GetInfoString() => (classType + "\n" + location);

        public Time GetStartTime() => new Time(startTime);
        public Time GetEndTime() => new Time(endTime);
        public Day GetDay() => day;
        public string GetDayString() => dayStrings[(int)day];
        public string GetClassType() => classType;
        public string GetLocation() => location;
    }
}
