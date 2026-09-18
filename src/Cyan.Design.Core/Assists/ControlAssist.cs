using Avalonia;
using Avalonia.Controls;

namespace Cyan.Design.Core.Assists;

/// <summary>控件附加属性辅助类，提供标签、提示、高度等附加属性</summary>
public static class ControlAssist
{
    /// <summary>标签附加属性</summary>
    public static readonly AttachedProperty<string> LabelProperty =
        AvaloniaProperty.RegisterAttached<Control, string>("Label", typeof(ControlAssist));

    /// <summary>提示文本附加属性</summary>
    public static readonly AttachedProperty<string> HintProperty =
        AvaloniaProperty.RegisterAttached<Control, string>("Hint", typeof(ControlAssist));

    /// <summary>最小高度附加属性</summary>
    public static readonly AttachedProperty<double> MinHeightProperty =
        AvaloniaProperty.RegisterAttached<Control, double>("MinHeight", typeof(ControlAssist));

    /// <summary>高度附加属性</summary>
    public static readonly AttachedProperty<double> HeightProperty =
        AvaloniaProperty.RegisterAttached<Control, double>("Height", typeof(ControlAssist));

    /// <summary>获取控件的标签</summary>
    public static string GetLabel(Control control) => control.GetValue(LabelProperty);
    /// <summary>设置控件的标签</summary>
    public static void SetLabel(Control control, string value) => control.SetValue(LabelProperty, value);

    /// <summary>获取控件的提示文本</summary>
    public static string GetHint(Control control) => control.GetValue(HintProperty);
    /// <summary>设置控件的提示文本</summary>
    public static void SetHint(Control control, string value) => control.SetValue(HintProperty, value);

    /// <summary>获取控件的最小高度</summary>
    public static double GetMinHeight(Control control) => control.GetValue(MinHeightProperty);
    /// <summary>设置控件的最小高度</summary>
    public static void SetMinHeight(Control control, double value) => control.SetValue(MinHeightProperty, value);

    /// <summary>获取控件的高度</summary>
    public static double GetHeight(Control control) => control.GetValue(HeightProperty);
    /// <summary>设置控件的高度</summary>
    public static void SetHeight(Control control, double value) => control.SetValue(HeightProperty, value);
}
