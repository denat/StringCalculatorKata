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
            var calculator = new Calculator();

            var result = calculator.Add("");

            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void TestAddSingleNumber()
        {
            var calculator = new Calculator();

            var result = calculator.Add("1");
            Assert.IsTrue(result == 1);

            result = calculator.Add("2");
            Assert.IsTrue(result == 2);
        }

        [TestMethod]
        public void TestAddTwoNumbers()
        {
            var calculator = new Calculator();

            var result = calculator.Add("1,2");
            Assert.IsTrue(result == 3);

            result = calculator.Add("4,6");
            Assert.IsTrue(result == 10);
        }

        [TestMethod]
        public void TestAddTenNumbers()
        {
            var calculator = new Calculator();

            var result = calculator.Add("1,2,3,4,5,1,2,3,4,5");
            Assert.IsTrue(result == 30);

            result = calculator.Add("4,6,1,1,1,1,6,5,4,3");
            Assert.IsTrue(result == 32);
        }

        [TestMethod]
        public void TestAddTwoNumbersWithNewLines()
        {
            var calculator = new Calculator();

            var result = calculator.Add("1\n2,3");
            Assert.IsTrue(result == 6);
        }

        [TestMethod]
        public void TestAddTenNumbersWithNewLines()
        {
            var calculator = new Calculator();

            var result = calculator.Add("1\n2,3\n4\n5\n6,7\n8\n9\n10");
            Assert.IsTrue(result == 55);
        }

        [TestMethod]
        public void TestAddTwoNumbersWithNewDelimiter()
        {
            var calculator = new Calculator();

            var result = calculator.Add("//;\n5;6");
            Assert.IsTrue(result == 11);
        }

        [TestMethod]
        public void TestAddTenNumbersWithNewDelimiter()
        {
            var calculator = new Calculator();

            var result = calculator.Add("//;\n1;2;3;4;5;6;7;8;9;10");
            Assert.IsTrue(result == 55);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddTwoNumbersSingleNegativeNumber()
        {
            var calculator = new Calculator();

            calculator.Add("5,-1");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddTwoNumbersFiveNegativeNumbers()
        {
            var calculator = new Calculator();

            calculator.Add("//;\n1;-2;3;4;-5;6;-7;-8;9;-10");
        }
    }
}
