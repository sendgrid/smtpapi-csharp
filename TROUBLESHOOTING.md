If you have a SendGrid issue, please contact our [support team](https://support.sendgrid.com).


## Table of Contents

* [Viewing the Request Body](#request-body)

<a name="request-body"></a>
## Viewing the Request Body

When debugging or testing, it may be useful to examine the raw request body to compare against the [documented format](https://sendgrid.com/docs/API_Reference/api_v3.html).

You can do this right before you call `var response = await client.SendEmailAsync(msg);` like so:

```csharp
Console.WriteLine(msg.Serialize());
```
