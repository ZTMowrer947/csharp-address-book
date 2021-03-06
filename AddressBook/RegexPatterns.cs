﻿using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AddressBook
{
	/// <summary>
	/// Static class of Regular Expressions used in the program.
	/// </summary>
	public static class RegexPatterns
	{
		public static Regex Name = new Regex(@"[A-Z][a-z]+");

		public static Dictionary<string, Regex> Address = new Dictionary<string, Regex>
		{
			{"Street Address", new Regex(@"^(\d{3,4} )?([A-z0-9]+ )+[A-z]+$")},
			{"City", new Regex(@"^([A-z]+ )?[A-z]+$")},
			{"State", new Regex(@"^[A-Z]{2}$")},
			{"Postal Code", new Regex(@"^\d{5}(-\d{4})?$")}
		};

		public static Regex PhoneNumber = new Regex(@"^(\+\d )?(\(\d{3}\) )?(\d{3}-\d{4})$");
	}
}
