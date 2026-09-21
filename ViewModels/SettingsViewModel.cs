using Diary.Models;
using Diary.Services;
using Diary.Themes;

namespace Diary.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private bool _isDarkTheme = ThemeManager.Current == ThemeKind.Dark;

    public AppStrings Strings => AppStrings.Instance;

    public IReadOnlyList<Language> Languages { get; } =
        new[] { Language.English, Language.Russian };

    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set
        {
            if (SetProperty(ref _isDarkTheme, value))
                ThemeManager.Apply(value ? ThemeKind.Dark : ThemeKind.Light);
        }
    }

    public Language SelectedLanguage
    {
        get => Localizer.Current;
        set => Localizer.SetLanguage(value);
    }
}