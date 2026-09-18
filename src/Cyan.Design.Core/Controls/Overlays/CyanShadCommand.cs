using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace Cyan.Design.Core.Controls.Overlays;

/// <summary>命令面板控件，提供搜索过滤与键盘选择</summary>
public class CyanShadCommand : Border
{
    /// <summary>搜索文本样式属性</summary>
    public static readonly StyledProperty<string?> SearchTextProperty =
        AvaloniaProperty.Register<CyanShadCommand, string?>(nameof(SearchText));

    /// <summary>搜索文本</summary>
    public string? SearchText
    {
        get => GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }

    private int _selectedIndex = -1;
    private TextBox? _input;

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        Dispatcher.UIThread.Post(Initialize, DispatcherPriority.Normal);
    }

    private void Initialize()
    {
        FindInput();
        FilterItems();
    }

    private List<CyanShadCommandItem> CollectAllItems()
    {
        var items = new List<CyanShadCommandItem>();
        var visited = new HashSet<object>();
        CollectItemsRecursive(this, items, visited);
        return items;
    }

    private static void CollectItemsRecursive(object? node, List<CyanShadCommandItem> items, HashSet<object> visited)
    {
        if (node is null || !visited.Add(node))
            return;

        if (node is CyanShadCommandItem item)
        {
            items.Add(item);
        }

        switch (node)
        {
            case Panel panel:
                foreach (var child in panel.Children)
                    CollectItemsRecursive(child, items, visited);
                break;
            case ContentControl cc:
                CollectItemsRecursive(cc.Content, items, visited);
                break;

            case Border b:
                CollectItemsRecursive(b.Child, items, visited);
                break;
            case ItemsControl ic:
                foreach (var child in ic.Items)
                    CollectItemsRecursive(child, items, visited);
                break;
        }
    }

    private void FindInput()
    {
        foreach (var descendant in this.GetVisualDescendants())
        {
            if (descendant is TextBox tb && tb.Classes.Contains("ShadCommandInput"))
            {
                if (_input == tb) return;
                if (_input is not null)
                {
                    _input.PropertyChanged -= OnInputPropertyChanged;
                    _input.KeyDown -= OnInputKeyDown;
                }
                _input = tb;
                _input.PropertyChanged += OnInputPropertyChanged;
                _input.KeyDown += OnInputKeyDown;
                return;
            }
        }
    }

    private void OnInputPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == TextBox.TextProperty && sender is TextBox tb)
        {
            SearchText = tb.Text;
        }
    }

    private void OnInputKeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Down:
                SelectNext();
                e.Handled = true;
                return;
            case Key.Up:
                SelectPrevious();
                e.Handled = true;
                return;
            case Key.Enter:
                InvokeSelected();
                e.Handled = true;
                return;
        }
    }

    /// <summary>属性改变事件处理</summary>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == SearchTextProperty)
        {
            FilterItems();
        }
    }

    /// <summary>根据当前搜索文本过滤命令项的可见性</summary>
    public void FilterItems()
    {
        try
        {
            DoFilterItems();
        }
        catch
        {
        }
    }

    private void DoFilterItems()
    {
        var search = (SearchText ?? string.Empty).Trim();
        var allItems = CollectAllItems();
        var visibleItems = new List<CyanShadCommandItem>();

        foreach (var item in allItems)
        {
            var value = item.Value ?? item.Content?.ToString() ?? string.Empty;
            var match = string.IsNullOrEmpty(search) ||
                value.Contains(search, StringComparison.OrdinalIgnoreCase);
            item.IsVisible = match;
            if (match) visibleItems.Add(item);
        }

        foreach (var descendant in this.GetVisualDescendants())
        {
            if (descendant is CyanShadCommandGroup group)
            {
                group.UpdateVisibility();
            }
        }

        foreach (var descendant in this.GetVisualDescendants())
        {
            if (descendant is CyanShadCommandEmpty empty)
            {
                empty.IsVisible = visibleItems.Count == 0;
            }
        }

        _selectedIndex = visibleItems.Count > 0 ? 0 : -1;
        UpdateSelection(visibleItems);
    }

    internal List<CyanShadCommandItem> GetVisibleItems()
    {
        return CollectAllItems().Where(i => i.IsVisible).ToList();
    }

    internal void SelectNext()
    {
        var items = GetVisibleItems();
        if (items.Count == 0) return;
        _selectedIndex = (_selectedIndex + 1) % items.Count;
        UpdateSelection(items);
        items[_selectedIndex].BringIntoView();
    }

    internal void SelectPrevious()
    {
        var items = GetVisibleItems();
        if (items.Count == 0) return;
        _selectedIndex = _selectedIndex <= 0 ? items.Count - 1 : _selectedIndex - 1;
        UpdateSelection(items);
        items[_selectedIndex].BringIntoView();
    }

    internal void InvokeSelected()
    {
        var items = GetVisibleItems();
        if (_selectedIndex >= 0 && _selectedIndex < items.Count)
        {
            items[_selectedIndex].InvokeClick();
        }
    }

    private void UpdateSelection(List<CyanShadCommandItem> items)
    {
        for (var i = 0; i < items.Count; i++)
        {
            if (i == _selectedIndex)
                items[i].Classes.Add("selected");
            else
                items[i].Classes.Remove("selected");
        }
    }
}

/// <summary>命令面板空状态提示控件</summary>
public class CyanShadCommandEmpty : TextBlock
{
    /// <summary>初始化 CyanShadCommandEmpty 的新实例</summary>
    public CyanShadCommandEmpty()
    {
        Padding = new Thickness(12, 10);
        HorizontalAlignment = HorizontalAlignment.Stretch;
        Foreground = this.TryFindResource("ColorTextSecondaryBrush", out var r) && r is IBrush b ? b : new SolidColorBrush(Color.Parse("#71717A"));
        FontSize = 14;
    }
}

/// <summary>命令面板分组控件，带分组标题</summary>
public class CyanShadCommandGroup : ContentControl
{
    /// <summary>分组标题样式属性</summary>
    public static readonly StyledProperty<string?> HeadingProperty =
        AvaloniaProperty.Register<CyanShadCommandGroup, string?>(nameof(Heading));

    /// <summary>分组标题</summary>
    public string? Heading
    {
        get => GetValue(HeadingProperty);
        set => SetValue(HeadingProperty, value);
    }

    /// <summary>根据子项可见性更新分组可见性</summary>
    public void UpdateVisibility()
    {
        try
        {
            var hasVisibleItem = false;
            var visited = new HashSet<object>();
            CheckVisibleItems(Content, visited, ref hasVisibleItem);
            IsVisible = hasVisibleItem;
        }
        catch
        {
        }
    }

    private static void CheckVisibleItems(object? node, HashSet<object> visited, ref bool hasVisible)
    {
        if (node is null || !visited.Add(node) || hasVisible)
            return;

        if (node is CyanShadCommandItem item && item.IsVisible)
        {
            hasVisible = true;
            return;
        }

        switch (node)
        {
            case Panel panel:
                foreach (var child in panel.Children)
                    CheckVisibleItems(child, visited, ref hasVisible);
                break;
            case ContentControl cc:
                CheckVisibleItems(cc.Content, visited, ref hasVisible);
                break;

            case Border b:
                CheckVisibleItems(b.Child, visited, ref hasVisible);
                break;
        }
    }
}

/// <summary>命令面板项控件</summary>
public class CyanShadCommandItem : Button
{
    /// <summary>项值样式属性</summary>
    public static readonly StyledProperty<string?> ValueProperty =
        AvaloniaProperty.Register<CyanShadCommandItem, string?>(nameof(Value));

    /// <summary>图标样式属性</summary>
    public static readonly StyledProperty<object?> IconProperty =
        AvaloniaProperty.Register<CyanShadCommandItem, object?>(nameof(Icon));

    /// <summary>快捷键样式属性</summary>
    public static readonly StyledProperty<string?> ShortcutProperty =
        AvaloniaProperty.Register<CyanShadCommandItem, string?>(nameof(Shortcut));

    /// <summary>项值</summary>
    public string? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>图标内容</summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>快捷键文本</summary>
    public string? Shortcut
    {
        get => GetValue(ShortcutProperty);
        set => SetValue(ShortcutProperty, value);
    }

    /// <summary>初始化 CyanShadCommandItem 的新实例</summary>
    public CyanShadCommandItem()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        HorizontalContentAlignment = HorizontalAlignment.Left;
    }

    internal void InvokeClick()
    {
        OnClick();
    }
}

/// <summary>命令面板分隔符控件</summary>
public class CyanShadCommandSeparator : Border
{
    /// <summary>初始化 CyanShadCommandSeparator 的新实例</summary>
    public CyanShadCommandSeparator()
    {
        Height = 1;
        Background = this.TryFindResource("ColorBorderBrush", out var r) && r is IBrush b ? b : new SolidColorBrush(Color.Parse("#E4E4E7"));
        Margin = new Thickness(0, 4);
    }
}

/// <summary>命令面板快捷键显示控件</summary>
public class CyanShadCommandShortcut : TextBlock
{
    /// <summary>初始化 CyanShadCommandShortcut 的新实例</summary>
    public CyanShadCommandShortcut()
    {
        FontSize = 12;
        Foreground = this.TryFindResource("ColorTextSecondaryBrush", out var r) && r is IBrush b ? b : new SolidColorBrush(Color.Parse("#71717A"));
        HorizontalAlignment = HorizontalAlignment.Right;
    }
}

/// <summary>命令面板对话框控件，以浮层形式承载命令面板</summary>
public class CyanShadCommandDialog : TemplatedControl
{
    /// <summary>是否展开样式属性</summary>
    public static readonly StyledProperty<bool> IsOpenProperty =
        AvaloniaProperty.Register<CyanShadCommandDialog, bool>(nameof(IsOpen));

    /// <summary>内容样式属性</summary>
    public static readonly StyledProperty<object?> ContentProperty =
        AvaloniaProperty.Register<CyanShadCommandDialog, object?>(nameof(Content));

    /// <summary>是否显示遮罩样式属性</summary>
    public static readonly StyledProperty<bool> ShowOverlayProperty =
        AvaloniaProperty.Register<CyanShadCommandDialog, bool>(nameof(ShowOverlay), defaultValue: true);

    /// <summary>是否展开</summary>
    public bool IsOpen
    {
        get => GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>对话框内容</summary>
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>是否显示遮罩层</summary>
    public bool ShowOverlay
    {
        get => GetValue(ShowOverlayProperty);
        set => SetValue(ShowOverlayProperty, value);
    }

    private Popup? _popup;
    private Grid? _overlayGrid;
    private Border? _overlayBorder;

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        if (_popup is not null)
        {
            _popup.Opened -= OnPopupOpened;
            _popup.Closed -= OnPopupClosed;
        }
        if (_overlayGrid is not null)
        {
            _overlayGrid.PointerPressed -= OnOverlayPointerPressed;
        }
        _popup = e.NameScope.Find<Popup>("PART_Popup");
        _overlayGrid = e.NameScope.Find<Grid>("PART_OverlayGrid");
        _overlayBorder = e.NameScope.Find<Border>("PART_OverlayBorder");
        if (_popup is not null)
        {
            _popup.PlacementTarget = TopLevel.GetTopLevel(this);
            _popup.IsOpen = IsOpen;
            _popup.Opened += OnPopupOpened;
            _popup.Closed += OnPopupClosed;
        }
        if (_overlayGrid is not null)
        {
            _overlayGrid.PointerPressed += OnOverlayPointerPressed;
        }
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (_popup is not null)
        {
            _popup.PlacementTarget = TopLevel.GetTopLevel(this);
        }
    }

    private void OnOverlayPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Source == _overlayGrid || e.Source == _overlayBorder)
        {
            IsOpen = false;
            e.Handled = true;
        }
    }

    private void OnPopupOpened(object? sender, EventArgs e)
    {
        if (_overlayGrid is not null && _popup?.PlacementTarget is Control target)
        {
            _overlayGrid.Width = target.Bounds.Width;
            _overlayGrid.Height = target.Bounds.Height;
        }
    }

    private void OnPopupClosed(object? sender, EventArgs e)
    {
        if (IsOpen)
            IsOpen = false;
    }

    /// <summary>属性改变事件处理</summary>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == IsOpenProperty && _popup is not null)
        {
            _popup.IsOpen = IsOpen;
        }
    }
}
