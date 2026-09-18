using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Cyan.Design.Core.Controls.Inputs;

/// <summary>搜索输入框控件，支持加载状态和搜索按钮</summary>
public class CyanSearch : CyanInput
{
    /// <summary>是否处于加载状态样式属性</summary>
    public static readonly StyledProperty<bool> LoadingProperty =
        AvaloniaProperty.Register<CyanSearch, bool>(nameof(Loading));

    /// <summary>搜索按钮内容样式属性</summary>
    public static readonly StyledProperty<object?> EnterButtonProperty =
        AvaloniaProperty.Register<CyanSearch, object?>(nameof(EnterButton));

    /// <summary>搜索按钮前景色样式属性</summary>
    public static readonly StyledProperty<IBrush?> SearchButtonForegroundProperty =
        AvaloniaProperty.Register<CyanSearch, IBrush?>(nameof(SearchButtonForeground));

    /// <summary>搜索按钮背景色样式属性</summary>
    public static readonly StyledProperty<IBrush?> SearchButtonBackgroundProperty =
        AvaloniaProperty.Register<CyanSearch, IBrush?>(nameof(SearchButtonBackground));

    /// <summary>搜索路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> SearchedEvent =
        RoutedEvent.Register<CyanSearch, RoutedEventArgs>(nameof(Searched), RoutingStrategies.Bubble);

    static CyanSearch()
    {
        LoadingProperty.Changed.AddClassHandler<CyanSearch>((c, _) => c.UpdateLoadingPseudoClass());
        EnterButtonProperty.Changed.AddClassHandler<CyanSearch>((c, _) => c.UpdateEnterButtonPseudoClasses());
    }

    /// <summary>是否处于加载状态</summary>
    public bool Loading
    {
        get => GetValue(LoadingProperty);
        set => SetValue(LoadingProperty, value);
    }

    /// <summary>搜索按钮内容</summary>
    public object? EnterButton
    {
        get => GetValue(EnterButtonProperty);
        set => SetValue(EnterButtonProperty, value);
    }

    /// <summary>搜索按钮前景色</summary>
    public IBrush? SearchButtonForeground
    {
        get => GetValue(SearchButtonForegroundProperty);
        set => SetValue(SearchButtonForegroundProperty, value);
    }

    /// <summary>搜索按钮背景色</summary>
    public IBrush? SearchButtonBackground
    {
        get => GetValue(SearchButtonBackgroundProperty);
        set => SetValue(SearchButtonBackgroundProperty, value);
    }

    /// <summary>搜索事件（按下回车或点击搜索按钮时触发）</summary>
    public event EventHandler<RoutedEventArgs>? Searched
    {
        add => AddHandler(SearchedEvent, value);
        remove => RemoveHandler(SearchedEvent, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        if (e.NameScope.Find<Button>("PART_SearchButton") is { } searchBtn)
            searchBtn.Click += OnSearchButtonClick;
        if (e.NameScope.Find<Button>("PART_SearchSuffixButton") is { } suffixBtn)
            suffixBtn.Click += OnSearchButtonClick;
        UpdateLoadingPseudoClass();
        UpdateEnterButtonPseudoClasses();
    }

    /// <summary>键盘按下事件处理</summary>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (e.Key == Key.Enter)
        {
            RaiseEvent(new RoutedEventArgs(SearchedEvent));
            e.Handled = true;
        }
    }

    private void OnSearchButtonClick(object? sender, RoutedEventArgs e)
        => RaiseEvent(new RoutedEventArgs(SearchedEvent));

    private void UpdateLoadingPseudoClass()
        => PseudoClasses.Set(":loading", Loading);

    private void UpdateEnterButtonPseudoClasses()
    {
        var value = EnterButton;
        var (hasButton, isIcon, isText) = ClassifyEnterButton(value);
        PseudoClasses.Set(":enterbutton", hasButton);
        PseudoClasses.Set(":enterbutton-icon", hasButton && isIcon);
        PseudoClasses.Set(":enterbutton-text", hasButton && isText);
        PseudoClasses.Set(":enterbutton-content", hasButton && !isIcon && !isText);
    }

    private static (bool hasButton, bool isIcon, bool isText) ClassifyEnterButton(object? value)
    {
        switch (value)
        {
            case null:
                return (false, false, false);
            case bool b:
                return (b, b, false);
            case string s:
                if (string.IsNullOrWhiteSpace(s))
                    return (false, false, false);
                if (s.Equals("true", StringComparison.OrdinalIgnoreCase))
                    return (true, true, false);
                return (true, false, true);
            default:
                return (true, false, false);
        }
    }
}
