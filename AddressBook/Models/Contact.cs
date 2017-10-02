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
			return base.ToString();
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
	}
}
