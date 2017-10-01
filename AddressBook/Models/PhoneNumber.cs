using AddressBook.Models.Enums;

namespace AddressBook.Models
{
	/// <summary>
	/// Class representing a contact's phone number field.
	/// </summary>
	public class PhoneNumber
	{
		// Properties
		public PhoneNumberType TypeOfPhoneNumber { get; protected set; }
		public string Number { get; protected set; }

		// Constructor
		public PhoneNumber(PhoneNumberType typeOfPhoneNumber, string number)
		{
			TypeOfPhoneNumber = typeOfPhoneNumber;
			Number = number;
		}

		//public static PhoneNumber Create()
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
