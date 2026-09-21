using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Diary.Commands;
using Diary.Models;
using Diary.Themes;

namespace Diary.ViewModels;

public class MainViewModel : ViewModelBase
{
    private DiaryEntry? _selectedEntry;
    private DateTime _selectedDate = DateTime.Today;
    private DateTime _displayedMonth = new(DateTime.Today.Year, DateTime.Today.Month, 1);

    public ObservableCollection<DiaryEntry> Entries { get; } = new();
    public ObservableCollection<DayCell> Days { get; } = new();
    public IReadOnlyList<string> DayHeaders { get; }

    public DiaryEntry? SelectedEntry
    {
        get => _selectedEntry;
        set
        {
            if (SetProperty(ref _selectedEntry, value))
            {
                OnPropertyChanged(nameof(HasSelection));
            }
        }
    }

    public bool HasSelection => SelectedEntry is not null;

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => SetProperty(ref _selectedDate, value);
    }

    public string MonthTitle => _displayedMonth.ToString("MMMM yyyy");

    public ICommand CloseEntryCommand { get; }
    public ICommand SelectDateCommand { get; }
    public ICommand OpenSettingsCommand { get; }
    public ICommand AddEntryCommand { get; }

    public MainViewModel()
    {
        ThemeManager.ThemeChanged += OnThemeChanged;
        var dateTimeFormat = DateTimeFormatInfo.CurrentInfo;
        var names = dateTimeFormat.ShortestDayNames;
        DayHeaders = Enumerable.Range(0, 7)
            .Select(i => names[(((int)dateTimeFormat.FirstDayOfWeek) + i) % 7])
            .ToList();

        CloseEntryCommand = new RelayCommand(_ => SelectedEntry = null);
        OpenSettingsCommand = new RelayCommand(_ =>
        {
            var window = new SettingsWindow { Owner = Application.Current.MainWindow };
            window.ShowDialog();
        });
        SelectDateCommand = new RelayCommand(p =>
        {
            if (p is DateTime date)
                SelectedDate = date.Date;
        });
        AddEntryCommand = new RelayCommand(_ =>
        {
            var entry = new DiaryEntry { Date = DateTime.Now, Title = "New Note", Content = "Write here..." };
            Entries.Add(entry);
            SelectedDate = entry.Date.Date;
            SelectedEntry = entry;
            RefreshDays();
        });

        Entries.Add(new DiaryEntry { Date = new DateTime(2026, 9, 21, 9, 0, 0), Title = "Morning run", Content = "5 km along the river." });
        Entries.Add(new DiaryEntry { Date = new DateTime(2026, 9, 21, 18, 30, 0), Title = "Grocery list", Content = "Milk, bread, eggs." });
        Entries.Add(new DiaryEntry { Date = new DateTime(2026, 9, 20, 15, 0, 0), Title = "Project sync notes", Content = "Discussed roadmap for Q4." });
        Entries.Add(new DiaryEntry { Date = new DateTime(2026, 9, 20, 21, 0, 0), Title = "Dinner with family", Content = "Great evening." });
        Entries.Add(new DiaryEntry { Date = new DateTime(2026, 9, 19, 12, 0, 0), Title = "Book: Atomic Habits", Content = "Finished chapter 4." });

        ReloadMonth();
    }

    private void OnThemeChanged()
    {
        RefreshDays();
    }

    private void RefreshDays()
    {
        var cells = Days.ToList();
        Days.Clear();
        foreach (var cell in cells)
            Days.Add(cell);
    }

    private void ReloadMonth()
    {
        Days.Clear();

        var firstDayOfWeek = DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;
        var offset = ((int)_displayedMonth.DayOfWeek - (int)firstDayOfWeek + 7) % 7;
        var start = _displayedMonth.AddDays(-offset);

        var counts = Entries
            .GroupBy(e => e.Date.Date)
            .ToDictionary(g => g.Key, g => g.Count());

        for (var i = 0; i < 42; i++)
        {
            var date = start.AddDays(i);
            Days.Add(new DayCell
            {
                Date = date,
                IsCurrentMonth = date.Month == _displayedMonth.Month,
                IsToday = date.Date == DateTime.Today,
                NoteCount = counts.TryGetValue(date.Date, out var count) ? count : 0
            });
        }

        OnPropertyChanged(nameof(MonthTitle));
    }
}