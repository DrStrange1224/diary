using System.Globalization;
using System.Windows.Data;
using Diary.Models;

namespace Diary.Converters;

public class LanguageToNameConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var language = value is Language l ? l : Language.English;
        return language == Language.Russian ? "Русский" : "English";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}