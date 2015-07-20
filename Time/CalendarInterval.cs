using Time;

namespace Time
{
    public class CalendarInterval : Interval<CalendarDate>
    {
        public static CalendarInterval Inclusive(CalendarDate start, CalendarDate end)
        {
            return new CalendarInterval(
                Interval<CalendarDate>.Closed(start, end)
            );
        }

        public static CalendarInterval Exclusive(CalendarDate start, CalendarDate end)
        {
            return new CalendarInterval(
                Interval<CalendarDate>.Open(start, end)
            );
        }

        private CalendarInterval(Interval<CalendarDate> other) : base(other) { }
    }

    public static class CalendarInterval_CalendarDate_Extensions
    {
        public static CalendarInterval To(this CalendarDate obj, CalendarDate toDate)
        {
            return CalendarInterval.Inclusive(obj, toDate);
        }
    }
}

