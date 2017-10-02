using NUnit.Framework;
using AddressBook.Models;

namespace AddressBook.Tests.Models
{
	[TestFixture()]
	public class ContactTests
	{
		public Contact contact;
		public Contact sameContact;
		public Contact differentContact;

		[OneTimeSetUp()]
		public void SetUp()
		{
			string name = "Testy McTest";
			string differentName = "Example Exampleton";

			contact = new Contact(name);
			sameContact = new Contact(name);
			differentContact = new Contact(differentName);
		}

		[Test()]
		public void RegexPatternTest()
		{
			int offset = contact.Name.IndexOf(' ');
			string firstName = contact.Name.Substring(0, offset);
			string lastName = contact.Name.Substring(offset + 1);

			Assert.True(RegexPatterns.Name.IsMatch(firstName));
			Assert.True(RegexPatterns.Name.IsMatch(lastName));
		}

		[Test()]
		public void StringConversionTest()
		{
			string nameField = "Name: Testy McTest";
			string addressField = "\nAddresses: None";
			string phoneField = "\nPhone Numbers: None";
			string faxField = "\nFax Numbers: None";
			string emailField = "\nEmail Addresses: None";

			string expected = nameField + addressField + phoneField + faxField + emailField;
			string actual = contact.ToString();
			// TODO
		}

		[Test()]
		public void EqualityTest()
		{
			Assert.AreEqual(contact, sameContact);
			Assert.AreNotSame(contact, sameContact);
			Assert.AreNotEqual(contact, differentContact);
		}
	}
}