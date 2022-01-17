using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMMIPL.Models
{
    public class Comman
    {

       
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            DateTime Dt;
            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];
                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }
            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }
    }
}