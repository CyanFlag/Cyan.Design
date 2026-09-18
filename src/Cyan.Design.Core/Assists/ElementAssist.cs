using Avalonia;
using Avalonia.Controls;

namespace Cyan.Design.Core.Assists;

/// <summary>元素附加属性辅助类</summary>
public static class ElementAssist
{
    /// <summary>加载时自动获取焦点的附加属性</summary>
    public static readonly AttachedProperty<bool> FocusOnLoadProperty =
        AvaloniaProperty.RegisterAttached<Control, bool>("FocusOnLoad", typeof(ElementAssist));

    /// <summary>获取控件是否在加载时自动获取焦点</summary>
    public static bool GetFocusOnLoad(Control control) => control.GetValue(FocusOnLoadProperty);
    /// <summary>设置控件是否在加载时自动获取焦点</summary>
    public static void SetFocusOnLoad(Control control, bool value) => control.SetValue(FocusOnLoadProperty, value);
}
