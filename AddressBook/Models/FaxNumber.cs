﻿using AddressBook.Models.Enums;
using AddressBook.Functions;

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
		}

		/// <summary>
		/// Interactively creates a new fax number.
		/// </summary>
		/// <returns>The new fax number.</returns>
		public new static FaxNumber Create()
		{
			string number = MainFunctions.GetAndValidateInput("Fax Number", RegexPatterns.PhoneNumber);

			FaxNumber newFaxNumber = new FaxNumber(number);
			return newFaxNumber;
		}

		/// <summary>
		/// Interactively modifies a fax number.
		/// </summary>
		public override void Edit()
		{
			Number = MainFunctions.GetAndValidateInput("Fax Number", RegexPatterns.PhoneNumber);
		}

		// Overrides
		public override string ToString()
		{
			return Number;
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

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
