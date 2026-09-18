using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Cyan.Design.Core.Controls.Common;
using Path = Avalonia.Controls.Shapes.Path;

namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>进度条控件，用于展示任务执行进度</summary>
public class CyanProgress : TemplatedControl
{
    /// <summary>进度条类型的样式属性</summary>
    public static readonly StyledProperty<ProgressType> TypeProperty =
        AvaloniaProperty.Register<CyanProgress, ProgressType>(nameof(Type), ProgressType.Line);

    /// <summary>当前进度的样式属性</summary>
    public static readonly StyledProperty<double> PercentProperty =
        AvaloniaProperty.Register<CyanProgress, double>(nameof(Percent));

    /// <summary>是否显示进度信息的样式属性</summary>
    public static readonly StyledProperty<bool> ShowInfoProperty =
        AvaloniaProperty.Register<CyanProgress, bool>(nameof(ShowInfo), true);

    /// <summary>进度状态的样式属性</summary>
    public static readonly StyledProperty<ProgressStatus> StatusProperty =
        AvaloniaProperty.Register<CyanProgress, ProgressStatus>(nameof(Status), ProgressStatus.Normal);

    /// <summary>进度条颜色的样式属性</summary>
    public static readonly StyledProperty<IBrush?> StrokeColorProperty =
        AvaloniaProperty.Register<CyanProgress, IBrush?>(nameof(StrokeColor));

    /// <summary>轨道颜色的样式属性</summary>
    public static readonly StyledProperty<IBrush?> RailColorProperty =
        AvaloniaProperty.Register<CyanProgress, IBrush?>(nameof(RailColor));

    /// <summary>控件尺寸的样式属性</summary>
    public static readonly StyledProperty<ControlSize> SizeProperty =
        AvaloniaProperty.Register<CyanProgress, ControlSize>(nameof(Size), ControlSize.Middle);

    /// <summary>线条宽度的样式属性</summary>
    public static readonly StyledProperty<double> StrokeWidthProperty =
        AvaloniaProperty.Register<CyanProgress, double>(nameof(StrokeWidth), 6);

    /// <summary>仪表盘缺口角度的样式属性</summary>
    public static readonly StyledProperty<double> GapDegreeProperty =
        AvaloniaProperty.Register<CyanProgress, double>(nameof(GapDegree), 75);

    /// <summary>线条端点形状的样式属性</summary>
    public static readonly StyledProperty<ProgressStrokeLinecap> StrokeLinecapProperty =
        AvaloniaProperty.Register<CyanProgress, ProgressStrokeLinecap>(nameof(StrokeLinecap), ProgressStrokeLinecap.Round);

    /// <summary>成功段进度的样式属性</summary>
    public static readonly StyledProperty<double> SuccessPercentProperty =
        AvaloniaProperty.Register<CyanProgress, double>(nameof(SuccessPercent));

    /// <summary>进度信息格式化文本的样式属性</summary>
    public static readonly StyledProperty<string?> FormatProperty =
        AvaloniaProperty.Register<CyanProgress, string?>(nameof(Format));

    /// <summary>圆形进度条宽度的样式属性</summary>
    public static readonly StyledProperty<double> CircleWidthProperty =
        AvaloniaProperty.Register<CyanProgress, double>(nameof(CircleWidth), 120);

    /// <summary>步骤总数的样式属性</summary>
    public static readonly StyledProperty<int> StepsProperty =
        AvaloniaProperty.Register<CyanProgress, int>(nameof(Steps));

    /// <summary>步骤间距的样式属性</summary>
    public static readonly StyledProperty<double> GapProperty =
        AvaloniaProperty.Register<CyanProgress, double>(nameof(Gap), 2);

    /// <summary>进度信息位置的样式属性</summary>
    public static readonly StyledProperty<ProgressInfoPosition> InfoPositionProperty =
        AvaloniaProperty.Register<CyanProgress, ProgressInfoPosition>(nameof(InfoPosition), ProgressInfoPosition.Right);

    static CyanProgress()
    {
        TypeProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        PercentProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        StatusProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        StrokeColorProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        RailColorProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        SizeProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        StrokeWidthProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        GapDegreeProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        StrokeLinecapProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        ShowInfoProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        SuccessPercentProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        FormatProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        CircleWidthProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        StepsProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        GapProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
        InfoPositionProperty.Changed.AddClassHandler<CyanProgress>((p, _) => p.UpdateAll());
    }

    /// <summary>进度条类型</summary>
    public ProgressType Type
    {
        get => GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>当前进度百分比</summary>
    public double Percent
    {
        get => GetValue(PercentProperty);
        set => SetValue(PercentProperty, value);
    }

    /// <summary>是否显示进度信息</summary>
    public bool ShowInfo
    {
        get => GetValue(ShowInfoProperty);
        set => SetValue(ShowInfoProperty, value);
    }

    /// <summary>进度状态</summary>
    public ProgressStatus Status
    {
        get => GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>进度条颜色</summary>
    public IBrush? StrokeColor
    {
        get => GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);
    }

    /// <summary>轨道颜色</summary>
    public IBrush? RailColor
    {
        get => GetValue(RailColorProperty);
        set => SetValue(RailColorProperty, value);
    }

    /// <summary>控件尺寸</summary>
    public ControlSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>线条宽度</summary>
    public double StrokeWidth
    {
        get => GetValue(StrokeWidthProperty);
        set => SetValue(StrokeWidthProperty, value);
    }

    /// <summary>仪表盘缺口角度</summary>
    public double GapDegree
    {
        get => GetValue(GapDegreeProperty);
        set => SetValue(GapDegreeProperty, value);
    }

    /// <summary>线条端点形状</summary>
    public ProgressStrokeLinecap StrokeLinecap
    {
        get => GetValue(StrokeLinecapProperty);
        set => SetValue(StrokeLinecapProperty, value);
    }

    /// <summary>成功段进度百分比</summary>
    public double SuccessPercent
    {
        get => GetValue(SuccessPercentProperty);
        set => SetValue(SuccessPercentProperty, value);
    }

    /// <summary>进度信息格式化文本</summary>
    public string? Format
    {
        get => GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }

    /// <summary>圆形进度条宽度</summary>
    public double CircleWidth
    {
        get => GetValue(CircleWidthProperty);
        set => SetValue(CircleWidthProperty, value);
    }

    /// <summary>步骤总数</summary>
    public int Steps
    {
        get => GetValue(StepsProperty);
        set => SetValue(StepsProperty, value);
    }

    /// <summary>步骤间距</summary>
    public double Gap
    {
        get => GetValue(GapProperty);
        set => SetValue(GapProperty, value);
    }

    /// <summary>进度信息位置</summary>
    public ProgressInfoPosition InfoPosition
    {
        get => GetValue(InfoPositionProperty);
        set => SetValue(InfoPositionProperty, value);
    }

    private Border? _lineRail;
    private Border? _lineTrack;
    private Border? _lineSuccessTrack;
    private TextBlock? _infoText;
    private Path? _circleRail;
    private Path? _circleTrack;
    private TextBlock? _circleText;
    private Control? _statusIcon;
    private Panel? _circlePanel;
    private Grid? _lineBar;
    private Panel? _stepsPanel;
    private Panel? _circleStepsPanel;

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _lineRail = e.NameScope.Find<Border>("PART_LineRail");
        _lineTrack = e.NameScope.Find<Border>("PART_LineTrack");
        _lineSuccessTrack = e.NameScope.Find<Border>("PART_LineSuccessTrack");
        _infoText = e.NameScope.Find<TextBlock>("PART_InfoText");
        _circleRail = e.NameScope.Find<Path>("PART_CircleRail");
        _circleTrack = e.NameScope.Find<Path>("PART_CircleTrack");
        _circleText = e.NameScope.Find<TextBlock>("PART_CircleText");
        _statusIcon = e.NameScope.Find<Control>("PART_StatusIcon");
        _circlePanel = e.NameScope.Find<Panel>("PART_CirclePanel");
        _lineBar = e.NameScope.Find<Grid>("PART_LineBar");
        _stepsPanel = e.NameScope.Find<Panel>("PART_StepsPanel");
        _circleStepsPanel = e.NameScope.Find<Panel>("PART_CircleStepsPanel");

        UpdateAll();
    }

    private ProgressStatus GetEffectiveStatus()
    {
        if (Status != ProgressStatus.Normal)
            return Status;
        if (Percent >= 100)
            return ProgressStatus.Success;
        return ProgressStatus.Normal;
    }

    private IBrush GetStrokeColor()
    {
        if (StrokeColor != null)
            return StrokeColor;
        var (key, fallback) = GetEffectiveStatus() switch
        {
            ProgressStatus.Success => ("ColorSuccessBrush", Color.Parse("#52C41A")),
            ProgressStatus.Exception => ("ColorErrorBrush", Color.Parse("#FF4D4F")),
            _ => ("ColorPrimaryBrush", Color.Parse("#1677FF"))
        };
        if (this.TryFindResource(key, out var res) && res is IBrush b)
            return b;
        return new SolidColorBrush(fallback);
    }

    private IBrush GetRailColor()
    {
        if (RailColor != null)
            return RailColor;
        if (this.TryFindResource("ColorFillTertiaryBrush", out var res) && res is IBrush b)
            return b;
        return new SolidColorBrush(Color.Parse("#0F000000"));
    }

    private string GetInfoText()
    {
        if (Format != null)
            return Format;
        return $"{Math.Round(Percent)}%";
    }

    private void UpdateAll()
    {
        UpdatePseudoClasses();
        var isSteps = Steps > 0;
        if (Type == ProgressType.Line)
        {
            if (isSteps)
                UpdateLineSteps();
            else
                UpdateLine();
        }
        else
        {
            if (isSteps)
                UpdateCircleSteps();
            else
                UpdateCircle();
        }
    }

    private void UpdatePseudoClasses()
    {
        var status = GetEffectiveStatus();
        PseudoClasses.Set(":line", Type == ProgressType.Line);
        PseudoClasses.Set(":circle", Type == ProgressType.Circle);
        PseudoClasses.Set(":dashboard", Type == ProgressType.Dashboard);
        PseudoClasses.Set(":steps", Steps > 0);
        PseudoClasses.Set(":success", status == ProgressStatus.Success);
        PseudoClasses.Set(":exception", status == ProgressStatus.Exception);
        PseudoClasses.Set(":active", status == ProgressStatus.Active);
        PseudoClasses.Set(":small", Size == ControlSize.Small);
        PseudoClasses.Set(":large", Size == ControlSize.Large);
        PseudoClasses.Set(":has-info", ShowInfo);
        PseudoClasses.Set(":has-success-segment", SuccessPercent > 0);
        PseudoClasses.Set(":info-right", InfoPosition == ProgressInfoPosition.Right);
        PseudoClasses.Set(":info-top", InfoPosition == ProgressInfoPosition.Top);
        PseudoClasses.Set(":info-bottom", InfoPosition == ProgressInfoPosition.Bottom);
        PseudoClasses.Set(":info-inside", InfoPosition == ProgressInfoPosition.Inside);
        PseudoClasses.Set(":info-center", InfoPosition == ProgressInfoPosition.Center);
        PseudoClasses.Set(":info-follow", InfoPosition == ProgressInfoPosition.Follow);
    }

    private void UpdateLine()
    {
        if (_lineBar != null) _lineBar.IsVisible = true;
        if (_stepsPanel != null) _stepsPanel.IsVisible = false;

        if (_lineTrack != null)
        {
            _lineTrack.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            var parent = _lineTrack.Parent as Grid;
            if (parent != null)
            {
                var colDefs = parent.ColumnDefinitions;
                var p = Math.Clamp(Percent, 0, 100);
                if (colDefs.Count >= 2)
                {
                    colDefs[0].Width = new GridLength(p, GridUnitType.Star);
                    colDefs[1].Width = new GridLength(100 - p, GridUnitType.Star);
                }
            }
            _lineTrack.Background = GetStrokeColor();
        }

        if (_lineRail != null)
        {
            _lineRail.Background = GetRailColor();
        }

        if (_lineSuccessTrack != null && SuccessPercent > 0)
        {
            _lineSuccessTrack.Background = new SolidColorBrush(Color.Parse("#52C41A"));
            var parent = _lineSuccessTrack.Parent as Grid;
            if (parent != null && parent.ColumnDefinitions.Count >= 2)
            {
                parent.ColumnDefinitions[1].Width = new GridLength(Math.Clamp(SuccessPercent, 0, 100), GridUnitType.Star);
            }
        }

        if (_infoText != null)
        {
            _infoText.Text = GetInfoText();
            _infoText.IsVisible = ShowInfo;
        }

    }

    private void UpdateCircle()
    {
        if (_circleStepsPanel != null) _circleStepsPanel.IsVisible = false;
        if (_circleRail != null) _circleRail.IsVisible = true;

        var width = Size == ControlSize.Small ? CircleWidth * 0.75 : CircleWidth;
        var strokeWidth = StrokeWidth;
        var cx = width / 2;
        var cy = width / 2;
        var r = (width - strokeWidth) / 2;

        var status = GetEffectiveStatus();
        var strokeColor = GetStrokeColor();
        var railColor = GetRailColor();

        if (_circlePanel != null)
        {
            _circlePanel.Width = width;
            _circlePanel.Height = width;
        }

        if (Type == ProgressType.Circle)
        {
            var startAngle = -90.0;
            var totalArc = 360.0;

            if (_circleRail != null)
            {
                _circleRail.Data = CreateCirclePath(cx, cy, r);
                _circleRail.Stroke = railColor;
                _circleRail.StrokeThickness = strokeWidth;
            }

            if (_circleTrack != null)
            {
                var p = Math.Clamp(Percent, 0, 100);
                if (p > 0)
                {
                    var endAngle = startAngle + totalArc * p / 100;
                    _circleTrack.Data = CreateArcPath(cx, cy, r, startAngle, endAngle);
                    _circleTrack.Stroke = strokeColor;
                    _circleTrack.StrokeThickness = strokeWidth;
                    _circleTrack.IsVisible = true;
                }
                else
                {
                    _circleTrack.IsVisible = false;
                }
            }
        }
        else
        {
            var gapDegree = GapDegree;
            var startAngle = 90.0 + gapDegree / 2;
            var totalArc = 360.0 - gapDegree;

            if (_circleRail != null)
            {
                var railEndAngle = startAngle + totalArc;
                _circleRail.Data = CreateArcPath(cx, cy, r, startAngle, railEndAngle);
                _circleRail.Stroke = railColor;
                _circleRail.StrokeThickness = strokeWidth;
            }

            if (_circleTrack != null)
            {
                var p = Math.Clamp(Percent, 0, 100);
                if (p > 0)
                {
                    var endAngle = startAngle + totalArc * p / 100;
                    _circleTrack.Data = CreateArcPath(cx, cy, r, startAngle, endAngle);
                    _circleTrack.Stroke = strokeColor;
                    _circleTrack.StrokeThickness = strokeWidth;
                    _circleTrack.IsVisible = true;
                }
                else
                {
                    _circleTrack.IsVisible = false;
                }
            }
        }

        if (_circleText != null)
        {
            _circleText.IsVisible = ShowInfo && status is ProgressStatus.Normal or ProgressStatus.Active;
            _circleText.Text = GetInfoText();
            _circleText.FontSize = width < 60 ? 12 : 16;
        }

        if (_statusIcon != null)
        {
            _statusIcon.IsVisible = ShowInfo && status is ProgressStatus.Success or ProgressStatus.Exception;
        }
    }

    private void UpdateLineSteps()
    {
        if (_lineBar != null) _lineBar.IsVisible = false;
        if (_stepsPanel == null) return;
        _stepsPanel.IsVisible = true;

        var n = Steps;
        if (n <= 0) return;

        _stepsPanel.Children.Clear();

        var gap = Gap;
        var p = Math.Clamp(Percent, 0, 100);
        var completed = p >= 100 ? n : (int)Math.Floor(p / 100.0 * n);

        var strokeColor = GetStrokeColor();
        var railColor = GetRailColor();
        var barHeight = Size == ControlSize.Small ? 6.0 : 8.0;

        var grid = new Grid();
        for (var i = 0; i < n; i++)
            grid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(1, GridUnitType.Star)));

        for (var i = 0; i < n; i++)
        {
            var border = new Border
            {
                Height = barHeight,
                CornerRadius = new CornerRadius(100),
                Background = i < completed ? strokeColor : railColor,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch
            };
            var leftMargin = i == 0 ? 0 : gap / 2;
            var rightMargin = i == n - 1 ? 0 : gap / 2;
            border.Margin = new Thickness(leftMargin, 0, rightMargin, 0);
            Grid.SetColumn(border, i);
            grid.Children.Add(border);
        }

        _stepsPanel.Children.Add(grid);

        if (_infoText != null)
        {
            _infoText.Text = GetInfoText();
            _infoText.IsVisible = ShowInfo;
        }
    }

    private void UpdateCircleSteps()
    {
        if (_circleStepsPanel != null) _circleStepsPanel.IsVisible = false;
        if (_circleRail == null || _circleTrack == null || _circlePanel == null) return;

        var n = Steps;
        if (n <= 0) return;

        var width = Size == ControlSize.Small ? CircleWidth * 0.75 : CircleWidth;
        var strokeWidth = StrokeWidth;
        var cx = width / 2;
        var cy = width / 2;
        var r = (width - strokeWidth) / 2;

        _circlePanel.Width = width;
        _circlePanel.Height = width;

        var gap = Gap;
        var circumference = 2 * Math.PI * r;
        var gapAngle = gap * 360.0 / circumference;
        var totalArc = 360.0 - gapAngle * n;
        var segmentAngle = totalArc / n;

        var p = Math.Clamp(Percent, 0, 100);
        var completed = p >= 100 ? n : (int)Math.Floor(p / 100.0 * n);

        var status = GetEffectiveStatus();
        var strokeColor = GetStrokeColor();
        var railColor = GetRailColor();

        var startAngle = -90.0;
        var railGeo = new PathGeometry();
        var trackGeo = new PathGeometry();

        for (var i = 0; i < n; i++)
        {
            var segStart = startAngle + i * (segmentAngle + gapAngle);
            var segEnd = segStart + segmentAngle;
            var figure = CreateArcFigure(cx, cy, r, segStart, segEnd);
            if (i < completed)
                trackGeo.Figures.Add(figure);
            else
                railGeo.Figures.Add(figure);
        }

        _circleRail.IsVisible = true;
        _circleRail.Data = railGeo;
        _circleRail.Stroke = railColor;
        _circleRail.StrokeThickness = strokeWidth;
        _circleRail.StrokeLineCap = PenLineCap.Round;

        _circleTrack.IsVisible = true;
        _circleTrack.Data = trackGeo;
        _circleTrack.Stroke = strokeColor;
        _circleTrack.StrokeThickness = strokeWidth;
        _circleTrack.StrokeLineCap = PenLineCap.Round;

        if (_circleText != null)
        {
            _circleText.IsVisible = ShowInfo && status is ProgressStatus.Normal or ProgressStatus.Active;
            _circleText.Text = GetInfoText();
            _circleText.FontSize = width < 60 ? 12 : 16;
        }

        if (_statusIcon != null)
        {
            _statusIcon.IsVisible = ShowInfo && status is ProgressStatus.Success or ProgressStatus.Exception;
        }
    }

    private static PathFigure CreateArcFigure(double cx, double cy, double r, double startAngle, double endAngle)
    {
        var startRad = startAngle * Math.PI / 180;
        var endRad = endAngle * Math.PI / 180;

        var startX = cx + r * Math.Cos(startRad);
        var startY = cy + r * Math.Sin(startRad);
        var endX = cx + r * Math.Cos(endRad);
        var endY = cy + r * Math.Sin(endRad);

        var arcAngle = endAngle - startAngle;
        var isLargeArc = Math.Abs(arcAngle) > 180;

        var figure = new PathFigure { StartPoint = new Point(startX, startY), IsClosed = false };
        figure.Segments.Add(new ArcSegment
        {
            Point = new Point(endX, endY),
            Size = new Size(r, r),
            IsLargeArc = isLargeArc,
            SweepDirection = SweepDirection.Clockwise
        });
        return figure;
    }

    private static Geometry CreateCirclePath(double cx, double cy, double r)
    {
        return new EllipseGeometry(new Rect(cx - r, cy - r, r * 2, r * 2));
    }

    private static Geometry CreateArcPath(double cx, double cy, double r, double startAngle, double endAngle)
    {
        var startRad = startAngle * Math.PI / 180;
        var endRad = endAngle * Math.PI / 180;

        var startX = cx + r * Math.Cos(startRad);
        var startY = cy + r * Math.Sin(startRad);
        var endX = cx + r * Math.Cos(endRad);
        var endY = cy + r * Math.Sin(endRad);

        var arcAngle = endAngle - startAngle;
        var largeArcFlag = Math.Abs(arcAngle) > 180 ? 1 : 0;
        var sweepFlag = 1;

        var pathStr = $"M {startX:F4} {startY:F4} A {r:F4} {r:F4} 0 {largeArcFlag} {sweepFlag} {endX:F4} {endY:F4}";
        return PathGeometry.Parse(pathStr);
    }
}
