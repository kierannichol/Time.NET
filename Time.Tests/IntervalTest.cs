using NUnit.Framework;

namespace Time.Tests
{
    [TestFixture]
    public class IntervalTest
    {
        [Test]
        public void IsEmptyTests()
        {
            var emptyInterval = Interval<int>.Open(1,1);
            var smallOpenInterval = Interval<int>.Open(1,2);
            var singleValueInterval = Interval<int>.Closed(1,1);

            Assert.That(emptyInterval.IsEmpty, Is.True);
            Assert.That(smallOpenInterval.IsEmpty, Is.False);
            Assert.That(singleValueInterval.IsEmpty, Is.False);
        }

        [Test]
        public void IsDegenerateTests()
        {
            var emptyInterval = Interval<int>.Open(1,1);
            var smallOpenInterval = Interval<int>.Open(1,2);
            var singleValueInterval = Interval<int>.Closed(1,1);

            Assert.That(emptyInterval.IsDegenerate, Is.False);
            Assert.That(smallOpenInterval.IsDegenerate, Is.False);
            Assert.That(singleValueInterval.IsDegenerate, Is.True);
        }

        [Test]
        public void ToStringTests()
        {
            var emptyInterval = Interval<int>.Open(0,0);
            var openInterval = Interval<int>.Open(1, 10);
            var closedInterval = Interval<int>.Closed(2, 5);
            var leftClosedRightOpen = Interval<int>.LeftClosedRightOpen(1, 2);
            var leftOpenRightClosed = Interval<int>.LeftOpenRightClosed(3, 5);
            var leftBoundedRightUnbounded = Interval<int>.LeftClosed(1);
            var leftUnboundedRightBounded = Interval<int>.RightOpen(5);

            Assert.That(emptyInterval.ToString(), Is.EqualTo("{}"));
            Assert.That(openInterval.ToString(), Is.EqualTo("(1,10)"));
            Assert.That(closedInterval.ToString(), Is.EqualTo("[2,5]"));
            Assert.That(leftClosedRightOpen.ToString(), Is.EqualTo("[1,2)"));
            Assert.That(leftOpenRightClosed.ToString(), Is.EqualTo("(3,5]"));

            Assert.That(leftBoundedRightUnbounded.ToString(), Is.EqualTo("[1,∞)"));
            Assert.That(leftUnboundedRightBounded.ToString(), Is.EqualTo("(-∞,5)"));
        }

        [Test]
        public void EqualsTests()
        {
            var emptyInterval1 = Interval<int>.Open(0,0);
            var emptyInterval2 = Interval<int>.Open(10,10);

            var openInterval1 = Interval<int>.Open(5, 10);
            var openInterval2 = Interval<int>.Open(5, 10);

            var closedInterval1 = Interval<int>.Closed(5, 10);
            var closedInterval2 = Interval<int>.Closed(5, 10);

            Assert.That(emptyInterval1, Is.EqualTo(emptyInterval2));

            Assert.That(openInterval1, Is.EqualTo(openInterval2));

            Assert.That(closedInterval1, Is.EqualTo(closedInterval2));
        }

        [Test]
        public void ContainsTests()
        {
            var openInterval = Interval<int>.Open(2,5);
            var closedInterval = Interval<int>.Closed(2,5);
            var infiniteInterval = Interval<int>.Unbound;
            var emptyInterval = Interval<int>.Open(5,5);
            var leftOpenRightClosed = Interval<int>.LeftOpenRightClosed(3, 5);

            var leftBoundedRightUnbounded = Interval<int>.LeftClosed(1);

            Assert.That(openInterval.Contains(1), Is.False);
            Assert.That(openInterval.Contains(2), Is.False);
            Assert.That(openInterval.Contains(3), Is.True);
            Assert.That(openInterval.Contains(4), Is.True);
            Assert.That(openInterval.Contains(5), Is.False);
            Assert.That(openInterval.Contains(6), Is.False);

            Assert.That(closedInterval.Contains(1), Is.False);
            Assert.That(closedInterval.Contains(2), Is.True);
            Assert.That(closedInterval.Contains(3), Is.True);
            Assert.That(closedInterval.Contains(4), Is.True);
            Assert.That(closedInterval.Contains(5), Is.True);
            Assert.That(closedInterval.Contains(6), Is.False);

            Assert.That(infiniteInterval.Contains(int.MinValue), Is.True);
            Assert.That(infiniteInterval.Contains(0), Is.True);
            Assert.That(infiniteInterval.Contains(int.MaxValue), Is.True);

            Assert.That(emptyInterval.Contains(4), Is.False);
            Assert.That(emptyInterval.Contains(5), Is.False);
            Assert.That(emptyInterval.Contains(6), Is.False);

            Assert.That(leftOpenRightClosed.Contains(2), Is.False);
            Assert.That(leftOpenRightClosed.Contains(3), Is.False);
            Assert.That(leftOpenRightClosed.Contains(4), Is.True);
            Assert.That(leftOpenRightClosed.Contains(5), Is.True);
            Assert.That(leftOpenRightClosed.Contains(6), Is.False);

            Assert.That(leftBoundedRightUnbounded.Contains(0), Is.False);
            Assert.That(leftBoundedRightUnbounded.Contains(1), Is.True);
            Assert.That(leftBoundedRightUnbounded.Contains(int.MaxValue), Is.True);
        }
    }
}

