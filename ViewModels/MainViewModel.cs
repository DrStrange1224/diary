using System.Collections.ObjectModel;
using Diary.Models;

namespace Diary.ViewModels;

public class MainViewModel : ViewModelBase
{
    private DiaryEntry? _selectedEntry;
    private DateTime _selectedDate = DateTime.Today;

    public ObservableCollection<DiaryEntry> Entries { get; } = new();

    public DiaryEntry? SelectedEntry
    {
        get => _selectedEntry;
        set => SetProperty(ref _selectedEntry, value);
    }

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => SetProperty(ref _selectedDate, value);
    }

    public MainViewModel()
    {
        var today = DateTime.Today;
        Entries.Add(new DiaryEntry { Date = today.AddHours(9), Title = "Morning run", Content = "5 km along the river." });
        Entries.Add(new DiaryEntry { Date = today.SetTime(18, 30), Title = "Grocery list", Content = "Milk, bread, eggs." });
        Entries.Add(new DiaryEntry { Date = today.AddDays(-1).AddHours(15), Title = "Project sync notes", Content = "Discussed roadmap for Q4." });
        Entries.Add(new DiaryEntry { Date = today.AddDays(-1).SetTime(21, 0), Title = "Dinner with family", Content = "Great evening." });
        Entries.Add(new DiaryEntry { Date = today.AddDays(-2).AddHours(12), Title = "Book: Atomic Habits", Content = "Finished chapter 4." });
    }
}

internal static class DateTimeExtensions
{
    public static DateTime SetTime(this DateTime date, int hour, int minute)
        => date.Date.Add(new TimeSpan(hour, minute, 0));
}