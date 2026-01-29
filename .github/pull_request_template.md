---
name: Pull Request
about: Propose changes to the bot
title: "[TYPE] Short description of change"
labels: ""
assignees: ""
---

## Description

<!-- Please include a summary of the change and which issue is fixed. Please also include relevant motivation and context. -->

## Type of change

<!-- Please check the options that apply -->

- [ ] 🐛 **Bug fix** (non-breaking change which fixes an issue)
- [ ] ✨ **New feature** (non-breaking change which adds functionality)
- [ ] 📚 **Documentation** (updates to documentation or comments)
- [ ] 🔧 **Refactor** (code restructuring without changing external behavior)
- [ ] ⚠️ **Breaking change** (fix or feature that would cause existing functionality to not work as expected)

## Compatibility Check

<!-- THIS IS CRITICAL. The bot runs in a restricted .NET Framework 4.8 / C# 7.3 environment. -->

- [ ] I have verified this code compiles with **C# 7.3** constraints (no `new()`, no `record`, no `using var`, etc.)
- [ ] I have respected the suppressions in `.editorconfig`

## How Has This Been Tested?

<!-- Please describe the tests that you ran to verify your changes. -->

- [ ] **Automated Tests**: Ran `_tests/TestRunner.cs`
- [ ] **Manual Verification**: Imported into Streamer.bot and ran `!giveaway system test`
- [ ] **Feature specific testing**: [Describe]

## Checklist

- [ ] My code follows the style guidelines of this project
- [ ] I have performed a self-review of my own code
- [ ] I have commented my code, particularly in hard-to-understand areas
- [ ] I have made corresponding changes to the documentation
- [ ] I have updated the CHANGELOG.md (under Unreleased)
- [ ] My changes generate no new warnings (except those suppressed by design)
