using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.ComponentModel;
using Smtpapi;

namespace Example
{
	class MainClass
	{
		static bool mailSent = false;
		private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
		{
			// Get the unique identifier for this asynchronous operation.
			String token = (string) e.UserState;

			if (e.Cancelled)
			{
				Console.WriteLine("[{0}] Send canceled.", token);
			}
			if (e.Error != null)
			{
				Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
			} else
			{
				Console.WriteLine("Message sent.");
			}
			mailSent = true;
		}

		static string XsmtpapiHeaderAsJson ()
		{
			var header = new Header ();

			var uniqueArgs = new Dictionary<string, string> {
				{
					"foo",
					"bar"
				},
				{
					"chunky",
					"bacon"
				}
			};
			header.AddUniqueArgs (uniqueArgs);

			return header.JsonString ();
		}

		public static void Main (string[] args)
		{
			var xmstpapiJson = XsmtpapiHeaderAsJson();

			SmtpClient client = new SmtpClient();
			client.Port = 587;
			client.Host = "smtp.sendgrid.net";
			client.Timeout = 10000;
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.UseDefaultCredentials = false;

			Console.WriteLine ("Enter your SendGrid username:");
			string username = Console.ReadLine ();

			Console.WriteLine ("Enter your SendGrid password (warning: password will be visible):");
			string password = Console.ReadLine ();

			client.Credentials = new System.Net.NetworkCredential(username,password);

			MailMessage mail = new MailMessage();
			mail.From = new MailAddress("please-reply@example.com");
			mail.Subject = "Good Choice Signing Up for Our Service!";
			mail.Body = "Hi there. Thanks for signing up for Appsterify.ly. It's disruptive!";

			// add the custom header that we built above
			mail.Headers.Add( "X-SMTPAPI", xmstpapiJson );

			//async event handler
			client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
			string state = "test1";

			Console.WriteLine ("Enter an email address to which a test email will be sent:");
			string email = Console.ReadLine ();

			// Remember that MIME To's are different than SMTPAPI Header To's!
			mail.To.Add(new MailAddress(email));

			client.SendAsync(mail,state);

			Console.WriteLine("Sending message... press c to cancel, or wait for completion. Press any other key to exit.");
			string answer = Console.ReadLine();

			if (answer.StartsWith("c") && mailSent == false)
			{
				client.SendAsyncCancel();
			}

			mail.Dispose();
		}
	}
}
