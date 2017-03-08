# Change Log
All notable changes to this project will be documented in this file.

## [1.3.1] - 2017-3-8
### Updated
- Pull request #14: Refactor to JSON.net and simplify the code
- Thanks to [Leon de Pruyssenaere de la Woestijne](https://github.com/leonpw) for the PR!

## [1.3.0] - 2015-4-22
### Added
- travis-ci build
- SetAsmGroupId() method for suppression groups (ASM)
- SetSendAt() and SetSendEachAt() methods for scheduled sends
- SetIpPool() method for using IP Pools

### Changed
- HeaderSettingsNode.AddArray now takes a collection of type object rather
  than string

## [1.2.0] - 2015-1-26
### Added
- This changelog. Nice.
- Example of using header `To` array and substitutions in README.

### Changed
- Unicode characters in X-SMTPAPI header values or keys are now encoded as ASCII escape sequences.
- Null header values or keys result in a `ArgumentNullException`.

### Fixed
- Removed `System.Core` reference
- Removed invalid symbol path from compiled DLL (I think)
