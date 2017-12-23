using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using AddressBook.Models;

namespace AddressBook.Functions
{
	/// <summary>
	/// Static function class containing functions for Reading/Writing JSON data.
	/// </summary>
	public static class JSONHandlingFunctions
	{
		/// <summary>
		/// Reads the JSON file at the specified path and returns the deserialized set of contacts.
		/// </summary>
		/// <param name="filePath">The path of the JSON file to read.</param>
		/// <returns>The deserialized set of contacts.</returns>
		public static HashSet<Contact> ReadContactsFromFile(string filePath)
		{
			JsonSerializer serializer = new JsonSerializer();

			using (StreamReader sr = new StreamReader(filePath))
			using (JsonTextReader reader = new JsonTextReader(sr))
			{
				return serializer.Deserialize<HashSet<Contact>>(reader);
			}
		}

		/// <summary>
		/// Writes the specified set of contacts to the JSON file at the specified path.
		/// </summary>
		/// <param name="filePath">The path of the JSON file to write to.</param>
		/// <param name="contacts">The set of contacts to write to the JSON file.</param>
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
