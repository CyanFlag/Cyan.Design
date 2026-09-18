using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Cyan.Design.Core.Converters;

/// <summary>字符串相关值转换器</summary>
public static class StringConverters
{
    /// <summary>判断值与参数字符串是否相等的转换器</summary>
    public static readonly IValueConverter IsMatch = new IsMatchConverter();

    private sealed class IsMatchConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var v = value as string ?? string.Empty;
            var p = parameter as string ?? string.Empty;
            return string.Equals(v, p, StringComparison.Ordinal);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
