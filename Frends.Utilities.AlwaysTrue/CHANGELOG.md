# Changelog

## [2.0.0] - 2026-11-03
### Changed
- **[Breaking Change]** The Task now requires `Input`, `Options` and `CancellationToken` parameters.
- **[Breaking Change]** The result now contains `Success` and `Error` properties in addition to `TaskResult`.
- **[Breaking Change]** The Task now targets .NET 8.
- Added `ThrowErrorOnFailure` and `ErrorMessageOnFailure` options for error handling.

## [1.0.0] - 2022-08-23
### Added
- Initial implementation