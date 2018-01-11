using AddressBook.Models.Enums;
using AddressBook.Functions;
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

		/// <summary>
		/// Interactively creates a new phone number.
		/// </summary>
		/// <returns>The new phone number.</returns>
		public static PhoneNumber Create()
		{
			string typeOfPhoneNumberAsString = MainFunctions.ReadLine("Type: (Home/work/mobile/other) ").ToLower();
			char firstLetter = 'h';
			PhoneNumberType typeOfPhoneNumber = PhoneNumberType.Home;

			try {
				firstLetter = typeOfPhoneNumberAsString.ToCharArray()[0];
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
			} catch (IndexOutOfRangeException) {
				Console.WriteLine("Keeping default of Home");
			}

			string number = MainFunctions.GetAndValidateInput("Phone Number", RegexPatterns.PhoneNumber);

			PhoneNumber newPhoneNumber = new PhoneNumber(typeOfPhoneNumber, number);
			return newPhoneNumber;
		}

		/// <summary>
		/// Interactively modifies a phone number.
		/// </summary>
		public virtual void Edit()
		{
			bool editType = MainFunctions.InputStartsWith("Edit phone number type? (y/N) ", "y");
			if (editType)
			{
				string typeOfPhoneNumberAsString = MainFunctions.ReadLine("Type: (Home/work/mobile/other) ").ToLower();
				char firstLetter = 'h';
				PhoneNumberType typeOfPhoneNumber = PhoneNumberType.Home;

				try
				{
					firstLetter = typeOfPhoneNumberAsString.ToCharArray()[0];
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

					TypeOfPhoneNumber = typeOfPhoneNumber;
				}
				catch (IndexOutOfRangeException)
				{
					Console.WriteLine("Keeping default of Home");
				}
			}

			Console.WriteLine();

			bool editNumber = MainFunctions.InputStartsWith("Edit number? (y/N) ", "y");
			if (editNumber)
			{
				Number = MainFunctions.GetAndValidateInput("Phone Number", RegexPatterns.PhoneNumber);
			}
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

			string formatString = $"{typeOfPhoneNumberAsString}: {Number}";

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
