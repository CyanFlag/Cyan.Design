using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace Cyan.Design.Core.Converters;

/// <summary>Thickness 相关值转换器</summary>
public static class ThicknessConverters
{
    /// <summary>取 Thickness 四边最大值的转换器</summary>
    public static readonly IValueConverter ToLargest = new ToLargestConverter();

    private sealed class ToLargestConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Thickness t)
            {
                return Math.Max(Math.Max(t.Left, t.Right), Math.Max(t.Top, t.Bottom));
            }
            return 0d;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
