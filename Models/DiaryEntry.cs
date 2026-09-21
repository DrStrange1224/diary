using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Diary.Models;

public class DiaryEntry : INotifyPropertyChanged
{
    private string _title = string.Empty;
    private string _content = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Date { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; } = DateTime.Now;

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

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}