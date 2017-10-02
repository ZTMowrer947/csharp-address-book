using NUnit.Framework;
using AddressBook.Models;
using AddressBook.Models.Enums;

namespace AddressBook.Tests.Models
{
	[TestFixture()]
	public class ContactTests
	{
		public Contact contact;
		public Contact sameContact;
		public Contact differentContact;

		public Address address;
		public PhoneNumber phoneNumber;
		public FaxNumber faxNumber;
		public EmailAddress emailAddress;

		[OneTimeSetUp()]
		public void SetUp()
		{
			string name = "Testy McTest";
			string differentName = "Example Exampleton";

			contact = new Contact(name);
			sameContact = new Contact(name);
			differentContact = new Contact(differentName);

			address = new Address(AddressType.Home, "123 Example Road", "Portland", "OR", "12345");
			differentContact.Addresses.Add(address);

			phoneNumber = new PhoneNumber(PhoneNumberType.Home, "(123) 456-7890");
			differentContact.PhoneNumbers.Add(phoneNumber);

			faxNumber = new FaxNumber("(234) 567-8901");
			differentContact.FaxNumbers.Add(faxNumber);

			emailAddress = new EmailAddress(EmailAddressType.Home, "exampleton@example.tld");
			differentContact.EmailAddresses.Add(emailAddress);
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
			string phoneField = "\n\nPhone Numbers: None";
			string faxField = "\n\nFax Numbers: None";
			string emailField = "\n\nEmail Addresses: None";

			string expected = nameField + addressField + phoneField + faxField + emailField;
			string actual = contact.ToString();

			Assert.AreEqual(expected, actual);

			string differentNameField = "Name: Example Exampleton";
			
			string diffAddressField = addressField.Replace("None", string.Format("\n\t{0}", address.ToString()));
			string diffPhoneField = phoneField.Replace("None", string.Format("\n\t{0}", phoneNumber.ToString()));
			string diffFaxField = faxField.Replace("None", string.Format("\n\t{0}", faxNumber.ToString()));
			string diffEmailField = emailField.Replace("None", string.Format("\n\t{0}", emailAddress.ToString()));

			string differentExpected = differentNameField + diffAddressField + diffPhoneField + diffFaxField + diffEmailField;
			string differentActual = differentContact.ToString();

			Assert.AreEqual(differentExpected, differentActual);
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