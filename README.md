# Last Stand Launcher

Official Windows launcher for **Last Stand** (Dark Age of Camelot private shard). Forked from the Atlas Freeshard launcher and rebranded for Last Stand infrastructure.

The solution provides game patching, login queue management, realm population display, and one-click game launch via `connect.exe`.

## Solution structure

| Project | Output | Purpose |
|---------|--------|---------|
| **WPFLauncher** | `LastStandLauncher.exe` | Main WPF launcher (.NET Framework 4.8) |
| **Lister** | `Lister.exe` | Internal WinForms tool to generate patch lists (MD5 + file size) |

```
LS-OpenDAoC-Launcher/
├── WPFLauncher.sln
├── README.md
├── docs/
│   └── ROADMAP.md          # Known issues and planned work
├── WPFLauncher/            # Main launcher application
│   ├── MainWindow.xaml(.cs)
│   ├── OptionsWindow.xaml(.cs)
│   ├── BreadWindow.xaml(.cs)
│   ├── Utils/
│   │   ├── Constants.cs    # URLs, messages, server IPs
│   │   ├── Updater.cs      # Patch version + file sync
│   │   └── File.cs         # Patch file metadata model
│   └── img/                # UI assets
└── Lister/                 # Patch list generator (staff/dev)
    └── Source files/
```

## Requirements

- Windows 10 or later
- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- Visual Studio 2019+ (or Build Tools) for development
- NuGet packages restored from `packages/` (packages.config style)

## Building

Open `WPFLauncher.sln` in Visual Studio and build **Release | Any CPU**.

Output paths:

- Launcher: `bin/Release/WPFLauncher/LastStandLauncher.exe`
- Lister: `bin/Release/Lister/Lister.exe`

The launcher manifest requests **administrator** privileges (`requireAdministrator` in `WPFLauncher.manifest`).

## Runtime flow

### Play button

1. **Check for patches** — Compare local patch version (`Settings.localVersion`) against `https://patch.laststand.net/version-new.txt`.
2. **Download updates** — If outdated, fetch `patchlist-new.txt`, verify MD5 hashes, download missing files. Supports self-update via `LSLauncherUpdate.bat`.
3. **Join queue** — POST credentials to `https://queue.laststand.net/api/v1/queue/join`.
4. **Poll queue** — If queued, poll `/api/v1/queue` every 10 seconds until position is 0.
5. **Launch game** — Run hidden `cmd.exe` with:
   ```
   {GameFolder}connect.exe game1127.dll {serverIP} {username} {password} {quickCharacter}
   ```

### Options (persisted in user settings)

| Setting | Default | Description |
|---------|---------|-------------|
| Remember account | On | Save username/password locally |
| Keep launcher open | On | Exit launcher after launching game |
| Minimize to tray | Off | Hide window when minimized |
| Connect to PTR | Off | Use PTR server instead of live |
| Force re-patch | — | Resets `localVersion` to 0 |

### External services

| Service | URL |
|---------|-----|
| Patch version | `https://patch.laststand.net/version-new.txt` |
| Patch list | `https://patch.laststand.net/patchlist-new.txt` |
| Queue API | `https://queue.laststand.net` |
| Realm stats | `https://api.laststand.net/stats` |
| Discord required check | `https://api.laststand.net/utils/discordrequired` |
| Live server | `livelaststand.ddns.net` |
| PTR server | `ptr.livelaststand.ddns.net` |

Player-facing links (register, Discord link, patch notes) still point to `atlasl.ink` — see [docs/ROADMAP.md](docs/ROADMAP.md).

## Key source files

| File | Responsibility |
|------|----------------|
| `App.xaml.cs` | Single-instance mutex (`LastStandLauncher`) |
| `MainWindow.xaml.cs` | UI, patching, queue, game launch, realm counts |
| `Updater.cs` | Remote version check, patch list parsing, MD5 verification |
| `Constants.cs` | Central configuration for URLs, IPs, and user messages |
| `OptionsWindow.xaml.cs` | User preference dialog |
| `Lister/Forms/lForm.cs` | Browse folder → generate tab-separated patch list |

## Logging

Serilog writes daily rolling logs to `logs/LastStandLauncher-{date}.txt` relative to the working directory.

## Lister (patch list tool)

Used by staff when publishing patches:

1. Browse a game/client folder.
2. Tool computes MD5 hash and file size for every file.
3. Save output as `patchlist.txt` with base URL header (`https://patch.laststand.net/`).

## Version info

- Assembly version: `1.0.7` (`Properties/AssemblyInfo.cs`)
- Display version constant: `1.0.0` (`Constants.LauncherVersion`) — may be out of sync

## Further reading

See [docs/ROADMAP.md](docs/ROADMAP.md) for known bugs, technical debt, and a suggested feature roadmap.
