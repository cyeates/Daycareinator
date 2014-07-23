using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Assumes Monday is first day of the week.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            int diff = date.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }

            return date.AddDays(-1 * diff).Date;
        }
    }
}
