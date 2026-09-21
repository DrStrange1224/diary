# DIARY

A lightweight, modern desktop diary application for Windows, built with WPF (.NET).

Write daily notes, browse them grouped by day, and get an instant overview of your activity
from a color-coded month calendar — all in a clean, themeable interface.

## Features

- **Notes** — create notes with one click, edit title and content directly in the viewer
- **Grouped list** — notes are grouped and sorted by day with local time formatting
- **Month calendar** — custom calendar grid (Monday start) with note-count coloring per day
- **Light & Dark themes** — switch in Settings; the choice persists across restarts
- **English & Russian localization** — switch language in Settings; both the interface
  and all date formatting (calendar, day groups) update instantly and persist
- **SQLite storage** — every note is saved instantly as you type (no "Save" button)
- **Rounded, modern UI** — custom control styles, slim scrollbars, card layout

## Technology

- **.NET 10** / C# (`net10.0-windows`)
- **WPF** with a hand-rolled **MVVM** structure (no external MVVM toolkit)
- **Microsoft.Data.Sqlite 10.0.12** for note storage
- **System.Text.Json** for app settings
- **DynamicResource theming** — two theme dictionaries swapped at runtime

## Project structure

```
Diary/
├── App.xaml / App.xaml.cs        # startup, merged theme dictionary
├── MainWindow.xaml (+ .cs)       # main screen: list, calendar, note viewer
├── SettingsWindow.xaml (+ .cs)   # theme & language settings
├── Commands/
│   └── RelayCommand.cs           # simple ICommand implementation
├── Converters/
│   ├── EntryCountToBrushConverter.cs      # day-cell color by note count
│   ├── InverseBooleanToVisibilityConverter.cs
│   └── LanguageToNameConverter.cs        # language names in the selector
├── Models/
│   ├── AppSettings.cs            # persisted app settings (theme, language)
│   ├── AppStrings.cs             # localized UI strings (observable)
│   ├── DayCell.cs                # calendar day cell
│   ├── DiaryEntry.cs             # a single note (observable)
│   └── Language.cs               # English / Russian enum
├── Services/
│   ├── AppSettingsService.cs     # settings.json load/save
│   ├── Localizer.cs              # language state, string tables, culture
│   └── NoteStore.cs              # SQLite load/save for notes
├── Themes/
│   ├── Light.xaml / Dark.xaml    # color palettes (swapped at runtime)
│   ├── Styles.xaml               # shared control styles
│   ├── ThemeKind.cs              # theme enum
│   └── ThemeManager.cs           # runtime theme switching
└── ViewModels/
    ├── MainViewModel.cs
    ├── SettingsViewModel.cs
    └── ViewModelBase.cs          # INotifyPropertyChanged base
```

## Getting started

Prerequisites: [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows).

```sh
# build and run
dotnet run

# build only
dotnet build

# publish a single-file, self-contained Windows executable
dotnet publish -c Release -r win-x64 --self-contained -p:PublishSingleFile=true
```

The published executable is written to
`bin/Release/net10.0-windows/win-x64/publish/Diary.exe` — copy it anywhere and run;
no separate .NET installation is required.

## Data storage

The app keeps its data next to the executable, so keeping the application folder
together also keeps your data with it:

| File          | Purpose                     |
|---------------|-----------------------------|
| `diary.db`    | SQLite database of notes    |
| `settings.json` | persisted app settings (theme, language) |

Both files are created automatically on first run. To back up your diary, copy
`diary.db`. To port your appearance and language too, copy `settings.json` as well.

> Note: because data is written to the app folder, install to a user-writable
> location (for example a folder under your profile) rather than `Program Files`.

## License

All rights reserved.