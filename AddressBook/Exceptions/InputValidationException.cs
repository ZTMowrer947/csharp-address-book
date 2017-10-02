namespace AddressBook.Exceptions
{
	/// <summary>
	/// Input was invalid (did not match its regex)
	/// </summary>
	public class InputValidationException: AddressBookException
	{
		public InputValidationException(string message) : base(message) { }
		public InputValidationException() : base() { }
	}
}
