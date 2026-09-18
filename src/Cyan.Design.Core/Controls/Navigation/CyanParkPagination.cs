using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Navigation;

/// <summary>分页项类型</summary>
public enum PaginationItemType
{
    /// <summary>页码项</summary>
    Page,
    /// <summary>省略号项</summary>
    Ellipsis
}

/// <summary>分页项信息</summary>
public sealed class PaginationItemInfo
{
    /// <summary>分页项类型</summary>
    public required PaginationItemType Type { get; init; }
    /// <summary>页码值</summary>
    public int Value { get; init; }
}

/// <summary>分页控件，提供页码导航</summary>
public class CyanParkPagination : TemplatedControl
{
    /// <summary>总记录数样式属性</summary>
    public static readonly StyledProperty<int> CountProperty =
        AvaloniaProperty.Register<CyanParkPagination, int>(nameof(Count));

    /// <summary>当前页码样式属性</summary>
    public static readonly StyledProperty<int> PageProperty =
        AvaloniaProperty.Register<CyanParkPagination, int>(nameof(Page), 1, defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    /// <summary>默认页码样式属性</summary>
    public static readonly StyledProperty<int> DefaultPageProperty =
        AvaloniaProperty.Register<CyanParkPagination, int>(nameof(DefaultPage), 1);

    /// <summary>每页记录数样式属性</summary>
    public static readonly StyledProperty<int> PageSizeProperty =
        AvaloniaProperty.Register<CyanParkPagination, int>(nameof(PageSize), 10);

    /// <summary>默认每页记录数样式属性</summary>
    public static readonly StyledProperty<int> DefaultPageSizeProperty =
        AvaloniaProperty.Register<CyanParkPagination, int>(nameof(DefaultPageSize), 10);

    /// <summary>当前页相邻页数样式属性</summary>
    public static readonly StyledProperty<int> SiblingCountProperty =
        AvaloniaProperty.Register<CyanParkPagination, int>(nameof(SiblingCount), 1);

    /// <summary>分页尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> PaginationSizeProperty =
        AvaloniaProperty.Register<CyanParkPagination, ControlSize>(nameof(PaginationSize), ControlSize.Middle);

    /// <summary>总页数样式属性</summary>
    public static readonly StyledProperty<int> TotalPagesProperty =
        AvaloniaProperty.Register<CyanParkPagination, int>(nameof(TotalPages), 1);

    /// <summary>页码变化路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> PageChangedEvent =
        RoutedEvent.Register<CyanParkPagination, RoutedEventArgs>(nameof(PageChanged), RoutingStrategies.Bubble);

    private StackPanel? _itemsHost;
    private Button? _prevButton;
    private Button? _nextButton;

    /// <summary>总记录数</summary>
    public int Count
    {
        get => GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    /// <summary>当前页码</summary>
    public int Page
    {
        get => GetValue(PageProperty);
        set => SetValue(PageProperty, value);
    }

    /// <summary>默认页码</summary>
    public int DefaultPage
    {
        get => GetValue(DefaultPageProperty);
        set => SetValue(DefaultPageProperty, value);
    }

    /// <summary>每页记录数</summary>
    public int PageSize
    {
        get => GetValue(PageSizeProperty);
        set => SetValue(PageSizeProperty, value);
    }

    /// <summary>默认每页记录数</summary>
    public int DefaultPageSize
    {
        get => GetValue(DefaultPageSizeProperty);
        set => SetValue(DefaultPageSizeProperty, value);
    }

    /// <summary>当前页相邻显示的页数</summary>
    public int SiblingCount
    {
        get => GetValue(SiblingCountProperty);
        set => SetValue(SiblingCountProperty, value);
    }

    /// <summary>分页尺寸</summary>
    public ControlSize PaginationSize
    {
        get => GetValue(PaginationSizeProperty);
        set => SetValue(PaginationSizeProperty, value);
    }

    /// <summary>总页数</summary>
    public int TotalPages => GetValue(TotalPagesProperty);

    /// <summary>页码变化事件</summary>
    public event EventHandler<RoutedEventArgs>? PageChanged
    {
        add => AddHandler(PageChangedEvent, value);
        remove => RemoveHandler(PageChangedEvent, value);
    }

    static CyanParkPagination()
    {
        CountProperty.Changed.AddClassHandler<CyanParkPagination>((t, _) => t.Rebuild());
        PageProperty.Changed.AddClassHandler<CyanParkPagination>((t, _) => t.Rebuild());
        PageSizeProperty.Changed.AddClassHandler<CyanParkPagination>((t, _) => t.Rebuild());
        SiblingCountProperty.Changed.AddClassHandler<CyanParkPagination>((t, _) => t.Rebuild());
        PaginationSizeProperty.Changed.AddClassHandler<CyanParkPagination>((t, _) => t.UpdatePseudoClasses());
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _itemsHost = e.NameScope.Find<StackPanel>("PART_ItemsHost");
        _prevButton = e.NameScope.Find<Button>("PART_PrevButton");
        _nextButton = e.NameScope.Find<Button>("PART_NextButton");

        if (_prevButton is not null)
            _prevButton.Click += OnPrevClick;
        if (_nextButton is not null)
            _nextButton.Click += OnNextClick;

        UpdatePseudoClasses();
        Rebuild();
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (Page <= 0 && DefaultPage > 0)
            Page = DefaultPage;
        if (PageSize <= 0 && DefaultPageSize > 0)
            PageSize = DefaultPageSize;
    }

    private void OnPrevClick(object? sender, RoutedEventArgs e)
    {
        if (Page > 1)
            SetPage(Page - 1);
    }

    private void OnNextClick(object? sender, RoutedEventArgs e)
    {
        if (Page < TotalPages)
            SetPage(Page + 1);
    }

    private void SetPage(int newPage)
    {
        Page = newPage;
        RaiseEvent(new RoutedEventArgs(PageChangedEvent));
    }

    private void Rebuild()
    {
        if (_itemsHost is null) return;

        var totalPages = PageSize > 0 ? (int)Math.Ceiling((double)Count / PageSize) : 1;
        if (totalPages < 1) totalPages = 1;
        SetValue(TotalPagesProperty, totalPages);

        var currentPage = Math.Clamp(Page, 1, totalPages);
        if (Page != currentPage)
        {
            Page = currentPage;
            return;
        }

        if (_prevButton is not null)
            _prevButton.IsEnabled = currentPage > 1;
        if (_nextButton is not null)
            _nextButton.IsEnabled = currentPage < totalPages;

        _itemsHost.Children.Clear();

        foreach (var p in GetPages(currentPage, totalPages, SiblingCount))
        {
            _itemsHost.Children.Add(p.Type == PaginationItemType.Ellipsis
                ? CreateEllipsis()
                : CreatePageButton(p.Value, currentPage));
        }
    }

    private static List<PaginationItemInfo> GetPages(int current, int total, int sibling)
    {
        var result = new List<PaginationItemInfo>();

        if (total <= 2 * sibling + 5)
        {
            for (var i = 1; i <= total; i++)
                result.Add(new PaginationItemInfo { Type = PaginationItemType.Page, Value = i });
            return result;
        }

        var left = Math.Max(current - sibling, 1);
        var right = Math.Min(current + sibling, total);
        var showLeftEllipsis = left > 2;
        var showRightEllipsis = right < total - 1;

        result.Add(new PaginationItemInfo { Type = PaginationItemType.Page, Value = 1 });

        if (showLeftEllipsis)
            result.Add(new PaginationItemInfo { Type = PaginationItemType.Ellipsis });

        for (var i = left; i <= right; i++)
        {
            if (i == 1 || i == total) continue;
            result.Add(new PaginationItemInfo { Type = PaginationItemType.Page, Value = i });
        }

        if (showRightEllipsis)
            result.Add(new PaginationItemInfo { Type = PaginationItemType.Ellipsis });

        result.Add(new PaginationItemInfo { Type = PaginationItemType.Page, Value = total });
        return result;
    }

    private Button CreatePageButton(int value, int current)
    {
        var btn = new Button
        {
            Content = value.ToString(),
            Classes = { "pagination-item" },
        };
        if (value == current)
            btn.Classes.Add("selected");
        btn.Click += (_, _) => SetPage(value);
        return btn;
    }

    private TextBlock CreateEllipsis()
    {
        return new TextBlock
        {
            Text = "\u22EF",
            Classes = { "pagination-ellipsis" },
        };
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":small", PaginationSize == ControlSize.Small);
        PseudoClasses.Set(":large", PaginationSize == ControlSize.Large);
    }
}