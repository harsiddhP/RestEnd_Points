using System;
using System.Collections.Generic;
using System.Text;

namespace RestEndpoints
{
    class Student
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public double[] GPARecord { get; set; }

    }
}
