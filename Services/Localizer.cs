using System.Globalization;
using Diary.Models;

namespace Diary.Services;

public static class Localizer
{
    public static event Action? Changed;

    private static Language _current = Language.English;

    public static Language Current => _current;

    public static void SetLanguage(Language language)
    {
        if (_current == language)
            return;

        _current = language;
        Changed?.Invoke();
    }

    public static CultureInfo Culture =>
        Current == Language.Russian
            ? CultureInfo.GetCultureInfo("ru-RU")
            : CultureInfo.GetCultureInfo("en-US");

    public static string Get(string key)
    {
        var table = Current == Language.Russian ? Russian : English;
        return table.TryGetValue(key, out var value) ? value : key;
    }

    private static readonly IReadOnlyDictionary<string, string> English = new Dictionary<string, string>
    {
        ["AddNote"] = "Add note",
        ["Settings"] = "Settings",
        ["CloseEntry"] = "Close entry",
        ["AppTheme"] = "App theme",
        ["Language"] = "Language",
        ["Light"] = "Light",
        ["Dark"] = "Dark",
        ["DefaultTitle"] = "New Note",
        ["DefaultContent"] = "Write here...",
        ["Created"] = "Created"
    };

    private static readonly IReadOnlyDictionary<string, string> Russian = new Dictionary<string, string>
    {
        ["AddNote"] = "Добавить заметку",
        ["Settings"] = "Настройки",
        ["CloseEntry"] = "Закрыть",
        ["AppTheme"] = "Тема",
        ["Language"] = "Язык",
        ["Light"] = "Светлая",
        ["Dark"] = "Тёмная",
        ["DefaultTitle"] = "Новая заметка",
        ["DefaultContent"] = "Напишите здесь...",
        ["Created"] = "Создано"
    };
}