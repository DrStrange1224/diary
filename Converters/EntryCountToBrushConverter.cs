using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Diary.Converters;

public class EntryCountToBrushConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var count = value is int n ? n : 0;
        return count switch
        {
            0 => Brushes.Transparent,
            1 => new SolidColorBrush(Color.FromRgb(0xFF, 0xF4, 0x9E)),
            2 => new SolidColorBrush(Color.FromRgb(0xFF, 0xEB, 0x3B)),
            3 => new SolidColorBrush(Color.FromRgb(0x9C, 0xCC, 0x65)),
            4 => new SolidColorBrush(Color.FromRgb(0x66, 0xBB, 0x6A)),
            5 => new SolidColorBrush(Color.FromRgb(0x38, 0x8E, 0x3C)),
            _ => new SolidColorBrush(Color.FromRgb(0x1B, 0x5E, 0x20))
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}