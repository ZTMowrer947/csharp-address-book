using System.Collections.Generic;

namespace AddressBook.Models
{
	/// <summary>
	/// Class representing a contact in an address book.
	/// </summary>
	public sealed class Contact
	{
		// Properties
		private string firstName;
		private string lastName;
		
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

		//public static Contact Create()
		//{

		//}

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
