using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Metadata;

namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>滑块刻度标记数据模型</summary>
public class SliderMark
{
    /// <summary>刻度对应的值</summary>
    public double Value { get; set; }
    /// <summary>刻度显示的标签文本</summary>
    public string? Label { get; set; }
}

/// <summary>滑块控件，用于在给定范围内选择数值</summary>
public class CyanSlider : TemplatedControl
{
    /// <summary>当前值的样式属性</summary>
    public static readonly StyledProperty<double> ValueProperty =
        AvaloniaProperty.Register<CyanSlider, double>(nameof(Value));

    /// <summary>最小值的样式属性</summary>
    public static readonly StyledProperty<double> MinimumProperty =
        AvaloniaProperty.Register<CyanSlider, double>(nameof(Minimum));

    /// <summary>最大值的样式属性</summary>
    public static readonly StyledProperty<double> MaximumProperty =
        AvaloniaProperty.Register<CyanSlider, double>(nameof(Maximum), 100);

    /// <summary>步长的样式属性</summary>
    public static readonly StyledProperty<double> StepProperty =
        AvaloniaProperty.Register<CyanSlider, double>(nameof(Step));

    /// <summary>是否禁用的样式属性</summary>
    public static readonly StyledProperty<bool> DisabledProperty =
        AvaloniaProperty.Register<CyanSlider, bool>(nameof(Disabled));

    /// <summary>是否反向的样式属性</summary>
    public static readonly StyledProperty<bool> ReverseProperty =
        AvaloniaProperty.Register<CyanSlider, bool>(nameof(Reverse));

    /// <summary>是否为范围选择模式的样式属性</summary>
    public static readonly StyledProperty<bool> RangeProperty =
        AvaloniaProperty.Register<CyanSlider, bool>(nameof(Range));

    /// <summary>范围选择起始值的样式属性</summary>
    public static readonly StyledProperty<double> RangeStartProperty =
        AvaloniaProperty.Register<CyanSlider, double>(nameof(RangeStart));

    /// <summary>范围选择结束值的样式属性</summary>
    public static readonly StyledProperty<double> RangeEndProperty =
        AvaloniaProperty.Register<CyanSlider, double>(nameof(RangeEnd), 100);

    /// <summary>是否显示提示框的样式属性</summary>
    public static readonly StyledProperty<bool> TooltipVisibleProperty =
        AvaloniaProperty.Register<CyanSlider, bool>(nameof(TooltipVisible));

    /// <summary>值改变事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ValueChangedEvent =
        RoutedEvent.Register<CyanSlider, RoutedEventArgs>(nameof(ValueChanged), RoutingStrategies.Bubble);

    /// <summary>范围改变事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> RangeChangedEvent =
        RoutedEvent.Register<CyanSlider, RoutedEventArgs>(nameof(RangeChanged), RoutingStrategies.Bubble);

    private const double ThumbSize = 14;
    private const double ThumbHalf = ThumbSize / 2;

    static CyanSlider()
    {
        ValueProperty.Changed.AddClassHandler<CyanSlider>((s, _) => s.UpdateLayout());
        MinimumProperty.Changed.AddClassHandler<CyanSlider>((s, _) => s.UpdateLayout());
        MaximumProperty.Changed.AddClassHandler<CyanSlider>((s, _) => s.UpdateLayout());
        DisabledProperty.Changed.AddClassHandler<CyanSlider>((s, _) => s.UpdatePseudoClasses());
        ReverseProperty.Changed.AddClassHandler<CyanSlider>((s, _) => s.UpdateLayout());
        RangeProperty.Changed.AddClassHandler<CyanSlider>((s, _) => s.UpdateLayout());
        RangeStartProperty.Changed.AddClassHandler<CyanSlider>((s, _) => s.UpdateLayout());
        RangeEndProperty.Changed.AddClassHandler<CyanSlider>((s, _) => s.UpdateLayout());
        TooltipVisibleProperty.Changed.AddClassHandler<CyanSlider>((s, _) => s.UpdateTooltipVisibility());
    }

    /// <summary>初始化控件实例</summary>
    public CyanSlider()
    {
        Focusable = true;
    }

    /// <summary>当前值</summary>
    public double Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
    /// <summary>最小值</summary>
    public double Minimum { get => GetValue(MinimumProperty); set => SetValue(MinimumProperty, value); }
    /// <summary>最大值</summary>
    public double Maximum { get => GetValue(MaximumProperty); set => SetValue(MaximumProperty, value); }
    /// <summary>步长</summary>
    public double Step { get => GetValue(StepProperty); set => SetValue(StepProperty, value); }
    /// <summary>是否禁用</summary>
    public bool Disabled { get => GetValue(DisabledProperty); set => SetValue(DisabledProperty, value); }
    /// <summary>是否反向</summary>
    public bool Reverse { get => GetValue(ReverseProperty); set => SetValue(ReverseProperty, value); }
    /// <summary>是否为范围选择模式</summary>
    public bool Range { get => GetValue(RangeProperty); set => SetValue(RangeProperty, value); }
    /// <summary>范围选择起始值</summary>
    public double RangeStart { get => GetValue(RangeStartProperty); set => SetValue(RangeStartProperty, value); }
    /// <summary>范围选择结束值</summary>
    public double RangeEnd { get => GetValue(RangeEndProperty); set => SetValue(RangeEndProperty, value); }
    /// <summary>是否显示提示框</summary>
    public bool TooltipVisible { get => GetValue(TooltipVisibleProperty); set => SetValue(TooltipVisibleProperty, value); }

    private readonly List<SliderMark> _marks = new();
    /// <summary>刻度标记集合</summary>
    [Content]
    public List<SliderMark> Marks => _marks;

    /// <summary>值改变事件</summary>
    public event EventHandler<RoutedEventArgs>? ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    /// <summary>范围改变事件</summary>
    public event EventHandler<RoutedEventArgs>? RangeChanged
    {
        add => AddHandler(RangeChangedEvent, value);
        remove => RemoveHandler(RangeChangedEvent, value);
    }

    private Grid? _container;
    private Border? _rail;
    private Border? _track;
    private Border? _thumb;
    private Border? _thumb2;
    private Panel? _marksPanel;
    private Panel? _markDots;
    private Border? _tooltip;
    private TextBlock? _tooltipText;
    private Border? _tooltip2;
    private TextBlock? _tooltip2Text;
    private bool _dragging;
    private int _draggingThumb;

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _container = e.NameScope.Find<Grid>("PART_Container");
        _rail = e.NameScope.Find<Border>("PART_Rail");
        _track = e.NameScope.Find<Border>("PART_Track");
        _thumb = e.NameScope.Find<Border>("PART_Thumb");
        _thumb2 = e.NameScope.Find<Border>("PART_Thumb2");
        _marksPanel = e.NameScope.Find<Panel>("PART_MarksPanel");
        _markDots = e.NameScope.Find<Panel>("PART_MarkDots");
        _tooltip = e.NameScope.Find<Border>("PART_Tooltip");
        _tooltipText = e.NameScope.Find<TextBlock>("PART_TooltipText");
        _tooltip2 = e.NameScope.Find<Border>("PART_Tooltip2");
        _tooltip2Text = e.NameScope.Find<TextBlock>("PART_Tooltip2Text");

        if (_rail != null)
            _rail.PointerPressed += OnRailPointerPressed;

        if (_thumb != null)
            _thumb.PointerPressed += (_, ev) => OnThumbPointerPressed(ev, 1);

        if (_thumb2 != null)
            _thumb2.PointerPressed += (_, ev) => OnThumbPointerPressed(ev, 2);

        if (_container != null)
            _container.SizeChanged += (_, _) => { UpdateLayout(); UpdateMarks(); };

        UpdatePseudoClasses();
        UpdateLayout();
        UpdateMarks();
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":disabled", Disabled);
        PseudoClasses.Set(":range", Range);
    }

    private double GetRatio(double value)
    {
        var range = Maximum - Minimum;
        if (range <= 0) return 0;
        return Math.Clamp((value - Minimum) / range, 0, 1);
    }

    private void UpdateLayout()
    {
        if (_container == null || _track == null || _thumb == null) return;

        var containerWidth = _container.Bounds.Width;
        if (containerWidth <= 0) return;

        var railWidth = containerWidth - ThumbSize;

        if (Range)
        {
            if (_thumb2 != null) _thumb2.IsVisible = true;

            var ratio1 = GetRatio(RangeStart);
            var ratio2 = GetRatio(RangeEnd);
            if (Reverse) { ratio1 = 1 - ratio1; ratio2 = 1 - ratio2; }

            var left = Math.Min(ratio1, ratio2) * railWidth;
            var right = Math.Max(ratio1, ratio2) * railWidth;

            _track.HorizontalAlignment = HorizontalAlignment.Left;
            _track.Margin = new Thickness(7 + left, 0, 0, 0);
            _track.Width = right - left;

            _thumb.Margin = new Thickness(ratio1 * railWidth, 0, 0, 0);
            _thumb2!.Margin = new Thickness(ratio2 * railWidth, 0, 0, 0);
        }
        else
        {
            if (_thumb2 != null) _thumb2.IsVisible = false;

            var ratio = GetRatio(Value);
            if (Reverse) ratio = 1 - ratio;

            _track.Width = ratio * railWidth;
            _track.Margin = new Thickness(7, 0, 0, 0);
            _thumb.Margin = new Thickness(ratio * railWidth, 0, 0, 0);
            _track.HorizontalAlignment = Reverse ? HorizontalAlignment.Right : HorizontalAlignment.Left;

            if (Reverse)
            {
                _track.Margin = new Thickness(7, 0, 0, 0);
                _track.HorizontalAlignment = HorizontalAlignment.Left;
                _track.Width = (1 - ratio) * railWidth;
            }
        }

        UpdateTooltipPosition();
    }

    private void UpdateTooltipPosition()
    {
        if (_container == null) return;
        var containerWidth = _container.Bounds.Width;
        if (containerWidth <= 0) return;
        var railWidth = containerWidth - ThumbSize;

        if (Range)
        {
            var ratio1 = GetRatio(RangeStart);
            var ratio2 = GetRatio(RangeEnd);
            if (Reverse) { ratio1 = 1 - ratio1; ratio2 = 1 - ratio2; }

            if (_tooltip != null && _tooltipText != null)
            {
                _tooltipText.Text = RangeStart.ToString("F0");
                _tooltip.Margin = new Thickness(ratio1 * railWidth + ThumbHalf, -26, 0, 0);
            }
            if (_tooltip2 != null && _tooltip2Text != null)
            {
                _tooltip2Text.Text = RangeEnd.ToString("F0");
                _tooltip2.Margin = new Thickness(ratio2 * railWidth + ThumbHalf, -26, 0, 0);
            }
        }
        else
        {
            var ratio = GetRatio(Value);
            if (Reverse) ratio = 1 - ratio;

            if (_tooltip != null && _tooltipText != null)
            {
                _tooltipText.Text = Value.ToString("F0");
                _tooltip.Margin = new Thickness(ratio * railWidth + ThumbHalf, -26, 0, 0);
            }
            if (_tooltip2 != null) _tooltip2.IsVisible = false;
        }
    }

    private void UpdateTooltipVisibility()
    {
        var show = TooltipVisible || _dragging;
        if (_tooltip != null) _tooltip.IsVisible = show;
        if (_tooltip2 != null) _tooltip2.IsVisible = show && Range;
    }

    private void UpdateMarks()
    {
        if (_marksPanel == null || _markDots == null || _container == null) return;

        _marksPanel.Children.Clear();
        _markDots.Children.Clear();

        if (_marks.Count == 0)
        {
            _marksPanel.IsVisible = false;
            return;
        }

        _marksPanel.IsVisible = true;

        var containerWidth = _container.Bounds.Width;
        if (containerWidth <= 0) return;

        var railWidth = containerWidth - ThumbSize;

        var markBrush = this.TryFindResource("ColorBorderBrush", out var r) && r is IBrush b ? b : new SolidColorBrush(Colors.Gray);

        foreach (var mark in _marks)
        {
            var ratio = GetRatio(mark.Value);
            if (Reverse) ratio = 1 - ratio;
            var pos = ratio * railWidth + ThumbHalf;

            var dot = new Border
            {
                Width = 2,
                Height = 4,
                CornerRadius = new CornerRadius(1),
                Background = markBrush,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(pos - 1, 0, 0, 0)
            };
            _markDots.Children.Add(dot);

            var label = new TextBlock
            {
                Text = mark.Label ?? mark.Value.ToString(),
                FontSize = 12,
                Foreground = markBrush,
                VerticalAlignment = VerticalAlignment.Top
            };

            if (ratio <= 0.01)
            {
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.Margin = new Thickness(0, 4, 0, 0);
            }
            else if (ratio >= 0.99)
            {
                label.HorizontalAlignment = HorizontalAlignment.Right;
                label.Margin = new Thickness(0, 4, 0, 0);
            }
            else
            {
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.Margin = new Thickness(pos, 4, 0, 0);
            }

            _marksPanel.Children.Add(label);
        }
    }

    private double PositionToValue(double x)
    {
        if (_container == null) return Value;
        var containerWidth = _container.Bounds.Width;
        var railWidth = containerWidth - ThumbSize;
        if (railWidth <= 0) return Value;

        var ratio = (x - ThumbHalf) / railWidth;
        if (Reverse) ratio = 1 - ratio;
        ratio = Math.Clamp(ratio, 0, 1);

        var value = Minimum + ratio * (Maximum - Minimum);
        if (Step > 0)
            value = Math.Round(value / Step) * Step;
        return Math.Clamp(value, Minimum, Maximum);
    }

    private void OnRailPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (Disabled || _container == null) return;
        var pos = e.GetPosition(_container);
        var newValue = PositionToValue(pos.X);

        if (Range)
        {
            var distStart = Math.Abs(newValue - RangeStart);
            var distEnd = Math.Abs(newValue - RangeEnd);
            if (distStart <= distEnd)
            {
                RangeStart = Math.Clamp(newValue, Minimum, RangeEnd);
                _draggingThumb = 1;
            }
            else
            {
                RangeEnd = Math.Clamp(newValue, RangeStart, Maximum);
                _draggingThumb = 2;
            }
            RaiseEvent(new RoutedEventArgs(RangeChangedEvent));
        }
        else
        {
            SetValueFromPosition(pos.X);
            _draggingThumb = 1;
        }

        e.Pointer.Capture(_container);
        _dragging = true;
        PseudoClasses.Set(":dragging", true);
        UpdateTooltipVisibility();
    }

    private void OnThumbPointerPressed(PointerPressedEventArgs e, int thumbIndex)
    {
        if (Disabled || _container == null) return;
        e.Pointer.Capture(_container);
        _dragging = true;
        _draggingThumb = thumbIndex;
        PseudoClasses.Set(":dragging", true);
        UpdateTooltipVisibility();
    }

    /// <summary>指针移动事件处理</summary>
    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);
        if (!_dragging || Disabled || _container == null) return;
        var pos = e.GetPosition(_container);
        var newValue = PositionToValue(pos.X);

        if (Range)
        {
            if (_draggingThumb == 1)
            {
                if (newValue < RangeEnd)
                    RangeStart = newValue;
            }
            else
            {
                if (newValue > RangeStart)
                    RangeEnd = newValue;
            }
            RaiseEvent(new RoutedEventArgs(RangeChangedEvent));
        }
        else
        {
            SetValueFromPosition(pos.X);
        }
    }

    /// <summary>指针释放事件处理</summary>
    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        if (!_dragging) return;
        _dragging = false;
        _draggingThumb = 0;
        PseudoClasses.Set(":dragging", false);
        e.Pointer.Capture(null);
        UpdateTooltipVisibility();
    }

    private void SetValueFromPosition(double x)
    {
        var newValue = PositionToValue(x);
        if (Math.Abs(newValue - Value) > 0.001)
        {
            Value = newValue;
            RaiseEvent(new RoutedEventArgs(ValueChangedEvent));
        }
    }

    /// <summary>键盘按下事件处理</summary>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (Disabled) return;
        var step = Step > 0 ? Step : 1;

        if (Range)
        {
            switch (e.Key)
            {
                case Key.Left or Key.Down:
                    RangeStart = Math.Clamp(RangeStart - step, Minimum, RangeEnd);
                    RaiseEvent(new RoutedEventArgs(RangeChangedEvent));
                    e.Handled = true;
                    break;
                case Key.Right or Key.Up:
                    RangeEnd = Math.Clamp(RangeEnd + step, RangeStart, Maximum);
                    RaiseEvent(new RoutedEventArgs(RangeChangedEvent));
                    e.Handled = true;
                    break;
                case Key.Home:
                    RangeStart = Minimum;
                    RaiseEvent(new RoutedEventArgs(RangeChangedEvent));
                    e.Handled = true;
                    break;
                case Key.End:
                    RangeEnd = Maximum;
                    RaiseEvent(new RoutedEventArgs(RangeChangedEvent));
                    e.Handled = true;
                    break;
            }
            return;
        }

        switch (e.Key)
        {
            case Key.Left or Key.Down:
                Value = Math.Clamp(Value - step, Minimum, Maximum);
                e.Handled = true;
                break;
            case Key.Right or Key.Up:
                Value = Math.Clamp(Value + step, Minimum, Maximum);
                e.Handled = true;
                break;
            case Key.Home:
                Value = Minimum;
                e.Handled = true;
                break;
            case Key.End:
                Value = Maximum;
                e.Handled = true;
                break;
        }
    }
}
