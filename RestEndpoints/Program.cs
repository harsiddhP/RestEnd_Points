using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestEndpoints
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                //Base URL
                client.BaseAddress = new Uri("ENTER CLIENT BASE ADDRESS HERE");    
                client.DefaultRequestHeaders.Accept.Clear();
                
                //Client format: JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));  
                HttpResponseMessage respone;

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~GET~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                
                //Async call to server - returns respone code & data 
                respone = await client.GetAsync("api/Students");

                //Declare lists 
                List<int> yearList = new List<int>();
                List<KeyValuePair<int, double>> OverallGPAPerStudent = new List<KeyValuePair<int, double>>();
                List<KeyValuePair<int, double>> largestDiffMaxAndMax = new List<KeyValuePair<int, double>>();
                List<KeyValuePair<int, double>> yearandGPA = new List<KeyValuePair<int, double>>();

                if (respone.IsSuccessStatusCode)  
                {
                    //Reads server data - assigned to student list
                    List<Student> students = await respone.Content.ReadAsAsync<List<Student>>();

                    for (int i = 0; i < students.Count; i++)
                    {
                        List<double> tempList = new List<double>();
                        int temp = 0;
                        foreach (var item in students[i].GPARecord)
                        {
                            tempList.Add(item);
                            yearandGPA.Add(new KeyValuePair<int, double>(students[i].StartYear + temp, item));
                            temp++;
                        }
                        yearList.Add(students[i].StartYear);
                        OverallGPAPerStudent.Add(new KeyValuePair<int, double>(students[i].ID, (tempList.Sum() / tempList.Count)));
                        largestDiffMaxAndMax.Add(new KeyValuePair<int, double>(students[i].ID, tempList.Max() - tempList.Min()));
                    }
                }

                //Declaring objects for CalculateData class, StudentAggregate class 
                //Call CalculateData methods and pass list as parameter
                CalculateData calculate = new CalculateData();     
                StudentAggregate studentAggregate = new StudentAggregate
                {
                    YourName = "Harsiddh Patel",
                    YourEmail = "harsiddh.patel7@gmail.com",
                    YearWithHighestAttendance = calculate.YearWithHighestAttendences(yearList),   
                    YearWithHighestOverallGpa = calculate.YearWithHighestOverallGpa(yearandGPA),
                    Top10StudentIdsWithHighestGpa = calculate.Top10StudentIdsWithHighestGpa(OverallGPAPerStudent),
                    StudentIdMostInconsistent = calculate.StudentIdMostInconsistent(largestDiffMaxAndMax)
                };

                //Printing results to console
                Console.WriteLine("YourName = Harsiddh Patel");
                Console.WriteLine("YourEmail = harsiddh.patel7@gmail.com");
                Console.WriteLine("YearWithHighestAttendance: {0}", studentAggregate.YearWithHighestAttendance);
                Console.WriteLine("YearWithHighestOverallGpa: {0}", studentAggregate.YearWithHighestOverallGpa);
                Console.Write("Top10StudentIdsWithHighestGpa: [");
                foreach (int studentID in studentAggregate.Top10StudentIdsWithHighestGpa)
                {
                    Console.Write(" " + studentID); 
                }
                Console.Write("]\n");               
                Console.WriteLine("StudentIdMostInconsistent: {0}", studentAggregate.StudentIdMostInconsistent);


                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~PUT~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                client.DefaultRequestHeaders.Accept.Clear();

                //Client format: JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Async call to server - returns respone code & updates data on the server 
                respone = await client.PutAsJsonAsync("api/StudentAggregate", studentAggregate);

                if (respone.IsSuccessStatusCode)
                {
                    Console.WriteLine("Successfully Updated");
                }
            }
            Console.ReadLine();
        }
    }
}
