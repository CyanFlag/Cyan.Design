using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Inputs;

/// <summary>文本输入框控件，支持前缀、后缀、附加内容和字数统计</summary>
public class CyanInput : TextBox
{
    /// <summary>输入框尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> InputSizeProperty =
        AvaloniaProperty.Register<CyanInput, ControlSize>(nameof(InputSize), ControlSize.Middle);

    /// <summary>输入框状态样式属性</summary>
    public static readonly StyledProperty<InputStatus> InputStatusProperty =
        AvaloniaProperty.Register<CyanInput, InputStatus>(nameof(InputStatus), InputStatus.Default);

    /// <summary>输入框外观样式属性</summary>
    public static readonly StyledProperty<InputVariant> InputVariantProperty =
        AvaloniaProperty.Register<CyanInput, InputVariant>(nameof(InputVariant), InputVariant.Outlined);

    /// <summary>是否允许清空样式属性</summary>
    public static readonly StyledProperty<bool> AllowClearProperty =
        AvaloniaProperty.Register<CyanInput, bool>(nameof(AllowClear));

    /// <summary>前缀内容样式属性</summary>
    public static readonly StyledProperty<object?> PrefixProperty =
        AvaloniaProperty.Register<CyanInput, object?>(nameof(Prefix));

    /// <summary>后缀内容样式属性</summary>
    public static readonly StyledProperty<object?> SuffixProperty =
        AvaloniaProperty.Register<CyanInput, object?>(nameof(Suffix));

    /// <summary>是否显示字数统计样式属性</summary>
    public static readonly StyledProperty<bool> ShowCountProperty =
        AvaloniaProperty.Register<CyanInput, bool>(nameof(ShowCount));

    /// <summary>前置附加内容样式属性</summary>
    public static readonly StyledProperty<object?> AddonBeforeProperty =
        AvaloniaProperty.Register<CyanInput, object?>(nameof(AddonBefore));

    /// <summary>后置附加内容样式属性</summary>
    public static readonly StyledProperty<object?> AddonAfterProperty =
        AvaloniaProperty.Register<CyanInput, object?>(nameof(AddonAfter));

    /// <summary>字数统计文本样式属性</summary>
    public static readonly StyledProperty<string> CountTextProperty =
        AvaloniaProperty.Register<CyanInput, string>(nameof(CountText), "0");

    /// <summary>清空路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ClearedEvent =
        RoutedEvent.Register<CyanInput, RoutedEventArgs>(nameof(Cleared), RoutingStrategies.Bubble);

    /// <summary>提交路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> SubmittedEvent =
        RoutedEvent.Register<CyanInput, RoutedEventArgs>(nameof(Submitted), RoutingStrategies.Bubble);

    static CyanInput()
    {
        TextProperty.Changed.AddClassHandler<CyanInput>(OnTextChanged);
        MaxLengthProperty.Changed.AddClassHandler<CyanInput>((c, _) => c.UpdateCountText());
        AllowClearProperty.Changed.AddClassHandler<CyanInput>((c, _) => c.UpdateClearButtonVisibility());
        IsEnabledProperty.Changed.AddClassHandler<CyanInput>((c, _) => c.UpdateClearButtonVisibility());
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

    /// <summary>是否允许清空</summary>
    public bool AllowClear
    {
        get => GetValue(AllowClearProperty);
        set => SetValue(AllowClearProperty, value);
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

    /// <summary>是否显示字数统计</summary>
    public bool ShowCount
    {
        get => GetValue(ShowCountProperty);
        set => SetValue(ShowCountProperty, value);
    }

    /// <summary>前置附加内容</summary>
    public object? AddonBefore
    {
        get => GetValue(AddonBeforeProperty);
        set => SetValue(AddonBeforeProperty, value);
    }

    /// <summary>后置附加内容</summary>
    public object? AddonAfter
    {
        get => GetValue(AddonAfterProperty);
        set => SetValue(AddonAfterProperty, value);
    }

    /// <summary>字数统计文本</summary>
    public string CountText
    {
        get => GetValue(CountTextProperty);
        set => SetValue(CountTextProperty, value);
    }

    /// <summary>清空事件</summary>
    public event EventHandler<RoutedEventArgs>? Cleared
    {
        add => AddHandler(ClearedEvent, value);
        remove => RemoveHandler(ClearedEvent, value);
    }

    /// <summary>提交事件（按下回车时触发）</summary>
    public event EventHandler<RoutedEventArgs>? Submitted
    {
        add => AddHandler(SubmittedEvent, value);
        remove => RemoveHandler(SubmittedEvent, value);
    }

    /// <summary>键盘按下事件处理</summary>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (e.Key == Key.Enter && !e.Handled)
        {
            RaiseEvent(new RoutedEventArgs(SubmittedEvent));
            e.Handled = true;
        }
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        if (e.NameScope.Find<Button>("PART_ClearButton") is { } clearBtn)
            clearBtn.Click += OnClearButtonClick;
        UpdateClearButtonVisibility();
        UpdateCountText();
    }

    private static void OnTextChanged(CyanInput c, AvaloniaPropertyChangedEventArgs e)
    {
        c.UpdateClearButtonVisibility();
        c.UpdateCountText();
    }

    private void OnClearButtonClick(object? sender, RoutedEventArgs e)
    {
        Text = string.Empty;
        RaiseEvent(new RoutedEventArgs(ClearedEvent));
    }

    private void UpdateClearButtonVisibility()
        => PseudoClasses.Set(":clearvisible", CountTextHelper.ShouldShowClearButton(AllowClear, Text, IsReadOnly, IsEnabled));

    private void UpdateCountText()
        => CountText = CountTextHelper.BuildCountText(Text, MaxLength);
}
