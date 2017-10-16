using AddressBook.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Net.Mail;

namespace AddressBook.Models
{
	/// <summary>
	/// Class representing a contact's email address field.
	/// </summary>
	public sealed class EmailAddress
	{
		// Properties
		[JsonProperty(PropertyName = "type")]
		public EmailAddressType TypeOfEmailAddress { get; private set; }

		[JsonProperty(PropertyName = "address")]
		public string Address { get; private set; }

		// Constructors
		[JsonConstructor()]
		public EmailAddress(EmailAddressType typeOfEmailAddress, string address)
		{
			TypeOfEmailAddress = typeOfEmailAddress;
			Address = address;
		}

		public EmailAddress(string address)
		{
			TypeOfEmailAddress = EmailAddressType.Personal;
			Address = address;
		}

		public static EmailAddress Create()
		{
			string typeOfEmailAddressAsString = Functions.ReadLine("Type: (Personal/work/school/other) ").ToLower();
			char firstLetter = 'h';
			EmailAddressType typeOfEmailAddress = EmailAddressType.Personal;

			try {
				firstLetter = typeOfEmailAddressAsString.ToCharArray()[0];
				switch (firstLetter)
				{
					case 'w':
						typeOfEmailAddress = EmailAddressType.Work;
						break;

					case 's':
						typeOfEmailAddress = EmailAddressType.School;
						break;

					case 'o':
						typeOfEmailAddress = EmailAddressType.Other;
						break;

					default:
						Console.WriteLine("Keeping default of Personal");
						break;
				}
			} catch (IndexOutOfRangeException) {
				Console.WriteLine("Keeping default of Personal");
			}

			string address = "";

			while (true) {
				string emailAddressString = Functions.ReadLine("Email Address: ");

				try {
					MailAddress mailAddress = new MailAddress(emailAddressString);
				} catch (FormatException) {
					Console.Write("Invalid email address. Press any key to retry");
					Console.Clear();
					continue;
				}

				address = emailAddressString;
				break;
			}

			EmailAddress newEmailAddress = new EmailAddress(typeOfEmailAddress, address);
			return newEmailAddress;
		}

		// Overrides
		public override string ToString()
		{
			string typeOfEmailAddressAsString = "";

			switch (TypeOfEmailAddress)
			{
				case EmailAddressType.Personal:
					typeOfEmailAddressAsString = "Personal";
					break;

				case EmailAddressType.Work:
					typeOfEmailAddressAsString = "Work";
					break;

				case EmailAddressType.School:
					typeOfEmailAddressAsString = "School";
					break;

				case EmailAddressType.Other:
					typeOfEmailAddressAsString = "Other";
					break;
			}

			string formatString = string.Format("{0}: {1}", typeOfEmailAddressAsString, Address);

			return formatString;
		}

		public override int GetHashCode()
		{
			int hash = TypeOfEmailAddress.GetHashCode();
			if (Address != null) hash += Address.GetHashCode();

			return hash;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) {
				return false;
			} else {
				EmailAddress that = (EmailAddress)obj;

				return TypeOfEmailAddress == that.TypeOfEmailAddress && Address == that.Address;
			}
		}
	}
}
