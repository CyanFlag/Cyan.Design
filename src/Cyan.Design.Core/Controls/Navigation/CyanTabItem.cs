using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Cyan.Design.Core.Controls.Navigation;

/// <summary>标签项控件，支持关闭操作</summary>
public class CyanTabItem : TabItem
{
    /// <summary>是否可关闭样式属性</summary>
    public static readonly StyledProperty<bool> ClosableProperty =
        AvaloniaProperty.Register<CyanTabItem, bool>(nameof(Closable));

    /// <summary>关闭路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ClosedEvent =
        RoutedEvent.Register<CyanTabItem, RoutedEventArgs>(nameof(Closed), RoutingStrategies.Bubble);

    /// <summary>是否可关闭</summary>
    public bool Closable
    {
        get => GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    /// <summary>关闭事件</summary>
    public event EventHandler<RoutedEventArgs>? Closed
    {
        add => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var closeBtn = e.NameScope.Find<Border>("PART_CloseButton");
        if (closeBtn is not null)
        {
            closeBtn.PointerPressed -= OnClosePointerPressed;
            closeBtn.PointerPressed += OnClosePointerPressed;
        }
    }

    private void OnClosePointerPressed(object? sender, PointerPressedEventArgs e)
    {
        e.Handled = true;
        RaiseEvent(new RoutedEventArgs(ClosedEvent));
    }
}
