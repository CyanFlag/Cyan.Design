using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace Cyan.Design.Core.Controls.Layout;

/// <summary>分隔线控件，支持水平/垂直方向、虚线及对齐方式</summary>
public class CyanDivider : ContentControl
{
    /// <summary>方向样式属性</summary>
    public static readonly StyledProperty<Orientation> OrientationProperty =
        AvaloniaProperty.Register<CyanDivider, Orientation>(nameof(Orientation), Orientation.Horizontal);

    /// <summary>是否虚线样式属性</summary>
    public static readonly StyledProperty<bool> DashedProperty =
        AvaloniaProperty.Register<CyanDivider, bool>(nameof(Dashed));

    /// <summary>对齐方式样式属性</summary>
    public static readonly StyledProperty<DividerAlign> AlignProperty =
        AvaloniaProperty.Register<CyanDivider, DividerAlign>(nameof(Align), DividerAlign.Center);

    /// <summary>分隔线方向</summary>
    public Orientation Orientation
    {
        get => GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>是否以虚线显示</summary>
    public bool Dashed
    {
        get => GetValue(DashedProperty);
        set => SetValue(DashedProperty, value);
    }

    /// <summary>内嵌文本的对齐方式</summary>
    public DividerAlign Align
    {
        get => GetValue(AlignProperty);
        set => SetValue(AlignProperty, value);
    }
}
