using Diary.Themes;

namespace Diary.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private bool _isDarkTheme = ThemeManager.Current == ThemeKind.Dark;

    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set
        {
            if (SetProperty(ref _isDarkTheme, value))
                ThemeManager.Apply(value ? ThemeKind.Dark : ThemeKind.Light);
        }
    }
}