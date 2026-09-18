using System.Collections.Generic;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Cyan.Design.Core.Controls.Navigation;

/// <summary>导航视图控件，提供可折叠侧边栏导航</summary>
public class CyanNavigationView : TemplatedControl
{
    /// <summary>导航项列表样式属性</summary>
    public static readonly StyledProperty<IList<CyanNavigationViewItem>> ItemsProperty =
        AvaloniaProperty.Register<CyanNavigationView, IList<CyanNavigationViewItem>>(nameof(Items));

    /// <summary>底部导航项列表样式属性</summary>
    public static readonly StyledProperty<IList<CyanNavigationViewItem>> FooterItemsProperty =
        AvaloniaProperty.Register<CyanNavigationView, IList<CyanNavigationViewItem>>(nameof(FooterItems));

    /// <summary>选中项样式属性</summary>
    public static readonly StyledProperty<CyanNavigationViewItem?> SelectedItemProperty =
        AvaloniaProperty.Register<CyanNavigationView, CyanNavigationViewItem?>(nameof(SelectedItem));

    /// <summary>窗格标题样式属性</summary>
    public static readonly StyledProperty<string?> PaneTitleProperty =
        AvaloniaProperty.Register<CyanNavigationView, string?>(nameof(PaneTitle));

    /// <summary>窗格显示模式样式属性</summary>
    public static readonly StyledProperty<CyanNavigationViewPaneDisplayMode> PaneDisplayModeProperty =
        AvaloniaProperty.Register<CyanNavigationView, CyanNavigationViewPaneDisplayMode>(
            nameof(PaneDisplayMode), defaultValue: CyanNavigationViewPaneDisplayMode.Left);

    /// <summary>窗格是否展开样式属性</summary>
    public static readonly StyledProperty<bool> IsPaneOpenProperty =
        AvaloniaProperty.Register<CyanNavigationView, bool>(nameof(IsPaneOpen), defaultValue: true);

    /// <summary>展开时窗格宽度样式属性</summary>
    public static readonly StyledProperty<double> OpenPaneLengthProperty =
        AvaloniaProperty.Register<CyanNavigationView, double>(nameof(OpenPaneLength), defaultValue: 240);

    /// <summary>紧凑模式窗格宽度样式属性</summary>
    public static readonly StyledProperty<double> CompactPaneLengthProperty =
        AvaloniaProperty.Register<CyanNavigationView, double>(nameof(CompactPaneLength), defaultValue: 48);

    /// <summary>是否启用后退样式属性</summary>
    public static readonly StyledProperty<bool> IsBackEnabledProperty =
        AvaloniaProperty.Register<CyanNavigationView, bool>(nameof(IsBackEnabled));

    /// <summary>实际窗格宽度样式属性</summary>
    public static readonly StyledProperty<double> ActualPaneWidthProperty =
        AvaloniaProperty.Register<CyanNavigationView, double>(nameof(ActualPaneWidth), defaultValue: 240);

    /// <summary>请求后退路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> BackRequestedEvent =
        RoutedEvent.Register<CyanNavigationView, RoutedEventArgs>(nameof(BackRequested), RoutingStrategies.Bubble);

    /// <summary>选中项变化路由事件</summary>
    public static readonly RoutedEvent<SelectionChangedEventArgs> SelectionChangedEvent =
        RoutedEvent.Register<CyanNavigationView, SelectionChangedEventArgs>(nameof(SelectionChanged), RoutingStrategies.Bubble);

    /// <summary>请求后退事件</summary>
    public event EventHandler<RoutedEventArgs>? BackRequested
    {
        add => AddHandler(BackRequestedEvent, value);
        remove => RemoveHandler(BackRequestedEvent, value);
    }

    /// <summary>选中项变化事件</summary>
    public event EventHandler<SelectionChangedEventArgs>? SelectionChanged
    {
        add => AddHandler(SelectionChangedEvent, value);
        remove => RemoveHandler(SelectionChangedEvent, value);
    }

    /// <summary>初始化 CyanNavigationView 的新实例</summary>
    public CyanNavigationView()
    {
        Items = new AvaloniaList<CyanNavigationViewItem>();
        FooterItems = new AvaloniaList<CyanNavigationViewItem>();
    }

    /// <summary>导航项列表</summary>
    public IList<CyanNavigationViewItem> Items
    {
        get => GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    /// <summary>底部导航项列表</summary>
    public IList<CyanNavigationViewItem> FooterItems
    {
        get => GetValue(FooterItemsProperty);
        set => SetValue(FooterItemsProperty, value);
    }

    /// <summary>当前选中项</summary>
    public CyanNavigationViewItem? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    /// <summary>窗格标题</summary>
    public string? PaneTitle
    {
        get => GetValue(PaneTitleProperty);
        set => SetValue(PaneTitleProperty, value);
    }

    /// <summary>窗格显示模式</summary>
    public CyanNavigationViewPaneDisplayMode PaneDisplayMode
    {
        get => GetValue(PaneDisplayModeProperty);
        set => SetValue(PaneDisplayModeProperty, value);
    }

    /// <summary>窗格是否展开</summary>
    public bool IsPaneOpen
    {
        get => GetValue(IsPaneOpenProperty);
        set => SetValue(IsPaneOpenProperty, value);
    }

    /// <summary>展开时窗格宽度</summary>
    public double OpenPaneLength
    {
        get => GetValue(OpenPaneLengthProperty);
        set => SetValue(OpenPaneLengthProperty, value);
    }

    /// <summary>紧凑模式窗格宽度</summary>
    public double CompactPaneLength
    {
        get => GetValue(CompactPaneLengthProperty);
        set => SetValue(CompactPaneLengthProperty, value);
    }

    /// <summary>是否启用后退</summary>
    public bool IsBackEnabled
    {
        get => GetValue(IsBackEnabledProperty);
        set => SetValue(IsBackEnabledProperty, value);
    }

    /// <summary>实际窗格宽度</summary>
    public double ActualPaneWidth
    {
        get => GetValue(ActualPaneWidthProperty);
        set => SetValue(ActualPaneWidthProperty, value);
    }

    static CyanNavigationView()
    {
        IsPaneOpenProperty.Changed.AddClassHandler<CyanNavigationView>(OnPaneOpenChanged);
        OpenPaneLengthProperty.Changed.AddClassHandler<CyanNavigationView>(OnPaneMetricsChanged);
        CompactPaneLengthProperty.Changed.AddClassHandler<CyanNavigationView>(OnPaneMetricsChanged);
        PaneDisplayModeProperty.Changed.AddClassHandler<CyanNavigationView>(OnPaneModeChanged);
    }

    private static void OnPaneOpenChanged(CyanNavigationView c, AvaloniaPropertyChangedEventArgs e)
        => c.UpdateActualPaneWidth();

    private static void OnPaneMetricsChanged(CyanNavigationView c, AvaloniaPropertyChangedEventArgs e)
        => c.UpdateActualPaneWidth();

    private static void OnPaneModeChanged(CyanNavigationView c, AvaloniaPropertyChangedEventArgs e)
        => c.UpdateActualPaneWidth();

    private void UpdateActualPaneWidth()
    {
        var mode = PaneDisplayMode;
        var open = IsPaneOpen;
        ActualPaneWidth = mode switch
        {
            CyanNavigationViewPaneDisplayMode.LeftMinimal => open ? OpenPaneLength : 0,
            CyanNavigationViewPaneDisplayMode.LeftCompact => open ? OpenPaneLength : CompactPaneLength,
            _ => open ? OpenPaneLength : CompactPaneLength,
        };
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (e.NameScope.Find<Button>("PART_PaneToggleButton") is { } toggle)
            toggle.Click += (_, _) => IsPaneOpen = !IsPaneOpen;

        if (e.NameScope.Find<ListBox>("PART_Nav") is { } nav)
        {
            nav.SelectionChanged += (_, args) =>
            {
                if (nav.SelectedItem is CyanNavigationViewItem item)
                {
                    SelectedItem = item;
                    RaiseEvent(new SelectionChangedEventArgs(SelectionChangedEvent, args.RemovedItems, args.AddedItems));
                }
            };
        }

        if (e.NameScope.Find<ListBox>("PART_FooterNav") is { } footer)
        {
            footer.SelectionChanged += (_, args) =>
            {
                if (footer.SelectedItem is CyanNavigationViewItem item)
                {
                    SelectedItem = item;
                    RaiseEvent(new SelectionChangedEventArgs(SelectionChangedEvent, args.RemovedItems, args.AddedItems));
                }
            };
        }

        UpdateActualPaneWidth();
    }
}
