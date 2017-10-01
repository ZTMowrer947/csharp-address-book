namespace AddressBook.Models
{
	/// <summary>
	/// Class representing a contact's address field.
	/// </summary>
	public sealed class Address
	{
		// Properties
		public string Type { get; private set; }
		public string StreetAddress { get; private set; }
		public string City { get; private set; }
		public string State { get; private set; }
		public string PostalCode { get; private set; }

		// Constructor
		public Address(string type, string streetAddress, string city, string state, string postalCode)
		{
			Type = type;
			StreetAddress = streetAddress;
			City = city;
			State = state;
			PostalCode = postalCode;
		}

		// public static Address Create()
		// {
		// }

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
