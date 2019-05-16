using System;

namespace API.Services
{
    internal static class ConvertionExtensions
    {
        public static DateTime ParseDate(this string date)
        {
            var year = date.Substring(0, 4);
            var month = date.Substring(4, 2);
            var day = date.Substring(6, 2);
            var hour = date.Substring(8, 2);
            var minute = date.Substring(10, 2);
            var second = date.Substring(12, 2);
            var offset = Convert.ToInt32(date.Substring(15, 3));

            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            var timeOffset = easternZone.BaseUtcOffset.Add(TimeSpan.FromDays(offset));
            

            return DateTime.Parse($"{year}-{month}-{day} {hour}:{minute}:{second}").Add(timeOffset);
        }
    }
}