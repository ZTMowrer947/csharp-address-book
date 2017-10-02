using AddressBook.Models.Enums;
using Newtonsoft.Json;

namespace AddressBook.Models
{
	/// <summary>
	/// Class representing a contact's email address field.
	/// </summary>
	public sealed class EmailAddress
	{
		// Properties
		[JsonProperty(PropertyName = "type")]
		public EmailAddressType TypeOfEmailAddress { get; private set; }

		[JsonProperty(PropertyName = "address")]
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
			string typeOfEmailAddressAsString = "";

			switch (TypeOfEmailAddress)
			{
				case EmailAddressType.Home:
					typeOfEmailAddressAsString = "Home";
					break;

				case EmailAddressType.Work:
					typeOfEmailAddressAsString = "Work";
					break;

				case EmailAddressType.School:
					typeOfEmailAddressAsString = "School";
					break;

				case EmailAddressType.Other:
					typeOfEmailAddressAsString = "Other";
					break;
			}

			string formatString = string.Format("{0}: {1}", typeOfEmailAddressAsString, Address);

			return formatString;
		}

		public override int GetHashCode()
		{
			int hash = TypeOfEmailAddress.GetHashCode();
			if (Address != null) hash += Address.GetHashCode();

			return hash;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			} else {
				EmailAddress that = (EmailAddress)obj;

				return TypeOfEmailAddress == that.TypeOfEmailAddress && Address == that.Address;
			}
		}
	}
}
