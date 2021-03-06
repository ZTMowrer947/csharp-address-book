﻿using System.Net.Mail;
using NUnit.Framework;
using AddressBook.Models;
using AddressBook.Models.Enums;
using System;

namespace AddressBook.Tests.Models
{
	[TestFixture()]
	public class EmailAddressTests
	{
		public EmailAddress emailAddress;
		public EmailAddress sameEmailAddress;
		public EmailAddress differentEmailAddress;

		[OneTimeSetUp()]
		public void SetUp()
		{
			EmailAddressType differentType = EmailAddressType.Work;

			string address = "me@example.com";

			emailAddress = new EmailAddress(address);
			sameEmailAddress = new EmailAddress(address);
			differentEmailAddress = new EmailAddress(differentType, address);
		}

		[Test()]
		public void EmailValidityTest()
		{
			TestDelegate validFormatTest = () =>
			{
				MailAddress mailAddress = new MailAddress(emailAddress.Address);
			};

			TestDelegate nullStringTest = () =>
			{
				var mailAddress = new MailAddress("");
			};

			Assert.DoesNotThrow(validFormatTest);
			Assert.Throws(typeof(ArgumentException), nullStringTest);
		}

		[Test()]
		public void StringConversionTest()
		{
			string expected = "Personal: me@example.com";
			string actual = emailAddress.ToString();

			Assert.AreEqual(expected, actual);
		}

		[Test()]
		public void EqualityTest()
		{
			Assert.AreEqual(emailAddress, sameEmailAddress);
			Assert.AreNotSame(emailAddress, sameEmailAddress);
			Assert.AreNotEqual(emailAddress, differentEmailAddress);
		}
	}
}