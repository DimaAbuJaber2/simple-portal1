using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal
{
    public class Course
    {
        public String name;
        public int no_of_hours;
        public int C_id;

        public Course(string n,int h,int c)
        {
            name = n;
            no_of_hours = h;
            C_id = c;
        }

       
    }
}