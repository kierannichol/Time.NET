using System;
using NUnit.Framework;

namespace Time.Tests
{
    [TestFixture]
    public class CalendarIntervalTest
    {
        [Test]
        public void InclusiveTests()
        {
            var interval = CalendarInterval.Inclusive(
                CalendarDate.From(2015, 01, 01),
                CalendarDate.From(2015, 01, 31)
            );

            Assert.That(interval.Contains(CalendarDate.From(2014, 12, 31)), Is.False);
            Assert.That(interval.Contains(CalendarDate.From(2015, 01, 01)), Is.True);
            Assert.That(interval.Contains(CalendarDate.From(2015, 01, 02)), Is.True);

            Assert.That(interval.Contains(CalendarDate.From(2015, 01, 30)), Is.True);
            Assert.That(interval.Contains(CalendarDate.From(2015, 01, 31)), Is.True);
            Assert.That(interval.Contains(CalendarDate.From(2015, 02, 01)), Is.False);
        }

        [Test]
        public void ExclusiveTests()
        {
            var interval = CalendarInterval.Exclusive(
                CalendarDate.From(2015, 01, 01),
                CalendarDate.From(2015, 01, 31)
            );

            Assert.That(interval.Contains(CalendarDate.From(2014, 12, 31)), Is.False);
            Assert.That(interval.Contains(CalendarDate.From(2015, 01, 01)), Is.False);
            Assert.That(interval.Contains(CalendarDate.From(2015, 01, 02)), Is.True);

            Assert.That(interval.Contains(CalendarDate.From(2015, 01, 30)), Is.True);
            Assert.That(interval.Contains(CalendarDate.From(2015, 01, 31)), Is.False);
            Assert.That(interval.Contains(CalendarDate.From(2015, 02, 01)), Is.False);
        }

        [Test]
        public void ToExensionMethodTests()
        {
            var startDate = CalendarDate.From(2015, 01, 01);
            var endDate = CalendarDate.From(2015, 01, 15);
            var resultInterval = startDate.To(endDate);
            var expectedInterval = CalendarInterval.Inclusive(startDate, endDate);

            Assert.That(resultInterval, Is.EqualTo(expectedInterval));
        }
    }
}

