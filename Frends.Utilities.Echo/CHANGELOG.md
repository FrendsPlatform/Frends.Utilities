# Changelog

## [2.0.0] - 2026-11-03
### Changed
- **[Breaking Change]** The Task now requires `Options` and a cancellation token as parameters.
- **[Breaking Change]** The result now contains `Success` and `Error` properties.
- **[Breaking Change]** The Task now targets .NET 8.
- Added `ThrowErrorOnFailure` and `ErrorMessageOnFailure` options for controlling error handling.

## [1.0.0] - 2022-09-06
### Added
- Initial implementation