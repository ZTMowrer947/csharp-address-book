using AddressBook.Models.Enums;

namespace AddressBook.Models
{
	/// <summary>
	/// Class representing a contact's fax number field.
	/// </summary>
	public sealed class FaxNumber : PhoneNumber
	{
		// Constructor
		public FaxNumber(string number) : base(PhoneNumberType.Fax, number)
		{
			TypeOfPhoneNumber = PhoneNumberType.Fax;
		}

		//public new static FaxNumber Create()
		//{

		//}

		// Override
		public override string ToString()
		{
			return Number;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			} else {
				FaxNumber that = (FaxNumber)obj;
				return base.Equals(that);
			}
		}
	}
}
