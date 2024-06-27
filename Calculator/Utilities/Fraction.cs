using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Utilities
{
    class Fraction
    {

        #region fields
        public int Numerator { get; }

        public int Denominator { get; }
        #endregion


        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be 0");
            }
            Numerator = numerator;
            Denominator = denominator;
        }

        public static Fraction Simplify(Fraction fr)
        {
            int hcf = MathUtils.HCF(fr.Numerator, fr.Denominator);
            if (hcf != 1)
            {
                return new Fraction(fr.Numerator / hcf, fr.Denominator / hcf);
            }
            return fr;
        }

        #region overrides
        public static Fraction operator +(Fraction fr) => fr;
        public static Fraction operator -(Fraction fr) => new Fraction(-fr.Numerator, fr.Denominator);

        public static Fraction operator +(Fraction a, Fraction b)
            => new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);

        public static Fraction operator -(Fraction a, Fraction b)
            => a + (-b);

        public static Fraction operator *(Fraction a, Fraction b)
            => new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Numerator == 0)
            {
                throw new DivideByZeroException();
            }
            return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        public static Fraction operator ^(Fraction a, int b)
            => new Fraction(a.Numerator ^ b, a.Denominator ^ b);

        public static Fraction TryParse(string input)
        {
            string[] parts = input.Split('/');
            if (parts.Length != 2)
            {
                throw new ArgumentException("input has more than 1 divisor");
            }

            int.TryParse(parts[0], out int num);
            int.TryParse(parts[1], out int den);
            return Simplify(new Fraction(num, den));
        }

        public override string ToString()
        {
            if (Numerator == 0)
            {
                return "0";
            }
            return $"{Numerator}/{Denominator}";
        }

        #endregion
    }
}
