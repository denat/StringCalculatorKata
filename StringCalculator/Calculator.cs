using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator
    {
        private static string NewDelimiterNotation = "//{0}\n";
        private static string NewLargeDelimiterNotation = "//[{0}]\n";

        public int Add(string numbers)
        {
            if (numbers == "")
                return 0;

            string[] delimiters = null;
            if (AreNewDelimitersSet(numbers))
            {
                delimiters = GetNewDelimiters(numbers);

                var offset = 0;
                var extras = delimiters.Length > 1 ? 2 : (delimiters[0].Length > 1 ? 2 : 0);

                // Multiple delimiters
                foreach (var d in delimiters)
                {
                    offset += d.Length + extras;
                }

                numbers = numbers.Substring(3 + offset);
            }
            else
                delimiters = new string[] { ",", "\n" }; // Default delimiters

            var nums = numbers.Split(delimiters, StringSplitOptions.None).Select(x => int.Parse(x));

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

        private bool AreNewDelimitersSet(string numbers)
        {
            return numbers.StartsWith("//");
        }

        private string[] GetNewDelimiters(string numbers)
        {
            var match = Regex.Match(numbers, @"//(\[[^\[\]]+\])+\n");

            if (match.Success)
            {
                var delimiters = new List<string>();

                foreach (Capture c in match.Groups[1].Captures)
                {
                    delimiters.Add(c.Value.Substring(1, c.Value.IndexOf(']') - 1));
                }

                return delimiters.ToArray();
            }
            else
            {
                var delimiter = numbers.Substring(2, numbers.IndexOf('\n') - 2);
                return new string[] { delimiter };
            }
        }

        private IEnumerable<int> GetNegativeNumbers(IEnumerable<int> numbers)
        {
            return numbers.Where(x => x < 0);
        }
    }
}
