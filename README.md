![SendGrid Logo](https://uiux.s3.amazonaws.com/2016-logos/email-logo%402x.png)

[![Travis Badge](https://travis-ci.org/sendgrid/smtpapi-csharp.svg?branch=master)](https://travis-ci.org/sendgrid/smtpapi-csharp)
[![Email Notifications Badge](https://dx.sendgrid.com/badge/csharp)](https://dx.sendgrid.com/newsletter/csharp)
[![Twitter Follow](https://img.shields.io/twitter/follow/sendgrid.svg?style=social&label=Follow)](https://twitter.com/sendgrid)
[![GitHub contributors](https://img.shields.io/github/contributors/sendgrid/smtpapi-csharp.svg)](https://github.com/sendgrid/smtpapi-csharp/graphs/contributors)
[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](./LICENSE.txt)

**This module helps build SendGrid's SMTP API headers.**

# Announcements

All updates to this module is documented in our [CHANGELOG](https://github.com/sendgrid/smtpapi-csharp/blob/master/CHANGELOG.md).

# Table of Contents
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Usage](#usage)
- [Roadmap](#roadmap)
- [How to Contribute](#contribute)
- [About](#about)
- [License](#license)

<a name="installation"></a>
# Installation

## Prerequisites

- .NET version 4.5.2
- [The SendGrid service](https://sendgrid.com/free?source=smtpapi-csharp)

## Setup Environment Variables

### Environment Variable

Update the development environment with your [SENDGRID_API_KEY](https://app.sendgrid.com/settings/api_keys), for example:

1. Make a copy of the .env_sample file and name it .env
2. Edit the file changing only the value of SENDGRID_PASSWORD variable
3. Save the file
4. Add the .env file to your environment path

## Install Package

To use SendGrid.SmtpApi in your C# project, you can either <a href="https://github.com/sendgrid/smtpapi-sendgrid.git">download the SendGrid C# .NET libraries directly from our Github repository</a> or, if you have the NuGet package manager installed, you can grab them automatically.

```bash
PM> Install-Package SendGrid.SmtpApi
```

Once you have the library properly referenced in your project, you can include calls to them in your code.

For a sample implementation, check the [Example](https://github.com/sendgrid/smtpapi-csharp/blob/master/Smtpapi/Example/Program.cs)

<a name="quick-start"></a>
# Quick Start

```csharp
var header = new Header();

var uniqueArgs = new Dictionary<string,string> {
  { "foo", "bar" },
  { "chunky", "bacon"}
};

header.AddUniqueArgs(uniqueArgs);

var xmstpapiJson = header.JsonString();
```
You can then use the generated JSON in conjunction with your favorite SMTP library.

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
mail.From = new MailAddress("you@yourcompany.com");
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

<a name="usage"></a>
# Usage

- [SendGrid Docs](https://sendgrid.com/docs/API_Reference/SMTP_API/index.html)
- [Example Code](https://github.com/sendgrid/smtpapi-csharp/blob/master/Smtpapi/Example/Program.cs)

<a name="roadmap"></a>
# Roadmap

If you are interested in the future direction of this project, please take a look at our [milestones](https://github.com/sendgrid/smtpapi-csharp/milestones). We would love to hear your feedback.

<a name="contribute"></a>
# How to Contribute

We encourage contribution to our projects, please see our [CONTRIBUTING](https://github.com/sendgrid/smtpapi-csharp/blob/master/CONTRIBUTING.md) guide for details.

Quick links:

- [Feature Request](https://github.com/sendgrid/smtpapi-csharp/blob/master/CONTRIBUTING.md)
- [Bug Reports](https://github.com/sendgrid/smtpapi-csharp/blob/master/CONTRIBUTING.md#submit-a-bug-report)
- [Sign the CLA to Create a Pull Request](https://github.com/sendgrid/smtpapi-csharp/blob/master/CONTRIBUTING.md#cla)
- [Improvements to the Codebase](https://github.com/sendgrid/smtpapi-csharp/blob/master/CONTRIBUTING.md#improvements-to-the-codebase)

<a name="about"></a>
# About

smtpapi-csharp is guided and supported by the SendGrid [Developer Experience Team](mailto:dx@sendgrid.com).

smtpapi-csharp is maintained and funded by SendGrid, Inc. The names and logos for smtpapi-csharp are trademarks of SendGrid, Inc.

<a name="license"></a>
# License
[The MIT License (MIT)](LICENSE.txt)
