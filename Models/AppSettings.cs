using Diary.Themes;

namespace Diary.Models;

public class AppSettings
{
    public ThemeKind AppTheme { get; set; } = ThemeKind.Light;
    public Language Language { get; set; } = Language.English;
}