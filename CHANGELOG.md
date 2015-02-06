# Change Log
All notable changes to this project will be documented in this file.

## [1.3.0] - 2015-2-6
### Added
- ASM Group ID setting

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
