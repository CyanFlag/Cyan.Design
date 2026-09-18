using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Inputs;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanEditableTest
{
    [AvaloniaFact]
    public void CyanEditable_Default_Properties_Should_Be_Expected()
    {
        var ed = new CyanEditable();
        Assert.Equal(string.Empty, ed.Value);
        Assert.Equal(string.Empty, ed.DefaultValue);
        Assert.Equal("Click to edit", ed.Placeholder);
        Assert.False(ed.IsEditing);
        Assert.False(ed.IsDisabled);
        Assert.Equal(EditableActivationMode.Focus, ed.ActivationMode);
        Assert.Equal(EditableSubmitMode.Both, ed.SubmitMode);
        Assert.False(ed.ShowControls);
        Assert.Equal(ControlSize.Middle, ed.EditableSize);
        Assert.True(ed.SelectOnFocus);
        Assert.Equal(-1, ed.MaxLength);
    }

    [AvaloniaFact]
    public void CyanEditable_Value_Should_Be_Settable()
    {
        var ed = new CyanEditable { Value = "Hello" };
        Assert.Equal("Hello", ed.Value);
    }

    [AvaloniaFact]
    public void CyanEditable_DefaultValue_Should_Be_Settable()
    {
        var ed = new CyanEditable { DefaultValue = "Default" };
        Assert.Equal("Default", ed.DefaultValue);
    }

    [AvaloniaFact]
    public void CyanEditable_Placeholder_Should_Be_Settable()
    {
        var ed = new CyanEditable { Placeholder = "Edit me" };
        Assert.Equal("Edit me", ed.Placeholder);
    }

    [AvaloniaFact]
    public void CyanEditable_IsDisabled_Should_Be_Settable()
    {
        var ed = new CyanEditable { IsDisabled = true };
        Assert.True(ed.IsDisabled);
    }

    [AvaloniaFact]
    public void CyanEditable_ActivationMode_Should_Be_Settable()
    {
        var ed = new CyanEditable { ActivationMode = EditableActivationMode.DoubleClick };
        Assert.Equal(EditableActivationMode.DoubleClick, ed.ActivationMode);
    }

    [AvaloniaFact]
    public void CyanEditable_SubmitMode_Should_Be_Settable()
    {
        var ed = new CyanEditable { SubmitMode = EditableSubmitMode.Enter };
        Assert.Equal(EditableSubmitMode.Enter, ed.SubmitMode);
    }

    [AvaloniaFact]
    public void CyanEditable_ShowControls_Should_Be_Settable()
    {
        var ed = new CyanEditable { ShowControls = true };
        Assert.True(ed.ShowControls);
    }

    [AvaloniaFact]
    public void CyanEditable_EditableSize_Should_Be_Settable()
    {
        var ed = new CyanEditable { EditableSize = ControlSize.Small };
        Assert.Equal(ControlSize.Small, ed.EditableSize);
    }

    [AvaloniaFact]
    public void CyanEditable_SelectOnFocus_Should_Be_Settable()
    {
        var ed = new CyanEditable { SelectOnFocus = false };
        Assert.False(ed.SelectOnFocus);
    }

    [AvaloniaFact]
    public void CyanEditable_MaxLength_Should_Be_Settable()
    {
        var ed = new CyanEditable { MaxLength = 10 };
        Assert.Equal(10, ed.MaxLength);
    }

    [AvaloniaFact]
    public void CyanEditable_EnterEdit_When_Disabled_Should_Not_Enter_Editing()
    {
        var ed = new CyanEditable { IsDisabled = true };
        ed.EnterEdit();
        Assert.False(ed.IsEditing);
    }

    [AvaloniaFact]
    public void CyanEditable_EnterEdit_When_Not_Disabled_Should_Enter_Editing()
    {
        var ed = new CyanEditable { Value = "Test" };
        ed.EnterEdit();
        Assert.True(ed.IsEditing);
    }

    [AvaloniaFact]
    public void CyanEditable_Submit_When_Not_Editing_Should_Noop()
    {
        var ed = new CyanEditable();
        ed.Submit();
        Assert.False(ed.IsEditing);
    }

    [AvaloniaFact]
    public void CyanEditable_Cancel_When_Not_Editing_Should_Noop()
    {
        var ed = new CyanEditable();
        ed.Cancel();
        Assert.False(ed.IsEditing);
    }

    [AvaloniaFact]
    public void CyanEditable_Cancel_After_EnterEdit_Should_Exit_Editing()
    {
        var ed = new CyanEditable { Value = "Test" };
        ed.EnterEdit();
        ed.Cancel();
        Assert.False(ed.IsEditing);
    }
}