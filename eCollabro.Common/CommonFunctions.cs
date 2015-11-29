using System;

namespace eCollabro.Common
{
    public static class CommonFunctions
    {
        public static string GetTimeInterval(DateTime date)
        {
            TimeSpan timeSpan= DateTime.UtcNow - date;
            string timeInterval=string.Empty;
            if(timeSpan.Days>365) //year and months
            {
                timeInterval="more than year ago";
            }
            else if(timeSpan.Days>30) 
            {
                timeInterval=Convert.ToInt32((timeSpan.Days/30)).ToString()+ " month(s) ago";
            }
            else if(timeSpan.Days>0)
            {
                timeInterval=(timeSpan.Days).ToString()+ " day(s) ago";
            }
            else if(timeSpan.Hours>0)
            {
                timeInterval=(timeSpan.Hours).ToString()+ " hour(s) ago";
            }
            else
            {
                timeInterval=(timeSpan.Minutes).ToString()+ " minute(s) ago";
            }
            return timeInterval;
        }
    }
}
