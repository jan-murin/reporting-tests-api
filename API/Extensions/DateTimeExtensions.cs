using System;

namespace Reporting.Tests.API.API.Extensions
{
    public static class DateTimeExtensions
    { 
        public static DateTime? StringToDateTimeNullableTransform(string day)
        {
            if (!string.IsNullOrEmpty(day))
            {
                var dayDelta = 0;
                var monthDelta = 0;
                if (day.ToLower().Contains("today"))
                {
                    ParseDayMonthDeltas(day, ref monthDelta, ref dayDelta);
                }

                return DateTime.Now.AddMonths(monthDelta).AddDays(dayDelta).Date;
            }
            return null;
        }

        private static void ParseDayMonthDeltas(string day, ref int monthDelta, ref int dayDelta)
        {
            if (day.Contains("+ThreeMonths"))
            {
                monthDelta = 3;
                day = day.Replace("+ThreeMonths", "");
            }
            if (day.Contains("-ThreeMonths"))
            {
                monthDelta = -3;
                day = day.Replace("-ThreeMonths", "");
            }
            if (day.Contains("+SixMonths"))
            {
                monthDelta = 6;
                day = day.Replace("+SixMonths", "");
            }
            if (day.Contains("-SixMonths"))
            {
                monthDelta = -6;
                day = day.Replace("-SixMonths", "");
            }
            
            if (day.Contains("+"))
            {
                var substring = day.Substring(day.LastIndexOf('+'));
                dayDelta = int.Parse(substring);
            }
            else if (day.Contains("-"))
            {
                var substring = day.Substring(day.LastIndexOf('-'));
                dayDelta = int.Parse(substring);
            }
        }
        
        public static DateTime ConvertDateFrom(this DateTime dateFrom)
        {
            var date = dateFrom.Date;
            var timeSpan = new TimeSpan(0, 0, 0);
            var convertedDatetime = date + timeSpan;
            return convertedDatetime;
        }

        public static DateTime? ConvertDateFromNullable(this DateTime? dateFrom)
        {
            DateTime? convertedDate = null;
            if (dateFrom != null)
            {
                var date = dateFrom.Value.Date;
                var timeSpan = new TimeSpan(0, 0, 0);
                var convertedDatetime = date + timeSpan;
                convertedDate = convertedDatetime;
            }

            return convertedDate;
        }

        public static DateTime ConvertDateTo(this DateTime dateTo)
        {
            var date = dateTo.Date;
            var timeSpan = new TimeSpan(23, 59, 59);
            var convertedDatetime = date + timeSpan;
            return convertedDatetime;
        }

        public static DateTime? ConvertDateToNullable(this DateTime? dateTo)
        {
            DateTime? convertedDate = null;
            if (dateTo != null)
            {
                var date = dateTo.Value.Date;
                var timeSpan = new TimeSpan(23, 59, 59);
                var convertedDatetime = date + timeSpan;
                convertedDate = convertedDatetime;
            }

            return convertedDate;
        }
    }
}
