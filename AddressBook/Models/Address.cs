using AddressBook.Models.Enums;
using AddressBook.Functions;
using Newtonsoft.Json;
using System;

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

		// Constructors
		[JsonConstructor()]
		public Address(AddressType typeOfAddress, string streetAddress, string city, string state, string postalCode)
		{
			TypeOfAddress = typeOfAddress;
			StreetAddress = streetAddress;
			City = city;
			State = state;
			PostalCode = postalCode;
		}

		public Address(string streetAddress, string city, string state, string postalCode)
		{
			TypeOfAddress = AddressType.Home;
			StreetAddress = streetAddress;
			City = city;
			State = state;
			PostalCode = postalCode;
		}

		/// <summary>
		/// Interactively creates a new address.
		/// </summary>
		/// <returns>The new address.</returns>
		public static Address Create()
		{
			string typeOfAddressAsString = MainFunctions.ReadLine("Type: (Home/work/other) ").ToLower();
			char firstLetter = 'h';
			AddressType typeOfAddress = AddressType.Home;

			try
			{
				firstLetter = typeOfAddressAsString.ToCharArray()[0];
				switch (firstLetter)
				{
					case 'w':
						typeOfAddress = AddressType.Work;
						break;

					case 'o':
						typeOfAddress = AddressType.Other;
						break;

					default:
						Console.WriteLine("Keeping default of Home");
						break;
				}
			} catch (IndexOutOfRangeException) {
				Console.WriteLine("Keeping default of Home");
			}

			string streetAddress = MainFunctions.GetAndValidateInput("Street Address", RegexPatterns.Address["Street Address"]);
			string city = MainFunctions.GetAndValidateInput("City", RegexPatterns.Address["City"]);
			string state = MainFunctions.GetAndValidateInput("State", RegexPatterns.Address["State"]);
			string postalCode = MainFunctions.GetAndValidateInput("Postal Code", RegexPatterns.Address["Postal Code"]);

			Address newAddress = typeOfAddress == AddressType.Home ? new Address(streetAddress, city, state, postalCode) : new Address(typeOfAddress, streetAddress, city, state, postalCode);
			return newAddress;
		}

		/// <summary>
		/// Interactively modifies an address.
		/// </summary>
		public void Edit()
		{
			bool editType = MainFunctions.InputStartsWith("Edit address type? (y/N) ", "y");
			if (editType)
			{
				string typeOfAddressAsString = MainFunctions.ReadLine("Type: (Home/work/other) ").ToLower();
				char firstLetter = 'h';
				AddressType typeOfAddress = AddressType.Home;

				try
				{
					firstLetter = typeOfAddressAsString.ToCharArray()[0];
					switch (firstLetter)
					{
						case 'w':
							typeOfAddress = AddressType.Work;
							break;

						case 'o':
							typeOfAddress = AddressType.Other;
							break;

						default:
							Console.WriteLine("Keeping default of Home");
							break;
					}
				}
				catch (IndexOutOfRangeException)
				{
					Console.WriteLine("Keeping default of Home");
				}

				TypeOfAddress = typeOfAddress;

			}

			Console.WriteLine();

			bool editStreetAddress = MainFunctions.InputStartsWith("Edit street address? (y/N) ", "y");
			if (editStreetAddress)
			{
				StreetAddress = MainFunctions.GetAndValidateInput("Street Address", RegexPatterns.Address["Street Address"]);
			}

			Console.WriteLine();

			bool editCity = MainFunctions.InputStartsWith("Edit city? (y/N) ", "y");
			if (editCity)
			{
				City = MainFunctions.GetAndValidateInput("City", RegexPatterns.Address["City"]);
			}

			Console.WriteLine();

			bool editState = MainFunctions.InputStartsWith("Edit state? (y/N) ", "y");
			if (editState)
			{
				State = MainFunctions.GetAndValidateInput("State", RegexPatterns.Address["State"]);
			}

			Console.WriteLine();

			bool editPostalCode = MainFunctions.InputStartsWith("Edit postal code? (y/N) ", "y");
			if (editPostalCode)
			{
				PostalCode = MainFunctions.GetAndValidateInput("Postal Code", RegexPatterns.Address["Postal Code"]);
			}
		}

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
