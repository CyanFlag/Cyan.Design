using Avalonia;
using Avalonia.Controls;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Layout;

/// <summary>间距控件，用于在组件之间设置水平和垂直间距</summary>
public class CyanSpace : StackPanel
{
    /// <summary>排列方向样式属性</summary>
    public static readonly StyledProperty<SpaceDirection> DirectionProperty =
        AvaloniaProperty.Register<CyanSpace, SpaceDirection>(nameof(Direction), SpaceDirection.Horizontal);

    /// <summary>间距大小样式属性</summary>
    public static readonly StyledProperty<ControlSize> SpaceSizeProperty =
        AvaloniaProperty.Register<CyanSpace, ControlSize>(nameof(SpaceSize), ControlSize.Middle);

    /// <summary>排列方向</summary>
    public SpaceDirection Direction
    {
        get => GetValue(DirectionProperty);
        set => SetValue(DirectionProperty, value);
    }

    /// <summary>间距大小</summary>
    public ControlSize SpaceSize
    {
        get => GetValue(SpaceSizeProperty);
        set => SetValue(SpaceSizeProperty, value);
    }
}
