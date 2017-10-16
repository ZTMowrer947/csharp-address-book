using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace AddressBook.Models
{
	/// <summary>
	/// Class representing a contact in an address book.
	/// </summary>
	public sealed class Contact
	{
		// Properties
		[JsonIgnore()]
		private string firstName;

		[JsonIgnore()]
		private string lastName;

		[JsonProperty(PropertyName = "name")]
		public string Name
		{
			get
			{
				return string.Format("{0} {1}", firstName, lastName);
			}

			private set
			{
				int spaceOffset = value.IndexOf(' ');

				firstName = value.Substring(0, spaceOffset);
				lastName = value.Substring(spaceOffset + 1);
			}
		}

		public HashSet<Address> Addresses { get; }
		public HashSet<PhoneNumber> PhoneNumbers { get; }
		public HashSet<FaxNumber> FaxNumbers { get; }
		public HashSet<EmailAddress> EmailAddresses { get; }

		// Constructor
		public Contact(string name)
		{
			Name = name;
			Addresses = new HashSet<Address>();
			PhoneNumbers = new HashSet<PhoneNumber>();
			FaxNumbers = new HashSet<FaxNumber>();
			EmailAddresses = new HashSet<EmailAddress>();
		}

		public static Contact Create()
		{
			string firstName = Functions.GetAndValidateInput("First Name", RegexPatterns.Name);
			string lastName = Functions.GetAndValidateInput("Last Name", RegexPatterns.Name);
			string fullName = string.Format("{0} {1}", firstName, lastName);

			Contact newContact = new Contact(fullName);

			bool addAddresses = !Functions.InputStartsWith("Add addresses? (Y/n) ", "n");
			if (addAddresses) {
				while (true) {
					Console.Clear();

					Address newAddress = Address.Create();
					Console.WriteLine(newAddress);

					bool addressOK = !Functions.InputStartsWith("Is this address OK? (Y/n) ", "n");

					if (addressOK) {
						newContact.Addresses.Add(newAddress);
						Console.WriteLine("Added new address to new contact's set of addresses.");
					} else {
						Console.WriteLine("Discarded new address.");
					}

					bool addAnother = !Functions.InputStartsWith("Add another? (Y/n) ", "n");
					if (!addAnother) break;
				}
			}

			bool addPhoneNumbers = !Functions.InputStartsWith("Add phone numbers? (Y/n) ", "n");
			if (addPhoneNumbers)
			{
				while (true)
				{
					Console.Clear();

					PhoneNumber newPhoneNumber = PhoneNumber.Create();
					Console.WriteLine(newPhoneNumber);

					bool addressOK = !Functions.InputStartsWith("Is this phone number OK? (Y/n) ", "n");

					if (addressOK)
					{
						newContact.PhoneNumbers.Add(newPhoneNumber);
						Console.WriteLine("Added new phone number to new contact's set of phone numbers.");
					}
					else
					{
						Console.WriteLine("Discarded new phone number.");
					}

					bool addAnother = !Functions.InputStartsWith("Add another? (Y/n) ", "n");
					if (!addAnother) break;
				}
			}

			bool addFaxNumbers = !Functions.InputStartsWith("Add fax numbers? (Y/n) ", "n");
			if (addFaxNumbers)
			{
				while (true)
				{
					Console.Clear();

					FaxNumber newFaxNumber = FaxNumber.Create();
					Console.WriteLine(newFaxNumber);

					bool addressOK = !Functions.InputStartsWith("Is this fax number OK? (Y/n) ", "n");

					if (addressOK)
					{
						newContact.FaxNumbers.Add(newFaxNumber);
						Console.WriteLine("Added new fax number to new contact's set of fax numbers.");
					}
					else
					{
						Console.WriteLine("Discarded new fax number.");
					}

					bool addAnother = !Functions.InputStartsWith("Add another? (Y/n) ", "n");
					if (!addAnother) break;
				}
			}

			bool addEmailAddresses = !Functions.InputStartsWith("Add email addresses? (Y/n) ", "n");
			if (addEmailAddresses)
			{
				while (true)
				{
					Console.Clear();

					EmailAddress newEmailAddress = EmailAddress.Create();
					Console.WriteLine(newEmailAddress);

					bool emailAddressOK = !Functions.InputStartsWith("Is this email address OK? (Y/n) ", "n");

					if (emailAddressOK)
					{
						newContact.EmailAddresses.Add(newEmailAddress);
						Console.WriteLine("Added new email address to new contact's set of email addresses.");
					}
					else
					{
						Console.WriteLine("Discarded new email address.");
					}

					bool addAnother = !Functions.InputStartsWith("Add another? (Y/n) ", "n");
					if (!addAnother) break;
				}
			}

			return newContact;
		}

		// Overrides
		public override string ToString()
		{
			string nameField = string.Format("Name: {0}", Name);
			string startingAddressField = "\nAddresses: ";
			string startingPhoneNumberField = "\n\nPhone Numbers: ";
			string startingFaxNumberField = "\n\nFax Numbers: ";
			string startingEmailAddressField = "\n\nEmail Addresses: ";

			string fieldEntrySeparator = "\n\t";

			string addressFields = startingAddressField + (Addresses.Count == 0 ? "None" : "");
			string phoneNumberFields = startingPhoneNumberField + (PhoneNumbers.Count == 0 ? "None" : "");
			string faxNumberFields = startingFaxNumberField + (FaxNumbers.Count == 0 ? "None" : "");
			string emailAddressFields = startingEmailAddressField + (EmailAddresses.Count == 0 ? "None" : "");

			foreach (Address address in Addresses)
			{
				string entryString = fieldEntrySeparator + address.ToString();
				addressFields += entryString;
			}

			foreach (PhoneNumber phoneNumber in PhoneNumbers)
			{
				string entryString = fieldEntrySeparator + phoneNumber.ToString();
				phoneNumberFields += entryString;
			}

			foreach (FaxNumber faxNumber in FaxNumbers)
			{
				string entryString = fieldEntrySeparator + faxNumber.ToString();
				faxNumberFields += entryString;
			}

			foreach (EmailAddress emailAddress in EmailAddresses)
			{
				string entryString = fieldEntrySeparator + emailAddress.ToString();
				emailAddressFields += entryString;
			}

			string formatString = nameField +
				addressFields +
				phoneNumberFields +
				faxNumberFields +
				emailAddressFields;

			return formatString;
		}

		public override int GetHashCode()
		{
			int hash = 0;

			if (Name != null) hash += Name.GetHashCode();

			int addressHash = 0;
			int phoneHash = 0;
			int faxHash = 0;
			int emailHash = 0;

			foreach (Address address in Addresses)
			{
				addressHash += address.GetHashCode();
			}

			hash += addressHash;

			foreach (PhoneNumber phoneNumber in PhoneNumbers)
			{
				phoneHash += phoneNumber.GetHashCode();
			}

			hash += phoneHash;

			foreach (FaxNumber faxNumber in FaxNumbers)
			{
				faxHash += faxNumber.GetHashCode();
			}

			hash += faxHash;

			foreach (EmailAddress emailAddress in EmailAddresses)
			{
				emailHash = emailAddress.GetHashCode();
			}

			hash += emailHash;

			return hash;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			} else {
				Contact that = (Contact)obj;

				return (
					Name == that.Name &&
					Addresses.SetEquals(that.Addresses) &&
					PhoneNumbers.SetEquals(that.PhoneNumbers) &&
					FaxNumbers.SetEquals(that.FaxNumbers) &&
					EmailAddresses.SetEquals(that.EmailAddresses)
				);
			}
		}
	}
}
