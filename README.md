smtpapi-csharp
==============

Easily Build [SendGrid X-SMTPAPI Headers](http://sendgrid.com/docs/API_Reference/SMTP_API/index.html). 

This is a beta and method names are likely to change.

```csharp
using Smtpapi;

var header = new Header();

var uniqueArgs = new Dictionary<string,string> {
  { "foo", "bar" },
  { "chunky", "bacon"}
};

header.AddUniqueIdentifier(uniqueArgs);

var xmstpapiJson = header.AsJson();
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
For a more complete example, look at the included [Example project](https://github.com/sendgrid/smtpapi-csharp/blob/master/Xsmtpapi/Example/Program.cs).
