using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using Logic;

namespace CourseScheduler
{
    public partial class MainWindow : Form
    {
        private Scheduler scheduler;
        private Random rand;
        private List<List<CourseSection>> schedules;

        public MainWindow()
        {
            InitializeComponent();

            scheduler = new Scheduler();
            rand = new Random();
        }

        private void scheduleGenButton_Click(object sender, EventArgs e)
        {
            scheduler.LoadCourses(userInputBox.Lines);

            schedules = scheduler.GenSchedules();

            if (schedules.Count == 0)
            {
                courseLabel.Text = "No possible schedules";
                return;
            }

            schedulesComboBox.Items.Clear();
            for (int i = 0; i < schedules.Count; i++)
            {
                schedulesComboBox.Items.Add("Schedule " + (i + 1));
            }

            schedulesComboBox.SelectedIndex = 0;
        }

        private void schedulesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            courseLabel.Text = scheduler.GetScheduleText(schedules[schedulesComboBox.SelectedIndex]);
            scheduleGrid.Image = CreateGridImage(scheduleGrid.Width-1, scheduleGrid.Height-1, schedules[schedulesComboBox.SelectedIndex]);
        }

        private void randScheduleButton_Click(object sender, EventArgs e)
        {
            if (schedulesComboBox.Items.Count > 0)
            {
                schedulesComboBox.SelectedIndex = rand.Next(schedulesComboBox.Items.Count);
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (schedulesComboBox.Items.Count <= 0)
            {
                return;
            }
            if ((schedulesComboBox.SelectedIndex + 1) >= schedulesComboBox.Items.Count)
            {
                schedulesComboBox.SelectedIndex = 0;
            }
            else
            {
                schedulesComboBox.SelectedIndex++;
            }
        }

        private Bitmap CreateGridImage(int imageWidth, int imageHeight, List<CourseSection> schedule)
        {
            TimeTableBuilder timeTableBuilder = new TimeTableBuilder(imageWidth, imageHeight, 480, 1320); //start time = 8:00am, end time = 10:00pm

            //first build the grid
            timeTableBuilder.BuildGrid();

            //next, build each timeslot
            Color[] colours = { Color.Red, Color.Yellow, Color.Green, Color.DarkCyan, Color.Purple };
            int colourIndex = 0;

            foreach (CourseSection course in schedule)
            {
                List<TimeSlot> timeSlots = course.GetTimeSlots();
                foreach (TimeSlot timeSlot in timeSlots)
                {
                    timeTableBuilder.BuildTimeSlot(timeSlot, Color.Red);
                    //colourIndex++;
                }
            }

            return timeTableBuilder.GetImage();

            //Bitmap image = new Bitmap((int)imageWidth+1, (int)imageHeight+1);
            //Graphics g = Graphics.FromImage(image);
            //g.Clear(Color.White);

            //Font font = new Font("Arial", 8);
            //SolidBrush textBrush = new SolidBrush(Color.Black);
            //Pen darkPen = new Pen(textBrush, 1f);
            //Pen softPen = new Pen(Color.LightGray, 0.8f);

            //float gridStartHeight = 15f;
            //float gridStartWidth = 47f;

            //float gridColWidth = (imageWidth - gridStartWidth) / 5;

            ////draw the days
            //g.DrawLine(darkPen, 0f, gridStartHeight, imageWidth, gridStartHeight);

            //string[] daysStrings = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            //for (int i = 0; i < daysStrings.Length; i++)
            //{
            //    g.DrawString(daysStrings[i], font, textBrush, gridStartWidth + gridColWidth * i, 0f);

            //    float x = gridStartWidth + gridColWidth * (i + 1);
            //    g.DrawLine(darkPen, x, 0f, x, gridStartHeight);
            //    g.DrawLine(softPen, x, gridStartHeight, x, imageHeight);
            //}

            ////draw the times
            //g.DrawLine(darkPen, gridStartWidth, 0f, gridStartWidth, imageHeight);

            ////define the first time, last time, total number of minutes, and the number of times
            //uint firstTime = 480; //8:00 am
            //uint lastTime = 1320; //10:00 pm
            //uint numTimes = (lastTime - firstTime) / 30;

            //uint time = firstTime;
            ////calculate the y cord of the time
            //float timeY = TimeToY(gridStartHeight, imageHeight, firstTime, lastTime, time);
            //for (int i = 0; i < numTimes; i++)
            //{
            //    //convert the time into the form: hour:minutes am/pm
            //    string am = "am";
            //    uint hour = time / 60;
            //    if (hour >= 12)
            //        am = "pm";
            //    if (hour > 12)
            //        hour -= 12;

            //    uint minutes = time % 60;
            //    string minutesS = minutes + "";
            //    if (minutes == 0)
            //        minutesS += "0";

            //    //draw the time
            //    g.DrawString(hour + ":" + minutesS + am, font, textBrush, 0f, timeY);

            //    //increment the time by 30 to get the next time
            //    time += 30;

            //    //calc the y cord and draw the lines for the next time
            //    timeY = TimeToY(gridStartHeight, imageHeight, firstTime, lastTime, time);
            //    g.DrawLine(softPen, gridStartWidth, timeY, imageWidth, timeY);
            //    g.DrawLine(darkPen, 0f, timeY, gridStartWidth, timeY);
            //}

            //Color[] colours = { Color.Red, Color.Yellow, Color.Green, Color.DarkCyan, Color.Purple };
            //int colourIndex = 0;
            //SolidBrush courseBrush = new SolidBrush(Color.Black);
            //foreach (Course course in schedule)
            //{
            //    //if course does not have lectures, don't draw anything for it on the grid
            //    if (!(course is Lecture))
            //        continue;

            //    Lecture lec = (Lecture)course;

            //    Lecture.ImageAttributes[] imageInfos = lec.GetImageAttributes();

            //    courseBrush.Color = colours[colourIndex];
            //    foreach (Lecture.ImageAttributes imageInfo in imageInfos)
            //    {
            //        //calculate the y cord for the top and bottom of the rectangle
            //        float y1 = TimeToY(gridStartHeight, imageHeight, firstTime, lastTime, imageInfo.startTimeMin);
            //        float y2 = TimeToY(gridStartHeight, imageHeight, firstTime, lastTime, imageInfo.endTimeMin);

            //        foreach (int dayIndex in imageInfo.dayIndices)
            //        {
            //            //calculate the x cord for the left and right of the rectangle
            //            float x1 = gridStartWidth + dayIndex * gridColWidth;
            //            float x2 = x1 + gridColWidth;

            //            //draw the rectangles
            //            g.FillRectangle(courseBrush, x1, y1, x2 - x1, y2 - y1);
            //            g.DrawRectangle(darkPen, x1, y1, x2 - x1, y2 - y1);
            //            g.DrawString(imageInfo.text, font, textBrush, x1, y1);
            //        }
            //    }
            //    colourIndex++;
            //}

            //g.DrawRectangle(darkPen, 0f, 0f, imageWidth, imageHeight);

            //font.Dispose();
            //textBrush.Dispose();
            //darkPen.Dispose();
            //softPen.Dispose();
            //courseBrush.Dispose();
            //g.Dispose();
        }
    }
}
