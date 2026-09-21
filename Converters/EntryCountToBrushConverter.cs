using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Diary.Converters;

public class EntryCountToBrushConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var count = value is int n ? n : 0;
        if (count <= 0)
            return Brushes.Transparent;

        var key = count switch
        {
            1 => "Level1",
            2 => "Level2",
            3 => "Level3",
            4 => "Level4",
            5 => "Level5",
            _ => "Level6"
        };

        return Application.Current?.TryFindResource(key) as Brush ?? Brushes.Transparent;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}