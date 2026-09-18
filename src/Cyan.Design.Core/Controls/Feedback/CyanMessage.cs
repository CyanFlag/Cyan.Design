using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Threading;

namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>全局消息提示项控件，用于展示单条消息内容</summary>
public class CyanMessageItem : TemplatedControl
{
    /// <summary>消息文本的样式属性</summary>
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<CyanMessageItem, string>(nameof(Text));

    /// <summary>消息类型的样式属性</summary>
    public static readonly StyledProperty<MessageType> MessageTypeProperty =
        AvaloniaProperty.Register<CyanMessageItem, MessageType>(nameof(MessageType), MessageType.Info);

    static CyanMessageItem()
    {
        MessageTypeProperty.Changed.AddClassHandler<CyanMessageItem>((m, _) => m.UpdateTypePseudoClasses());
    }

    /// <summary>消息文本</summary>
    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>消息类型</summary>
    public MessageType MessageType
    {
        get => GetValue(MessageTypeProperty);
        set => SetValue(MessageTypeProperty, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        UpdateTypePseudoClasses();
    }

    private void UpdateTypePseudoClasses()
    {
        PseudoClasses.Set(":type-success", MessageType == MessageType.Success);
        PseudoClasses.Set(":type-info", MessageType == MessageType.Info);
        PseudoClasses.Set(":type-warning", MessageType == MessageType.Warning);
        PseudoClasses.Set(":type-error", MessageType == MessageType.Error);
        PseudoClasses.Set(":type-loading", MessageType == MessageType.Loading);
    }
}

/// <summary>全局消息提示静态工具类，用于在指定面板上显示消息</summary>
public static class CyanMessage
{
    /// <summary>在指定面板上显示一条全局消息</summary>
    public static async void Show(Panel host, string text, MessageType type = MessageType.Info, int durationMs = 2000)
    {
        var msg = new CyanMessageItem
        {
            Text = text,
            MessageType = type,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(0, 16, 0, 0),
        };
        host.Children.Add(msg);
        await Task.Delay(durationMs);
        await Dispatcher.UIThread.InvokeAsync(() => host.Children.Remove(msg));
    }
}
