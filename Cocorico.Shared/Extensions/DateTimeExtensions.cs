using Cocorico.Shared.Exceptions;
using System;

namespace Cocorico.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateDifference DateDifferenceTo(this DateTime from, DateTime to)
        {
            var difference = from.CompareTo(to);

            if (difference < 0) return DateDifference.Sooner;
            if (difference == 0) return DateDifference.AtTheSameTime;
            if (difference >= 0) return DateDifference.Later;

            throw new UnexpectedException();
        }
    }
}
