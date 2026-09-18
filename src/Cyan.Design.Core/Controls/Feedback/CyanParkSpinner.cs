using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using Avalonia.Threading;

namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>Park 风格旋转加载指示器的尺寸枚举</summary>
public enum ParkSpinnerSize
{
    /// <summary>继承父级尺寸</summary>
    Inherit,
    /// <summary>超小尺寸</summary>
    XS,
    /// <summary>小尺寸</summary>
    SM,
    /// <summary>中等尺寸</summary>
    MD,
    /// <summary>大尺寸</summary>
    LG,
    /// <summary>超大尺寸</summary>
    XL,
    /// <summary>最大尺寸</summary>
    XXL
}

/// <summary>Park 风格旋转加载指示器控件</summary>
public class CyanParkSpinner : Control
{
    /// <summary>指示器尺寸的样式属性</summary>
    public static readonly StyledProperty<ParkSpinnerSize> SpinnerSizeProperty =
        AvaloniaProperty.Register<CyanParkSpinner, ParkSpinnerSize>(nameof(SpinnerSize), ParkSpinnerSize.MD);

    /// <summary>旋转一周耗时（毫秒）的样式属性</summary>
    public static readonly StyledProperty<int> SpeedProperty =
        AvaloniaProperty.Register<CyanParkSpinner, int>(nameof(Speed), 1000);

    /// <summary>线条粗细的样式属性</summary>
    public static readonly StyledProperty<double> ThicknessProperty =
        AvaloniaProperty.Register<CyanParkSpinner, double>(nameof(Thickness), 2);

    /// <summary>前景色的样式属性</summary>
    public static readonly StyledProperty<IBrush?> ForegroundProperty =
        AvaloniaProperty.Register<CyanParkSpinner, IBrush?>(nameof(Foreground));

    /// <summary>轨道颜色的样式属性</summary>
    public static readonly StyledProperty<IBrush?> TrackColorProperty =
        AvaloniaProperty.Register<CyanParkSpinner, IBrush?>(nameof(TrackColor));

    /// <summary>指示器尺寸</summary>
    public ParkSpinnerSize SpinnerSize
    {
        get => GetValue(SpinnerSizeProperty);
        set => SetValue(SpinnerSizeProperty, value);
    }

    /// <summary>旋转一周耗时（毫秒）</summary>
    public int Speed
    {
        get => GetValue(SpeedProperty);
        set => SetValue(SpeedProperty, value);
    }

    /// <summary>线条粗细</summary>
    public double Thickness
    {
        get => GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    /// <summary>前景色</summary>
    public IBrush? Foreground
    {
        get => GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }

    /// <summary>轨道颜色</summary>
    public IBrush? TrackColor
    {
        get => GetValue(TrackColorProperty);
        set => SetValue(TrackColorProperty, value);
    }

    private double _angle;
    private DispatcherTimer? _timer;

    private static readonly double[] s_sizes = [0, 12, 16, 20, 24, 28, 32];

    static CyanParkSpinner()
    {
        SpinnerSizeProperty.Changed.AddClassHandler<CyanParkSpinner>((t, _) => t.UpdateSize());
        AffectsRender<CyanParkSpinner>(ThicknessProperty, ForegroundProperty, TrackColorProperty);
    }

    /// <summary>初始化控件实例</summary>
    public CyanParkSpinner()
    {
        UpdateSize();
    }

    private void UpdateSize()
    {
        var idx = (int)SpinnerSize;
        if (idx > 0 && idx < s_sizes.Length)
        {
            var s = s_sizes[idx];
            Width = s;
            Height = s;
        }
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        StartAnimation();
    }

    /// <summary>从视觉树分离时处理</summary>
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        StopAnimation();
    }

    private void StartAnimation()
    {
        StopAnimation();
        _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(16), DispatcherPriority.Normal, OnTick);
        _timer.Start();
    }

    private void StopAnimation()
    {
        _timer?.Stop();
        _timer = null;
    }

    private void OnTick(object? sender, EventArgs e)
    {
        var speed = Speed > 0 ? Speed : 1000;
        _angle = (_angle + 360.0 * 16.0 / speed) % 360;
        InvalidateVisual();
    }

    /// <summary>渲染加载指示器</summary>
    public override void Render(DrawingContext context)
    {
        var size = Math.Min(Width, Height);
        if (size <= 0 || !IsVisible) return;

        var thickness = Math.Max(0.5, Thickness);
        var radius = (size - thickness) / 2;
        if (radius <= 0) return;

        var center = size / 2;
        var fg = Foreground ?? (this.TryFindResource("ColorTextBrush", out var r) && r is IBrush b ? b : Brushes.Black);
        var pen = new Pen(fg, thickness) { LineCap = PenLineCap.Round };

        if (TrackColor is not null)
        {
            var trackPen = new Pen(TrackColor, thickness);
            var fullCircle = new EllipseGeometry(new Rect(thickness / 2, thickness / 2, size - thickness, size - thickness));
            context.DrawGeometry(null, trackPen, fullCircle);
        }

        var figure = new PathFigure
        {
            StartPoint = new Point(center + radius, center),
            IsClosed = false,
        };
        figure.Segments.Add(new ArcSegment
        {
            Point = new Point(center, center - radius),
            Size = new Size(radius, radius),
            RotationAngle = 0,
            IsLargeArc = true,
            SweepDirection = SweepDirection.Clockwise,
        });
        var geometry = new PathGeometry { Figures = { figure } };
        geometry.Transform = new RotateTransform(_angle, center, center);
        context.DrawGeometry(null, pen, geometry);
    }
}
