using NUnit.Framework;
using System.Globalization;

namespace Time.Tests
{
    [TestFixture]
    public class CalendarDateTest
    {
        readonly CalendarDate baseDate = CalendarDate.From(2015, 07, 18);

        readonly CalendarDate dateSameAsBase = CalendarDate.From(2015, 07, 18);
        readonly CalendarDate dateDifferentDayFromBase = CalendarDate.From(2015, 07, 02);
        readonly CalendarDate dateDifferentMonthFromBase = CalendarDate.From(2015, 12, 18);
        readonly CalendarDate dateDifferentYearFromBase = CalendarDate.From(2012, 07, 18);

        readonly CalendarDate pastDate = CalendarDate.From(2000, 01, 01);
        readonly CalendarDate previousDay = CalendarDate.From(2015, 07, 17);
        readonly CalendarDate nextDay = CalendarDate.From(2015, 07, 19);
        readonly CalendarDate futureDate = CalendarDate.From(2020, 01, 01);

        [Test]
        public void FromWithpecificDate()
        {
            var date = CalendarDate.From(2015, 07, 18);
            Assert.That(date.Year, Is.EqualTo(2015));
            Assert.That(date.Month, Is.EqualTo(7));
            Assert.That(date.Day, Is.EqualTo(18));
        }

        [Test]
        public void ToStringTests()
        {
            var date = CalendarDate.From(2015, 07, 18);
            Assert.That(date.ToString(), Is.EqualTo("2015-07-18"));
        }

        [Test]
        public void GetHashCodeTests()
        {
            Assert.That(baseDate.GetHashCode(), Is.EqualTo(baseDate.GetHashCode()));
            Assert.That(baseDate.GetHashCode(), Is.EqualTo(dateSameAsBase.GetHashCode()));
            Assert.That(baseDate.GetHashCode(), Is.Not.EqualTo(dateDifferentDayFromBase.GetHashCode()));
            Assert.That(baseDate.GetHashCode(), Is.Not.EqualTo(dateDifferentMonthFromBase.GetHashCode()));
            Assert.That(baseDate.GetHashCode(), Is.Not.EqualTo(dateDifferentYearFromBase.GetHashCode()));
        }

        [Test]
        public void EqualsTests()
        {
            Assert.That(baseDate, Is.EqualTo(baseDate));
            Assert.That(baseDate, Is.EqualTo(dateSameAsBase));
            Assert.That(baseDate, Is.Not.EqualTo(dateDifferentDayFromBase));
            Assert.That(baseDate, Is.Not.EqualTo(dateDifferentMonthFromBase));
            Assert.That(baseDate, Is.Not.EqualTo(dateDifferentYearFromBase));
        }

        [Test]
        public void IsBeforeTest()
        {
            Assert.That(baseDate.IsBefore(pastDate), Is.False);
            Assert.That(baseDate.IsBefore(previousDay), Is.False);
            Assert.That(baseDate.IsBefore(baseDate), Is.False);
            Assert.That(baseDate.IsBefore(nextDay), Is.True);
            Assert.That(baseDate.IsBefore(futureDate), Is.True);
        }

        [Test]
        public void IsAfterTest()
        {
            Assert.That(baseDate.IsAfter(pastDate), Is.True);
            Assert.That(baseDate.IsAfter(previousDay), Is.True);
            Assert.That(baseDate.IsAfter(baseDate), Is.False);
            Assert.That(baseDate.IsAfter(nextDay), Is.False);
            Assert.That(baseDate.IsAfter(futureDate), Is.False);
        }

        [Test]
        public void CompareToTests()
        {
            Assert.That(baseDate.CompareTo(pastDate), Is.GreaterThan(0));
            Assert.That(baseDate.CompareTo(previousDay), Is.GreaterThan(0));
            Assert.That(baseDate.CompareTo(baseDate), Is.EqualTo(0));
            Assert.That(baseDate.CompareTo(nextDay), Is.LessThan(0));
            Assert.That(baseDate.CompareTo(futureDate), Is.LessThan(0));
        }
    }
}

