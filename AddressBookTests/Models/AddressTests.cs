using System.Text.RegularExpressions;
using NUnit.Framework;
using AddressBook.Models;
using AddressBook.Models.Enums;

namespace AddressBook.Tests.Models
{
	[TestFixture()]
	public class AddressTests
	{
		public Address testAddress;
		public Address sameTestAddress;
		public Address differentTestAddress;

		[OneTimeSetUp()]
		public void SetUp() {
			AddressType differentTypeOfAddress = AddressType.Work;
			string streetAddress = "123 Example Road";
			string city = "Portland";
			string state = "OR";
			string postalCode = "12345";

			testAddress = new Address(streetAddress, city, state, postalCode);
			sameTestAddress = new Address(streetAddress, city, state, postalCode);
			differentTestAddress = new Address(differentTypeOfAddress, streetAddress, city, state, postalCode);
		}

		[Test()]
		public void RegexPatternTest()
		{
			Regex streetAddressRegex = RegexPatterns.Address["Street Address"];
			Regex cityRegex = RegexPatterns.Address["City"];
			Regex stateRegex = RegexPatterns.Address["State"];
			Regex postalCodeRegex = RegexPatterns.Address["Postal Code"];

			Assert.True(streetAddressRegex.IsMatch(testAddress.StreetAddress));
			Assert.True(cityRegex.IsMatch(testAddress.City));
			Assert.True(stateRegex.IsMatch(testAddress.State));
			Assert.True(postalCodeRegex.IsMatch(testAddress.PostalCode));
		}

		[Test()]
		public void StringConversionTest()
		{
			string expected = "Home: 123 Example Road, Portland, OR 12345";
			string actual = testAddress.ToString();

			Assert.AreEqual(expected, actual);
		}

		[Test()]
		public void EqualityTest()
		{
			Assert.AreEqual(testAddress, sameTestAddress);
			Assert.AreNotSame(testAddress, sameTestAddress);
			Assert.AreNotEqual(testAddress, differentTestAddress);
		}
	}
}