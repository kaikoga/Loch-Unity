# Changelog

All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [3.0.0] - 2026-05-24

### Changed

- Substition API is completely overhauled. Loc.TrFormat(Substitutuon) is Loc.Format(Substitutuon).Tr.
- Assembly is no longer auto detected for UIElements.
  - Use LochElementTool.Localize<T>() and LochElementTool.LocalizeWith<T>() to manually assign the Assembly for translation.
- TrEnum<T>() is always translated using the target type's Assembly, instead of the context Assembly.
- Supports Nullable Reference Types.

## [2.0.2] - 2026-03-11

### Fixed

- Fixed Loch translation not displayed properly after Play Mode change
- Fixed PropAsEnumPopup not displayed with PropertyScope

## [2.0.1] - 2026-03-05

### Fixed

- Fixed compile error without NDMF.

## [2.0.0] - 2026-02-26

- Initial release.