using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.VisualTree;

namespace Cyan.Design.Core.Controls.Display;

/// <summary>徽标控件，用于显示计数、状态点或文本标记</summary>
public class CyanBadge : ContentControl
{
    /// <summary>计数样式属性</summary>
    public static readonly StyledProperty<int> CountProperty =
        AvaloniaProperty.Register<CyanBadge, int>(nameof(Count), 0);

    /// <summary>溢出计数阈值样式属性</summary>
    public static readonly StyledProperty<int> OverflowCountProperty =
        AvaloniaProperty.Register<CyanBadge, int>(nameof(OverflowCount), 99);

    /// <summary>是否在计数为零时仍显示样式属性</summary>
    public static readonly StyledProperty<bool> ShowZeroProperty =
        AvaloniaProperty.Register<CyanBadge, bool>(nameof(ShowZero));

    /// <summary>是否以圆点形式显示样式属性</summary>
    public static readonly StyledProperty<bool> DotProperty =
        AvaloniaProperty.Register<CyanBadge, bool>(nameof(Dot));

    /// <summary>偏移量样式属性</summary>
    public static readonly StyledProperty<Point> OffsetProperty =
        AvaloniaProperty.Register<CyanBadge, Point>(nameof(Offset));

    /// <summary>徽标尺寸样式属性</summary>
    public static readonly StyledProperty<BadgeSize> SizeProperty =
        AvaloniaProperty.Register<CyanBadge, BadgeSize>(nameof(Size), BadgeSize.Default);

    /// <summary>徽标状态样式属性</summary>
    public static readonly StyledProperty<BadgeStatus> StatusProperty =
        AvaloniaProperty.Register<CyanBadge, BadgeStatus>(nameof(Status), BadgeStatus.None);

    /// <summary>文本内容样式属性</summary>
    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<CyanBadge, string?>(nameof(Text));

    /// <summary>自定义颜色样式属性</summary>
    public static readonly StyledProperty<IBrush?> ColorProperty =
        AvaloniaProperty.Register<CyanBadge, IBrush?>(nameof(Color));

    /// <summary>徽标实际显示颜色样式属性</summary>
    public static readonly StyledProperty<IBrush?> BadgeColorProperty =
        AvaloniaProperty.Register<CyanBadge, IBrush?>(nameof(BadgeColor));

    /// <summary>标题样式属性，用于悬停提示</summary>
    public static readonly StyledProperty<string?> TitleProperty =
        AvaloniaProperty.Register<CyanBadge, string?>(nameof(Title));

    private Border? _countBorder;
    private TextBlock? _countText;
    private Border? _dotBorder;
    private StackPanel? _statusPanel;


    static CyanBadge()
    {
        CountProperty.Changed.AddClassHandler<CyanBadge>((b, _) => b.UpdateDisplay());
        OverflowCountProperty.Changed.AddClassHandler<CyanBadge>((b, _) => b.UpdateDisplay());
        ShowZeroProperty.Changed.AddClassHandler<CyanBadge>((b, _) => b.UpdateDisplay());
        DotProperty.Changed.AddClassHandler<CyanBadge>((b, _) => b.UpdateDisplay());
        OffsetProperty.Changed.AddClassHandler<CyanBadge>((b, _) => b.UpdateOffset());
        SizeProperty.Changed.AddClassHandler<CyanBadge>((b, _) => b.UpdatePseudoClasses());
        StatusProperty.Changed.AddClassHandler<CyanBadge>((b, _) => { b.UpdatePseudoClasses(); b.UpdateBadgeColor(); b.UpdateDisplay(); });
        ColorProperty.Changed.AddClassHandler<CyanBadge>((b, _) => b.UpdateBadgeColor());
    }

    /// <summary>计数，徽标显示的数字</summary>
    public int Count
    {
        get => GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    /// <summary>溢出计数阈值，超过该值显示加号</summary>
    public int OverflowCount
    {
        get => GetValue(OverflowCountProperty);
        set => SetValue(OverflowCountProperty, value);
    }

    /// <summary>是否在计数为零时仍显示徽标</summary>
    public bool ShowZero
    {
        get => GetValue(ShowZeroProperty);
        set => SetValue(ShowZeroProperty, value);
    }

    /// <summary>是否以圆点形式显示徽标</summary>
    public bool Dot
    {
        get => GetValue(DotProperty);
        set => SetValue(DotProperty, value);
    }

    /// <summary>徽标偏移量，用于调整位置</summary>
    public Point Offset
    {
        get => GetValue(OffsetProperty);
        set => SetValue(OffsetProperty, value);
    }

    /// <summary>徽标尺寸</summary>
    public BadgeSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>徽标状态，用于显示不同颜色的状态点</summary>
    public BadgeStatus Status
    {
        get => GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>文本内容，用于自定义显示文本</summary>
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>自定义徽标颜色</summary>
    public IBrush? Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <summary>徽标实际显示颜色</summary>
    public IBrush? BadgeColor
    {
        get => GetValue(BadgeColorProperty);
        set => SetValue(BadgeColorProperty, value);
    }

    /// <summary>标题，用于悬停提示</summary>
    public string? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _countBorder = e.NameScope.Find<Border>("PART_Count");
        _countText = e.NameScope.Find<TextBlock>("PART_CountText");
        _dotBorder = e.NameScope.Find<Border>("PART_Dot");
        _statusPanel = e.NameScope.Find<StackPanel>("PART_Status");

        UpdatePseudoClasses();
        UpdateBadgeColor();
        UpdateDisplay();
        UpdateOffset();
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdatePseudoClasses();
        UpdateBadgeColor();
        UpdateDisplay();
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":small", Size == BadgeSize.Small);
        PseudoClasses.Set(":hasstatus", Status != BadgeStatus.None);
        PseudoClasses.Set(":status-success", Status == BadgeStatus.Success);
        PseudoClasses.Set(":status-processing", Status == BadgeStatus.Processing);
        PseudoClasses.Set(":status-error", Status == BadgeStatus.Error);
        PseudoClasses.Set(":status-warning", Status == BadgeStatus.Warning);
        PseudoClasses.Set(":status-default", Status == BadgeStatus.Default);
        PseudoClasses.Set(":standalone", Content == null);
    }

    private void UpdateBadgeColor()
    {
        if (Color != null)
        {
            BadgeColor = Color;
            return;
        }
        if (Status == BadgeStatus.None)
        {
            return;
        }
        BadgeColor = Status switch
        {
            BadgeStatus.Success => this.FindResource("ColorSuccessBrush") as IBrush,
            BadgeStatus.Processing => this.FindResource("ColorInfoBrush") as IBrush,
            BadgeStatus.Error => this.FindResource("ColorErrorBrush") as IBrush,
            BadgeStatus.Warning => this.FindResource("ColorWarningBrush") as IBrush,
            BadgeStatus.Default => this.TryFindResource("ColorBorderBrush", out var r) && r is IBrush b ? b : new SolidColorBrush(0xFFD9D9D9),
            _ => null
        };
    }

    private void UpdateDisplay()
    {
        if (_countBorder == null || _dotBorder == null || _statusPanel == null || _countText == null)
            return;

        bool isStatus = Status != BadgeStatus.None;
        _statusPanel.IsVisible = isStatus;

        if (isStatus)
        {
            _countBorder.IsVisible = false;
            _dotBorder.IsVisible = false;
            PseudoClasses.Set(":hascount", false);
            PseudoClasses.Set(":hasdot", false);
            return;
        }

        bool hasDot = Dot;
        _dotBorder.IsVisible = hasDot;

        bool showCount = !hasDot && (Count > 0 || (Count == 0 && ShowZero));
        _countBorder.IsVisible = showCount;

        if (showCount)
        {
            _countText.Text = Count > OverflowCount ? $"{OverflowCount}+" : Count.ToString();
        }

        PseudoClasses.Set(":hascount", showCount);
        PseudoClasses.Set(":hasdot", hasDot);
        PseudoClasses.Set(":overflow", showCount && Count > OverflowCount);
    }

    private void UpdateOffset()
    {
        if (_countBorder == null || _dotBorder == null) return;
        var offset = Offset;
        bool isStandalone = Content == null;
        if (isStandalone)
        {
            _countBorder.Margin = new Thickness(offset.X, offset.Y, 0, 0);
            _dotBorder.Margin = new Thickness(offset.X, offset.Y, 0, 0);
        }
        else
        {
            _countBorder.Margin = new Thickness(offset.X, -10 + offset.Y, -2, 0);
            _dotBorder.Margin = new Thickness(offset.X, -3 + offset.Y, -2, 0);
        }
    }

    /// <summary>属性改变事件处理</summary>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == ContentProperty)
        {
            UpdatePseudoClasses();
            UpdateOffset();
        }
    }
}
