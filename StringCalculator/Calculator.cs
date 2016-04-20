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
        public int Add(string numbers)
        {
            if (numbers == "")
                return 0;

            string[] delimiters = null;
            if (NewDelimitersGiven(numbers))
            {
                delimiters = GetNewDelimiters(numbers);

                var offset = 0;
                var sqBracketsLength = delimiters.Length > 1 ? 2 : (delimiters[0].Length > 1 ? 2 : 0);

                // Multiple delimiters
                foreach (var d in delimiters)
                {
                    offset += d.Length + sqBracketsLength;
                }

                numbers = numbers.Substring(3 + offset);
            }
            else
                delimiters = new string[] { ",", "\n" }; // Default delimiters

            var values = numbers.Split(delimiters, StringSplitOptions.None).Select(x => int.Parse(x));

            CheckForNegativeValues(values);

            values = FilterInvalidValues(values);

            return values.Sum();
        }

        private IEnumerable<int> FilterInvalidValues(IEnumerable<int> nums)
        {
            return nums.Where(x => x <= 1000);
        }

        private void CheckForNegativeValues(IEnumerable<int> nums)
        {
            var negativeNumbers = GetNegativeNumbers(nums);
            if (negativeNumbers.Count() > 0)
                throw new Exception("Negatives not allowed: " + string.Join(", ", negativeNumbers));
        }

        private bool NewDelimitersGiven(string numbers)
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
