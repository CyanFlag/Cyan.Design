using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Inputs;

/// <summary>数字输入框步进事件参数</summary>
public class StepEventArgs : RoutedEventArgs
{
    /// <summary>步进偏移量</summary>
    public double Offset { get; }
    /// <summary>步进类型（增加或减少）</summary>
    public StepType Type { get; }
    /// <summary>步进触发源</summary>
    public StepEmitter Emitter { get; }
    /// <summary>当前数值</summary>
    public double? Value { get; }

    /// <summary>构造步进事件参数</summary>
    public StepEventArgs(double offset, StepType type, StepEmitter emitter, double? value, RoutedEvent routedEvent)
        : base(routedEvent)
    {
        Offset = offset;
        Type = type;
        Emitter = emitter;
        Value = value;
    }
}

/// <summary>数字输入框控件，支持步进、范围限制和精度控制</summary>
public class CyanInputNumber : TextBox
{
    /// <summary>当前数值样式属性</summary>
    public static readonly StyledProperty<double?> ValueProperty =
        AvaloniaProperty.Register<CyanInputNumber, double?>(nameof(Value), defaultBindingMode: BindingMode.TwoWay);

    /// <summary>最小值样式属性</summary>
    public static readonly StyledProperty<double?> MinProperty =
        AvaloniaProperty.Register<CyanInputNumber, double?>(nameof(Min));

    /// <summary>最大值样式属性</summary>
    public static readonly StyledProperty<double?> MaxProperty =
        AvaloniaProperty.Register<CyanInputNumber, double?>(nameof(Max));

    /// <summary>步进值样式属性</summary>
    public static readonly StyledProperty<double> StepProperty =
        AvaloniaProperty.Register<CyanInputNumber, double>(nameof(Step), 1);

    /// <summary>数值精度样式属性</summary>
    public static readonly StyledProperty<int> PrecisionProperty =
        AvaloniaProperty.Register<CyanInputNumber, int>(nameof(Precision), -1);

    /// <summary>是否显示步进控件样式属性</summary>
    public static readonly StyledProperty<bool> ShowControlsProperty =
        AvaloniaProperty.Register<CyanInputNumber, bool>(nameof(ShowControls), true);

    /// <summary>是否启用键盘导航样式属性</summary>
    public static readonly StyledProperty<bool> KeyboardNavigationProperty =
        AvaloniaProperty.Register<CyanInputNumber, bool>(nameof(KeyboardNavigation), true);

    /// <summary>是否响应滚轮变化样式属性</summary>
    public static readonly StyledProperty<bool> ChangeOnWheelProperty =
        AvaloniaProperty.Register<CyanInputNumber, bool>(nameof(ChangeOnWheel));

    /// <summary>是否在失焦时提交变化样式属性</summary>
    public static readonly StyledProperty<bool> ChangeOnBlurProperty =
        AvaloniaProperty.Register<CyanInputNumber, bool>(nameof(ChangeOnBlur), true);

    /// <summary>小数分隔符样式属性</summary>
    public static readonly StyledProperty<string?> DecimalSeparatorProperty =
        AvaloniaProperty.Register<CyanInputNumber, string?>(nameof(DecimalSeparator));

    /// <summary>手柄可见性样式属性</summary>
    public static readonly StyledProperty<HandleVisible> HandleVisibleProperty =
        AvaloniaProperty.Register<CyanInputNumber, HandleVisible>(nameof(HandleVisible), HandleVisible.Always);

    /// <summary>控件模式样式属性</summary>
    public static readonly StyledProperty<NumberControlMode> ControlModeProperty =
        AvaloniaProperty.Register<CyanInputNumber, NumberControlMode>(nameof(ControlMode), NumberControlMode.Vertical);

    /// <summary>输入框尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> InputSizeProperty =
        AvaloniaProperty.Register<CyanInputNumber, ControlSize>(nameof(InputSize), ControlSize.Middle);

    /// <summary>输入框状态样式属性</summary>
    public static readonly StyledProperty<InputStatus> InputStatusProperty =
        AvaloniaProperty.Register<CyanInputNumber, InputStatus>(nameof(InputStatus), InputStatus.Default);

    /// <summary>输入框外观样式属性</summary>
    public static readonly StyledProperty<InputVariant> InputVariantProperty =
        AvaloniaProperty.Register<CyanInputNumber, InputVariant>(nameof(InputVariant), InputVariant.Outlined);

    /// <summary>前缀内容样式属性</summary>
    public static readonly StyledProperty<object?> PrefixProperty =
        AvaloniaProperty.Register<CyanInputNumber, object?>(nameof(Prefix));

    /// <summary>后缀内容样式属性</summary>
    public static readonly StyledProperty<object?> SuffixProperty =
        AvaloniaProperty.Register<CyanInputNumber, object?>(nameof(Suffix));

    /// <summary>值变更路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ValueChangedEvent =
        RoutedEvent.Register<CyanInputNumber, RoutedEventArgs>(nameof(ValueChanged), RoutingStrategies.Bubble);

    /// <summary>步进路由事件</summary>
    public static readonly RoutedEvent<StepEventArgs> SteppedEvent =
        RoutedEvent.Register<CyanInputNumber, StepEventArgs>(nameof(Stepped), RoutingStrategies.Bubble);

    /// <summary>按下回车路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> PressEnterEvent =
        RoutedEvent.Register<CyanInputNumber, RoutedEventArgs>(nameof(PressEnter), RoutingStrategies.Bubble);

    static CyanInputNumber()
    {
        ValueProperty.Changed.AddClassHandler<CyanInputNumber>(OnValueChanged);
        MinProperty.Changed.AddClassHandler<CyanInputNumber>((c, _) => c.UpdatePseudoClasses());
        MaxProperty.Changed.AddClassHandler<CyanInputNumber>((c, _) => c.UpdatePseudoClasses());
        ShowControlsProperty.Changed.AddClassHandler<CyanInputNumber>((c, _) => c.UpdatePseudoClasses());
        HandleVisibleProperty.Changed.AddClassHandler<CyanInputNumber>((c, _) => c.UpdatePseudoClasses());
    }

    /// <summary>当前数值</summary>
    public double? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>最小值</summary>
    public double? Min
    {
        get => GetValue(MinProperty);
        set => SetValue(MinProperty, value);
    }

    /// <summary>最大值</summary>
    public double? Max
    {
        get => GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }

    /// <summary>步进值</summary>
    public double Step
    {
        get => GetValue(StepProperty);
        set => SetValue(StepProperty, value);
    }

    /// <summary>数值精度（小数位数）</summary>
    public int Precision
    {
        get => GetValue(PrecisionProperty);
        set => SetValue(PrecisionProperty, value);
    }

    /// <summary>是否显示步进控件</summary>
    public bool ShowControls
    {
        get => GetValue(ShowControlsProperty);
        set => SetValue(ShowControlsProperty, value);
    }

    /// <summary>是否启用键盘导航（上下键步进）</summary>
    public bool KeyboardNavigation
    {
        get => GetValue(KeyboardNavigationProperty);
        set => SetValue(KeyboardNavigationProperty, value);
    }

    /// <summary>是否响应滚轮变化</summary>
    public bool ChangeOnWheel
    {
        get => GetValue(ChangeOnWheelProperty);
        set => SetValue(ChangeOnWheelProperty, value);
    }

    /// <summary>是否在失焦时提交变化</summary>
    public bool ChangeOnBlur
    {
        get => GetValue(ChangeOnBlurProperty);
        set => SetValue(ChangeOnBlurProperty, value);
    }

    /// <summary>小数分隔符</summary>
    public string? DecimalSeparator
    {
        get => GetValue(DecimalSeparatorProperty);
        set => SetValue(DecimalSeparatorProperty, value);
    }

    /// <summary>手柄可见性</summary>
    public HandleVisible HandleVisible
    {
        get => GetValue(HandleVisibleProperty);
        set => SetValue(HandleVisibleProperty, value);
    }

    /// <summary>控件模式（垂直或水平）</summary>
    public NumberControlMode ControlMode
    {
        get => GetValue(ControlModeProperty);
        set => SetValue(ControlModeProperty, value);
    }

    /// <summary>输入框尺寸</summary>
    public ControlSize InputSize
    {
        get => GetValue(InputSizeProperty);
        set => SetValue(InputSizeProperty, value);
    }

    /// <summary>输入框状态</summary>
    public InputStatus InputStatus
    {
        get => GetValue(InputStatusProperty);
        set => SetValue(InputStatusProperty, value);
    }

    /// <summary>输入框外观</summary>
    public InputVariant InputVariant
    {
        get => GetValue(InputVariantProperty);
        set => SetValue(InputVariantProperty, value);
    }

    /// <summary>前缀内容</summary>
    public object? Prefix
    {
        get => GetValue(PrefixProperty);
        set => SetValue(PrefixProperty, value);
    }

    /// <summary>后缀内容</summary>
    public object? Suffix
    {
        get => GetValue(SuffixProperty);
        set => SetValue(SuffixProperty, value);
    }

    /// <summary>数值格式化函数</summary>
    public Func<double?, string>? Formatter { get; set; }

    /// <summary>文本解析函数</summary>
    public Func<string, double?>? Parser { get; set; }

    /// <summary>值变更事件</summary>
    public event EventHandler<RoutedEventArgs>? ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    /// <summary>步进事件</summary>
    public event EventHandler<StepEventArgs>? Stepped
    {
        add => AddHandler(SteppedEvent, value);
        remove => RemoveHandler(SteppedEvent, value);
    }

    /// <summary>按下回车事件</summary>
    public event EventHandler<RoutedEventArgs>? PressEnter
    {
        add => AddHandler(PressEnterEvent, value);
        remove => RemoveHandler(PressEnterEvent, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        if (e.NameScope.Find<Button>("PART_UpButton") is { } upBtn)
            upBtn.Click += OnUpButtonClick;
        if (e.NameScope.Find<Button>("PART_DownButton") is { } downBtn)
            downBtn.Click += OnDownButtonClick;
        UpdatePseudoClasses();
        SyncTextFromValue();
    }

    /// <summary>键盘按下事件处理</summary>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (e.Handled || !KeyboardNavigation)
            return;
        if (e.Key == Key.Up)
        {
            StepValue(Step, StepType.Up, StepEmitter.KeyDown);
            e.Handled = true;
        }
        else if (e.Key == Key.Down)
        {
            StepValue(-Step, StepType.Down, StepEmitter.KeyDown);
            e.Handled = true;
        }
        else if (e.Key == Key.Enter)
        {
            CommitText();
            RaiseEvent(new RoutedEventArgs(PressEnterEvent));
            e.Handled = true;
        }
    }

    /// <summary>失去焦点事件处理</summary>
    protected override void OnLostFocus(FocusChangedEventArgs e)
    {
        base.OnLostFocus(e);
        if (ChangeOnBlur)
            CommitText();
    }

    /// <summary>指针滚轮变化事件处理</summary>
    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        base.OnPointerWheelChanged(e);
        if (e.Handled || !ChangeOnWheel)
            return;
        var delta = e.Delta.Y;
        if (delta > 0)
            StepValue(Step, StepType.Up, StepEmitter.Wheel);
        else if (delta < 0)
            StepValue(-Step, StepType.Down, StepEmitter.Wheel);
        e.Handled = true;
    }

    /// <summary>按步进值增加数值</summary>
    public void Increment()
        => StepValue(Step, StepType.Up, StepEmitter.Handler);

    /// <summary>按步进值减少数值</summary>
    public void Decrement()
        => StepValue(-Step, StepType.Down, StepEmitter.Handler);

    private void OnUpButtonClick(object? sender, RoutedEventArgs e)
        => Increment();

    private void OnDownButtonClick(object? sender, RoutedEventArgs e)
        => Decrement();

    private static void OnValueChanged(CyanInputNumber c, AvaloniaPropertyChangedEventArgs e)
    {
        c.SyncTextFromValue();
        c.UpdatePseudoClasses();
        c.RaiseEvent(new RoutedEventArgs(ValueChangedEvent));
    }

    private void StepValue(double offset, StepType type, StepEmitter emitter)
    {
        var current = Value ?? Min ?? 0;
        var newVal = current + offset;
        SetValueAndClamp(newVal);
        RaiseEvent(new StepEventArgs(offset, type, emitter, Value, SteppedEvent));
    }

    private void SetValueAndClamp(double val)
    {
        if (Min.HasValue && val < Min.Value)
            val = Min.Value;
        if (Max.HasValue && val > Max.Value)
            val = Max.Value;
        val = ApplyPrecision(val);
        Value = val;
    }

    private void CommitText()
    {
        if (string.IsNullOrWhiteSpace(Text))
        {
            Value = null;
            return;
        }
        var parsed = ParseText(Text);
        if (parsed.HasValue)
            SetValueAndClamp(parsed.Value);
        else
            SyncTextFromValue();
    }

    private double? ParseText(string text)
    {
        if (Parser != null)
            return Parser(text);
        var normalized = text;
        if (!string.IsNullOrEmpty(DecimalSeparator))
            normalized = normalized.Replace(DecimalSeparator, ".");
        if (double.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            return result;
        return null;
    }

    private void SyncTextFromValue()
    {
        if (Value.HasValue)
            Text = FormatValue(Value.Value);
        else
            Text = string.Empty;
    }

    private string FormatValue(double val)
    {
        if (Formatter != null)
            return Formatter(val);
        var p = Precision >= 0 ? Precision : 0;
        var formatted = val.ToString($"F{p}", CultureInfo.InvariantCulture);
        if (!string.IsNullOrEmpty(DecimalSeparator))
            formatted = formatted.Replace(".", DecimalSeparator);
        return formatted;
    }

    private double ApplyPrecision(double val)
    {
        if (Precision >= 0)
            return Math.Round(val, Precision);
        var stepStr = Step.ToString("G", CultureInfo.InvariantCulture);
        var dot = stepStr.IndexOf('.');
        if (dot >= 0 && stepStr.Length - dot - 1 > 0)
            return Math.Round(val, stepStr.Length - dot - 1);
        return Math.Round(val, 0);
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":controls", ShowControls);
        PseudoClasses.Set(":handleauto", HandleVisible == HandleVisible.Auto);
        var v = Value;
        PseudoClasses.Set(":atmin", v.HasValue && Min.HasValue && Math.Abs(v.Value - Min.Value) < 1e-9);
        PseudoClasses.Set(":atmax", v.HasValue && Max.HasValue && Math.Abs(v.Value - Max.Value) < 1e-9);
    }
}
