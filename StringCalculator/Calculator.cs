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
            if (numbers.StartsWith("//")) {
                var delimiter = numbers.Substring(2, 1)[0];
                delimiters = new char[] { delimiter };
                numbers = numbers.Substring(4);
            } else
                delimiters = new char[] { ',', '\n' };

            var nums = numbers.Split(delimiters).Select(x => int.Parse(x));
            var result = 0;

            foreach (var num in nums)
            {
                result += num;
            }

            return result;
        }
    }
}
