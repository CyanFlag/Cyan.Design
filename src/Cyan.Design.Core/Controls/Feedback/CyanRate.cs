using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>评分控件，用于对内容进行星级评价</summary>
public class CyanRate : TemplatedControl
{
    /// <summary>星星总数的样式属性</summary>
    public static readonly StyledProperty<int> CountProperty =
        AvaloniaProperty.Register<CyanRate, int>(nameof(Count), 5);

    /// <summary>当前评分值的样式属性</summary>
    public static readonly StyledProperty<double> ValueProperty =
        AvaloniaProperty.Register<CyanRate, double>(nameof(Value));

    /// <summary>是否允许半选的样式属性</summary>
    public static readonly StyledProperty<bool> AllowHalfProperty =
        AvaloniaProperty.Register<CyanRate, bool>(nameof(AllowHalf));

    /// <summary>是否允许清除的样式属性</summary>
    public static readonly StyledProperty<bool> AllowClearProperty =
        AvaloniaProperty.Register<CyanRate, bool>(nameof(AllowClear), true);

    /// <summary>是否禁用的样式属性</summary>
    public static readonly StyledProperty<bool> DisabledProperty =
        AvaloniaProperty.Register<CyanRate, bool>(nameof(Disabled));

    /// <summary>是否只读的样式属性</summary>
    public static readonly StyledProperty<bool> ReadOnlyProperty =
        AvaloniaProperty.Register<CyanRate, bool>(nameof(ReadOnly));

    /// <summary>星星字符的样式属性</summary>
    public static readonly StyledProperty<string> CharacterProperty =
        AvaloniaProperty.Register<CyanRate, string>(nameof(Character), "★");

    /// <summary>星星字号的样式属性</summary>
    public static readonly StyledProperty<double> StarFontSizeProperty =
        AvaloniaProperty.Register<CyanRate, double>(nameof(StarFontSize), 20);

    /// <summary>评分值改变事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ValueChangedEvent =
        RoutedEvent.Register<CyanRate, RoutedEventArgs>(nameof(ValueChanged), RoutingStrategies.Bubble);

    static CyanRate()
    {
        CountProperty.Changed.AddClassHandler<CyanRate>((c, _) => c.CreateStars());
        ValueProperty.Changed.AddClassHandler<CyanRate>((c, _) => c.UpdateStarDisplay());
        AllowHalfProperty.Changed.AddClassHandler<CyanRate>((c, _) => c.UpdateStarDisplay());
        DisabledProperty.Changed.AddClassHandler<CyanRate>((c, _) => c.UpdatePseudoClasses());
        ReadOnlyProperty.Changed.AddClassHandler<CyanRate>((c, _) => c.UpdatePseudoClasses());
        CharacterProperty.Changed.AddClassHandler<CyanRate>((c, _) => c.UpdateStarCharacters());
        StarFontSizeProperty.Changed.AddClassHandler<CyanRate>((c, _) => c.UpdateStarCharacters());
    }

    /// <summary>星星总数</summary>
    public int Count
    {
        get => GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    /// <summary>当前评分值</summary>
    public double Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>是否允许半选</summary>
    public bool AllowHalf
    {
        get => GetValue(AllowHalfProperty);
        set => SetValue(AllowHalfProperty, value);
    }

    /// <summary>是否允许清除</summary>
    public bool AllowClear
    {
        get => GetValue(AllowClearProperty);
        set => SetValue(AllowClearProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool Disabled
    {
        get => GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    /// <summary>是否只读</summary>
    public bool ReadOnly
    {
        get => GetValue(ReadOnlyProperty);
        set => SetValue(ReadOnlyProperty, value);
    }

    /// <summary>星星字符</summary>
    public string Character
    {
        get => GetValue(CharacterProperty);
        set => SetValue(CharacterProperty, value);
    }

    /// <summary>星星字号</summary>
    public double StarFontSize
    {
        get => GetValue(StarFontSizeProperty);
        set => SetValue(StarFontSizeProperty, value);
    }

    /// <summary>评分值改变事件</summary>
    public event EventHandler<RoutedEventArgs>? ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    private readonly List<Panel> _starPanels = new();
    private readonly List<TextBlock> _starFirsts = new();
    private readonly List<TextBlock> _starSeconds = new();
    private StackPanel? _container;
    private double? _hoverValue;

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _container = e.NameScope.Find<StackPanel>("PART_Container");
        CreateStars();
    }

    /// <summary>指针移动事件处理</summary>
    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);
        if (_starPanels.Count == 0) return;
        var overStar = false;
        foreach (var panel in _starPanels)
        {
            var pos = e.GetPosition(panel);
            if (pos.X >= 0 && pos.X <= panel.Bounds.Width &&
                pos.Y >= 0 && pos.Y <= panel.Bounds.Height)
            {
                overStar = true;
                break;
            }
        }
        if (!overStar)
        {
            _hoverValue = null;
            UpdateStarDisplay();
        }
    }

    private IBrush GetGoldBrush() =>
        this.TryFindResource("ColorWarningBrush", out var r) && r is IBrush b ? b : new SolidColorBrush(Color.Parse("#FADB14"));

    private IBrush GetGrayBrush() =>
        this.TryFindResource("ColorFillTertiaryBrush", out var r) && r is IBrush b ? b : new SolidColorBrush(Color.Parse("#0F000000"));

    private void CreateStars()
    {
        if (_container == null) return;

        _container.Children.Clear();
        _starPanels.Clear();
        _starFirsts.Clear();
        _starSeconds.Clear();

        var interactive = !Disabled && !ReadOnly;

        for (var i = 0; i < Count; i++)
        {
            var panel = new Panel
            {
                Margin = new Thickness(0, 0, i < Count - 1 ? 8 : 0, 0)
            };

            var second = new TextBlock
            {
                Text = Character,
                FontSize = StarFontSize,
                Foreground = GetGrayBrush()
            };

            var first = new TextBlock
            {
                Text = Character,
                FontSize = StarFontSize,
                Foreground = GetGoldBrush(),
                ClipToBounds = true,
                Width = 0
            };

            second.SizeChanged += (_, _) => UpdateStarDisplay();

            panel.Children.Add(second);
            panel.Children.Add(first);

            if (interactive)
            {
                var index = i;
                panel.PointerPressed += (_, e) => OnStarPressed(index, e);
                panel.PointerMoved += (_, e) => OnStarMoved(index, e);

            }

            _container.Children.Add(panel);
            _starPanels.Add(panel);
            _starFirsts.Add(first);
            _starSeconds.Add(second);
        }

        UpdateStarDisplay();
    }

    private void UpdateStarCharacters()
    {
        var ch = Character;
        for (var i = 0; i < _starFirsts.Count; i++)
        {
            _starFirsts[i].Text = ch;
            _starSeconds[i].Text = ch;
            _starFirsts[i].FontSize = StarFontSize;
            _starSeconds[i].FontSize = StarFontSize;
        }
    }

    private void UpdateStarDisplay()
    {
        var displayValue = _hoverValue ?? Value;

        for (var i = 0; i < _starPanels.Count; i++)
        {
            var second = _starSeconds[i];
            var first = _starFirsts[i];
            var starWidth = second.Bounds.Width;
            if (starWidth <= 0) continue;

            double fillWidth;
            if (displayValue >= i + 1)
                fillWidth = starWidth;
            else if (AllowHalf && displayValue >= i + 0.5)
                fillWidth = starWidth / 2;
            else
                fillWidth = 0;

            first.Width = fillWidth;
        }
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":disabled", Disabled);
        PseudoClasses.Set(":readonly", ReadOnly);
    }

    private double GetStarValue(int index, PointerEventArgs e)
    {
        var panel = _starPanels[index];
        var pos = e.GetPosition(panel);
        var isLeftHalf = pos.X < panel.Bounds.Width / 2;
        return AllowHalf && isLeftHalf ? index + 0.5 : index + 1;
    }

    private void OnStarPressed(int index, PointerPressedEventArgs e)
    {
        if (Disabled || ReadOnly) return;
        e.Pointer.Capture(_starPanels[index]);

        var newValue = GetStarValue(index, e);
        if (AllowClear && Math.Abs(newValue - Value) < 0.001)
            Value = 0;
        else
            Value = newValue;

        RaiseEvent(new RoutedEventArgs(ValueChangedEvent));
    }

    private void OnStarMoved(int index, PointerEventArgs e)
    {
        if (Disabled || ReadOnly) return;
        _hoverValue = GetStarValue(index, e);
        UpdateStarDisplay();
    }

    private void OnStarLeave()
    {
        _hoverValue = null;
        UpdateStarDisplay();
    }
}
