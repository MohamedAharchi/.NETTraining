using System;
using NFluent;
using NUnit.Framework;

namespace CSharpDiscovery
{
    [TestFixture]
    public class TypesTests
    {
        [Test]
        public void NFluentAndNUnitAreWorking()
        {
            Check.That(true).IsTrue();
        }

        [Test]
        public void AnIntIsNotEqualToSameIntStringRepresentation()
        {
            string integerAsAString = "1";
            int integer = 1;
            Check.That(integerAsAString).Not.Equals(integer);
        }

        [Test]
        public void AnIntIsNotEqualToSameIntAsFloat()
        {
            float integerAsAFloat = 2;
            int integer = 2;
            Check.That(integerAsAFloat).Not.Equals(integer);
        }

        [Test]
        public void AnIntIsNotEqualToSameIntAsDouble()
        {
            double integerAsADouble = 2;
            int integer = 2;
            Check.That(integerAsADouble).Not.Equals(integer);
        }

        [Test]
        public void AnIntIsNotEqualToSameIntAsDecimal()
        {
            decimal integerAsADecimal = 2;
            int integer = 2;
            Check.That(integerAsADecimal).Not.Equals(integer);
        }

        [Test]
        public void AnIntIsNotEqualToSameIntAsLong()
        {
            long integerAsLong = 2;
            int integer = 2;
            Check.That(integerAsLong).Not.Equals(integer);
        }

        [Test]
        public void AnIntIsEqualToSameIntAsInt32()
        {
            Int32 integerAsInt32 = 2;
            int integer = 2;
            Check.That(integerAsInt32).Equals(integer);
        }

        [Test]
        public void AFloatCanBeCastedToInteger()
        {
            float single = 1.0F;
            int expectedInteger = 1;
            int singleCastedToInteger = (int)single;
            Check.That(singleCastedToInteger).Equals(expectedInteger);
        }

        [Test]
        public void AnIntCanBeImplicitlyCastedToFloat()
        {
            int integer = 1;
            float expectedSingle = 1.0F;
            float singleImplicitlyCastToInteger = integer;
            Check.That(singleImplicitlyCastToInteger).Equals(expectedSingle);
        }

        [Test]
        public void AStringCanBeParsedToInteger()
        {
            string integerString = "30";
            int expectedInteger = 30;
            int integerParsed = int.Parse(integerString);
            Check.That(integerParsed).Equals(expectedInteger);
        }

        public int ParseFloatStringAsInteger()
        {
            string floatAtString = "30,0";
            return int.Parse(floatAtString);
        }

        [Test]
        public void AFloatStringRepresentationCannotBeParsedToInteger()
        {
            // Create a method named ParseFloatStringAsInteger that takes no argument, return void and parse a float string representation "30.0"
            Check.That(ParseFloatStringAsInteger).Throws<FormatException>();
        }

        [Test]
        public void TryToParseAStringToInteger()
        {
            string floatString = "30.0";
            int expectedInteger = 0;

            // Use int.TryParse, /!\ it uses an "out" argument
            int integerParsed = expectedInteger;
            bool tryParseFailed = int.TryParse(floatString, out integerParsed);
            Check.That(integerParsed).Equals(default(int));
            Check.That(tryParseFailed).IsFalse();
        }

        [Test]
        public void UseVarForLessVerbosityButKeepStrongTyping()
        {
            var integerAsAString = "2";
            var integer = 2;
            Check.That(integerAsAString).Not.Equals(integer);
        }

        [Test]
        public void NullableIntWithNullValueDoesNotHaveValue()
        {
            // use "int?" to declare a nullable int initialized with null, then try also with Nullable<int>
            //int? nullableInteger = null;
            Nullable<int> nullableInteger = null;
            Check.That(nullableInteger.HasValue).IsFalse();
        }


        public int GetNullableIntValue()
        {
            int? nullableInteger = null;
            return nullableInteger.Value;
        }
        [Test]
        public void GettingValueOfANullableIntwithNullValueThrowsAnInvalidOperationException()
        {
            // Create a method named GetNullableIntValue that takes no argument, return void and access a nullable int value (nullableInteger.Value)
            Check.That(GetNullableIntValue).Throws<InvalidOperationException>();
        }

        [Test]
        public void NullableIntWithNullValueDoesNotHaveValue2()
        {
            // use "int?" to declare a nullable int initialized with 30
            int? nullableInteger = 30;
            Check.That(nullableInteger.Value).Equals(30);
        }
    }
}
