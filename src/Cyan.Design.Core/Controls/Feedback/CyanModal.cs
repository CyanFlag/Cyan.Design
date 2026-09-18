using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Cyan.Design.Core.Controls.Buttons;

namespace Cyan.Design.Core.Controls.Feedback;

/// <summary>模态对话框控件，用于在浮层中显示重要信息或收集用户输入</summary>
public class CyanModal : ContentControl
{


    /// <summary>是否打开对话框的样式属性</summary>
    public static readonly StyledProperty<bool> OpenProperty =
        AvaloniaProperty.Register<CyanModal, bool>(nameof(Open), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

    /// <summary>对话框标题的样式属性</summary>
    public static readonly StyledProperty<object?> TitleProperty =
        AvaloniaProperty.Register<CyanModal, object?>(nameof(Title));

    /// <summary>是否显示关闭按钮的样式属性</summary>
    public static readonly StyledProperty<bool> ClosableProperty =
        AvaloniaProperty.Register<CyanModal, bool>(nameof(Closable), true);

    /// <summary>是否显示遮罩层的样式属性</summary>
    public static readonly StyledProperty<bool> MaskProperty =
        AvaloniaProperty.Register<CyanModal, bool>(nameof(Mask), true);

    /// <summary>点击遮罩层是否允许关闭对话框的样式属性</summary>
    public static readonly StyledProperty<bool> MaskClosableProperty =
        AvaloniaProperty.Register<CyanModal, bool>(nameof(MaskClosable), true);

    /// <summary>是否支持键盘操作（ESC 关闭）的样式属性</summary>
    public static readonly StyledProperty<bool> KeyboardProperty =
        AvaloniaProperty.Register<CyanModal, bool>(nameof(Keyboard), true);

    /// <summary>是否垂直居中显示的样式属性</summary>
    public static readonly StyledProperty<bool> CenteredProperty =
        AvaloniaProperty.Register<CyanModal, bool>(nameof(Centered));

    /// <summary>对话框宽度的样式属性</summary>
    public static readonly StyledProperty<double> ModalWidthProperty =
        AvaloniaProperty.Register<CyanModal, double>(nameof(ModalWidth), 520);

    /// <summary>底部内容的样式属性</summary>
    public static readonly StyledProperty<object?> FooterProperty =
        AvaloniaProperty.Register<CyanModal, object?>(nameof(Footer));

    /// <summary>确认按钮文本的样式属性</summary>
    public static readonly StyledProperty<string> OkTextProperty =
        AvaloniaProperty.Register<CyanModal, string>(nameof(OkText), "确定");

    /// <summary>取消按钮文本的样式属性</summary>
    public static readonly StyledProperty<string> CancelTextProperty =
        AvaloniaProperty.Register<CyanModal, string>(nameof(CancelText), "取消");

    /// <summary>确认按钮类型的样式属性</summary>
    public static readonly StyledProperty<ButtonType> OkTypeProperty =
        AvaloniaProperty.Register<CyanModal, ButtonType>(nameof(OkType), ButtonType.Primary);

    /// <summary>确认按钮是否处于加载状态的样式属性</summary>
    public static readonly StyledProperty<bool> ConfirmLoadingProperty =
        AvaloniaProperty.Register<CyanModal, bool>(nameof(ConfirmLoading));

    /// <summary>对话框是否处于加载状态的样式属性</summary>
    public static readonly StyledProperty<bool> LoadingProperty =
        AvaloniaProperty.Register<CyanModal, bool>(nameof(Loading));

    /// <summary>确认按钮点击事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> OkEvent =
        RoutedEvent.Register<CyanModal, RoutedEventArgs>(nameof(Ok), RoutingStrategies.Bubble);

    /// <summary>取消按钮点击事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> CancelEvent =
        RoutedEvent.Register<CyanModal, RoutedEventArgs>(nameof(Cancel), RoutingStrategies.Bubble);

    static CyanModal()
    {
        OpenProperty.Changed.AddClassHandler<CyanModal>((m, _) => m.OnOpenChanged());
        CenteredProperty.Changed.AddClassHandler<CyanModal>((m, _) => m.UpdatePseudoClasses());
        MaskProperty.Changed.AddClassHandler<CyanModal>((m, _) => m.UpdatePseudoClasses());
        ClosableProperty.Changed.AddClassHandler<CyanModal>((m, _) => m.UpdatePseudoClasses());
        FooterProperty.Changed.AddClassHandler<CyanModal>((m, _) => m.UpdatePseudoClasses());
        TitleProperty.Changed.AddClassHandler<CyanModal>((m, _) => m.UpdatePseudoClasses());
        ConfirmLoadingProperty.Changed.AddClassHandler<CyanModal>((m, _) => m.UpdatePseudoClasses());
        LoadingProperty.Changed.AddClassHandler<CyanModal>((m, _) => m.UpdatePseudoClasses());
        ModalWidthProperty.Changed.AddClassHandler<CyanModal>((m, _) => m.UpdateWidth());
    }

    /// <summary>是否打开对话框</summary>
    public bool Open
    {
        get => GetValue(OpenProperty);
        set => SetValue(OpenProperty, value);
    }

    /// <summary>对话框标题</summary>
    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>是否显示关闭按钮</summary>
    public bool Closable
    {
        get => GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    /// <summary>是否显示遮罩层</summary>
    public bool Mask
    {
        get => GetValue(MaskProperty);
        set => SetValue(MaskProperty, value);
    }

    /// <summary>点击遮罩层是否允许关闭对话框</summary>
    public bool MaskClosable
    {
        get => GetValue(MaskClosableProperty);
        set => SetValue(MaskClosableProperty, value);
    }

    /// <summary>是否支持键盘操作（ESC 关闭）</summary>
    public bool Keyboard
    {
        get => GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    /// <summary>是否垂直居中显示</summary>
    public bool Centered
    {
        get => GetValue(CenteredProperty);
        set => SetValue(CenteredProperty, value);
    }

    /// <summary>对话框宽度</summary>
    public double ModalWidth
    {
        get => GetValue(ModalWidthProperty);
        set => SetValue(ModalWidthProperty, value);
    }

    /// <summary>底部内容</summary>
    public object? Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }

    /// <summary>确认按钮文本</summary>
    public string OkText
    {
        get => GetValue(OkTextProperty);
        set => SetValue(OkTextProperty, value);
    }

    /// <summary>取消按钮文本</summary>
    public string CancelText
    {
        get => GetValue(CancelTextProperty);
        set => SetValue(CancelTextProperty, value);
    }

    /// <summary>确认按钮类型</summary>
    public ButtonType OkType
    {
        get => GetValue(OkTypeProperty);
        set => SetValue(OkTypeProperty, value);
    }

    /// <summary>确认按钮是否处于加载状态</summary>
    public bool ConfirmLoading
    {
        get => GetValue(ConfirmLoadingProperty);
        set => SetValue(ConfirmLoadingProperty, value);
    }

    /// <summary>对话框是否处于加载状态</summary>
    public bool Loading
    {
        get => GetValue(LoadingProperty);
        set => SetValue(LoadingProperty, value);
    }

    /// <summary>确认按钮点击事件</summary>
    public event EventHandler<RoutedEventArgs>? Ok
    {
        add => AddHandler(OkEvent, value);
        remove => RemoveHandler(OkEvent, value);
    }

    /// <summary>取消按钮点击事件</summary>
    public event EventHandler<RoutedEventArgs>? Cancel
    {
        add => AddHandler(CancelEvent, value);
        remove => RemoveHandler(CancelEvent, value);
    }

    private Border? _contentBorder;

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        var mask = e.NameScope.Find<Border>("PART_Mask");
        if (mask != null)
            mask.PointerPressed += OnMaskPointerPressed;

        var close = e.NameScope.Find<Control>("PART_Close");
        if (close != null)
            close.PointerPressed += OnClosePointerPressed;

        var okButton = e.NameScope.Find<Button>("PART_OkButton");
        if (okButton != null)
            okButton.Click += OnOkButtonClick;

        var cancelButton = e.NameScope.Find<Button>("PART_CancelButton");
        if (cancelButton != null)
            cancelButton.Click += OnCancelButtonClick;

        _contentBorder = e.NameScope.Find<Border>("PART_Content");
        UpdateWidth();
    }

    private void OnMaskPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (MaskClosable)
            Close();
    }

    private void OnClosePointerPressed(object? sender, PointerPressedEventArgs e)
    {
        Close();
        e.Handled = true;
    }

    private void OnOkButtonClick(object? sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(OkEvent));
    }

    private void OnCancelButtonClick(object? sender, RoutedEventArgs e)
    {
        Close();
        RaiseEvent(new RoutedEventArgs(CancelEvent));
    }

    /// <summary>关闭对话框</summary>
    public void Close()
    {
        Open = false;
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        UpdatePseudoClasses();
    }

    /// <summary>键盘按下事件处理</summary>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (Keyboard && e.Key == Key.Escape && Open)
        {
            Close();
            e.Handled = true;
            return;
        }

        if (Open && e.Key == Key.Tab)
        {
            var focusable = GetFocusableDescendants();
            if (focusable.Count == 0)
            {
                base.OnKeyDown(e);
                return;
            }

            var focused = focusable.FirstOrDefault(f => f.IsFocused);
            var currentIndex = focused is null ? -1 : focusable.IndexOf(focused);
            var nextIndex = e.KeyModifiers.HasFlag(KeyModifiers.Shift)
                ? (currentIndex <= 0 ? focusable.Count - 1 : currentIndex - 1)
                : (currentIndex >= focusable.Count - 1 ? 0 : currentIndex + 1);

            if (nextIndex != currentIndex)
            {
                focusable[nextIndex].Focus();
                e.Handled = true;
                return;
            }
        }

        base.OnKeyDown(e);
    }

    private void OnOpenChanged()
    {
        UpdatePseudoClasses();

        if (Open)
        {
            var first = GetFocusableDescendants().FirstOrDefault();
            first?.Focus();
        }
    }

    private List<Control> GetFocusableDescendants()
    {
        var result = new List<Control>();
        foreach (var descendant in this.GetVisualDescendants())
        {
            if (descendant is Control c && c.Focusable && c.IsEffectivelyVisible && c.IsEnabled)
                result.Add(c);
        }
        return result;
    }

    private void UpdatePseudoClasses()
    {
        IsVisible = Open;
        PseudoClasses.Set(":open", Open);
        PseudoClasses.Set(":centered", Centered);
        PseudoClasses.Set(":no-mask", !Mask);
        PseudoClasses.Set(":no-closable", !Closable);
        PseudoClasses.Set(":has-footer", Footer != null);
        PseudoClasses.Set(":has-title", Title != null);
        PseudoClasses.Set(":confirm-loading", ConfirmLoading);
        PseudoClasses.Set(":loading", Loading);
    }

    private void UpdateWidth()
    {
        if (_contentBorder != null)
            _contentBorder.Width = ModalWidth;
    }
}
