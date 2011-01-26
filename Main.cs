using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AddressBook;

namespace GenerateContacts
{
	public class Application
	{
		static void Main (string[] args)
		{
			UIApplication.Main (args);
		}
	}

	// The name AppDelegate is referenced in the MainWindow.xib file.
	public partial class AppDelegate : UIApplicationDelegate
	{
		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			var button = UIButton.FromType(UIButtonType.RoundedRect);
			button.Frame = new RectangleF(0, 0, 200, 60);
			button.Center = window.Center;
			button.SetTitle("Populate contacts", UIControlState.Normal);
			button.TouchUpInside += delegate(object sender, EventArgs e) {
				PopulateContacts();
				var alert = new UIAlertView("Import Complete", string.Format("{0} contacts have been imported", _names.Count), null, "Ok");
				alert.Show();
			};

			window.AddSubview (button);
			window.MakeKeyAndVisible ();
			return true;
		}

		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{
		}

		private void PopulateContacts()
		{
			var addressBook = new ABAddressBook();
			var people = addressBook.GetPeople();

			// clear address book
			foreach(var person in people)
			{
				addressBook.Remove(person);
			}

			addressBook.Save();

			var random = new Random();
			// create 200 random contacts
			foreach (var name in _names)
			{
			  // create an ABRecordRef
				var record = new ABPerson();
				record.FirstName = name[0];
				record.LastName = name[1];
				var phones = new ABMutableStringMultiValue();
				phones.Add(GeneratePhone(random).ToString(), ABPersonPhoneLabel.Mobile);

				if(random.Next() % 2 == 1) {
					phones.Add(GeneratePhone(random).ToString(), ABPersonPhoneLabel.Main);
				}

				record.SetPhones(phones);

				addressBook.Add(record);
			}
			addressBook.Save();
		}

		public static long GeneratePhone(Random random)
		{
			long number = 0;
			while(number < 2222222222 || number > 9999999999)
			{
				number = NextNumber(random);
			}
			return number;
		}

		public static UInt32 NextNumber(Random random)
		{
			var buffer = new byte[sizeof(UInt32)];
			random.NextBytes(buffer);
			return BitConverter.ToUInt32(buffer, 0);
		}

		private List<string[]> _names = new List<string[]> {
			new string[] {"Jacob", "Emily"},
			new string[] {"Michael", "Emma"},
			new string[] {"Joshua", "Madison"},
			new string[] {"Matthew", "Abigail"},
			new string[] {"Ethan", "Olivia"},
			new string[] {"Andrew", "Isabella"},
			new string[] {"Daniel", "Hannah"},
			new string[] {"Anthony", "Samantha"},
			new string[] {"Christopher", "Ava"},
			new string[] {"Joseph", "Ashley"},
			new string[] {"William", "Sophia"},
			new string[] {"Alexander", "Elizabeth"},
			new string[] {"Ryan", "Alexis"},
			new string[] {"David", "Grace"},
			new string[] {"Nicholas", "Sarah"},
			new string[] {"Tyler", "Alyssa"},
			new string[] {"James", "Mia"},
			new string[] {"John", "Natalie"},
			new string[] {"Jonathan", "Chloe"},
			new string[] {"Nathan", "Brianna"},
			new string[] {"Samuel", "Lauren"},
			new string[] {"Christian", "Ella"},
			new string[] {"Noah", "Anna"},
			new string[] {"Dylan", "Taylor"},
			new string[] {"Benjamin", "Kayla"},
			new string[] {"Logan", "Hailey"},
			new string[] {"Brandon", "Jessica"},
			new string[] {"Gabriel", "Victoria"},
			new string[] {"Zachary", "Jasmine"},
			new string[] {"Jose", "Sydney"},
			new string[] {"Elijah", "Julia"},
			new string[] {"Angel", "Destiny"},
			new string[] {"Kevin", "Morgan"},
			new string[] {"Jack", "Kaitlyn"},
			new string[] {"Caleb", "Savannah"},
			new string[] {"Justin", "Katherine"},
			new string[] {"Austin", "Alexandra"},
			new string[] {"Evan", "Rachel"},
			new string[] {"Robert", "Lily"},
			new string[] {"Thomas", "Megan"},
			new string[] {"Luke", "Kaylee"},
			new string[] {"Mason", "Jennifer"},
			new string[] {"Aidan", "Angelina"},
			new string[] {"Jackson", "Makayla"},
			new string[] {"Isaiah", "Allison"},
			new string[] {"Jordan", "Brooke"},
			new string[] {"Gavin", "Maria"},
			new string[] {"Connor", "Trinity"},
			new string[] {"Aiden", "Lillian"},
			new string[] {"Isaac", "Mackenzie"}
		};
	}
}

