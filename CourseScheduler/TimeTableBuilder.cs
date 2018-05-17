using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class TimeTableBuilder
    {
        private Bitmap image;

        private float gridStartHeight;
        private float gridStartWidth;
        private float gridColWidth;

        private uint firstTimeMin;
        private uint lastTimeMin;
        private uint numTimes;

        public TimeTableBuilder(int _imageWidth, int _imageHeight, uint _firstTimeMin, uint _lastTimeMin)
        {
            image = new Bitmap(_imageWidth, _imageHeight);

            gridStartHeight = 15f;
            gridStartWidth = 47f;
            gridColWidth = (image.Width - gridStartWidth) / 5;

            firstTimeMin = _firstTimeMin;
            lastTimeMin = _lastTimeMin;
            numTimes = (lastTimeMin - firstTimeMin) / 30;
        }

        public void BuildGrid()
        {
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);

            Font font = new Font("Arial", 8);
            SolidBrush textBrush = new SolidBrush(Color.Black);
            Pen darkPen = new Pen(textBrush, 1f);
            Pen softPen = new Pen(Color.LightGray, 0.8f);

            //draw the days
            g.DrawLine(darkPen, 0f, gridStartHeight, image.Width, gridStartHeight);

            string[] daysStrings = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            for (int i = 0; i < daysStrings.Length; i++)
            {
                g.DrawString(daysStrings[i], font, textBrush, gridStartWidth + gridColWidth * i, 0f);

                float x = gridStartWidth + gridColWidth * (i + 1);
                g.DrawLine(darkPen, x, 0f, x, gridStartHeight);
                g.DrawLine(softPen, x, gridStartHeight, x, image.Height);
            }

            //draw the times
            g.DrawLine(darkPen, gridStartWidth, 0f, gridStartWidth, image.Height);

            uint time = firstTimeMin;
            //calculate the y cord of the time
            float timeY = TimeToY(gridStartHeight, image.Height, firstTimeMin, lastTimeMin, time);
            for (int i = 0; i < numTimes; i++)
            {
                //convert the time into the form: hour:minutes am/pm
                string am = "am";
                uint hour = time / 60;
                if (hour >= 12)
                    am = "pm";
                if (hour > 12)
                    hour -= 12;

                uint minutes = time % 60;
                string minutesS = minutes + "";
                if (minutes == 0)
                    minutesS += "0";

                //draw the time
                g.DrawString(hour + ":" + minutesS + am, font, textBrush, 0f, timeY);

                //increment the time by 30 to get the next time
                time += 30;

                //calc the y cord and draw the lines for the next time
                timeY = TimeToY(gridStartHeight, image.Height, firstTimeMin, lastTimeMin, time);
                g.DrawLine(softPen, gridStartWidth, timeY, image.Width, timeY);
                g.DrawLine(darkPen, 0f, timeY, gridStartWidth, timeY);
            }

            //dispose of graphics components
            softPen.Dispose();
            darkPen.Dispose();
            textBrush.Dispose();
            font.Dispose();
            g.Dispose();
        }

        public void BuildTimeSlot(TimeSlot _timeSlot, Color _color)
        {
            //create graphics components
            Graphics g = Graphics.FromImage(image);
            Font font = new Font("Arial", 7);
            SolidBrush textBrush = new SolidBrush(Color.Black);
            Pen darkPen = new Pen(textBrush, 1f);
            Pen softPen = new Pen(Color.LightGray, 0.8f);
            SolidBrush courseBrush = new SolidBrush(_color);

            //calculate the y cord for the top and bottom of the rectangle
            float y1 = TimeToY(gridStartHeight, image.Height, firstTimeMin, lastTimeMin, _timeSlot.GetStartTime().ToMinutes());
            float y2 = TimeToY(gridStartHeight, image.Height, firstTimeMin, lastTimeMin, _timeSlot.GetEndTime().ToMinutes());

            //calculate the x cord for the left and right of the rectangle
            float x1 = gridStartWidth + (int)_timeSlot.GetDay() * gridColWidth;
            float x2 = x1 + gridColWidth;

            //draw the rectangles
            g.FillRectangle(courseBrush, x1, y1, x2 - x1, y2 - y1);
            g.DrawRectangle(darkPen, x1, y1, x2 - x1, y2 - y1);
            g.DrawString(_timeSlot.GetInfoString(), font, textBrush, x1, y1);

            //dispose of graphics components
            courseBrush.Dispose();
            softPen.Dispose();
            darkPen.Dispose();
            textBrush.Dispose();
            font.Dispose();
            g.Dispose();
        }

        public Bitmap GetImage() => new Bitmap(image);

        private float TimeToY(float startHeight, float totalHeight, uint firstTime, uint lastTime, uint time)
            => (totalHeight - startHeight) * ((float)(time - firstTime) / (lastTime - firstTime)) + startHeight;
    }
}
