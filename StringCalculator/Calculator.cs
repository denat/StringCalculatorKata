using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator
    {
        private static string NewDelimiterNotation = "//{0}\n"; 

        public int Add(string numbers)
        {
            if (numbers == "")
                return 0;

            string[] delimiter = null;
            if (IsNewDelimiterSet(numbers)) {
                delimiter = GetNewDelimiter(numbers);
                numbers = numbers.Substring(string.Format(NewDelimiterNotation, delimiter[0]).Length);
            }
            else
                delimiter = new string[] { ",", "\n" };

            var nums = numbers.Split(delimiter, StringSplitOptions.None).Select(x => int.Parse(x));

            CheckForNegativeNumbers(nums);

            nums = FilterInvalidNumbers(nums);

            var result = 0;
            foreach (var num in nums)
            {
                result += num;
            }

            return result;
        }

        private IEnumerable<int> FilterInvalidNumbers(IEnumerable<int> nums)
        {
            return nums.Where(x => x <= 1000);
        }

        private void CheckForNegativeNumbers(IEnumerable<int> nums)
        {
            var negativeNumbers = GetNegativeNumbers(nums);
            if (negativeNumbers.Count() > 0)
                throw new Exception("Negatives not allowed: " + string.Join(", ", negativeNumbers));
        }

        private bool IsNewDelimiterSet(string numbers)
        {
            return numbers.StartsWith("//");
        }

        private string[] GetNewDelimiter(string numbers)
        {
            var delimiter = numbers.Substring(2, numbers.IndexOf('\n')-2);
            return new string[] { delimiter };
        }

        private IEnumerable<int> GetNegativeNumbers(IEnumerable<int> numbers)
        {
            return numbers.Where(x => x < 0);
        }
    }
}
