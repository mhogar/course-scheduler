using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Logic
{
    public class Scheduler
    {
        private List<List<CourseSection>> coursesList;
        private List<List<CourseSection>> schedules;

        public Scheduler()
        {
            coursesList = new List<List<CourseSection>>();
            schedules = new List<List<CourseSection>>();
        }

        public List<List<CourseSection>> GenSchedules()
        {
            schedules.Clear();
            GeneratePossibleSchedules(new List<CourseSection>(), coursesList.Count);
            return schedules;
        }

        private void GeneratePossibleSchedules(List<CourseSection> branchCourses, int numCourses)
        {
            //if the size of the branch is equal to the number of courses (base case)
            if (branchCourses.Count == numCourses)
            {
                //add a copy of branch to the possible schedules
                schedules.Add(new List<CourseSection>(branchCourses));
                //remove last element and return
                branchCourses.Remove(branchCourses[branchCourses.Count - 1]);
                return;
            }

            bool hasConflict = false;
            //get the next list of courses to be added at this height of the tree
            foreach (CourseSection newCourse in coursesList[branchCourses.Count])   
            {
                //check all courses in the current branch for conflicts
                foreach (CourseSection choosenCourse in branchCourses)
                {
                    if (newCourse.HasConflict(choosenCourse))
                    {
                        hasConflict = true;
                        break;
                    }
                }
                if (!hasConflict)
                {
                    //add the course the current branch and perform a recursive call
                    branchCourses.Add(newCourse);
                    GeneratePossibleSchedules(branchCourses, numCourses);
                }
                hasConflict = false;
            }

            //if branch is not empty remove last element
            if (branchCourses.Count > 0)
                branchCourses.Remove(branchCourses[branchCourses.Count - 1]);
        }

        public string GetScheduleText(List<CourseSection> schedule)
        {
            string s = "";
            foreach (CourseSection course in schedule)
            {
                s += course.GetInfoString() + "\n\n";
            }
            return s;
        }

        public void LoadCourses(string[] inputText)
        {
            coursesList.Clear();

            int line = 1;
            while (line < inputText.Length)
            {
                if (inputText[line++] != "Open")
                    continue;

                CourseSection newCourse = new CourseSection();

                //get course name
                ReadCourseName(inputText[line], ref newCourse);

                line += 2;
                //read the timeslots
                List<TimeSlot> timeSlots = new List<TimeSlot>();
                do {
                    string classType = inputText[line].Substring(0, 3);
                    if (classType == "LEC" || classType == "LAB")
                    {
                        List<TimeSlot.Day> days = ReadDays(inputText[line++]);
                        Time[] times = ReadTimes(inputText[line++]);
                        string location = inputText[line++];

                        foreach (TimeSlot.Day day in days)
                            timeSlots.Add(new TimeSlot(times[0], times[1], day, classType, location));
                    }
                    else { break; }
                } while (true);
                newCourse.SetTimeSlots(timeSlots);

                //read instructor
                newCourse.SetInstructor(inputText[line]);

                bool foundCourse = false;
                //check if course list already exist in list
                foreach (List<CourseSection> list in coursesList)
                {
                    //if course list exists, add to the list
                    if (list[0].GetName() == newCourse.GetName())
                    {
                        list.Add(newCourse);
                        foundCourse = true;
                    }
                }

                //course list doesn't exist, created new list and add course to list
                if (!foundCourse)
                    coursesList.Add(new List<CourseSection> { newCourse });
            }

            //sort list by the number of sections per course
            coursesList = coursesList.OrderBy(o => o.Count).ToList();
        }

        private void ReadCourseName(string line, ref CourseSection course)
        {
            string[] nameSubstrings = line.Split(new char[] { ' ' }, 3);
            string[] codeSubstrings = nameSubstrings[0].Split(new char[] { '*' });
            course.SetCode(codeSubstrings[0] + "*" + codeSubstrings[1]);
            course.SetSection(codeSubstrings[2]);
            course.SetName(nameSubstrings[2]);
        }

        private List<TimeSlot.Day> ReadDays(string line)
        {
            List<TimeSlot.Day> days = new List<TimeSlot.Day>();

            string[] tokens = line.Split();
            for (int i = 1; i < tokens.Length; i++)
            {
                TimeSlot.Day day = TimeSlot.CreateDay(tokens[i].TrimEnd(new char[] { ',' }));
                if (day != TimeSlot.Day.NONE)
                    days.Add(day);
            }

            return days;
        }

        private Time[] ReadTimes(string line)
        {
            Time[] times = new Time[2];

            string[] tokens = line.Split();

            string[] tokens2 = tokens[0].Split(new char[] { ':' });
            times[0] = new Time(uint.Parse(tokens2[0]), uint.Parse(tokens2[1].Substring(0, 2)), tokens2[1].Substring(2) == "AM");

            tokens2 = tokens[2].Split(new char[] { ':' });
            times[1] = new Time(uint.Parse(tokens2[0]), uint.Parse(tokens2[1].Substring(0, 2)), tokens2[1].Substring(2) == "AM");

            return times;
        }

        //private Lecture.LecDateTimeLoc ReadLecture(string daysLine, string timeLine, string locationLine)
        //{
        //    string[] timeLineSubs = timeLine.Split();
        //    string[] timeSubs = timeLineSubs[0].Split(new char[] { ':' });
        //    Lecture.LecDateTimeLoc.Time startTime = new Lecture.LecDateTimeLoc.Time(uint.Parse(timeSubs[0]),
        //        uint.Parse(timeSubs[1].Substring(0,2)), timeSubs[1].Substring(2).CompareTo("AM") == 0);

        //    timeSubs = timeLineSubs[2].Split(new char[] { ':' });
        //    Lecture.LecDateTimeLoc.Time endTime = new Lecture.LecDateTimeLoc.Time(uint.Parse(timeSubs[0]),
        //        uint.Parse(timeSubs[1].Substring(0,2)), timeSubs[1].Substring(2).CompareTo("AM") == 0);

        //    string[] daysSubs = daysLine.Split();
        //    string days = "";
        //    for (int i = 1; i < daysSubs.Length; i++)
        //    {
        //        days += daysSubs[i].TrimEnd(new char[] { ',' }) + " ";
        //    }
        //    return new Lecture.LecDateTimeLoc(startTime, endTime, new Lecture.LecDateTimeLoc.Days(days.TrimEnd()), locationLine);
        //}
    }
}
