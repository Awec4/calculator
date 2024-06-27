using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Utilities
{
    class MathUtils
    {
        // Highest Common Factor/Greatest Common Divisor - Euclid's Algorithm. Stein's algorithm is faster but no self-respecting senior dev wants to review bitshifts on a Thursday evening/Friday morning
        public static int HCF(int a, int b)
        {
            // Base cases: hcf(n, 0) = hcf(0, n) = n
            if (a == 0) {
                return b;
            }
            else if (b == 0) {
                return a;
            }

            int t; // temp
            while (b != 0) {
                t = b;
                b = a % b;
                a = t;
            }
            return a;
        }
    }
}
