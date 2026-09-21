using System.Windows;

namespace Diary.Themes;

public static class ThemeManager
{
    public static event Action? ThemeChanged;

    public static ThemeKind Current { get; private set; } = ThemeKind.Light;

    public static void Apply(ThemeKind kind)
    {
        Current = kind;

        var fileName = kind == ThemeKind.Light ? "Light.xaml" : "Dark.xaml";
        var theme = new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Themes/" + fileName, UriKind.Absolute)
        };

        var merged = Application.Current.Resources.MergedDictionaries;
        merged.Clear();
        merged.Add(theme);

        if (merged.Count > 0)
            merged[0] = merged[0];

        ThemeChanged?.Invoke();
    }
}