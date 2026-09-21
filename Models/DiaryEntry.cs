using System.ComponentModel;
using System.Runtime.CompilerServices;
using Diary.Services;

namespace Diary.Models;

public class DiaryEntry : INotifyPropertyChanged
{
    private string _title = string.Empty;
    private string _content = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Date { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string Title
    {
        get => _title;
        set
        {
            if (_title == value) return;
            _title = value;
            OnPropertyChanged();
        }
    }

    public string Content
    {
        get => _content;
        set
        {
            if (_content == value) return;
            _content = value;
            OnPropertyChanged();
        }
    }

    public DateTime Day => Date.Date;
    public string DayLabel
    {
        get
        {
            var formatted = Date.ToString("dddd, MMMM d", Localizer.Culture);
            return char.ToUpperInvariant(formatted[0]) + formatted.Substring(1);
        }
    }
    public string CreatedLabel =>
        Localizer.Get("Created") + " " + CreatedAt.ToString("dd MMM yyyy, HH:mm", Localizer.Culture);

    public void RefreshLabels()
    {
        OnPropertyChanged(nameof(DayLabel));
        OnPropertyChanged(nameof(CreatedLabel));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}