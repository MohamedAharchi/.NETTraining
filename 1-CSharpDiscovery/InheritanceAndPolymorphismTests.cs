namespace CSharpDiscovery
{
    using System;
    using System.Linq;
    using NFluent;
    using NUnit.Framework;

    public sealed class StringCalculator:Calculator
    {
        public StringCalculator()
        {

        }

        public double Sum(string valeur)
        {
            string[] valuesToSum = valeur.Split('+');
            return valuesToSum.Sum(value => double.Parse(value));
        }
    }

    public class IntegerCalculator : Calculator
    {
        public IntegerCalculator()
        {
            
        }

        public new double Sum(double[] valuesToSum)
        {
            int i = 0;
            foreach (var value in valuesToSum)
            {
                valuesToSum[i] = (int) value;
                i++;
            }
            return base.Sum(valuesToSum);
        }
    }

    public class AnotherIntegerCalculator : Calculator
    {
        public AnotherIntegerCalculator()
        {

        }

        
        public override double Sum(double[] valuesToSum)
        {
            int i = 0;
            foreach (var value in valuesToSum)
            {
                valuesToSum[i] = (int)value;
                i++;
            }
            return base.Sum(valuesToSum);
        }
    }

    public abstract class AbstractStringCalculator
    {
        public abstract double Calculate(string valeur);
    }

    public class ProductStringCalculator : AbstractStringCalculator
    {
        public ProductStringCalculator()
        {

        }

        public override double Calculate(string valeur)
        {
            string[] valuesToProduct = valeur.Split('*');
            return valuesToProduct.Aggregate(1.0, (u1, next) => u1 * double.Parse(next));
        }
    }

    public class SumAbstractStringCalculator : AbstractStringCalculator
    {
        public SumAbstractStringCalculator()
        {

        }

        public override double Calculate(string valeur)
        {
            string[] valuesToSum = valeur.Split('+');
            return valuesToSum.Sum(value => double.Parse(value));
        }
    }

    public class ComposedStringCalculator
    {
        private IComputeStrategy[] computeStrategy;

        public ComposedStringCalculator(IComputeStrategy[] computeStrategy)
        {
            this.computeStrategy = computeStrategy;
        }

        public double Calculate(string operation)
        {
            foreach (var strategy in this.computeStrategy)
            {
                if (strategy.CanCalculate(operation))
                {
                    return strategy.Calculate(operation);
                }
            }
            return 0;
        }
    }

    [TestFixture]
    public class InheritanceAndPolymorphismTests
    {
        [Test]
        public void SplitCalculatorClassInTwoClasses()
        {
            // Create a StringCalculator class that derives from Calculator, and move Sum with string paremeter, instantiate the two classes
            // Make StringCalculator sealed, try to create a derived class from it => compiler complains
            var calculator = new Calculator();
            var stringCalculator = new StringCalculator();
            Check.That(calculator.Sum(new[] {1.3, 1.8})).Equals(stringCalculator.Sum("1,3+1,8"));
        }

        [Test]
        public void DefineAnIntegerCalculatorClassThatReplacesSumMethodOfCalculator()
        {
            // IntegerCalculator must a silly implementation that cast double to int before doing sum, use new to redefine Sum method => its a completely different method, DO NOT USE TOO MUCH new keyword, it breaks polymorphism
            var integerCalculator = new IntegerCalculator();
            double sum = integerCalculator.Sum(new[] { 1.4, 1.7 });
            Check.That(sum).Equals(2.0);
            Calculator calculator = integerCalculator;
            sum = calculator.Sum(new[] { 1.4, 1.7 });
            Check.That(sum).Equals(1.4 + 1.7);
        }

        [Test]
        public void DefineAnotherIntegerCalculatorClassThatOverridesSumMethodOfCalculator()
        {
            // IntegerCalculator must a silly implementation that cast double to int before doing sum, use override to redefine Sum method, base Sum method must be virtual
            var integerCalculator = new AnotherIntegerCalculator();
            double sum = integerCalculator.Sum(new[] { 1.4, 1.7 });
            Check.That(sum).Equals(2.0);
            Calculator calculator = integerCalculator;
            sum = calculator.Sum(new[] { 1.4, 1.7 });
            Check.That(sum).Equals(2.0);
        }

        [Test]
        public void DefineAnAbstractStringCalculatorClassAndImplementItForSumAndProduct()
        {
            AbstractStringCalculator calculator = new SumAbstractStringCalculator();
            var sum = calculator.Calculate("1+2,3");
            Check.That(sum).Equals(1 + 2.3);
            calculator = new ProductStringCalculator();
            var product = calculator.Calculate("2*2,6");
            Check.That(product).Equals(2 * 2.6);
        }

        [Test]
        public void CompositionAndPolymorphismWithInterfaceRatherThanInheritance()
        {
            var calculatorWithStrategies = new ComposedStringCalculator(new IComputeStrategy[] { new SumStrategy(), new ProductStrategy() });
            var sum = calculatorWithStrategies.Calculate("1,0+2,3");
            var product = calculatorWithStrategies.Calculate("2,0*2,3");
            Check.That(sum).Equals(1.0 + 2.3);
            Check.That(product).Equals(2.0 * 2.3);
        }
    }

    public class ProductStrategy : ProductStringCalculator, IComputeStrategy
    {
        public bool CanCalculate(string operation)
        {
            return operation.Contains("*");
        }
    }

    public class SumStrategy : SumAbstractStringCalculator, IComputeStrategy
    {

        public bool CanCalculate(string operation)
        {
            return operation.Contains("+");
        }
    }

    public interface IComputeStrategy
    {
        double Calculate(string operation);
        bool CanCalculate(string operation);
    }
}
