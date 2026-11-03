# Changelog

## [2.0.0] - 2026-11-03
### Changed
- **[Breaking Change]** Added a required `Options` parameter with `ThrowErrorOnFailure` and `ErrorMessageOnFailure` for error handling.
- **[Breaking Change]** Replaced the `SleepResult` result property with `Success` and added an `Error` property that is populated when the Task fails and `ThrowErrorOnFailure` is false.
- Updated the Task to target .NET 8.

## [1.0.0] - 2022-09-06
### Added
- Initial implementation