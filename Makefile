.PHONY: clean install test release

clean:
	dotnet clean Smtpapi/SendGrid.SmtpApi.sln

install:
	@dotnet --version || (echo "Dotnet is not installed, please install Dotnet CLI"; exit 1);
	dotnet restore Smtpapi/SendGrid.SmtpApi.sln

test:
	dotnet build -c Release Smtpapi/SendGrid.SmtpApi.sln
	dotnet test -c Release Smtpapi/SendGrid.SmtpApi.sln
	curl -s https://codecov.io/bash > .codecov
	chmod +x .codecov
	./.codecov

release:
	dotnet pack -c Release Smtpapi/SendGrid.SmtpApi.sln
