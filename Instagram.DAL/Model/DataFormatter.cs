using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.DAL.Model
{
    public class DataFormatter
    {
        protected String GetDate(string createdTime)
        {
            var PubDate = UnixTimeStampToDateTime(Convert.ToDouble(createdTime));
            var Subtract = DateTime.Now.Subtract(UnixTimeStampToDateTime(Convert.ToDouble(createdTime)));

            string timeAgo;
            if (Subtract.Days == 0)
            {
                if (Subtract.Hours > 0)
                {
                    switch (Subtract.Hours)
                    {
                        case 1:
                            timeAgo = "hour";
                            break;
                        default:
                            timeAgo = "hours";
                            break;
                    }

                    return String.Format("{0} {1} ago", Subtract.Hours, timeAgo);
                }
                if (Subtract.Minutes > 0)
                {
                    switch (Subtract.Minutes)
                    {
                        case 1:
                            timeAgo = "minute";
                            break;
                        default:
                            timeAgo = "minutes";
                            break;
                    }

                    return String.Format("{0} {1} ago", Subtract.Minutes, timeAgo);
                }

                return String.Format("{0} sec ago", Subtract.Seconds);
            }

            return String.Format("{0} days", Subtract.Days);
            //return String.Format("{0} {1} {2}", PubDate.Day, month[PubDate.Month - 1], PubDate.Year, Subtract.Days);
        }

        private string[] month = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
