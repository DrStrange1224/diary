using System;
using System.Windows;
using Diary.Models;
using Diary.Services;
using Diary.Themes;

namespace Diary;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var settings = AppSettingsService.Load();
        Localizer.SetLanguage(settings.Language);
        ThemeManager.Apply(settings.AppTheme);

        Localizer.Changed += SaveSettings;
        ThemeManager.ThemeChanged += SaveSettings;
    }

    private static void SaveSettings() =>
        AppSettingsService.Save(new AppSettings
        {
            AppTheme = ThemeManager.Current,
            Language = Localizer.Current
        });
}