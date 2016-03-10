using System.Collections.Generic;

namespace CSharpDiscovery
{
    using System;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using System.Security.Cryptography.X509Certificates;
    using NFluent;
    using NUnit.Framework;

    [TestFixture]
    public class LinqTests
    {
        [Test]
        public void UseAForeachLoopToSelectItemsStartingWithPlCaseSentitive()
        {
            var items = new[] { "plip", "foo", "bar", "plop", "plup", "Plap" };
            var filteredItems = new List<string>();
            foreach (var item in items)
            {
                if (item.StartsWith("pl"))
                {
                    filteredItems.Add(item);
                }
            }
            // foreach loop to add
            Check.That(filteredItems).ContainsExactly("plip", "plop", "plup");
        }

        [Test]
        public void TransformPreviousForeachLoopInALinqExpression()
        {
            var items = new[] { "plip", "foo", "bar", "plop", "plup", "Plap" };
            // use from in/where/select LINQ syntax to the same filter as with the foreach loop
            var filteredItems = from item in items where item.StartsWith("pl") select item;
            Check.That(filteredItems).ContainsExactly("plip", "plop", "plup");
        }

        [Test]
        public void ReplacePreviousLinqExpressionWithLinqExtensionMethodsOfIEnumerable()
        {
            var items = new[] { "plip", "foo", "bar", "plop", "plup", "Plap" };
            // use System.Linq.Enumerable extension methods
            var filteredItems = items.Where(item => item.StartsWith("pl")).ToList();
            Check.That(filteredItems).ContainsExactly("plip", "plop", "plup");
        }

        [Test]
        public void UseSelectExtensionMethodToTransformFilteredItemsToUpperCase()
        {
            var items = new[] { "plip", "foo", "bar", "plop", "plup", "Plap" };
            // use System.Linq.Enumerable extension methods
            var filteredItems = items.Where(item => item.StartsWith("pl")).Select(item => item.ToUpper()).ToList();
            Check.That(filteredItems).ContainsExactly("PLIP", "PLOP", "PLUP");
        }

        [Test]
        public void UseSkipAndTakeToKeepElementsAtAGivenRange()
        {
            var items = new[] { "plip", "foo", "bar", "plop", "plup", "Plap" };
            // use System.Linq.Enumerable extension methods
            var twoElementsStartingAtFourth = items.Skip(3).Take(2);
            Check.That(twoElementsStartingAtFourth).ContainsExactly("plop", "plup");
        }

        [Test]
        public void UseFirstToSelectFirstElementMatchingACondition()
        {
            var items = new[] { "plip", "foo", "bar", "plop", "plup", "Plap" };
            // use First to retrieve the first item having an 'o' at third position
            var firstItemHavingAnOAtThirdPosition = items.First(item => item[2] == 'o');
            Check.That(firstItemHavingAnOAtThirdPosition).Equals("foo");
        }

        [Test]
        public void FirstThrowsAnExceptionWhenConditionMatchesNoElement()
        {
            var items = new[] { "plip", "foo", "bar", "plop", "plup", "Plap" };
            //list.ForEach(i => Console.WriteLine(x() * i));
            Action firstWithAConditionMatchingNoElements = () => { var x = items.First(item => item == "plep"); };              
            //lambda using First filtering item == "plep"
            Check.That(firstWithAConditionMatchingNoElements).Throws<InvalidOperationException>();
        }

        [Test]
        public void FirstOrDefaultReturnsNullWhenConditionMatchesNoElement()
        {
            var items = new[] { "plip", "foo", "bar", "plop", "plup", "Plap" };
            // use FirstOrDefault filtering item == "plep"
            var firstItemEqualsToPlep = items.FirstOrDefault(item => item == "plep");
            Check.That(firstItemEqualsToPlep).IsNull();
        }

        [Test]
        public void SingleThrowsAnExceptionIfSeveralItemMatches()
        {
            var items = new[] { "plip", "foo", "bar", "plop", "plup", "Plap" };
            Action singleItemHavingAnOAtThirdPosition = () => { var x = items.Single(item => item[2] == 'o'); };
                //use Single with condition on third character being an 'o'
            Check.ThatCode(singleItemHavingAnOAtThirdPosition).Throws<InvalidOperationException>();
        }

        [Test]
        public void UseAggregateToComputeASum()
        {
            var valuesToSum = new[] { 1.2, 1.5, 5.3 };
            // use Aggregate giving a lambda expression that make the sum of two values => iteration is done by the Aggregate method, using this lambda expression at each iteration
            var sum = valuesToSum.Aggregate((u1, next) => u1 + next);
            Check.That(sum).Equals(1.2 + 1.5 + 5.3);
        }

        [Test]
        public void UseAggregateToConcatenateStringsFromArray()
        {
            var strings = new[] { "plip", "plop", "plup" };
            //words.Aggregate((workingSentence, next) => next + " " + workingSentence);
            // use Aggregate with a seed value t
            var concatenatedString = "Values of array are : , " + strings.Aggregate((word, next) => word + ", " + next);
            Check.That(concatenatedString).Equals("Values of array are : , plip, plop, plup");
        }
    }
}
