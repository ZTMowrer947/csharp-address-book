using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using AddressBook.Models;
using Newtonsoft.Json;

namespace AddressBook.Functions
{
	/// <summary>
	/// Static function class relating to the main menu.
	/// </summary>
	public static class MenuFunctions
	{
		/// <summary>
		/// Shows the commands used on the main menu.
		/// </summary>
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

		/// <summary>
		/// Creates and adds a new contact to the specified set of contacts.
		/// </summary>
		/// <param name="contactSet">The set of contacts to add the new contact to.</param>
		public static void AddNewContact(ref HashSet<Contact> contactSet)
		{
			Console.Clear();
			Console.WriteLine("Creating new Contact.\n");

			Contact newContact = Contact.Create();

			Console.WriteLine(newContact);
			bool contactOK = !MainFunctions.InputStartsWith("Is this contact OK? (Y/n) ", "n");

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

		/// <summary>
		/// Prints out all the contacts in the specified set.
		/// </summary>
		/// <param name="contacts">The set of contacts to print.</param>
		public static void PrintContacts(HashSet<Contact> contacts)
		{
			Console.WriteLine("Contacts:\n");
			if (contacts.Count > 0)
			{
				foreach (Contact contact in contacts)
				{
					Console.WriteLine(contact);
					Console.WriteLine();
				}
			}
			else
			{
				Console.WriteLine("None\n");
			}
		}

		/// <summary>
		/// Searches for contacts and returns the results.
		/// </summary>
		/// <param name="contacts">The set of contacts to search through.</param>
		/// <returns>The search results.</returns>
		public static IEnumerable<Contact> SearchForContacts(HashSet<Contact> contacts)
		{
			string searchBy = MainFunctions.ReadLine("Search By: (Name/address/phone number/fax number/email address) ");
			string searchFor = MainFunctions.ReadLine("Search For: ");

			HashSet<Contact> results = new HashSet<Contact>();

			char firstLetter = 'n';
			try
			{
				firstLetter = searchBy.ToLower().ToCharArray()[0];
			}
			catch (IndexOutOfRangeException) { }

			switch (firstLetter)
			{
				case 'a':
					Console.WriteLine("Searching by Address.");
					foreach (Contact contact in contacts)
					{
						var addressQuery =
							from address in contact.Addresses
							where address.ToString().Contains(searchFor)
							select contact;

						foreach (Contact queryContact in addressQuery)
						{
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

		/// <summary>
		/// Searches for and edits contacts.
		/// </summary>
		/// <param name="contacts">The set of contacts to search through and edit.</param>
		public static void EditContact(ref HashSet<Contact> contacts)
		{
			if (contacts.Count > 0)
			{
				IEnumerable<Contact> searchResults = SearchForContacts(contacts);
				Contact contactToEdit = new Contact(" ");

				if (searchResults.Count() == 1)
				{
					Console.WriteLine("\nOnly one result found, so that will be used.");
					contactToEdit = searchResults.First();
				}
				else if (searchResults.Count() > 1)
				{
					Console.WriteLine("Multiple search results found. Looping over them all. Select the one you want to edit.");
					foreach (Contact contact in searchResults)
					{
						Console.WriteLine(contact);
						bool editThisContact = !MainFunctions.InputStartsWith("Edit this contact? (Y/n)", "n");

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
					Console.WriteLine("Editing this contact:\n");
					Console.WriteLine(contactToEdit);

					Console.Write("\n\nPress any key to edit...");
					Console.ReadKey();

					Console.WriteLine("\nDeleting old contact...");
					contacts.Remove(contactToEdit);

					Console.WriteLine("\nDeleted old contact, editing selected contact...");
					contactToEdit.Edit();

					Console.WriteLine("\nEdited contact. Here is the resulting contact:");
					Console.WriteLine(contactToEdit);

					Console.WriteLine("\nAdding new contact to address book...");
					contacts.Add(contactToEdit);
					Console.WriteLine("\nAdded new contact to address book.");

				}
			}
			else
			{
				Console.WriteLine("No contacts");
			}
		}

		/// <summary>
		/// Searches for and deletes contacts.
		/// </summary>
		/// <param name="contacts">The set of contacts to search through and delete.</param>
		public static void DeleteContact(ref HashSet<Contact> contacts)
		{
			if (contacts.Count > 0)
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
						bool deleteThisContact = !MainFunctions.InputStartsWith("Delete this contact? (Y/n)", "n");

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
				}
			}
			else
			{
				Console.WriteLine("No contacts");
			}
		}

		/// <summary>
		/// Saves the specified set of contacts as JSON and exits the application.
		/// </summary>
		/// <param name="contacts">The set of contacts to save.</param>
		public static void SaveContactsAndExit(HashSet<Contact> contacts)
		{
			Console.WriteLine("Saving Contacts...");
			string currentDir = Directory.GetCurrentDirectory();
			string JSONFileName = "AddressBook.json";
			string JSONFilePath = Path.Combine(currentDir, JSONFileName);

			try
			{
				JSONHandlingFunctions.WriteContactsToFile(JSONFilePath, contacts);
				Console.WriteLine("Saved Contacts successfully!");
			}
			catch (JsonSerializationException jse)
			{
				Console.WriteLine(string.Format("Couldn't Save Contacts: {0}", jse.Message));
			}
			finally
			{
				Console.WriteLine("Exiting...");
				Environment.Exit(0);
			}
		}

		/// <summary>
		/// Prompts the user for a command and executes the respective function.
		/// </summary>
		/// <param name="contacts">The set of contacts to use.</param>
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
}
