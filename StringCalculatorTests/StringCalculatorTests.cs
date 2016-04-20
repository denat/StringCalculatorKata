using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator;

namespace StringCalculatorTests
{
    [TestClass]
    public class StringCalculatorTests
    {
        [TestMethod]
        public void TestAddEmptyString()
        {
            var result = Add("");

            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void TestAddSingleNumber()
        {
            var result = Add("1");
            Assert.IsTrue(result == 1);

            result = Add("2");
            Assert.IsTrue(result == 2);
        }

        [TestMethod]
        public void TestAddTwoNumbers()
        {
            var result = Add("1,2");
            Assert.IsTrue(result == 3);

            result = Add("4,6");
            Assert.IsTrue(result == 10);
        }

        [TestMethod]
        public void TestAddTenNumbers()
        {
            var result = Add("1,2,3,4,5,1,2,3,4,5");
            Assert.IsTrue(result == 30);

            result = Add("4,6,1,1,1,1,6,5,4,3");
            Assert.IsTrue(result == 32);
        }

        [TestMethod]
        public void TestAddTwoNumbersWithNewLines()
        {
            var result = Add("1\n2,3");

            Assert.IsTrue(result == 6);
        }

        [TestMethod]
        public void TestAddTenNumbersWithNewLines()
        {
            var result = Add("1\n2,3\n4\n5\n6,7\n8\n9\n10");

            Assert.IsTrue(result == 55);
        }

        [TestMethod]
        public void TestAddTwoNumbersWithNewDelimiter()
        {
            var result = Add("//;\n5;6");

            Assert.IsTrue(result == 11);
        }

        [TestMethod]
        public void TestAddTenNumbersWithNewDelimiter()
        {
            var result = Add("//;\n1;2;3;4;5;6;7;8;9;10");

            Assert.IsTrue(result == 55);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddTwoNumbersSingleNegativeNumber()
        {
            Add("5,-1");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddTenNumbersFiveNegativeNumbers()
        {
            Add("//;\n1;-2;3;4;-5;6;-7;-8;9;-10");
        }

        [TestMethod]
        public void TestIgnoreNumbersOver1000()
        {
            var result = Add("5,1001");
            Assert.IsTrue(result == 5);

            result = Add("2,2000,5,1001");
            Assert.IsTrue(result == 7);

            result = Add("1000,1000,1001");
            Assert.IsTrue(result == 2000);
        }

        [TestMethod]
        public void TestDelimitersOfAnyLength()
        {
            var result = Add("//[***]\n1***2***3");
            Assert.IsTrue(result == 6);

            result = Add("//[^%$^%%]\n5^%$^%%6^%$^%%7");
            Assert.IsTrue(result == 18);
        }

        [TestMethod]
        public void TestMultipleNewDelimiters()
        {
            var result = Add("//[%][#]\n1#2%3");

            Assert.IsTrue(result == 6);
        }

        [TestMethod]
        public void TestMultipleNewDelimitersOfAnySize()
        {
            var result = Add("//[%%%][###]\n1###2%%%3");

            Assert.IsTrue(result == 6);
        }

        private int Add(string input) {
            var calculator = new Calculator();

            return calculator.Add(input);
        }
    }
}
