using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tff.ProjectEuler.CS
{
    public class Question1
    {
        //If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9.
        //The sum of these multiples is 23.
        //Find the sum of all the multiples of 3 or 5 below 1000.
        public static Int32 TryOne()
        {
            List<Int32> selectedNumbers = new List<int>();
            for (int number = 0; number < 1000; number++)
            {
                if (number % 3 == 0)
                {
                    selectedNumbers.Add(number);
                }

                if (number % 5 == 0)
                {
                    if (!selectedNumbers.Contains(number))
                    {
                        selectedNumbers.Add(number);
                    }
                }
            }

            Int32 total = 0;
            foreach (Int32 number in selectedNumbers)
            {
                total += number;
            }

            return total;
        }

        public static Int32 TryTwo()
        {
            var total = Enumerable.Range(0, 1000)
                                .Where(number => (number % 3 == 0) || (number % 5 == 0))
                                .Sum();
            return total;
        }
    }
}
