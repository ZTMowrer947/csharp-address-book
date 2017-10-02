using AddressBook.Models.Enums;

namespace AddressBook.Models
{
	/// <summary>
	/// Class representing a contact's email address field.
	/// </summary>
	public sealed class EmailAddress
	{
		// Properties
		public EmailAddressType TypeOfEmailAddress { get; private set; }
		public string Address { get; private set; }

		// Constructor
		public EmailAddress(EmailAddressType typeOfEmailAddress, string address)
		{
			TypeOfEmailAddress = typeOfEmailAddress;
			Address = address;
		}

		//public static EmailAddress Create()
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
