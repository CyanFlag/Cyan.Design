using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Threading;

namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>通知提醒项控件，用于展示单条通知内容</summary>
public class CyanNotificationItem : TemplatedControl
{
    /// <summary>标题的样式属性</summary>
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<CyanNotificationItem, string>(nameof(Title));

    /// <summary>描述内容的样式属性</summary>
    public static readonly StyledProperty<string?> DescriptionProperty =
        AvaloniaProperty.Register<CyanNotificationItem, string?>(nameof(Description));

    /// <summary>通知类型的样式属性</summary>
    public static readonly StyledProperty<NotificationType> NotificationTypeProperty =
        AvaloniaProperty.Register<CyanNotificationItem, NotificationType>(nameof(NotificationType), NotificationType.Info);

    static CyanNotificationItem()
    {
        NotificationTypeProperty.Changed.AddClassHandler<CyanNotificationItem>((n, _) => n.UpdateTypePseudoClasses());
        DescriptionProperty.Changed.AddClassHandler<CyanNotificationItem>((n, _) => n.UpdateDescriptionPseudoClass());
    }

    /// <summary>标题</summary>
    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>描述内容</summary>
    public string? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>通知类型</summary>
    public NotificationType NotificationType
    {
        get => GetValue(NotificationTypeProperty);
        set => SetValue(NotificationTypeProperty, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        UpdateTypePseudoClasses();
        UpdateDescriptionPseudoClass();
    }

    private void UpdateTypePseudoClasses()
    {
        PseudoClasses.Set(":type-success", NotificationType == NotificationType.Success);
        PseudoClasses.Set(":type-info", NotificationType == NotificationType.Info);
        PseudoClasses.Set(":type-warning", NotificationType == NotificationType.Warning);
        PseudoClasses.Set(":type-error", NotificationType == NotificationType.Error);
    }

    private void UpdateDescriptionPseudoClass()
    {
        PseudoClasses.Set(":has-description", !string.IsNullOrEmpty(Description));
    }
}

/// <summary>通知提醒静态工具类，用于在指定面板上显示通知</summary>
public static class CyanNotification
{
    /// <summary>在指定面板上显示一条通知提醒</summary>
    public static async void Show(Panel host, string title, string? description = null,
        NotificationType type = NotificationType.Info, int durationMs = 4500)
    {
        var card = new CyanNotificationItem
        {
            Title = title,
            Description = description,
            NotificationType = type,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(0, 16, 16, 0),
        };
        host.Children.Add(card);
        await Task.Delay(durationMs);
        await Dispatcher.UIThread.InvokeAsync(() => host.Children.Remove(card));
    }
}
