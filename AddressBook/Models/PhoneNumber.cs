using AddressBook.Models.Enums;
using Newtonsoft.Json;
using System;

namespace AddressBook.Models
{
	/// <summary>
	/// Class representing a contact's phone number field.
	/// </summary>
	public class PhoneNumber
	{
		// Properties
		[JsonProperty(PropertyName = "type")]
		public PhoneNumberType TypeOfPhoneNumber { get; protected set; }

		[JsonProperty(PropertyName = "number")]
		public string Number { get; protected set; }

		// Constructors
		[JsonConstructor()]
		public PhoneNumber(PhoneNumberType typeOfPhoneNumber, string number)
		{
			TypeOfPhoneNumber = typeOfPhoneNumber;
			Number = number;
		}

		public PhoneNumber(string number)
		{
			TypeOfPhoneNumber = PhoneNumberType.Home;
			Number = number;
		}

		public static PhoneNumber Create()
		{
			Console.Write("Type: (Home/work/mobile/other) ");
			string typeOfPhoneNumberAsString = Console.ReadLine().ToLower();
			char firstLetter = typeOfPhoneNumberAsString.ToCharArray()[0];
			PhoneNumberType typeOfPhoneNumber = PhoneNumberType.Home;

			switch (firstLetter)
			{
				case 'w':
					typeOfPhoneNumber = PhoneNumberType.Work;
					break;

				case 'm':
					typeOfPhoneNumber = PhoneNumberType.Mobile;
					break;

				case 'o':
					typeOfPhoneNumber = PhoneNumberType.Other;
					break;

				default:
					Console.WriteLine("Keeping default of Home");
					break;
			}

			string number = Functions.GetAndValidateInput("Phone Number", RegexPatterns.PhoneNumber);

			PhoneNumber newPhoneNumber = new PhoneNumber(typeOfPhoneNumber, number);
			return newPhoneNumber;
		}

		// Overrides
		public override string ToString()
		{
			string typeOfPhoneNumberAsString = "";

			switch (TypeOfPhoneNumber)
			{
				case PhoneNumberType.Home:
					typeOfPhoneNumberAsString = "Home";
					break;

				case PhoneNumberType.Work:
					typeOfPhoneNumberAsString = "Work";
					break;

				case PhoneNumberType.Mobile:
					typeOfPhoneNumberAsString = "Mobile";
					break;

				case PhoneNumberType.Other:
					typeOfPhoneNumberAsString = "Other";
					break;
			}

			string formatString = string.Format("{0}: {1}", typeOfPhoneNumberAsString, Number);

			return formatString;
		}

		public override int GetHashCode()
		{
			int hash = TypeOfPhoneNumber.GetHashCode();
			if (Number != null) hash += Number.GetHashCode();
			return hash;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			} else {
				PhoneNumber that = (PhoneNumber)obj;

				return TypeOfPhoneNumber == that.TypeOfPhoneNumber && Number == that.Number;
			}
		}
	}
}
