using Diary.Themes;

namespace Diary.Models;

public class AppSettings
{
    public ThemeKind AppTheme { get; set; } = ThemeKind.Light;
}