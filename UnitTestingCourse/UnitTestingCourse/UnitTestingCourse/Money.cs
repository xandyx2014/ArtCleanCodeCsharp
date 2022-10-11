using System;
using System.Collections.Generic;

namespace Business
{
    public struct Money : IEqualityComparer<Money>, IComparable<Money>
    {
        private const int KopecFactor = 100;
        private readonly decimal _amountInRubles;

        private Money(decimal amountInRub)
        {
            _amountInRubles = Decimal.Round(amountInRub, 2);
        }

        private Money(long amountInKopecs)
        {
            _amountInRubles = (decimal) amountInKopecs / KopecFactor;
        }

        public static Money FromKopecs(long amountInKopecs)
        {
            return new Money(amountInKopecs);
        }

        public static Money FromRubles(decimal amountInRubles)
        {
            return new Money(amountInRubles);
        }

        public decimal AmountInRubles
        {
            get { return _amountInRubles; }
        }

        public long AmountInKopecs
        {
            get { return (int) (_amountInRubles * KopecFactor); }
        }

        public int CompareTo(Money other)
        {
            if (_amountInRubles < other._amountInRubles) return -1;
            if (_amountInRubles == other._amountInRubles) return 0;
            else return 1;
        }

        public bool Equals(Money x, Money y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Money obj)
        {
            return obj.GetHashCode();
        }

        public Money Add(Money other)
        {
            return new Money(_amountInRubles + other._amountInRubles);
        }

        public Money Subtract(Money other)
        {
            return new Money(_amountInRubles - other._amountInRubles);
        }

        public static Money operator +(Money m1, Money m2)
        {
            return m1.Add(m2);
        }

        public static Money operator -(Money m1, Money m2)
        {
            return m1.Subtract(m2);
        }

        public static Money operator *(Money m1, Money m2)
        {
            return new Money(m1._amountInRubles * m2._amountInRubles);
        }

        public static Money operator /(Money m1, Money m2)
        {
            return new Money(m1._amountInRubles / m2._amountInRubles);
        }

        public static bool operator ==(Money m1, Money m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(Money m1, Money m2)
        {
            return !m1.Equals(m2);
        }

        public static bool operator >(Money m1, Money m2)
        {
            return m1._amountInRubles > m2._amountInRubles;
        }

        public static bool operator >=(Money m1, Money m2)
        {
            return m1._amountInRubles >= m2._amountInRubles;
        }

        public static bool operator <(Money m1, Money m2)
        {
            return m1._amountInRubles < m2._amountInRubles;
        }

        public static bool operator <=(Money m1, Money m2)
        {
            return m1._amountInRubles <= m2._amountInRubles;
        }

        public override bool Equals(object other)
        {
            return (other is Money) && Equals((Money) other);
        }

        public bool Equals(Money other)
        {
            return _amountInRubles == other._amountInRubles;
        }

        public override int GetHashCode()
        {
            return (int) (AmountInKopecs ^ (AmountInKopecs >> 32));
        }
    }
}