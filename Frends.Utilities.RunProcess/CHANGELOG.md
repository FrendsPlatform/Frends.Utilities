# Changelog

## [2.0.0] - 2026-11-03
### Added
- Added `ThrowErrorOnFailure` and `ErrorMessageOnFailure` options. By default the Task keeps throwing on failure; when `ThrowErrorOnFailure` is disabled, failures are returned in the result `Error` property.
- Added `Success` and `Error` properties to the Task result.
- Task now supports cancellation.

### Changed
- **[Breaking Change]** Task now targets .NET 8.

## [1.1.0] - 2024-05-16
### Added
- You can now specify working directory for the process to run in (#12)

### Fixed
- Fixed a bug where the process is executed twice (#11)

## [1.0.0] - 2024-02-09
### Changed
- Initial implementation
