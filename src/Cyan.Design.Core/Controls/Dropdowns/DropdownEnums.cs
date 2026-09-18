namespace Cyan.Design.Core.Controls.Dropdowns;

/// <summary>下拉菜单触发方式</summary>
public enum DropdownTrigger
{
    /// <summary>悬停触发</summary>
    Hover,
    /// <summary>点击触发</summary>
    Click,
    /// <summary>右键菜单触发</summary>
    ContextMenu
}

/// <summary>下拉菜单位置</summary>
public enum DropdownPlacement
{
    /// <summary>左下</summary>
    BottomLeft,
    /// <summary>底部</summary>
    Bottom,
    /// <summary>右下</summary>
    BottomRight,
    /// <summary>左上</summary>
    TopLeft,
    /// <summary>顶部</summary>
    Top,
    /// <summary>右上</summary>
    TopRight,
    /// <summary>左侧</summary>
    Left,
    /// <summary>左上</summary>
    LeftTop,
    /// <summary>左下</summary>
    LeftBottom,
    /// <summary>右侧</summary>
    Right,
    /// <summary>右上</summary>
    RightTop,
    /// <summary>右下</summary>
    RightBottom
}
