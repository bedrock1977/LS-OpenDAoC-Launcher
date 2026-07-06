# Last Stand Launcher — Roadmap & Known Issues

Baseline captured **July 2026** after initial codebase review and Last Stand rebranding. Use this document to prioritize fixes before new features.

---

## Priority 1 — Bugs & broken behavior

These affect core launcher functionality today.

| ID | Issue | Location | Notes |
|----|-------|----------|-------|
| P1-1 | **Game path picker does nothing** | `MainWindow.xaml` | `btnPathSelect` is wired to `PatchButton_PreviewMouseDown` (opens patch notes). No folder/file dialog exists. |
| P1-2 | **`GameFolder` setting is misnamed** | Settings, `GetQuickCharacters()`, `BuildBat()` | Setting stores full path to `user.dat`, not a folder. Launch command concatenates `GameFolder + "connect.exe"` — path must end with `\` or launch fails. |
| P1-3 | **Hardcoded default install path** | `Constants.UserPath`, `Settings.settings`, `App.config` | Defaults to `D:\Program Files (x86)\Electronic Arts\LastStand\user.dat`. Wrong for most users. |
| P1-4 | **Silent patch download failures** | `MainWindow.UpdateFiles()` | Failed file downloads are caught and skipped with no user feedback. |
| P1-5 | **Play button cleared after update** | `MainWindow.UpdateFiles()` | After patching, `PlayButton.Content` is set to empty string instead of `"Play"`. |
| P1-6 | **Queue timer not disposed safely** | `StopPollingQueuePosition()` | Calls `_timer.Dispose()` without null check; can throw if timer never started. |
| P1-7 | **HttpClient Accept headers accumulate** | `CheckQueueAsync`, `StartPollingQueuePosition` | `DefaultRequestHeaders.Accept.Add()` called repeatedly — can cause duplicate header errors over time. |

---

## Priority 2 — Legacy / incomplete migration

Remaining Atlas-era artifacts after rebranding pass.

| ID | Issue | Location | Suggested fix |
|----|-------|----------|---------------|
| P2-1 | Player URLs still use `atlasl.ink` | `Constants.cs` | Replace with Last Stand URLs when available (register, Discord link, patch notes). |
| P2-2 | Bread easter egg API is Atlas | `BreadWindow.xaml.cs` | Point to Last Stand API or remove feature. |
| P2-3 | Lister namespace is `AtlasPatcher.Lister` | Lister project | Rename namespace for consistency (low user impact). |
| P2-4 | Version constant mismatch | `Constants.LauncherVersion` vs `AssemblyInfo` | Unify on single source of truth (`1.0.7` in assembly, `1.0.0` in constants). |
| P2-5 | Unused / dead code | `getDiscordStatus()`, `isDiscordRequired()` | Methods exist but are not called from Play flow. Remove or integrate. |
| P2-6 | Commented-out `CheckVersion()` on startup | `MainWindow` constructor | Version check only runs on window activate — consider explicit startup check. |

---

## Priority 3 — Security & reliability

| ID | Issue | Location | Notes |
|----|-------|----------|-------|
| P3-1 | **Passwords stored in plain text** | User settings | When "Remember account" is enabled, password saved unencrypted in `%LocalAppData%`. |
| P3-2 | **Credentials logged** | `BuildBat()` | Full launch command (including password) written to Serilog. |
| P3-3 | **Requires admin** | `WPFLauncher.manifest` | May be unnecessary for patching/launch; increases UAC friction. Review whether admin is required. |
| P3-4 | **Beta Newtonsoft.Json** | `packages.config` | Uses `13.0.2-beta1`; upgrade to stable release. |
| P3-5 | **Synchronous HTTP on UI thread** | `GetPlayerCount()`, `isDiscordRequired()` | Can cause UI stalls; should be async. |
| P3-6 | **No cancellation for patch download** | `UpdateFiles()` | Long downloads cannot be cancelled; UI locked during update. |

---

## Priority 4 — UX improvements

| ID | Feature | Description |
|----|---------|-------------|
| P4-1 | Game path browser | Folder picker for install directory; derive `user.dat` path automatically. |
| P4-2 | Patch progress UI | Dedicated progress bar instead of gradient text on Play button. |
| P4-3 | Queue status panel | Clearer queue state (position, ETA, cancel option). |
| P4-4 | Error messages with detail | Surface download/API errors instead of generic messages. |
| P4-5 | Realm stats refresh indicator | Show last-updated time or loading state for population counts. |
| P4-6 | Settings for game install path | Separate settings for install folder vs. `user.dat` path. |

---

## Priority 5 — Features (future)

| ID | Feature | Description |
|----|---------|-------------|
| P5-1 | Auto-detect DAoC install | Scan common paths / registry for existing install. |
| P5-2 | News / announcements panel | Fetch server news from API and display in launcher. |
| P5-3 | Repair install | Force re-download all patch files (beyond "Force re-patch" version reset). |
| P5-4 | Multi-language support | Localize UI strings. |
| P5-5 | Launcher auto-update notification | Background version check with toast/banner. |
| P5-6 | CI/CD build pipeline | Automated Release builds, artifact publishing. |
| P5-7 | Modernize stack | Evaluate .NET 8 + WPF or alternative UI framework long-term. |

---

## Repository hygiene

| ID | Issue | Notes |
|----|-------|-------|
| R-1 | Build outputs tracked in git | `bin/`, `packages/`, `.vs/` are committed. Consider expanding `.gitignore` and removing from history. |
| R-2 | Duplicate asset files | `home-bg-old.png`, `icon-old.ico`, `logo-old.png` — archive or remove. |
| R-3 | No automated tests | No unit or integration test projects exist. |

---

## Suggested execution order

### Phase A — Stabilize (1–2 weeks)

1. Fix game path picker and path handling (P1-1, P1-2, P1-3)
2. Fix post-update Play button state (P1-5)
3. Fix patch error handling (P1-4)
4. Fix queue/timer/HttpClient bugs (P1-6, P1-7)
5. Stop logging credentials (P3-2)
6. Unify version display (P2-4)

### Phase B — Complete migration (1 week)

1. Update player-facing URLs (P2-1)
2. Clean up dead code (P2-5, P2-6)
3. Resolve BreadWindow API (P2-2)

### Phase C — Polish & features

1. UX improvements (P4-*)
2. Security hardening for saved credentials (P3-1)
3. Feature backlog (P5-*)
4. Repository cleanup (R-*)

---

## Change log (documentation baseline)

| Date | Change |
|------|--------|
| 2026-07-06 | Initial project documentation and roadmap baseline |
| 2026-07-06 | Rebranded user-facing strings and assembly metadata from Atlas to Last Stand Launcher |
