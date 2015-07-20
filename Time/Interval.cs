using System;

namespace Time
{
    public class Interval<T> where T : IComparable<T>
    {
        private readonly Limit left;
        private readonly Limit right;

        public bool IsEmpty
        {
            get
            {
                var rightOpen = right as RightOpenLimit;
                var leftOpen = left as LeftOpenLimit;
                if (rightOpen == null || leftOpen == null) return false;

                return leftOpen.Value.Equals(rightOpen.Value);
            }
        }

        public bool IsDegenerate
        {
            get
            {
                var rightClosed = right as RightClosedLimit;
                var leftClosed = left as LeftClosedLimit;
                if (rightClosed == null || leftClosed == null) return false;

                return leftClosed.Value.Equals(rightClosed.Value);
            }
        }

        public static Interval<T> Unbound
        {
            get
            {
                return new Interval<T>(
                    new LeftUnboundedLimit(),
                    new RightUnboundedLimit()
                );
            }
        }

        public static Interval<T> Open(T left, T right)
        {
            return new Interval<T>(
                new LeftOpenLimit(left),
                new RightOpenLimit(right)
            );
        }

        public static Interval<T> Closed(T left, T right)
        {
            return new Interval<T>(
                new LeftClosedLimit(left),
                new RightClosedLimit(right)
            );
        }

        public static Interval<T> LeftOpen(T left)
        {
            return new Interval<T>(
                new LeftOpenLimit(left),
                new RightUnboundedLimit()
            );
        }

        public static Interval<T> LeftClosed(T left)
        {
            return new Interval<T>(
                new LeftClosedLimit(left),
                new RightUnboundedLimit()
            );
        }

        public static Interval<T> RightOpen(T right)
        {
            return new Interval<T>(
                new LeftUnboundedLimit(),
                new RightOpenLimit(right)
            );
        }

        public static Interval<T> RightClosed(T right)
        {
            return new Interval<T>(
                new LeftUnboundedLimit(),
                new RightClosedLimit(right)
            );
        }

        public static Interval<T> LeftOpenRightClosed(T left, T right)
        {
            return new Interval<T>(
                new LeftOpenLimit(left),
                new RightClosedLimit(right)
            );
        }

        public static Interval<T> LeftClosedRightOpen(T left, T right)
        {
            return new Interval<T>(
                new LeftClosedLimit(left),
                new RightOpenLimit(right)
            );
        }

        public bool Contains(T value)
        {
            return left.ValueInBounds(value) && right.ValueInBounds(value);
        }

        public override string ToString()
        {
            return IsEmpty ? "{}" : string.Format("{0},{1}", left.AsString(), right.AsString());
        }

        public override bool Equals(object obj)
        {
            var other = obj as Interval<T>;
            if (other == null) return false;
            if (IsEmpty && other.IsEmpty) return true;

            return left.Equals(other.left) && right.Equals(other.right);
        }

        public override int GetHashCode()
        {
            var hash = base.GetHashCode();
            hash += left.GetHashCode();
            hash += right.GetHashCode();
            return hash;
        }

        protected Interval(Interval<T> other)
        {
            left = other.left;
            right = other.right;
        }

        private Interval(Limit left, Limit right)
        {
            this.left = left;
            this.right = right;
        }

        protected abstract class Limit
        {
            public abstract bool ValueInBounds(T value);
            public abstract string AsString();
        }

        protected class LeftOpenLimit : Limit
        {
            public readonly T Value;

            public LeftOpenLimit(T value)
            {
                Value = value;
            }

            public override bool ValueInBounds(T value)
            {
                return Value.CompareTo(value) < 0;
            }

            public override string AsString()
            {
                return string.Format("({0}", Value);
            }

            public override bool Equals(object obj)
            {
                var other = obj as LeftOpenLimit;
                if (other == null) return false;
                return Value.CompareTo(other.Value) == 0;
            }

            public override int GetHashCode()
            {
                var hash = base.GetHashCode();
                hash += Value.GetHashCode();
                return hash;
            }
        }

        protected class RightOpenLimit : Limit
        {
            public readonly T Value;

            public RightOpenLimit(T value)
            {
                this.Value = value;
            }

            public override bool ValueInBounds(T value)
            {
                return this.Value.CompareTo(value) > 0;
            }

            public override string AsString()
            {
                return string.Format("{0})", Value);
            }

            public override bool Equals(object obj)
            {
                var other = obj as RightOpenLimit;
                if (other == null) return false;
                return Value.CompareTo(other.Value) == 0;
            }

            public override int GetHashCode()
            {
                var hash = base.GetHashCode();
                hash += Value.GetHashCode();
                return hash;
            }
        }

        protected class LeftClosedLimit : Limit
        {
            public readonly T Value;

            public LeftClosedLimit(T value)
            {
                this.Value = value;
            }

            public override bool ValueInBounds(T value)
            {
                return this.Value.CompareTo(value) <= 0;
            }

            public override string AsString()
            {
                return string.Format("[{0}", Value);
            }

            public override bool Equals(object obj)
            {
                var other = obj as LeftClosedLimit;
                if (other == null) return false;
                return Value.CompareTo(other.Value) == 0;
            }

            public override int GetHashCode()
            {
                var hash = base.GetHashCode();
                hash += Value.GetHashCode();
                return hash;
            }
        }

        protected class RightClosedLimit : Limit
        {
            public readonly T Value;

            public RightClosedLimit(T value)
            {
                this.Value = value;
            }

            public override bool ValueInBounds(T value)
            {
                return this.Value.CompareTo(value) >= 0;
            }

            public override string AsString()
            {
                return string.Format("{0}]", Value);
            }

            public override bool Equals(object obj)
            {
                var other = obj as RightClosedLimit;
                if (other == null) return false;
                return Value.CompareTo(other.Value) == 0;
            }

            public override int GetHashCode()
            {
                var hash = base.GetHashCode();
                hash += Value.GetHashCode();
                return hash;
            }
        }
            
        protected class RightUnboundedLimit : Limit
        {
            const string InfinitySymbol = "∞";

            public override bool ValueInBounds(T value)
            {
                return true;
            }

            public override string AsString()
            {
                return InfinitySymbol + ")";
            }

            public override bool Equals(object obj)
            {
                var other = obj as RightUnboundedLimit;
                return other != null;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        protected class LeftUnboundedLimit : Limit
        {
            const string InfinitySymbol = "∞";

            public override bool ValueInBounds(T value)
            {
                return true;
            }

            public override string AsString()
            {
                return "(-" + InfinitySymbol;
            }

            public override bool Equals(object obj)
            {
                var other = obj as LeftUnboundedLimit;
                return other != null;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
    }
}

