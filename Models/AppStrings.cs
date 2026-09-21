using System.ComponentModel;
using System.Runtime.CompilerServices;
using Diary.Services;

namespace Diary.Models;

public sealed class AppStrings : INotifyPropertyChanged
{
    public static AppStrings Instance { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    private AppStrings()
    {
        Localizer.Changed += NotifyAll;
    }

    public string AddNote => Localizer.Get("AddNote");
    public string Settings => Localizer.Get("Settings");
    public string CloseEntry => Localizer.Get("CloseEntry");
    public string AppTheme => Localizer.Get("AppTheme");
    public string Language => Localizer.Get("Language");
    public string Light => Localizer.Get("Light");
    public string Dark => Localizer.Get("Dark");
    public string DefaultTitle => Localizer.Get("DefaultTitle");
    public string DefaultContent => Localizer.Get("DefaultContent");

    private void NotifyAll()
    {
        OnPropertyChanged(nameof(AddNote));
        OnPropertyChanged(nameof(Settings));
        OnPropertyChanged(nameof(CloseEntry));
        OnPropertyChanged(nameof(AppTheme));
        OnPropertyChanged(nameof(Language));
        OnPropertyChanged(nameof(Light));
        OnPropertyChanged(nameof(Dark));
        OnPropertyChanged(nameof(DefaultTitle));
        OnPropertyChanged(nameof(DefaultContent));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}