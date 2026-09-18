using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Controls.Inputs;

/// <summary>可编辑文本激活模式</summary>
public enum EditableActivationMode
{
    /// <summary>聚焦激活</summary>
    Focus,
    /// <summary>双击激活</summary>
    DoubleClick,
    /// <summary>单击激活</summary>
    Click,
    /// <summary>不激活</summary>
    None
}

/// <summary>可编辑文本提交模式</summary>
public enum EditableSubmitMode
{
    /// <summary>按回车提交</summary>
    Enter,
    /// <summary>失焦提交</summary>
    Blur,
    /// <summary>回车或失焦均提交</summary>
    Both,
    /// <summary>不自动提交</summary>
    None
}

/// <summary>可编辑文本控件，支持就地编辑预览内容</summary>
public class CyanEditable : TemplatedControl
{
    /// <summary>当前值样式属性</summary>
    public static readonly StyledProperty<string> ValueProperty =
        AvaloniaProperty.Register<CyanEditable, string>(nameof(Value), string.Empty);

    /// <summary>默认值样式属性</summary>
    public static readonly StyledProperty<string> DefaultValueProperty =
        AvaloniaProperty.Register<CyanEditable, string>(nameof(DefaultValue), string.Empty);

    /// <summary>占位文本样式属性</summary>
    public static readonly StyledProperty<string> PlaceholderProperty =
        AvaloniaProperty.Register<CyanEditable, string>(nameof(Placeholder), "Click to edit");

    /// <summary>是否处于编辑状态样式属性</summary>
    public static readonly StyledProperty<bool> IsEditingProperty =
        AvaloniaProperty.Register<CyanEditable, bool>(nameof(IsEditing));

    /// <summary>是否禁用样式属性</summary>
    public static readonly StyledProperty<bool> IsDisabledProperty =
        AvaloniaProperty.Register<CyanEditable, bool>(nameof(IsDisabled));

    /// <summary>激活模式样式属性</summary>
    public static readonly StyledProperty<EditableActivationMode> ActivationModeProperty =
        AvaloniaProperty.Register<CyanEditable, EditableActivationMode>(nameof(ActivationMode), EditableActivationMode.Focus);

    /// <summary>提交模式样式属性</summary>
    public static readonly StyledProperty<EditableSubmitMode> SubmitModeProperty =
        AvaloniaProperty.Register<CyanEditable, EditableSubmitMode>(nameof(SubmitMode), EditableSubmitMode.Both);

    /// <summary>是否显示操作按钮样式属性</summary>
    public static readonly StyledProperty<bool> ShowControlsProperty =
        AvaloniaProperty.Register<CyanEditable, bool>(nameof(ShowControls));

    /// <summary>可编辑控件尺寸样式属性</summary>
    public static readonly StyledProperty<ControlSize> EditableSizeProperty =
        AvaloniaProperty.Register<CyanEditable, ControlSize>(nameof(EditableSize), ControlSize.Middle);

    /// <summary>聚焦时是否全选文本样式属性</summary>
    public static readonly StyledProperty<bool> SelectOnFocusProperty =
        AvaloniaProperty.Register<CyanEditable, bool>(nameof(SelectOnFocus), true);

    /// <summary>最大字符长度样式属性</summary>
    public static readonly StyledProperty<int> MaxLengthProperty =
        AvaloniaProperty.Register<CyanEditable, int>(nameof(MaxLength), -1);

    /// <summary>值变更路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> ValueChangedEvent =
        RoutedEvent.Register<CyanEditable, RoutedEventArgs>(nameof(ValueChanged), RoutingStrategies.Bubble);

    /// <summary>编辑状态变更路由事件</summary>
    public static readonly RoutedEvent<RoutedEventArgs> EditChangedEvent =
        RoutedEvent.Register<CyanEditable, RoutedEventArgs>(nameof(EditChanged), RoutingStrategies.Bubble);

    private string _editValue = string.Empty;
    private TextBox? _inputBox;
    private Control? _preview;
    private Button? _editButton;
    private Button? _submitButton;
    private Button? _cancelButton;
    private InputElement? _topLevel;

    static CyanEditable()
    {
        IsEditingProperty.Changed.AddClassHandler<CyanEditable>(OnIsEditingChanged);
    }

    /// <summary>当前值</summary>
    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>默认值</summary>
    public string DefaultValue
    {
        get => GetValue(DefaultValueProperty);
        set => SetValue(DefaultValueProperty, value);
    }

    /// <summary>占位文本</summary>
    public string Placeholder
    {
        get => GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>是否处于编辑状态</summary>
    public bool IsEditing
    {
        get => GetValue(IsEditingProperty);
        set => SetValue(IsEditingProperty, value);
    }

    /// <summary>是否禁用</summary>
    public bool IsDisabled
    {
        get => GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    /// <summary>激活模式</summary>
    public EditableActivationMode ActivationMode
    {
        get => GetValue(ActivationModeProperty);
        set => SetValue(ActivationModeProperty, value);
    }

    /// <summary>提交模式</summary>
    public EditableSubmitMode SubmitMode
    {
        get => GetValue(SubmitModeProperty);
        set => SetValue(SubmitModeProperty, value);
    }

    /// <summary>是否显示操作按钮</summary>
    public bool ShowControls
    {
        get => GetValue(ShowControlsProperty);
        set => SetValue(ShowControlsProperty, value);
    }

    /// <summary>可编辑控件尺寸</summary>
    public ControlSize EditableSize
    {
        get => GetValue(EditableSizeProperty);
        set => SetValue(EditableSizeProperty, value);
    }

    /// <summary>聚焦时是否全选文本</summary>
    public bool SelectOnFocus
    {
        get => GetValue(SelectOnFocusProperty);
        set => SetValue(SelectOnFocusProperty, value);
    }

    /// <summary>最大字符长度，-1 表示不限制</summary>
    public int MaxLength
    {
        get => GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    /// <summary>值变更事件</summary>
    public event EventHandler<RoutedEventArgs>? ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    /// <summary>编辑状态变更事件</summary>
    public event EventHandler<RoutedEventArgs>? EditChanged
    {
        add => AddHandler(EditChangedEvent, value);
        remove => RemoveHandler(EditChangedEvent, value);
    }

    /// <summary>应用控件模板</summary>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (_preview is not null)
            _preview.PointerPressed -= OnPreviewPointerPressed;
        if (_inputBox is not null)
        {
            _inputBox.KeyDown -= OnInputKeyDown;
            _inputBox.LostFocus -= OnInputLostFocus;
        }
        if (_editButton is not null)
            _editButton.Click -= OnEditButtonClick;
        if (_submitButton is not null)
            _submitButton.Click -= OnSubmitButtonClick;
        if (_cancelButton is not null)
            _cancelButton.Click -= OnCancelButtonClick;

        _inputBox = e.NameScope.Find<TextBox>("PART_Input");
        _preview = e.NameScope.Find<Control>("PART_Preview");
        _editButton = e.NameScope.Find<Button>("PART_EditButton");
        _submitButton = e.NameScope.Find<Button>("PART_SubmitButton");
        _cancelButton = e.NameScope.Find<Button>("PART_CancelButton");

        if (_preview is not null)
            _preview.PointerPressed += OnPreviewPointerPressed;
        if (_inputBox is not null)
        {
            _inputBox.KeyDown += OnInputKeyDown;
            _inputBox.LostFocus += OnInputLostFocus;
        }
        if (_editButton is not null)
            _editButton.Click += OnEditButtonClick;
        if (_submitButton is not null)
            _submitButton.Click += OnSubmitButtonClick;
        if (_cancelButton is not null)
            _cancelButton.Click += OnCancelButtonClick;
    }

    private void OnEditButtonClick(object? sender, RoutedEventArgs e) => EnterEdit();
    private void OnSubmitButtonClick(object? sender, RoutedEventArgs e) => Submit();
    private void OnCancelButtonClick(object? sender, RoutedEventArgs e) => Cancel();

    private void OnPreviewPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (IsDisabled || ActivationMode == EditableActivationMode.None)
            return;

        if (ActivationMode == EditableActivationMode.DoubleClick)
        {
            if (e.ClickCount < 2)
                return;
        }
        else if (ActivationMode == EditableActivationMode.Focus)
        {
            if (e.ClickCount > 1)
                return;
        }

        EnterEdit();
    }

    private void OnInputKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (SubmitMode is EditableSubmitMode.Enter or EditableSubmitMode.Both)
            {
                Submit();
                e.Handled = true;
            }
        }
        else if (e.Key == Key.Escape)
        {
            Cancel();
            e.Handled = true;
        }
    }

    private void OnInputLostFocus(object? sender, RoutedEventArgs e)
    {
        if (SubmitMode is EditableSubmitMode.Blur or EditableSubmitMode.Both)
        {
            Submit();
        }
    }

    /// <summary>进入编辑状态</summary>
    public void EnterEdit()
    {
        if (IsDisabled || IsEditing)
            return;

        _editValue = Value;
        IsEditing = true;
        RaiseEvent(new RoutedEventArgs(EditChangedEvent));
    }

    /// <summary>提交编辑内容</summary>
    public void Submit()
    {
        if (!IsEditing)
            return;

        var newValue = _inputBox?.Text ?? _editValue;
        if (newValue != Value)
        {
            Value = newValue;
            RaiseEvent(new RoutedEventArgs(ValueChangedEvent));
        }
        IsEditing = false;
        RaiseEvent(new RoutedEventArgs(EditChangedEvent));
    }

    /// <summary>取消编辑并恢复原值</summary>
    public void Cancel()
    {
        if (!IsEditing)
            return;

        if (_inputBox is not null)
            _inputBox.Text = Value;
        IsEditing = false;
        RaiseEvent(new RoutedEventArgs(EditChangedEvent));
    }

    private static void OnIsEditingChanged(CyanEditable sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (sender.IsEditing && sender._inputBox is not null)
        {
            sender._inputBox.Text = sender.Value;
            sender._inputBox.Focus();
            if (sender.SelectOnFocus)
                sender._inputBox.SelectionStart = 0;
            if (sender.SelectOnFocus)
                sender._inputBox.SelectionEnd = sender.Value.Length;

            if (sender._topLevel is not null)
                sender._topLevel.PointerPressed -= sender.OnTopLevelPointerPressed;
            sender._topLevel = TopLevel.GetTopLevel(sender) as InputElement;
            if (sender._topLevel is not null)
                sender._topLevel.PointerPressed += sender.OnTopLevelPointerPressed;
        }
        else if (!sender.IsEditing && sender._topLevel is not null)
        {
            sender._topLevel.PointerPressed -= sender.OnTopLevelPointerPressed;
            sender._topLevel = null;
        }
    }

    private void OnTopLevelPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!IsEditing)
            return;

        var root = TopLevel.GetTopLevel(this) as Visual;
        if (root is null)
            return;

        var pos = e.GetPosition(root);
        var bounds = this.Bounds;
        if (!bounds.Contains(pos))
        {
            if (SubmitMode is EditableSubmitMode.Blur or EditableSubmitMode.Both)
                Submit();
        }
    }

    /// <summary>附加到视觉树时处理</summary>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (string.IsNullOrEmpty(Value) && !string.IsNullOrEmpty(DefaultValue))
            Value = DefaultValue;
    }
}