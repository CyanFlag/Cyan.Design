namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>气泡确认框位置</summary>
public enum PopconfirmPlacement
{
    /// <summary>顶部</summary>
    Top,
    /// <summary>左上</summary>
    TopLeft,
    /// <summary>右上</summary>
    TopRight,
    /// <summary>底部</summary>
    Bottom,
    /// <summary>左下</summary>
    BottomLeft,
    /// <summary>右下</summary>
    BottomRight,
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

/// <summary>气泡确认框触发方式</summary>
public enum PopconfirmTrigger
{
    /// <summary>点击触发</summary>
    Click,
    /// <summary>悬停触发</summary>
    Hover
}
