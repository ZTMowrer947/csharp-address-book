using NUnit.Framework;
using AddressBook.Models;
using AddressBook.Models.Enums;

namespace AddressBook.Tests.Models
{
	[TestFixture()]
	public class PhoneNumberTests
	{
		public PhoneNumber phoneNumber;
		public PhoneNumber samePhoneNumber;
		public PhoneNumber differentPhoneNumber;

		[OneTimeSetUp()]
		public void SetUp()
		{
			PhoneNumberType differentTypeOfPhoneNumber = PhoneNumberType.Mobile;
			string number = "+1 (234) 567-8901";

			phoneNumber = new PhoneNumber(number);
			samePhoneNumber = new PhoneNumber(number);
			differentPhoneNumber = new PhoneNumber(differentTypeOfPhoneNumber, number);
		}

		[Test()]
		public void RegexPatternTest()
		{
			Assert.True(
				RegexPatterns.PhoneNumber
				.IsMatch(phoneNumber.Number)
			);
		}

		[Test()]
		public void StringConversionTest()
		{
			string expected = "Home: +1 (234) 567-8901";
			string actual = phoneNumber.ToString();

			Assert.AreEqual(expected, actual);
		}

		[Test()]
		public void EqualityTest()
		{
			Assert.AreEqual(phoneNumber, samePhoneNumber);
			Assert.AreNotSame(phoneNumber, samePhoneNumber);
			Assert.AreNotEqual(phoneNumber, differentPhoneNumber);
		}
	}
}