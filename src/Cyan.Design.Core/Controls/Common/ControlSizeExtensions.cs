namespace Cyan.Design.Core.Controls.Common;

/// <summary>
/// 提供 ControlSize 到具体度量值的映射，消除控件与样式中重复的 switch 分支。
/// 度量值与 AntDesign 5.x 规范保持一致。
/// </summary>
public static class ControlSizeExtensions
{
    /// <summary>
    /// 将 ControlSize 映射为控件高度（像素）。
    /// </summary>
    /// <param name="size">控件尺寸。</param>
    /// <returns>Large=40, Middle=32, Small=24。</returns>
    public static double ToControlHeight(this ControlSize size) => size switch
    {
        ControlSize.Large => 40,
        ControlSize.Middle => 32,
        ControlSize.Small => 24,
        _ => 32
    };

    /// <summary>
    /// 将 ControlSize 映射为字号（像素）。
    /// </summary>
    /// <param name="size">控件尺寸。</param>
    /// <returns>Large=16, Middle=14, Small=14。</returns>
    public static double ToFontSize(this ControlSize size) => size switch
    {
        ControlSize.Large => 16,
        ControlSize.Middle => 14,
        ControlSize.Small => 14,
        _ => 14
    };

    /// <summary>
    /// 将 ControlSize 映射为垂直内边距（像素）。
    /// </summary>
    /// <param name="size">控件尺寸。</param>
    /// <returns>Large=7, Middle=4, Small=1。</returns>
    public static double ToPadding(this ControlSize size) => size switch
    {
        ControlSize.Large => 7,
        ControlSize.Middle => 4,
        ControlSize.Small => 1,
        _ => 4
    };
}
