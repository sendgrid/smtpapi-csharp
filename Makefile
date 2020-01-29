.PHONY: test install

install:
	nuget restore Smtpapi/SendGrid.SmtpApi.sln
	nuget install NUnit.Runners -Version 2.6.4 -OutputDirectory testrunner

test: install
	xbuild /p:Configuration=Release Smtpapi/SendGrid.SmtpApi.sln
	mono ./testrunner/NUnit.Runners.2.6.4/tools/nunit-console.exe ./Smtpapi/HeaderTests/bin/Release/SendGrid.SmtpApi.HeaderTests.dll
	nuget pack ./Smtpapi/Smtpapi/SendGrid.SmtpApi.csproj -Properties Configuration=Release
	curl -s https://codecov.io/bash > .codecov
	chmod +x .codecov
	./.codecov
