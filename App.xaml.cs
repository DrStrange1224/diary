using System;
using System.Configuration;
using System.Data;
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
        ThemeManager.Apply(settings.AppTheme);
        ThemeManager.ThemeChanged += () =>
            AppSettingsService.Save(new AppSettings { AppTheme = ThemeManager.Current });
    }
}