using System;
using System.Collections.Generic;
using AddressBook.Models;

namespace AddressBook
{
	/// <summary>
	/// The main program class.
	/// </summary>
	public sealed class Program
	{
		/// <summary>
		/// The main method.
		/// </summary>
		/// <param name="args">Ignored parameters.</param>
		public static void Main(string[] args)
		{
			HashSet<Contact> addressBook = new HashSet<Contact>();

			Console.WriteLine("Welcome to the Address Book!\n");

			Functions.Menu.ShowHelp();
			Console.WriteLine();

			while (true) {
				Functions.Menu.ExecuteOptionChoice(ref addressBook);

				Console.Write("Press any key to return to the main menu...");
				Console.ReadKey();
				Console.Clear();
			}
		}
	}
}
