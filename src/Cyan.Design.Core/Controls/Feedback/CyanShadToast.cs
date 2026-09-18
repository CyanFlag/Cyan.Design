using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Threading;

namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>Shad 风格吐司提示的类型枚举</summary>
public enum ShadToastType
{
    /// <summary>默认类型</summary>
    Default,
    /// <summary>成功类型</summary>
    Success,
    /// <summary>信息类型</summary>
    Info,
    /// <summary>警告类型</summary>
    Warning,
    /// <summary>错误类型</summary>
    Error,
    /// <summary>加载中类型</summary>
    Loading
}

/// <summary>Shad 风格吐司提示控件，用于短暂展示全局消息</summary>
public class CyanShadToast : TemplatedControl
{
    /// <summary>标题的样式属性</summary>
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<CyanShadToast, string>(nameof(Title));

    /// <summary>描述内容的样式属性</summary>
    public static readonly StyledProperty<string?> DescriptionProperty =
        AvaloniaProperty.Register<CyanShadToast, string?>(nameof(Description));

    /// <summary>提示类型的样式属性</summary>
    public static readonly StyledProperty<ShadToastType> TypeProperty =
        AvaloniaProperty.Register<CyanShadToast, ShadToastType>(nameof(Type), ShadToastType.Default);

    static CyanShadToast()
    {
        TypeProperty.Changed.AddClassHandler<CyanShadToast>((t, _) => t.UpdateTypePseudoClasses());
        DescriptionProperty.Changed.AddClassHandler<CyanShadToast>((t, _) => t.UpdateDescriptionPseudoClass());
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

    /// <summary>提示类型</summary>
    public ShadToastType Type
    {
        get => GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    private Button? _closeButton;

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (_closeButton != null)
            _closeButton.Click -= OnCloseClick;

        _closeButton = e.NameScope.Find<Button>("PART_CloseButton");

        if (_closeButton != null)
            _closeButton.Click += OnCloseClick;

        UpdateTypePseudoClasses();
        UpdateDescriptionPseudoClass();
    }

    private void OnCloseClick(object? sender, RoutedEventArgs e)
    {
        if (Parent is Panel panel)
            panel.Children.Remove(this);
    }

    private void UpdateTypePseudoClasses()
    {
        PseudoClasses.Set(":type-default", Type == ShadToastType.Default);
        PseudoClasses.Set(":type-success", Type == ShadToastType.Success);
        PseudoClasses.Set(":type-info", Type == ShadToastType.Info);
        PseudoClasses.Set(":type-warning", Type == ShadToastType.Warning);
        PseudoClasses.Set(":type-error", Type == ShadToastType.Error);
        PseudoClasses.Set(":type-loading", Type == ShadToastType.Loading);
    }

    private void UpdateDescriptionPseudoClass()
    {
        PseudoClasses.Set(":has-description", !string.IsNullOrEmpty(Description));
    }
}

/// <summary>Shad 风格吐司提示容器，用于管理多个吐司提示的显示</summary>
public class CyanShadToaster : StackPanel
{
    /// <summary>初始化 Shad 风格吐司提示容器</summary>
    public CyanShadToaster()
    {
        Orientation = Orientation.Vertical;
        Spacing = 8;
        HorizontalAlignment = HorizontalAlignment.Right;
        VerticalAlignment = VerticalAlignment.Bottom;
        Margin = new Thickness(0, 0, 16, 16);
    }

    /// <summary>显示一条吐司提示</summary>
    public void Show(string title, string? description = null, ShadToastType type = ShadToastType.Default, int durationMs = 4000)
    {
        var toast = new CyanShadToast
        {
            Title = title,
            Description = description,
            Type = type,
        };
        Children.Add(toast);

        if (type != ShadToastType.Loading)
        {
            _ = Task.Delay(durationMs).ContinueWith(_ =>
            {
                Dispatcher.UIThread.Post(() => Children.Remove(toast));
            });
        }
    }

    /// <summary>移除指定的吐司提示</summary>
    public void Dismiss(CyanShadToast toast)
    {
        Children.Remove(toast);
    }
}
