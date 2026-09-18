using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Selections;

/// <summary>单选按钮组控件，用于在多个选项中进行单选</summary>
public class CyanRadioGroup : StackPanel
{
    private static int _groupCounter;
    private readonly string _groupName = $"CyanRadioGroup_{++_groupCounter}";

    /// <summary>当前选中值样式属性</summary>
    public static readonly StyledProperty<object?> ValueProperty =
        AvaloniaProperty.Register<CyanRadioGroup, object?>(nameof(Value), defaultBindingMode: BindingMode.TwoWay);

    /// <summary>默认值样式属性</summary>
    public static readonly StyledProperty<object?> DefaultValueProperty =
        AvaloniaProperty.Register<CyanRadioGroup, object?>(nameof(DefaultValue));

    /// <summary>是否禁用样式属性</summary>
    public static readonly StyledProperty<bool> DisabledProperty =
        AvaloniaProperty.Register<CyanRadioGroup, bool>(nameof(Disabled));

    /// <summary>选项类型样式属性</summary>
    public static readonly StyledProperty<RadioOptionType> OptionTypeProperty =
        AvaloniaProperty.Register<CyanRadioGroup, RadioOptionType>(nameof(OptionType), RadioOptionType.Default);

    /// <summary>按钮样式样式属性</summary>
    public static readonly StyledProperty<RadioButtonStyle> ButtonStyleProperty =
        AvaloniaProperty.Register<CyanRadioGroup, RadioButtonStyle>(nameof(ButtonStyle), RadioButtonStyle.Outline);

    /// <summary>尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> SizeProperty =
        AvaloniaProperty.Register<CyanRadioGroup, ControlSize>(nameof(Size), ControlSize.Middle);

    /// <summary>是否块级布局样式属性</summary>
    public static readonly StyledProperty<bool> BlockProperty =
        AvaloniaProperty.Register<CyanRadioGroup, bool>(nameof(Block));

    /// <summary>值变更路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ValueChangedEvent =
        RoutedEvent.Register<CyanRadioGroup, RoutedEventArgs>(nameof(ValueChanged), RoutingStrategies.Bubble);

    static CyanRadioGroup()
    {
        ValueProperty.Changed.AddClassHandler<CyanRadioGroup>(OnValueChanged);
        DisabledProperty.Changed.AddClassHandler<CyanRadioGroup>((g, _) => g.SyncChildren());
        OptionTypeProperty.Changed.AddClassHandler<CyanRadioGroup>((g, _) => g.SyncChildren());
        ButtonStyleProperty.Changed.AddClassHandler<CyanRadioGroup>((g, _) => g.SyncChildren());
        SizeProperty.Changed.AddClassHandler<CyanRadioGroup>((g, _) => g.SyncChildren());
        BlockProperty.Changed.AddClassHandler<CyanRadioGroup>((g, _) => g.UpdatePseudoClasses());
    }

    /// <summary>当前选中值</summary>
    public object? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>默认值</summary>
    public object? DefaultValue
    {
        get => GetValue(DefaultValueProperty);
        set => SetValue(DefaultValueProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool Disabled
    {
        get => GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    /// <summary>选项类型，默认或按钮样式</summary>
    public RadioOptionType OptionType
    {
        get => GetValue(OptionTypeProperty);
        set => SetValue(OptionTypeProperty, value);
    }

    /// <summary>按钮样式，描边或实心</summary>
    public RadioButtonStyle ButtonStyle
    {
        get => GetValue(ButtonStyleProperty);
        set => SetValue(ButtonStyleProperty, value);
    }

    /// <summary>尺寸</summary>
    public ControlSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>是否块级布局，撑满父容器</summary>
    public bool Block
    {
        get => GetValue(BlockProperty);
        set => SetValue(BlockProperty, value);
    }

    /// <summary>值变更事件</summary>
    public event EventHandler<RoutedEventArgs>? ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    /// <summary>附加到逻辑树时处理</summary>
    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnAttachedToLogicalTree(e);
        if (DefaultValue != null && Value == null)
            Value = DefaultValue;
        SyncChildren();
        UpdateValueFromChildren();
        UpdatePseudoClasses();
    }

    /// <summary>从逻辑树分离时处理</summary>
    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromLogicalTree(e);
    }

    internal void OnRadioChecked(CyanRadio radio)
    {
        var newVal = radio.Value;
        if (!ValuesEqual(Value, newVal))
            Value = newVal;
    }

    private static void OnValueChanged(CyanRadioGroup group, AvaloniaPropertyChangedEventArgs e)
    {
        group.SyncCheckedState();
        group.RaiseEvent(new RoutedEventArgs(ValueChangedEvent));
    }

    private void SyncChildren()
    {
        var radios = GetRadioChildren().ToList();
        for (var i = 0; i < radios.Count; i++)
        {
            var child = radios[i];
            child.GroupName = _groupName;
            if (Disabled)
                child.IsEnabled = false;
            child.OptionType = OptionType;
            child.ButtonStyle = ButtonStyle;
            child.RadioSize = Size;
            child.SetPositionPseudoClasses(i == 0, i == radios.Count - 1);
        }
    }

    private void SyncCheckedState()
    {
        foreach (var child in GetRadioChildren())
        {
            child.IsChecked = ValuesEqual(child.Value, Value);
        }
    }

    private void UpdateValueFromChildren()
    {
        foreach (var child in GetRadioChildren())
        {
            if (child.IsChecked == true)
            {
                Value = child.Value;
                return;
            }
        }
        if (Value != null)
            SyncCheckedState();
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":block", Block);
        PseudoClasses.Set(":button", OptionType == RadioOptionType.Button);
        PseudoClasses.Set(":solid", ButtonStyle == RadioButtonStyle.Solid);
    }

    private IEnumerable<CyanRadio> GetRadioChildren()
    {
        foreach (var child in Children)
        {
            if (child is CyanRadio radio)
                yield return radio;
        }
    }

    private static bool ValuesEqual(object? a, object? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b) || a.ToString() == b.ToString();
    }
}
