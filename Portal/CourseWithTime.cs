using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal
{
    public class CourseWithTime
    {
        public Course CourseItem;
        public string Lecture;
        public float start,end;

        
        public CourseWithTime(Course c,String l,float s,float e)
        {
            CourseItem = c;
            Lecture = l;
            start = s;
            end = e;
        }
    }
}