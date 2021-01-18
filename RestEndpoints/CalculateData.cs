using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestEndpoints
{
    class CalculateData
    {
        public CalculateData()
        {
            
        }

        //exercise 1
        /// <summary>
        /// The method will return year with highest attendences of study
        /// </summary>
        /// <param name="yearList"></param>
        /// <returns>int</returns>
        public int YearWithHighestAttendences(List<int> yearList)
        {
            int year = yearList.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                       .Select(grp => grp.Key).First();
            return year;
        }

        //exercise 2
        /// <summary>
        /// The method will return year with highest overall gpa 
        /// </summary>
        /// <param name="yearList"></param>
        /// <returns>int</returns>
        public int YearWithHighestOverallGpa(List<KeyValuePair<int, double>> list )
        {
            int year = (from item in list
                     group item by item.Key
                       into g
                     select
                         new KeyValuePair<int, double>(g.Key,
                                                          g.Sum(e => e.Value))).First().Key;
            return year;
        }

        //exercise 3
        /// <summary>
        /// The method will return top 10 studentID with highest overall GPA
        /// </summary>
        /// <param name="yearList"></param>
        /// <returns>list</returns>
        public List<int>  Top10StudentIdsWithHighestGpa(List<KeyValuePair<int, double>> OverallGPAPerStudent)
        {
            List<int> tempList =  new List<int>();
            List<KeyValuePair<int, double>> reorderList = OverallGPAPerStudent.OrderByDescending(x => x.Value).ToList();

            foreach (var item in reorderList)
            {
                tempList.Add(item.Key);
                if (tempList.Count == 10)
                    break;
            }
            return tempList;
        }

        //exercise 4
        /// <summary>
        /// The method will return student ID with the largest difference between their minimum and maximum GPA
        /// </summary>
        /// <param name="yearList"></param>
        /// <returns>int</returns>
        public int StudentIdMostInconsistent(List<KeyValuePair<int, double>> largestDiffMaxAndMin)
        {
            return largestDiffMaxAndMin.OrderByDescending(x => x.Value).First().Key;
        }
    }
}
