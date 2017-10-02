using AddressBook.Models.Enums;
using Newtonsoft.Json;

namespace AddressBook.Models
{
	/// <summary>
	/// Class representing a contact's address field.
	/// </summary>
	public sealed class Address
	{
		// Properties
		[JsonProperty(PropertyName = "type")]
		public AddressType TypeOfAddress { get; private set; }

		[JsonProperty(PropertyName = "street")]
		public string StreetAddress { get; private set; }

		[JsonProperty(PropertyName = "city")]
		public string City { get; private set; }

		[JsonProperty(PropertyName = "state")]
		public string State { get; private set; }

		[JsonProperty(PropertyName = "postal_code")]
		public string PostalCode { get; private set; }

		// Constructor
		public Address(AddressType typeOfAddress, string streetAddress, string city, string state, string postalCode)
		{
			TypeOfAddress = typeOfAddress;
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
			string typeOfAddressAsString = "";

			switch (TypeOfAddress)
			{
				case AddressType.Home:
					typeOfAddressAsString = "Home";
					break;

				case AddressType.Work:
					typeOfAddressAsString = "Work";
					break;

				case AddressType.Other:
					typeOfAddressAsString = "Other";
					break;
			}

			string formatString = string.Format("{0}: {1}, {2}, {3} {4}",
				typeOfAddressAsString,
				StreetAddress,
				City,
				State,
				PostalCode
			);

			return formatString;
		}

		public override int GetHashCode()
		{
			int hash = TypeOfAddress.GetHashCode();

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
					TypeOfAddress == that.TypeOfAddress &&
					StreetAddress == that.StreetAddress &&
					City == that.City &&
					State == that.State &&
					PostalCode == that.PostalCode
				);
			}
		}
	}
}
