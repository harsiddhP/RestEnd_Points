﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RestEndpoints
{
    class StudentAggregate
    {
        
        public string YourName { get; set; }

        public string YourEmail { get; set; }

        public int YearWithHighestAttendance { get; set; }

        public int YearWithHighestOverallGpa { get; set; }

        public List<int> Top10StudentIdsWithHighestGpa { get; set; }

        public int StudentIdMostInconsistent { get; set; }
    }

   
}
