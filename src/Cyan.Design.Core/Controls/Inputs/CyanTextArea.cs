using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Inputs;

/// <summary>多行文本输入框控件，支持自动调整高度和字数统计</summary>
public class CyanTextArea : TextBox
{
    /// <summary>输入框尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> InputSizeProperty =
        AvaloniaProperty.Register<CyanTextArea, ControlSize>(nameof(InputSize), ControlSize.Middle);

    /// <summary>输入框状态样式属性</summary>
    public static readonly StyledProperty<InputStatus> InputStatusProperty =
        AvaloniaProperty.Register<CyanTextArea, InputStatus>(nameof(InputStatus), InputStatus.Default);

    /// <summary>输入框外观样式属性</summary>
    public static readonly StyledProperty<InputVariant> InputVariantProperty =
        AvaloniaProperty.Register<CyanTextArea, InputVariant>(nameof(InputVariant), InputVariant.Outlined);

    /// <summary>是否自动调整高度样式属性</summary>
    public static readonly StyledProperty<bool> AutoSizeProperty =
        AvaloniaProperty.Register<CyanTextArea, bool>(nameof(AutoSize));

    /// <summary>最小行数样式属性</summary>
    public static readonly StyledProperty<int> MinRowsProperty =
        AvaloniaProperty.Register<CyanTextArea, int>(nameof(MinRows), -1);

    /// <summary>最大行数样式属性</summary>
    public static readonly StyledProperty<int> MaxRowsProperty =
        AvaloniaProperty.Register<CyanTextArea, int>(nameof(MaxRows), -1);

    /// <summary>是否显示字数统计样式属性</summary>
    public static readonly StyledProperty<bool> ShowCountProperty =
        AvaloniaProperty.Register<CyanTextArea, bool>(nameof(ShowCount));

    /// <summary>字数统计文本样式属性</summary>
    public static readonly StyledProperty<string> CountTextProperty =
        AvaloniaProperty.Register<CyanTextArea, string>(nameof(CountText), "0");


    static CyanTextArea()
    {
        AutoSizeProperty.Changed.AddClassHandler<CyanTextArea>(OnAutoSizeChanged);
        MinRowsProperty.Changed.AddClassHandler<CyanTextArea>((c, _) => c.ApplyAutoSize());
        MaxRowsProperty.Changed.AddClassHandler<CyanTextArea>((c, _) => c.ApplyAutoSize());
        TextProperty.Changed.AddClassHandler<CyanTextArea>((c, _) => c.UpdateCountText());
        MaxLengthProperty.Changed.AddClassHandler<CyanTextArea>((c, _) => c.UpdateCountText());
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

    /// <summary>是否自动调整高度</summary>
    public bool AutoSize
    {
        get => GetValue(AutoSizeProperty);
        set => SetValue(AutoSizeProperty, value);
    }

    /// <summary>最小行数，-1 表示不限制</summary>
    public int MinRows
    {
        get => GetValue(MinRowsProperty);
        set => SetValue(MinRowsProperty, value);
    }

    /// <summary>最大行数，-1 表示不限制</summary>
    public int MaxRows
    {
        get => GetValue(MaxRowsProperty);
        set => SetValue(MaxRowsProperty, value);
    }

    /// <summary>是否显示字数统计</summary>
    public bool ShowCount
    {
        get => GetValue(ShowCountProperty);
        set => SetValue(ShowCountProperty, value);
    }

    /// <summary>字数统计文本</summary>
    public string CountText
    {
        get => GetValue(CountTextProperty);
        set => SetValue(CountTextProperty, value);
    }


    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        ApplyAutoSize();
        UpdateCountText();
    }

    private static void OnAutoSizeChanged(CyanTextArea c, AvaloniaPropertyChangedEventArgs e)
        => c.ApplyAutoSize();

    private void ApplyAutoSize()
    {
        if (!AutoSize)
        {
            PseudoClasses.Set(":autosize", false);
            return;
        }
        PseudoClasses.Set(":autosize", true);
        if (MinRows > 0)
            MinHeight = MinRows * 22;
        if (MaxRows > 0)
            MaxHeight = MaxRows * 22;
    }

    private void UpdateCountText()
        => CountText = CountTextHelper.BuildCountText(Text, MaxLength);
}
