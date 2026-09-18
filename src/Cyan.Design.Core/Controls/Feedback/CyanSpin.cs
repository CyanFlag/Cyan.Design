using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>加载指示器控件，用于展示内容加载状态</summary>
public class CyanSpin : TemplatedControl
{
    /// <summary>是否正在旋转的样式属性</summary>
    public static readonly StyledProperty<bool> SpinningProperty =
        AvaloniaProperty.Register<CyanSpin, bool>(nameof(Spinning), true);

    /// <summary>控件尺寸的样式属性</summary>
    public static readonly StyledProperty<ControlSize> SizeProperty =
        AvaloniaProperty.Register<CyanSpin, ControlSize>(nameof(Size), ControlSize.Middle);

    /// <summary>提示文本的样式属性</summary>
    public static readonly StyledProperty<string?> TipProperty =
        AvaloniaProperty.Register<CyanSpin, string?>(nameof(Tip));

    /// <summary>延迟显示时间（毫秒）的样式属性</summary>
    public static readonly StyledProperty<int> DelayProperty =
        AvaloniaProperty.Register<CyanSpin, int>(nameof(Delay));

    /// <summary>被包裹内容的样式属性</summary>
    public static readonly StyledProperty<object?> ContentProperty =
        AvaloniaProperty.Register<CyanSpin, object?>(nameof(Content));

    static CyanSpin()
    {
        SpinningProperty.Changed.AddClassHandler<CyanSpin>((c, _) => c.UpdateVisibility());
        SizeProperty.Changed.AddClassHandler<CyanSpin>((c, _) => c.UpdateSize());
        TipProperty.Changed.AddClassHandler<CyanSpin>((c, _) => c.UpdateTip());
        ContentProperty.Changed.AddClassHandler<CyanSpin>((c, _) => c.UpdateVisibility());
    }

    /// <summary>是否正在旋转</summary>
    public bool Spinning
    {
        get => GetValue(SpinningProperty);
        set => SetValue(SpinningProperty, value);
    }

    /// <summary>控件尺寸</summary>
    public ControlSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>提示文本</summary>
    public string? Tip
    {
        get => GetValue(TipProperty);
        set => SetValue(TipProperty, value);
    }

    /// <summary>延迟显示时间（毫秒）</summary>
    public int Delay
    {
        get => GetValue(DelayProperty);
        set => SetValue(DelayProperty, value);
    }

    /// <summary>被包裹的内容</summary>
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    private ContentPresenter? _contentPresenter;
    private Border? _mask;
    private StackPanel? _spinOverlay;
    private Panel? _dotContainer;
    private readonly Border?[] _dots = new Border?[4];
    private TextBlock? _tipText;

    private DispatcherTimer? _animTimer;
    private DispatcherTimer? _delayTimer;
    private double _elapsedMs;
    private bool _overlayVisible;

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _contentPresenter = e.NameScope.Find<ContentPresenter>("PART_Content");
        _mask = e.NameScope.Find<Border>("PART_Mask");
        _spinOverlay = e.NameScope.Find<StackPanel>("PART_SpinOverlay");
        _dotContainer = e.NameScope.Find<Panel>("PART_DotContainer");
        _tipText = e.NameScope.Find<TextBlock>("PART_TipText");
        _dots[0] = e.NameScope.Find<Border>("PART_Dot1");
        _dots[1] = e.NameScope.Find<Border>("PART_Dot2");
        _dots[2] = e.NameScope.Find<Border>("PART_Dot3");
        _dots[3] = e.NameScope.Find<Border>("PART_Dot4");

        UpdateSize();
        UpdateTip();
        UpdateVisibility();
    }

    private void UpdateSize()
    {
        var (containerSize, dotSize) = Size switch
        {
            ControlSize.Small => (16.0, 3.0),
            ControlSize.Large => (36.0, 6.0),
            _ => (22.0, 4.0)
        };

        if (_dotContainer != null)
        {
            _dotContainer.Width = containerSize;
            _dotContainer.Height = containerSize;
        }

        foreach (var dot in _dots)
        {
            if (dot != null)
            {
                dot.Width = dotSize;
                dot.Height = dotSize;
            }
        }

        if (_tipText != null)
            _tipText.FontSize = Size == ControlSize.Small ? 12 : 14;
    }

    private void UpdateTip()
    {
        if (_tipText == null) return;
        var tip = Tip;
        _tipText.Text = tip;
        _tipText.IsVisible = !string.IsNullOrEmpty(tip);
    }

    private void UpdateVisibility()
    {
        var hasContent = Content != null;
        var shouldSpin = Spinning;

        if (_contentPresenter != null)
            _contentPresenter.IsVisible = hasContent;

        if (hasContent)
        {
            if (_mask != null) _mask.IsVisible = shouldSpin;
            ShowOverlay(shouldSpin);
        }
        else
        {
            if (_mask != null) _mask.IsVisible = false;
            ShowOverlay(true);
        }
    }

    private void ShowOverlay(bool show)
    {
        if (show == _overlayVisible) return;

        if (show)
        {
            if (Delay > 0)
            {
                _delayTimer?.Stop();
                _delayTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(Delay), DispatcherPriority.Normal, OnDelayElapsed);
                _delayTimer.Start();
            }
            else
            {
                SetOverlayVisible(true);
                StartAnimation();
            }
        }
        else
        {
            _delayTimer?.Stop();
            _delayTimer = null;
            SetOverlayVisible(false);
            StopAnimation();
        }
    }

    private void OnDelayElapsed(object? sender, EventArgs e)
    {
        _delayTimer?.Stop();
        _delayTimer = null;
        SetOverlayVisible(true);
        StartAnimation();
    }

    private void SetOverlayVisible(bool visible)
    {
        if (_spinOverlay != null) _spinOverlay.IsVisible = visible;
        _overlayVisible = visible;
    }

    private void StartAnimation()
    {
        if (_animTimer != null) return;
        _elapsedMs = 0;
        _animTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(16), DispatcherPriority.Normal, OnAnimTick);
        _animTimer.Start();
    }

    private void StopAnimation()
    {
        _animTimer?.Stop();
        _animTimer = null;
    }

    private void OnAnimTick(object? sender, EventArgs e)
    {
        _elapsedMs += 16;

        var containerSize = _dotContainer?.Bounds.Width ?? 22;
        var dotSize = _dots[0]?.Bounds.Width ?? 4;
        var r = (containerSize - dotSize) * 0.5;
        var cx = containerSize * 0.5;
        var cy = containerSize * 0.5;

        var baseAngle = (_elapsedMs / 1200.0 * 360) % 360;
        var elapsedSec = _elapsedMs / 1000.0;

        for (var i = 0; i < 4; i++)
        {
            var dotAngle = (baseAngle + i * 90 + 225) * Math.PI / 180;
            var x = cx + r * Math.Cos(dotAngle) - dotSize * 0.5;
            var y = cy + r * Math.Sin(dotAngle) - dotSize * 0.5;

            if (_dots[i] != null)
                _dots[i]!.Margin = new Thickness(x, y, 0, 0);

            var delay = i * 0.4;
            var cycle = (elapsedSec + delay) % 2.0;
            double opacity;
            if (cycle < 1.0)
                opacity = 0.3 + 0.7 * cycle;
            else
                opacity = 0.3 + 0.7 * (2.0 - cycle);

            if (_dots[i] != null)
                _dots[i]!.Opacity = opacity;
        }
    }
}
