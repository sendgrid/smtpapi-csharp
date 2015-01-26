smtpapi-csharp
==============

Easily Build [SendGrid X-SMTPAPI Headers](http://sendgrid.com/docs/API_Reference/SMTP_API/index.html). 

See the [changelog](https://github.com/sendgrid/smtpapi-csharp/blob/master/CHANGELOG.md) for release notes.

```csharp
using SendGrid.SmtpApi;

var header = new Header();

var uniqueArgs = new Dictionary<string,string> {
  { "foo", "bar" },
  { "chunky", "bacon"}
};

header.AddUniqueArgs(uniqueArgs);

var xmstpapiJson = header.JsonString();
```

You can then use generated JSON in conjunction with your favorite SMTP library.

```csharp
SmtpClient client = new SmtpClient();
client.Port = 587;
client.Host = "smtp.sendgrid.net";
client.Timeout = 10000;
client.DeliveryMethod = SmtpDeliveryMethod.Network;
client.UseDefaultCredentials = false;
client.Credentials = new System.Net.NetworkCredential("your_sendgrid_username","your_sendgrid_password");
 
MailMessage mail = new MailMessage();
mail.To.Add(new MailAddress("user@example.com"));
mail.From = "you@yourcompany.com";
mail.Subject = "this is a test email.";
mail.Body = "this is my test email body";

// add the custom header that we built above
mail.Headers.Add( "X-SMTPAPI", xmstpapiJson );
 
client.SendAsync(mail, null);
```
If you want to add multiple recipients to the X-SMTPAPI header for a mail merge type send, you can do something like the following:
```csharp
var header = new Header();

var recipients = new List<String> {"a@example.com", "b@exampe.com", "c@example.com"};
header.SetTo(recipients);

var subs = new List<String> {"A","B","C"};
header.AddSubstitution("%name%", subs);

var mail = new MailMessage
{
    From = new MailAddress("please-reply@example.com"),
    Subject = "Welcome",
    Body = "Hi there %name%"
};

// add the custom header that we built above
mail.Headers.Add("X-SMTPAPI", header.JsonString());
```

For a more complete example, look at the included [Example project](https://github.com/sendgrid/smtpapi-csharp/blob/master/Smtpapi/Example/Program.cs).
