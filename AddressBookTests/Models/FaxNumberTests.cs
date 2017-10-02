using NUnit.Framework;
using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Tests.Models
{
	[TestFixture()]
	public class FaxNumberTests
	{
		public FaxNumber faxNumber;
		public FaxNumber sameFaxNumber;
		public FaxNumber differentFaxNumber;

		[OneTimeSetUp()]
		public void SetUp()
		{
			string number = "(345) 789-0123";
			string differentNumber = "456-7890";

			faxNumber = new FaxNumber(number);
			sameFaxNumber = new FaxNumber(number);
			differentFaxNumber = new FaxNumber(differentNumber);
		}

		[Test()]
		public void EqualityTest()
		{
			Assert.AreEqual(faxNumber, sameFaxNumber);
			Assert.AreNotSame(faxNumber, sameFaxNumber);
			Assert.AreNotEqual(faxNumber, differentFaxNumber);
		}
	}
}