using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
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

			string currentDir = Directory.GetCurrentDirectory();
			string fileName = "AddressBook.json";
			string filePath = Path.Combine(currentDir, fileName);

			Console.WriteLine("Checking for saved contact file...");
			if (File.Exists(filePath))
			{
				Console.WriteLine("File found. Reading contacts...");

				try {
					HashSet<Contact> jsonContacts = Functions.JSONHandling.ReadContactsFromFile(filePath);
					Console.WriteLine("Contacts read successfully!\n");
					if (jsonContacts != null) {
						addressBook = jsonContacts;
					}
				}
				catch(JsonSerializationException jse)
				{
					Console.WriteLine(string.Format("Couldn't read contacts: {0}", jse.Message));
					Console.WriteLine("Using a blank address book.\n");
				}
			}
			else
			{
				Console.WriteLine("File not found. Creating it...");
				File.Create(filePath).Close();
				Console.WriteLine("File created. Using a blank address book.\n");
			}

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
