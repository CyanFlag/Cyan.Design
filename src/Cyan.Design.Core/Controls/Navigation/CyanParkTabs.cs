using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Navigation;

/// <summary>标签页外观变体</summary>
public enum ParkTabsVariant
{
    /// <summary>线条样式</summary>
    Line,
    /// <summary>柔和样式</summary>
    Subtle,
    /// <summary>封闭样式</summary>
    Enclosed
}

/// <summary>标签页方向</summary>
public enum ParkTabsOrientation
{
    /// <summary>水平方向</summary>
    Horizontal,
    /// <summary>垂直方向</summary>
    Vertical
}

/// <summary>标签页控件，支持多标签切换展示内容</summary>
public class CyanParkTabs : TemplatedControl
{
    /// <summary>当前选中值样式属性</summary>
    public static readonly StyledProperty<string> ValueProperty =
        AvaloniaProperty.Register<CyanParkTabs, string>(nameof(Value), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    /// <summary>默认选中值样式属性</summary>
    public static readonly StyledProperty<string> DefaultValueProperty =
        AvaloniaProperty.Register<CyanParkTabs, string>(nameof(DefaultValue));

    /// <summary>外观变体样式属性</summary>
    public static readonly StyledProperty<ParkTabsVariant> VariantProperty =
        AvaloniaProperty.Register<CyanParkTabs, ParkTabsVariant>(nameof(Variant), ParkTabsVariant.Line);

    /// <summary>方向样式属性</summary>
    public static readonly StyledProperty<ParkTabsOrientation> OrientationProperty =
        AvaloniaProperty.Register<CyanParkTabs, ParkTabsOrientation>(nameof(Orientation), ParkTabsOrientation.Horizontal);

    /// <summary>标签尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> TabsSizeProperty =
        AvaloniaProperty.Register<CyanParkTabs, ControlSize>(nameof(TabsSize), ControlSize.Middle);

    /// <summary>是否撑满容器样式属性</summary>
    public static readonly StyledProperty<bool> FittedProperty =
        AvaloniaProperty.Register<CyanParkTabs, bool>(nameof(Fitted));

    /// <summary>标签列表内容样式属性</summary>
    public static readonly StyledProperty<object?> ListContentProperty =
        AvaloniaProperty.Register<CyanParkTabs, object?>(nameof(ListContent));

    /// <summary>标签面板内容样式属性</summary>
    public static readonly StyledProperty<object?> PanelContentProperty =
        AvaloniaProperty.Register<CyanParkTabs, object?>(nameof(PanelContent));

    /// <summary>选中值变化路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ValueChangedEvent =
        RoutedEvent.Register<CyanParkTabs, RoutedEventArgs>(nameof(ValueChanged), RoutingStrategies.Bubble);

    private readonly ObservableCollection<CyanParkTabTrigger> _triggers = [];
    private readonly ObservableCollection<CyanParkTabContent> _contents = [];
    private Border? _indicator;

    /// <summary>当前选中值</summary>
    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>默认选中值</summary>
    public string DefaultValue
    {
        get => GetValue(DefaultValueProperty);
        set => SetValue(DefaultValueProperty, value);
    }

    /// <summary>外观变体</summary>
    public ParkTabsVariant Variant
    {
        get => GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }

    /// <summary>标签页方向</summary>
    public ParkTabsOrientation Orientation
    {
        get => GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>标签尺寸</summary>
    public ControlSize TabsSize
    {
        get => GetValue(TabsSizeProperty);
        set => SetValue(TabsSizeProperty, value);
    }

    /// <summary>是否撑满容器</summary>
    public bool Fitted
    {
        get => GetValue(FittedProperty);
        set => SetValue(FittedProperty, value);
    }

    /// <summary>标签列表内容</summary>
    public object? ListContent
    {
        get => GetValue(ListContentProperty);
        set => SetValue(ListContentProperty, value);
    }

    /// <summary>标签面板内容</summary>
    public object? PanelContent
    {
        get => GetValue(PanelContentProperty);
        set => SetValue(PanelContentProperty, value);
    }

    /// <summary>选中值变化事件</summary>
    public event EventHandler<RoutedEventArgs>? ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    internal ObservableCollection<CyanParkTabTrigger> Triggers => _triggers;
    internal ObservableCollection<CyanParkTabContent> Contents => _contents;

    static CyanParkTabs()
    {
        ValueProperty.Changed.AddClassHandler<CyanParkTabs>((t, _) => t.OnValueChanged());
        VariantProperty.Changed.AddClassHandler<CyanParkTabs>((t, _) => t.UpdatePseudoClasses());
        OrientationProperty.Changed.AddClassHandler<CyanParkTabs>((t, _) => t.UpdatePseudoClasses());
        FittedProperty.Changed.AddClassHandler<CyanParkTabs>((t, _) => t.UpdatePseudoClasses());
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _indicator = e.NameScope.Find<Border>("PART_Indicator");
        UpdatePseudoClasses();
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (string.IsNullOrEmpty(Value) && !string.IsNullOrEmpty(DefaultValue))
            Value = DefaultValue;
    }

    internal void RegisterTrigger(CyanParkTabTrigger trigger)
    {
        if (!_triggers.Contains(trigger))
            _triggers.Add(trigger);
        trigger.IsSelected = trigger.Value == Value;
    }

    internal void UnregisterTrigger(CyanParkTabTrigger trigger)
    {
        _triggers.Remove(trigger);
    }

    internal void RegisterContent(CyanParkTabContent content)
    {
        if (!_contents.Contains(content))
            _contents.Add(content);
        content.IsVisible = content.Value == Value;
    }

    internal void UnregisterContent(CyanParkTabContent content)
    {
        _contents.Remove(content);
    }

    internal void SelectTab(string newValue)
    {
        if (Value == newValue) return;
        Value = newValue;
    }

    private void OnValueChanged()
    {
        foreach (var t in _triggers)
            t.IsSelected = t.Value == Value;
        foreach (var c in _contents)
            c.IsVisible = c.Value == Value;

        RaiseEvent(new RoutedEventArgs(ValueChangedEvent));
        UpdatePseudoClasses();
        Dispatcher.UIThread.Post(UpdateIndicator, DispatcherPriority.Normal);
    }

    private void UpdateIndicator()
    {
        if (_indicator is null) return;
        foreach (var t in _triggers)
        {
            if (t.IsSelected && t.Bounds.Width > 0)
            {
                if (Variant == ParkTabsVariant.Enclosed)
                {
                    _indicator.Width = t.Bounds.Width;
                    _indicator.Height = t.Bounds.Height;
                    _indicator.Margin = new Thickness(t.Bounds.X, t.Bounds.Y, 0, 0);
                }
                else if (Orientation == ParkTabsOrientation.Horizontal)
                {
                    _indicator.Width = t.Bounds.Width;
                    _indicator.Height = 2;
                    _indicator.Margin = new Thickness(t.Bounds.X, 0, 0, 0);
                }
                else
                {
                    _indicator.Width = 2;
                    _indicator.Height = t.Bounds.Height;
                    _indicator.Margin = new Thickness(0, t.Bounds.Y, 0, 0);
                }
                _indicator.IsVisible = true;
                return;
            }
        }
        _indicator.IsVisible = false;
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":line", Variant == ParkTabsVariant.Line);
        PseudoClasses.Set(":subtle", Variant == ParkTabsVariant.Subtle);
        PseudoClasses.Set(":enclosed", Variant == ParkTabsVariant.Enclosed);
        PseudoClasses.Set(":vertical", Orientation == ParkTabsOrientation.Vertical);
        PseudoClasses.Set(":fitted", Fitted);
    }
}

/// <summary>标签页列表容器控件</summary>
public class CyanParkTabList : StackPanel
{
    private CyanParkTabs? _owner;

    /// <summary>初始化 CyanParkTabList 的新实例</summary>
    public CyanParkTabList()
    {
        Orientation = Avalonia.Layout.Orientation.Horizontal;
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _owner = this.GetVisualParent() as CyanParkTabs;
        if (_owner is not null && _owner.Orientation == ParkTabsOrientation.Vertical)
            Orientation = Avalonia.Layout.Orientation.Vertical;
    }

    internal CyanParkTabs? Owner => _owner;
}

/// <summary>标签页触发器控件，用于切换标签</summary>
public class CyanParkTabTrigger : HeaderedContentControl
{
    /// <summary>标签值样式属性</summary>
    public static readonly StyledProperty<string> ValueProperty =
        AvaloniaProperty.Register<CyanParkTabTrigger, string>(nameof(Value));

    /// <summary>是否选中样式属性</summary>
    public static readonly StyledProperty<bool> IsSelectedProperty =
        AvaloniaProperty.Register<CyanParkTabTrigger, bool>(nameof(IsSelected));

    /// <summary>是否禁用样式属性</summary>
    public static readonly StyledProperty<bool> IsDisabledProperty =
        AvaloniaProperty.Register<CyanParkTabTrigger, bool>(nameof(IsDisabled));

    private CyanParkTabs? _owner;

    static CyanParkTabTrigger()
    {
        IsSelectedProperty.Changed.AddClassHandler<CyanParkTabTrigger>((t, _) => t.UpdatePseudoClasses());
        IsDisabledProperty.Changed.AddClassHandler<CyanParkTabTrigger>((t, _) => t.UpdatePseudoClasses());
    }

    /// <summary>标签值</summary>
    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>是否处于选中状态</summary>
    public bool IsSelected
    {
        get => GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool IsDisabled
    {
        get => GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        var p = this.GetVisualParent();
        while (p is not null)
        {
            if (p is CyanParkTabs tabs)
            {
                _owner = tabs;
                _owner.RegisterTrigger(this);
                return;
            }
            p = p.GetVisualParent();
        }
    }

    /// <summary>从视觉树分离时处理</summary>
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _owner?.UnregisterTrigger(this);
    }

    /// <summary>指针按下事件处理</summary>
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        if (IsDisabled) return;
        _owner?.SelectTab(Value);
        e.Handled = true;
    }

    private void UpdatePseudoClasses()
    {
        PseudoClasses.Set(":selected", IsSelected);
        PseudoClasses.Set(":disabled", IsDisabled);
    }
}

/// <summary>标签页内容控件，对应某个标签的面板内容</summary>
public class CyanParkTabContent : ContentControl
{
    /// <summary>标签值样式属性</summary>
    public static readonly StyledProperty<string> ValueProperty =
        AvaloniaProperty.Register<CyanParkTabContent, string>(nameof(Value));

    private CyanParkTabs? _owner;

    /// <summary>标签值</summary>
    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        var p = this.GetVisualParent();
        while (p is not null)
        {
            if (p is CyanParkTabs tabs)
            {
                _owner = tabs;
                _owner.RegisterContent(this);
                return;
            }
            p = p.GetVisualParent();
        }
    }

    /// <summary>从视觉树分离时处理</summary>
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _owner?.UnregisterContent(this);
    }
}