using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            if (numbers == "")
                return 0;

            char[] delimiters = null;
            if (IsNewDelimiterSet(numbers)) {
                delimiters = GetNewDelimiter(numbers);
                numbers = numbers.Substring(4);
            }
            else
                delimiters = new char[] { ',', '\n' };

            var nums = numbers.Split(delimiters).Select(x => int.Parse(x));

            var negativeNumbers = GetNegativeNumbers(nums);
            if (negativeNumbers.Count() > 0)
                throw new Exception("Negatives not allowed: " + string.Join(", ", negativeNumbers));

            var result = 0;

            foreach (var num in nums)
            {
                result += num;
            }

            return result;
        }

        private bool IsNewDelimiterSet(string numbers)
        {
            return numbers.StartsWith("//");
        }

        private char[] GetNewDelimiter(string numbers)
        {
            var delimiter = numbers.Substring(2, 1)[0];
            return new char[] { delimiter };
        }

        private IEnumerable<int> GetNegativeNumbers(IEnumerable<int> numbers)
        {
            return numbers.Where(x => x < 0);
        }
    }
}
