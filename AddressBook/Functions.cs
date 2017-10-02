using System;
using System.Text.RegularExpressions;
using AddressBook.Exceptions;

namespace AddressBook
{
	/// <summary>
	/// Static class containing important functions.
	/// </summary>
	public static class Functions
	{
		/// <summary>
		/// Shadows Console.ReadLine with a version that accepts a prompt.
		/// </summary>
		/// <param name="prompt">The prompt that will be printed out by Console.Write.</param>
		/// <returns>The input recieved from Console.ReadLine.</returns>
		public static string ReadLine(string prompt)
		{
			Console.Write(prompt);
			return Console.ReadLine();
		}

		/// <summary>
		/// Gets input from ReadLine and loops until the input matches against the given regex.
		/// </summary>
		/// <param name="fieldName">The name of the field, used in the prompt.</param>
		/// <param name="regex">The regex to validate the input with.</param>
		/// <returns>The validated input.</returns>
		public static string GetAndValidateInput(string fieldName, Regex regex)
		{
			char firstLetter = fieldName.ToUpper().ToCharArray()[0];
			string name = firstLetter.ToString() + fieldName.Substring(1);
			string prompt = string.Format("{0}: ", name);

			string input = "";

			while (true)
			{
				try
				{
					input = ReadLine(prompt);

					if (regex.IsMatch(input))
					{
						break;
					}
					else
					{
						InputValidationException ive = new InputValidationException(string.Format("Invalid {0}.", name));
						throw ive;
					}
				}
				catch (InputValidationException ive)
				{
					Console.Write(string.Format("{0} Press any key to reenter...", ive.Message));
					Console.ReadKey();
					continue;
				}
			}

			return input;
		}
	}
}
