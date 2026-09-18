using Avalonia;
using Avalonia.Controls;

namespace Cyan.Design.Core.Controls.Icons;

/// <summary>图标控件，根据图标键名显示对应的图标字符</summary>
public class CyanIcon : TextBlock
{
    /// <summary>图标键名样式属性</summary>
    public static readonly StyledProperty<string?> IconKeyProperty =
        AvaloniaProperty.Register<CyanIcon, string?>(nameof(IconKey));

    static CyanIcon()
    {
        IconKeyProperty.Changed.AddClassHandler<CyanIcon>((c, e) =>
        {
            var key = e.NewValue as string;
            c.Text = string.IsNullOrEmpty(key) ? null : CyanIcons.Get(key);
        });
    }

    /// <summary>图标键名，用于指定要显示的图标</summary>
    public string? IconKey
    {
        get => GetValue(IconKeyProperty);
        set => SetValue(IconKeyProperty, value);
    }
}
