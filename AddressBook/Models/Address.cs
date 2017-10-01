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
			string formatString = string.Format("{0}: {1}, {2}, {3} {4}",
				Type,
				StreetAddress,
				City,
				State,
				PostalCode
			);

			return formatString;
		}

		public override int GetHashCode()
		{
			int hash = 0;
			if (Type != null) hash += Type.GetHashCode();
			if (StreetAddress != null) hash += StreetAddress.GetHashCode();
			if (City != null) hash += City.GetHashCode();
			if (State != null) hash += State.GetHashCode();
			if (PostalCode != null) hash += PostalCode.GetHashCode();

			return hash;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			} else {
				Address that = (Address)obj;

				return (
					Type == that.Type &&
					StreetAddress == that.StreetAddress &&
					City == that.City &&
					State == that.State &&
					PostalCode == that.PostalCode
				);
			}
		}
	}
}
