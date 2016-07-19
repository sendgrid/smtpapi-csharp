[![Travis Badge](https://travis-ci.org/sendgrid/smtpapi-csharp.svg?branch=master)](https://travis-ci.org/sendgrid/smtpapi-csharp)

**This module helps build SendGrid's SMTP API headers.**

# Announcements

All updates to this module is documented in our [CHANGELOG](https://github.com/sendgrid/smtpapi-csharp/blob/master/CHANGELOG.md).

# Installation

## Prerequisites

- .NET version 4.5.2
- The SendGrid service, starting at the [free level](https://sendgrid.com/free?source=smtpapi-csharp))

## Setup Environment Variables

Update your Environment (user space) with your SENDGRID_USERNAME and SENDGRID_PASSWORD.

## Install Package

To use SendGrid.SmtpApi in your C# project, you can either <a href="https://github.com/sendgrid/smtpapi-sendgrid.git">download the SendGrid C# .NET libraries directly from our Github repository</a> or, if you have the NuGet package manager installed, you can grab them automatically.

```bash
PM> Install-Package SendGrid.SmtpApi
```

Add the following namespace to use the library:

```csharp
using SendGrid.CSharp.HTTP.Client;
```

Once you have the library properly referenced in your project, you can include calls to them in your code.

For a sample implementation, check the [Example](https://github.com/sendgrid/smtpapi-csharp/blob/master/Smtpapi/Example/Program.cs)

# Quick Start

```csharp
using SendGrid.CSharp.HTTP.Client;

client.Host = "smtp.sendgrid.net";
client.Timeout = 10000;
client.DeliveryMethod = SmtpDeliveryMethod.Network;
client.UseDefaultCredentials = false;
String sendgrid_username = Environment.GetEnvironmentVariable("SENDGRID_USERNAME", EnvironmentVariableTarget.User);
String sendgrid_password = Environment.GetEnvironmentVariable("SENDGRID_PASSWORD", EnvironmentVariableTarget.User);
client.Credentials = new System.Net.NetworkCredential(sendgrid_username,sendgrid_password);

MailMessage mail = new MailMessage();
mail.To.Add(new MailAddress("test@example.com"));
mail.From = "test@example.com";
mail.Subject = "this is a test email.";
mail.Body = "this is my test email body";

// add the custom header that we built above
mail.Headers.Add( "X-SMTPAPI", xmstpapiJson );

client.SendAsync(mail, null);
```

If you want to add multiple recipients to the X-SMTPAPI header for a mail merge type send, you can do something like the following:

```csharp
var header = new Header();

var recipients = new List<String> {"test1@example.com", "test2@example.com", "test3@example.com"};
header.SetTo(recipients);

var subs = new List<String> {"A","B","C"};
header.AddSubstitution("%name%", subs);

var mail = new MailMessage
{
    From = new MailAddress("test@example.com"),
    Subject = "Welcome",
    Body = "Hi there %name%"
};

// add the custom header that we built above
mail.Headers.Add("X-SMTPAPI", header.JsonString());
```

# Usage

- [SendGrid Docs](https://sendgrid.com/docs/API_Reference/SMTP_API/index.html)
- [Example Code](https://github.com/sendgrid/smtpapi-csharp/blob/master/Smtpapi/Example/Program.cs)

## Roadmap

If you are intersted in the future direction of this project, please take a look at our [milestones](https://github.com/sendgrid/smtpapi-csharp/milestones). We would love to hear your feedback.

## How to Contribute

We encourage contribution to our projects, please see our [CONTRIBUTING](https://github.com/sendgrid/smtpapi-csharp/blob/master/CONTRIBUTING.md) guide for details.

Quick links:

- [Feature Request](https://github.com/sendgrid/smtpapi-csharp/blob/master/CONTRIBUTING.md)
- [Bug Reports](https://github.com/sendgrid/smtpapi-csharp/blob/master/CONTRIBUTING.md#submit_a_bug_report)
- [Sign the CLA to Create a Pull Request](https://github.com/sendgrid/smtpapi-csharp/blob/master/CONTRIBUTING.md#cla)
- [Improvements to the Codebase](https://github.com/sendgrid/smtpapi-csharp/blob/master/CONTRIBUTING.md#improvements_to_the_codebase)

# About

smtpapi-csharp is guided and supported by the SendGrid [Developer Experience Team](mailto:dx@sendgrid.com).

smtpapi-csharp is maintained and funded by SendGrid, Inc. The names and logos for smtpapi-csharp are trademarks of SendGrid, Inc.

![SendGrid Logo]
(https://uiux.s3.amazonaws.com/2016-logos/email-logo%402x.png)
