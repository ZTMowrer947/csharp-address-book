using NUnit.Framework;
using AddressBook.Models;

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
			string type = "Home";
			string differentType = "Work";
			string streetAddress = "123 Example Road";
			string city = "Portland";
			string state = "OR";
			string postalCode = "12345";

			testAddress = new Address(type, streetAddress, city, state, postalCode);
			sameTestAddress = new Address(type, streetAddress, city, state, postalCode);
			differentTestAddress = new Address(differentType, streetAddress, city, state, postalCode);
		}

		[Test()]
		public void StringConversionTest()
		{
			string expected = "Home: 123 Example Road, Portland, OR 12345";
			string actual = testAddress.ToString();

			Assert.AreEqual(expected, actual);
		}

		[Test()]
		public void EqualsTest()
		{
			Assert.AreEqual(testAddress, sameTestAddress);
			Assert.AreNotSame(testAddress, sameTestAddress);
			Assert.AreNotEqual(testAddress, differentTestAddress);
		}
	}
}