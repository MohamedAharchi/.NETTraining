using NFluent;
using NUnit.Framework;

namespace CSharpDiscovery
{
    using System.Linq;
    using System.Runtime.InteropServices;

    public class Calculator
    {
        public Calculator()
        {
            
        }

        public Calculator(string aName)
        {
            this.Name = aName;
        }

        public string Name { get; set; }

        public double Sum(double[] valuesToSum)
        {
            return valuesToSum.Sum();
        }

        public double Sum(string valeur)
        {
            string[] valuesToSum = valeur.Split('+');
            return valuesToSum.Sum(value => double.Parse(value));
        }
    }

    [TestFixture]
    public class ClassesTests
    {
        [Test]
        public void CreateACalculatorClassWithADefaultConstructorAndInstanciate()
        {
            Calculator calculator = new Calculator();
            Check.That(calculator).IsNotNull();
        }

        [Test]
        public void AddAnotherConstructorWithAFriendlyNameAndInstanciate()
        {
            // use a public member for Name for now, i.e public string Name;
            Calculator calculator = new Calculator("Calculator");
            Check.That(calculator.Name).Equals("Calculator");
        }

        [Test]
        public void AddAMethodThatMakeASumOfAnArrayOfDouble()
        {
            Calculator calculator = new Calculator();
            var valuesToSum = new[] { 1.3, 1.7 };
            // add a method Sum to calculator class
            double sumOfTheArray = calculator.Sum(valuesToSum);
            Check.That(sumOfTheArray).Equals(3.0);
        }

        [Test]
        public void AddAMethodOverloadThatMakeASumOfTwoDoubleFromStringRepresentation()
        {
            Calculator calculator = new Calculator();
            var sumOfTwoDoubleFromString = "1,0+2";
            // add a method with the same name that uses the previous method
            double onePlusTwo = calculator.Sum(sumOfTwoDoubleFromString);
            // tips : use string.Split
            Check.That(onePlusTwo).Equals(3.0);
        }

        [Test]
        public void AddAGetterForNameInsteadOfPublicNameMember()
        {
            Calculator calculator = new Calculator("Calculator");
            Check.That(calculator.Name).Equals("Calculator");
        }

        [Test]
        public void AddASetterForNameAndChangeNameWithIt()
        {
            Calculator calculator = new Calculator();
            calculator.Name = "Enhanced Calculator";
            Check.That(calculator.Name).Equals("Enhanced Calculator");
        }

        [Test]
        public void UseObjectInitilizerWithDefaultConstructor()
        {
            Calculator calculator = new Calculator { Name = "Calculator" };
            Check.That(calculator.Name).Equals("Calculator");
        }

        //[Test]
        //public void DefineConstantForPi()
        //{
        //    var sumOfADoubleAndPiConstant = "1,2 + pi";
        //    const double pi = 3.14;
        //    double sum = 4.34;
        //    // define pi constant (as double) and replace pi string with constant value
        //    Check.That(sum).Equals(4.34);
        //}

        //[Test]
        //public void StaticReadonlyMembers()
        //{
        //    var sumOfADoubleAndPiConstant = "1,2 + pi";
        //    // replace pi constant with a static readonly member
        //    Check.That(sum).Equals(4.34);
        //}

        //[Test]
        //public void MakeSumMethodsStaticAsTheyDoNotNeedAnyInstanceSpecific()
        //{
        //    // make sum methods static and call one these to retrieve a sum of double array values
        //    Check.That(sum).Equals(3.0);
        //}

        //[Test]
        //public void AddStaticCalculatorClass()
        //{
        //    // define a static class StaticCalculator that uses Calculator static methods & define static getter for Name
        //    Check.That(staticCalculator.Name).Equals("Static calculator");
        //}
    }
}
