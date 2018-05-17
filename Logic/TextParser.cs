using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class TextParser
    {
        public struct ErrorCode
        {
            public string message;
            public uint lineNumber;

            public ErrorCode(string _message, uint _lineNumber)
            {
                message = _message;
                lineNumber = _lineNumber;
            }
        }

        public ErrorCode ParseText(string[] _inputText)
        {
            uint line = 0;
            while (++line <= _inputText.Length)
            {
                if (_inputText[line] != "Open")
                    continue;

                CourseSection newCourse = new CourseSection();

                //read course name
                line++;
                try
                {
                    ReadCourseName(_inputText[line], newCourse);
                }
                catch (InvalidCourseNameException e)
                {
                    return new ErrorCode(e.Message, line);
                }



            }

            return new ErrorCode();
        }

        private void ReadCourseName(string _line, CourseSection _courseSection)
        {
            try
            {
                string[] nameSubstrings = _line.Split(new char[] { ' ' }, 3);
                string[] codeSubstrings = nameSubstrings[0].Split(new char[] { '*' });

                _courseSection.SetName(codeSubstrings[0] + "*" + codeSubstrings[1]);
                _courseSection.SetSection(codeSubstrings[2]);
                _courseSection.SetName(nameSubstrings[2]);
            }
            catch (System.IndexOutOfRangeException e)
            {
                throw new InvalidCourseNameException("Course name is not in the expected format.");
            }
        }

        private class InvalidCourseNameException : Exception
        {
            public InvalidCourseNameException(string _message) : base(_message) { }
        }
    }
}
