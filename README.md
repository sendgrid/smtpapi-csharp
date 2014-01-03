smtpapi-csharp
==============

Easily Build [SendGrid X-SMTPAPI Headers](http://sendgrid.com/docs/API_Reference/SMTP_API/index.html). 

You can then use generated JSON in conjunction with your favorite SMTP library.

```csharp
using Xsmpatpi;

var header = new Header();
header.AddTo("joe@example.com");

var uniqueArgs = new Dictionary<string,string> {
  { "foo", "bar" },
  { "chunky", "bacon"}
};

header.AddUniqueIdentifier(uniqueArgs);

var headerJson = header.AsJson()
```
