using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CourseSection
    {
        private List<TimeSlot> timeSlots;
        private string name;
        private string code;
        private string section;
        private string instructor;

        public CourseSection(string _name, string _code, string _section, string _instructor)
        {
            timeSlots = new List<TimeSlot>();
            name = _name;
            code = _code;
            section = _section;
            instructor = _instructor;
        }

        public CourseSection() : this("", "", "", "") { }

        public bool IsSameCourse(CourseSection _other) => (this.name == _other.name && this.code == _other.code);

        public string GetInfoString()
        {
            string s = code + ": " + name + " (" + section + ")\n";
            foreach (TimeSlot timeSlot in timeSlots)
            {
                s += timeSlot.GetClassType() + ": " +
                     timeSlot.GetDayString() + ", " +
                     timeSlot.GetStartTime() + " - " + timeSlot.GetEndTime() + ", " +
                     timeSlot.GetLocation() + "\n";
            }

            return s;
        }

        public bool HasConflict(CourseSection _other)
        {
            //check everyone of this course's timeslots against everyone of other's timeslots
            foreach (TimeSlot timeSlot1 in this.timeSlots)
            {
                foreach (TimeSlot timeSlot2 in _other.timeSlots)
                {
                    if (timeSlot1.HasConflict(timeSlot2))
                        return true;
                }
            }

            return false;
        }

        public string GetName() => name;
        public string GetCode() => code;
        public string GetSection() => section;
        public string GetInstructor() => instructor;
        public List<TimeSlot> GetTimeSlots() => new List<TimeSlot>(timeSlots);

        public void SetName(string _name) { name = _name; }
        public void SetCode(string _code) { code = _code; }
        public void SetSection(string _section) { section = _section; }
        public void SetInstructor(string _instructor) { instructor = _instructor; }
        public void SetTimeSlots(List<TimeSlot> _timeSlots) { timeSlots = _timeSlots; }
    }
}
