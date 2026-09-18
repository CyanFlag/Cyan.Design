using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace Cyan.Design.Core.Controls.Chat;

/// <summary>消息对齐方式</summary>
public enum ShadMessageAlign
{
    /// <summary>起始对齐，消息靠左显示</summary>
    Start,

    /// <summary>结束对齐，消息靠右显示</summary>
    End
}

/// <summary>聊天消息控件，用于显示单条消息内容</summary>
public class CyanShadMessage : StackPanel
{
    /// <summary>对齐方式样式属性</summary>
    public static readonly StyledProperty<ShadMessageAlign> AlignProperty =
        AvaloniaProperty.Register<CyanShadMessage, ShadMessageAlign>(nameof(Align), ShadMessageAlign.Start);

    /// <summary>消息对齐方式</summary>
    public ShadMessageAlign Align
    {
        get => GetValue(AlignProperty);
        set => SetValue(AlignProperty, value);
    }

    static CyanShadMessage()
    {
        AlignProperty.Changed.AddClassHandler<CyanShadMessage>((t, _) => t.UpdateAlignment());
    }

    /// <summary>初始化 CyanShadMessage 的新实例</summary>
    public CyanShadMessage()
    {
        Orientation = Avalonia.Layout.Orientation.Horizontal;
        Spacing = 8;
        UpdateAlignment();
    }

    private void UpdateAlignment()
    {
        HorizontalAlignment = Align == ShadMessageAlign.End
            ? HorizontalAlignment.Right
            : HorizontalAlignment.Left;
    }
}

/// <summary>聊天消息组控件，用于纵向排列多条消息</summary>
public class CyanShadMessageGroup : StackPanel
{
    /// <summary>初始化 CyanShadMessageGroup 的新实例</summary>
    public CyanShadMessageGroup()
    {
        Orientation = Avalonia.Layout.Orientation.Vertical;
        Spacing = 4;
    }
}

/// <summary>聊天消息内容控件，用于纵向排列消息的主体内容</summary>
public class CyanShadMessageContent : StackPanel
{
    /// <summary>初始化 CyanShadMessageContent 的新实例</summary>
    public CyanShadMessageContent()
    {
        Orientation = Avalonia.Layout.Orientation.Vertical;
        Spacing = 4;
    }
}

/// <summary>聊天消息头像控件</summary>
public class CyanShadMessageAvatar : ContentControl
{
}

/// <summary>聊天消息头部控件</summary>
public class CyanShadMessageHeader : ContentControl
{
}

/// <summary>聊天消息底部控件</summary>
public class CyanShadMessageFooter : ContentControl
{
}
