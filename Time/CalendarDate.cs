using System;
using System.Runtime.Serialization;

namespace Time
{
    public class CalendarDate : IComparable<CalendarDate>, IEquatable<CalendarDate>
    {
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }

        public static CalendarDate From(int year, int month, int day)
        {
            return new CalendarDate(year, month, day);
        }

        public bool IsBefore(CalendarDate date)
        {
            return this.Timestamp < date.Timestamp;
        }

        public bool IsAfter(CalendarDate date)
        {
            return this.Timestamp > date.Timestamp;
        }

        public int CompareTo(CalendarDate other)
        {
            return (int)((this.Timestamp - other.Timestamp) % int.MaxValue);
        }

        public override string ToString()
        {
            return string.Format("{0}-{1:D2}-{2:D2}", Year, Month, Day);
        }

        public override bool Equals(object obj)
        {
            var other = obj as CalendarDate;
            return other != null && Equals(other);
        }

        public bool Equals(CalendarDate other)
        {
            return 
                this.Year == other.Year 
                && this.Month == other.Month 
                && this.Day == other.Day;
        }

        public override int GetHashCode()
        {
            int hash = 53;
            hash += Year.GetHashCode();
            hash += Month.GetHashCode();
            hash += Day.GetHashCode();
            return hash;
        }

        private CalendarDate(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        internal long Timestamp
        {
            get
            {
                return Day*60*60*24 + Month*60*60*24*31 + 
                    Year*60*60*24*31*12;
            }
        }
    }
}

