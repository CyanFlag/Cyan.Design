using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Selections;

/// <summary>单选按钮控件，通常作为单选按钮组的子项</summary>
public class CyanRadio : RadioButton
{
    /// <summary>选项值样式属性</summary>
    public static readonly StyledProperty<object?> ValueProperty =
        AvaloniaProperty.Register<CyanRadio, object?>(nameof(Value));

    /// <summary>选项类型样式属性</summary>
    public static readonly StyledProperty<RadioOptionType> OptionTypeProperty =
        AvaloniaProperty.Register<CyanRadio, RadioOptionType>(nameof(OptionType), RadioOptionType.Default);

    /// <summary>尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> RadioSizeProperty =
        AvaloniaProperty.Register<CyanRadio, ControlSize>(nameof(RadioSize), ControlSize.Middle);

    /// <summary>按钮样式样式属性</summary>
    public static readonly StyledProperty<RadioButtonStyle> ButtonStyleProperty =
        AvaloniaProperty.Register<CyanRadio, RadioButtonStyle>(nameof(ButtonStyle), RadioButtonStyle.Outline);

    static CyanRadio()
    {
        IsCheckedProperty.Changed.AddClassHandler<CyanRadio>(OnIsCheckedChanged);
        OptionTypeProperty.Changed.AddClassHandler<CyanRadio>((r, _) => r.UpdatePseudoClasses());
        ButtonStyleProperty.Changed.AddClassHandler<CyanRadio>((r, _) => r.UpdatePseudoClasses());
    }

    /// <summary>选项值</summary>
    public object? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>选项类型，默认或按钮样式</summary>
    public RadioOptionType OptionType
    {
        get => GetValue(OptionTypeProperty);
        set => SetValue(OptionTypeProperty, value);
    }

    /// <summary>尺寸</summary>
    public ControlSize RadioSize
    {
        get => GetValue(RadioSizeProperty);
        set => SetValue(RadioSizeProperty, value);
    }

    /// <summary>按钮样式，描边或实心</summary>
    public RadioButtonStyle ButtonStyle
    {
        get => GetValue(ButtonStyleProperty);
        set => SetValue(ButtonStyleProperty, value);
    }

    private static void OnIsCheckedChanged(CyanRadio radio, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is true && radio.Parent is CyanRadioGroup group)
            group.OnRadioChecked(radio);
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":button", OptionType == RadioOptionType.Button);
        PseudoClasses.Set(":solid", ButtonStyle == RadioButtonStyle.Solid);
    }

    internal void SetPositionPseudoClasses(bool isFirst, bool isLast)
    {
        PseudoClasses.Set(":first", isFirst);
        PseudoClasses.Set(":last", isLast);
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(Avalonia.VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdatePseudoClasses();
    }
}
