using System;

namespace AddressBook.Exceptions
{
	/// <summary>
	/// The base exception class for this project.
	/// </summary>
	public class AddressBookException: Exception
	{
		public AddressBookException(string message) : base(message) { }

		public AddressBookException() : base() { }
	}
}
