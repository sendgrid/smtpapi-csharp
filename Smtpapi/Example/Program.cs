using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SendGrid.SmtpApi.Example
{
	internal class MainClass
	{
		private static bool _mailSent;

		private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
		{
			// Get the unique identifier for this asynchronous operation.
			var token = (string) e.UserState;

			if (e.Cancelled)
			{
				Console.WriteLine("[{0}] Send canceled.", token);
			}
			if (e.Error != null)
			{
				Console.WriteLine("[{0}] {1}", token, e.Error);
			}
			else
			{
				Console.WriteLine("Message sent.");
			}
			_mailSent = true;
		}

		private static string XsmtpapiHeaderAsJson()
		{
			var header = new Header();

			var uniqueArgs = new Dictionary<string, string>
			{
				{
					"foo",
					"bar"
				},
				{
					"chunky",
					"bacon"
				},
				{
					// UTF8 encoding test
					Encoding.UTF8.GetString(Encoding.Default.GetBytes("dead")),
					Encoding.UTF8.GetString(Encoding.Default.GetBytes("beef"))
				}
			};
			header.AddUniqueArgs(uniqueArgs);

            var subs = new List<String>() {"私はラーメンが大好き"};
            header.AddSubstitution("%tag%",subs);
			return header.JsonString();
		}

		public static void Main(string[] args)
		{
			var xmstpapiJson = XsmtpapiHeaderAsJson();

			var client = new SmtpClient
			{
				Port = 587,
				Host = "smtp.sendgrid.net",
				Timeout = 10000,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false
			};

			Console.WriteLine("Enter your SendGrid username:");
			var username = Console.ReadLine();

			Console.WriteLine("Enter your SendGrid password (warning: password will be visible):");
			var password = Console.ReadLine();

			client.Credentials = new NetworkCredential(username, password);

			var mail = new MailMessage
			{
				From = new MailAddress("please-reply@example.com"),
				Subject = "Good Choice Signing Up for Our Service!.",
                Body = "Hi there. Thanks for signing up for Appsterify.ly. It's disruptive! %tag%"
			};

			// add the custom header that we built above
			mail.Headers.Add("X-SMTPAPI", xmstpapiJson);
		    mail.BodyEncoding = Encoding.UTF8;

			//async event handler
			client.SendCompleted += SendCompletedCallback;
			const string state = "test1";

			Console.WriteLine("Enter an email address to which a test email will be sent:");
			var email = Console.ReadLine();

			if (email != null)
			{
				// Remember that MIME To's are different than SMTPAPI Header To's!
				mail.To.Add(new MailAddress(email));
				client.SendAsync(mail, state);

				Console.WriteLine("Sending message... press c to cancel, or wait for completion. Press any other key to exit.");
				var answer = Console.ReadLine();

				if (answer != null && answer.StartsWith("c") && _mailSent == false)
				{
					client.SendAsyncCancel();
				}
			}

			mail.Dispose();
		}
	}
}