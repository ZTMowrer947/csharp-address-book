using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AddressBook.Exceptions;
using AddressBook.Models;
using Newtonsoft.Json;

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
					Console.WriteLine();
					Console.ReadKey();
					continue;
				}
			}

			return input;
		}

		public static bool InputStartsWith(string prompt, string start) {
			return ReadLine(prompt).ToLower().StartsWith(start);
		}

		public static class Menu
		{
			public static void ShowHelp()
			{
				Dictionary<string, string> helpItems = new Dictionary<string, string>
				{
					{"h", "Show this help" },
					{"?", "Same as h" },
					{"a", "Add a new contact" },
					{"p", "Print all contacts" },
					{"s", "Search for contacts" },
					{"e", "Edit a contact" },
					{"d", "Delete a contact" },
					{"q", "Exit and Save contacts" },
					{"x", "Same as x" }
				};

				Console.WriteLine("Commands:\n");

				foreach (KeyValuePair<string, string> kvp in helpItems)
				{
					Console.WriteLine(string.Format("{0}: {1}", kvp.Key, kvp.Value));
				}

				Console.WriteLine();
			}

			public static void AddNewContact(ref HashSet<Contact> contactSet)
			{
				Console.Clear();
				Console.WriteLine("Creating new Contact.\n");

				Contact newContact = Contact.Create();

				Console.WriteLine(newContact);
				bool contactOK = !InputStartsWith("Is this contact OK? (Y/n) ", "n");

				if (contactOK)
				{
					contactSet.Add(newContact);
					Console.WriteLine("Added new contact to address book.");
				}
				else
				{
					Console.WriteLine("Discarded new contact.");
				}
			}

			public static void PrintContacts(HashSet<Contact> contacts)
			{
				Console.WriteLine("Contacts:\n");
				if (contacts.Count > 0) {
					foreach (Contact contact in contacts)
					{
						Console.WriteLine(contact);
						Console.WriteLine();
					}
				} else {
					Console.WriteLine("None\n");
				}
			}

			public static IEnumerable<Contact> SearchForContacts(HashSet<Contact> contacts) {
				string searchBy = ReadLine("Search By: (Name/address/phone number/fax number/email address) ");
				string searchFor = ReadLine("Search For: ");

				HashSet<Contact> results = new HashSet<Contact>();

				char firstLetter = 'n';
				try {
					firstLetter = searchBy.ToLower().ToCharArray()[0];
				} catch (IndexOutOfRangeException) {}

				switch(firstLetter) {
					case 'a':
						Console.WriteLine("Searching by Address.");
						foreach (Contact contact in contacts)
						{
							var addressQuery =
								from address in contact.Addresses
								where address.ToString().Contains(searchFor)
								select contact;

							foreach (Contact queryContact in addressQuery) {
								results.Add(queryContact);
							}
						}
						break;

					case 'p':
						Console.WriteLine("Searching by Phone Number.");
						foreach (Contact contact in contacts)
						{
							var phoneQuery =
								from phoneNumber in contact.PhoneNumbers
								where phoneNumber.ToString().Contains(searchFor)
								select contact;

							foreach (Contact queryContact in phoneQuery)
							{
								results.Add(queryContact);
							}
						}
						break;

					case 'f':
						Console.WriteLine("Searching by Fax Number.");
						foreach (Contact contact in contacts)
						{
							var faxQuery =
								from faxNumber in contact.FaxNumbers
								where faxNumber.ToString().Contains(searchFor)
								select contact;

							foreach (Contact queryContact in faxQuery)
							{
								results.Add(queryContact);
							}
						}
						break;

					case 'e':
						Console.WriteLine("Searching by Email Address.");
						foreach (Contact contact in contacts)
						{
							var emailQuery =
								from emailAddress in contact.EmailAddresses
								where emailAddress.ToString().Contains(searchFor)
								select contact;

							foreach (Contact queryContact in emailQuery)
							{
								results.Add(queryContact);
							}
						}
						break;

					default:
						Console.WriteLine("Searching by Name.");
						var nameQuery =
							from contact in contacts
							where contact.Name.Contains(searchFor)
							select contact;

						foreach (Contact queryContact in nameQuery)
						{
							results.Add(queryContact);
						}
						break;
				}

				return results;
			}

			public static void EditContact(ref HashSet<Contact> contacts)
			{
				IEnumerable<Contact> searchResults = SearchForContacts(contacts);
				Contact contactToEdit = new Contact(" ");

				if (searchResults.Count() == 1)
				{
					contactToEdit = searchResults.First();
				}
				else if (searchResults.Count() > 1)
				{
					Console.WriteLine("Multiple search results found. Looping over them all. Select the one you want to edit.");
					foreach (Contact contact in searchResults)
					{
						Console.WriteLine(contact);
						bool editThisContact = !InputStartsWith("Edit this contact? (Y/n)", "n");

						if (editThisContact)
						{
							contactToEdit = contact;
							break;
						}
					}

					if (contactToEdit == new Contact(" "))
					{
						Console.WriteLine("No contact was selected to be edited, so nothing will be edited");
					}
				}
				else
				{
					Console.WriteLine("Search results are empty, nothing to edit");
				}

				if (contactToEdit != new Contact(" "))
				{
					// TODO: Add edit method for contact class
					Console.WriteLine("TODO: Add edit method for contact class");
				}
			}

			public static Contact DeleteContact(ref HashSet<Contact> contacts)
			{
				IEnumerable<Contact> searchResults = SearchForContacts(contacts);
				Contact contactToDelete = new Contact(" ");

				if (searchResults.Count() == 1)
				{
					contactToDelete = searchResults.First();
				}
				else if (searchResults.Count() > 1)
				{
					Console.WriteLine("Multiple search results found. Looping over them all. Select the one you want to delete.");
					foreach (Contact contact in searchResults)
					{
						Console.WriteLine(contact);
						bool deleteThisContact = !InputStartsWith("Delete this contact? (Y/n)", "n");

						if (deleteThisContact)
						{
							contactToDelete = contact;
							break;
						}
					}

					if (contactToDelete == new Contact(" "))
					{
						Console.WriteLine("No contact was selected to be deleted, so nothing will be deleted");
					}
				}
				else
				{
					Console.WriteLine("Search results are empty, nothing to delete");
				}

				if (contactToDelete != new Contact(" "))
				{
					Console.WriteLine("Deleting Contact...");
					contacts.Remove(contactToDelete);
					Console.WriteLine("Contact Deleted.");
					return contactToDelete;
				}
				else
				{
					return null;
				}
			}

			public static void SaveContactsAndExit(HashSet<Contact> contacts)
			{
				Console.WriteLine("Saving Contacts...");
				string currentDir = Directory.GetCurrentDirectory();
				string JSONFileName = "AddressBook.json";
				string JSONFilePath = Path.Combine(currentDir, JSONFileName);

				try {
					JSONHandling.WriteContactsToFile(JSONFilePath, contacts);
					Console.WriteLine("Saved Contacts successfully!");
				} catch (JsonSerializationException jse) {
					Console.WriteLine(string.Format("Couldn't Save Contacts: {0}", jse.Message));
				} finally {
					Console.WriteLine("Exiting...");
					Environment.Exit(0);
				}
			}

			public static void ExecuteOptionChoice(ref HashSet<Contact> contacts)
			{
				Console.Write("> ");
				char option = Console.ReadKey().KeyChar;
				Console.WriteLine();

				switch (option)
				{
					case 'h':
					case '?':
						ShowHelp();
						break;

					case 'a':
						AddNewContact(ref contacts);
						break;

					case 'p':
						PrintContacts(contacts);
						break;

					case 's':
						IEnumerable<Contact> searchResults = SearchForContacts(contacts);

						Console.Write("Results: ");

						if (searchResults.Count() > 0)
						{
							foreach (Contact contact in searchResults)
							{
								Console.WriteLine(string.Format("\n{0}\n", contact));
							}
						}
						else
						{
							Console.WriteLine("None\n");
						}
						break;

					case 'e':
						EditContact(ref contacts);
						break;

					case 'd':
						DeleteContact(ref contacts);
						break;

					case 'q':
					case 'x':
						SaveContactsAndExit(contacts);
						break;

					default:
						Console.WriteLine("Invalid menu command.");
						break;
				}
			}
		}

		public static class JSONHandling
		{
			public static HashSet<Contact> ReadContactsFromFile(string filePath)
			{
				JsonSerializer serializer = new JsonSerializer();

				using (StreamReader sr = new StreamReader(filePath))
				using (JsonTextReader reader = new JsonTextReader(sr))
				{
					return serializer.Deserialize<HashSet<Contact>>(reader);
				}
			}

			public static void WriteContactsToFile(string filePath, HashSet<Contact> contacts)
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Formatting = Formatting.Indented;

				using (StreamWriter sw = new StreamWriter(filePath))
				using (JsonTextWriter writer = new JsonTextWriter(sw))
				{
					writer.Indentation = 4;

					serializer.Serialize(writer, contacts);
				}
			}
		}
	}
}
