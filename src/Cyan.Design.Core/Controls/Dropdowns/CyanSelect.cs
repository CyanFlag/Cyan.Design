using Avalonia;
using Avalonia.Controls;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Inputs;

namespace Cyan.Design.Core.Controls.Dropdowns;

/// <summary>选择器控件，基于下拉组合框扩展</summary>
public class CyanSelect : ComboBox
{
    /// <summary>输入尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> InputSizeProperty =
        AvaloniaProperty.Register<CyanSelect, ControlSize>(nameof(InputSize), ControlSize.Middle);

    /// <summary>输入状态样式属性</summary>
    public static readonly StyledProperty<InputStatus> InputStatusProperty =
        AvaloniaProperty.Register<CyanSelect, InputStatus>(nameof(InputStatus), InputStatus.Default);

    /// <summary>输入尺寸</summary>
    public ControlSize InputSize
    {
        get => GetValue(InputSizeProperty);
        set => SetValue(InputSizeProperty, value);
    }

    /// <summary>输入状态</summary>
    public InputStatus InputStatus
    {
        get => GetValue(InputStatusProperty);
        set => SetValue(InputStatusProperty, value);
    }
}
