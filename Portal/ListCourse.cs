using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal
{
    public class ListCourse
    {
        public List<CourseWithTime> Courses = new List<CourseWithTime>();
       
        public bool addCourse(CourseWithTime c)
        {
            
            bool found = false;
            bool valid = false;
           
            if (Courses.Count == 0)
                valid = true;
            foreach(CourseWithTime ci in Courses)
            {
                if((ci.CourseItem.C_id==c.CourseItem.C_id))
                {
                    found = true;
                    break;
                }
                if ((c.start >= ci.end))
                    valid = true;
                else if (c.end <= ci.start)
                    valid = true;
                else
                {
                    valid = false;
                    break;
                }
               
                
               
            }
            
           
            if (!(found) && (valid))
            {
                Courses.Add(c);
                return true;
            }

            else return false;
        }
       
        public void removeCourse(CourseWithTime c)
        {
            Courses.Remove(c);
        }
        public CourseWithTime getCourse(int itemid)
        {
            foreach (CourseWithTime item in Courses)
                if (item.CourseItem.C_id == itemid)
                    return item;
            return null;
        }
    }
}