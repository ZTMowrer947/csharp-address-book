﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AddressBook.Exceptions;
using AddressBook.Models;

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

				bool contactOK = !InputStartsWith("Is this contact OK? (Y/n", "n");

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

			public static void PrintContacts(ref HashSet<Contact> contacts)
			{
				foreach (Contact contact in contacts)
				{
					Console.WriteLine(contact);
					Console.WriteLine();
				}
			}

			public static IEnumerable<Contact> SearchForContacts(ref HashSet<Contact> contacts) {
				Console.WriteLine("Not Implemented Yet");
				return contacts; // To satisfy compiler for now
			}

			public static void EditContact(ref HashSet<Contact> contacts)
			{
				Console.WriteLine("Not Implemented Yet");
			}

			public static void DeleteContact(ref HashSet<Contact> contacts)
			{
				Console.WriteLine("Not Implemented Yet");
			}

			public static void ExecuteOptionChoice(ref HashSet<Contact> contacts)
			{
				Console.Write("> ");
				char option = Console.ReadKey().KeyChar;

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
						PrintContacts(ref contacts);
						break;

					case 's':
						SearchForContacts(ref contacts);
						break;

					case 'e':
						EditContact(ref contacts);
						break;

					case 'd':
						DeleteContact(ref contacts);
						break;

					case 'q':
					case 'x':
						Environment.Exit(0);
						break;

					default:
						Console.WriteLine("Invalid menu command.");
						break;
				}
			}
		}
	}
}
