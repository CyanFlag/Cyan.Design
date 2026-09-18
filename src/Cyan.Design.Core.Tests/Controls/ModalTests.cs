using Avalonia.Headless.XUnit;
using Avalonia.Input;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Core.Tests.Controls;

public class ModalTests
{
    [AvaloniaFact]
    public void CyanModal_Default_Properties_Should_Be_Expected()
    {
        var modal = new CyanModal();
        Assert.False(modal.Open);
        Assert.True(modal.Closable);
        Assert.True(modal.Mask);
        Assert.True(modal.MaskClosable);
        Assert.True(modal.Keyboard);
        Assert.False(modal.Centered);
        Assert.Equal(520, modal.ModalWidth);
        Assert.Equal("确定", modal.OkText);
        Assert.Equal("取消", modal.CancelText);
    }

    [AvaloniaFact]
    public void CyanModal_Open_True_Should_Set_IsVisible_True()
    {
        var modal = new CyanModal { Open = true };
        Assert.True(modal.IsVisible);
    }

    [AvaloniaFact]
    public void CyanModal_Open_False_Should_Set_IsVisible_False()
    {
        var modal = new CyanModal();
        modal.Open = true;
        Assert.True(modal.IsVisible);
        modal.Open = false;
        Assert.False(modal.IsVisible);
    }

    [AvaloniaFact]
    public void CyanModal_Close_Should_Set_Open_False()
    {
        var modal = new CyanModal { Open = true };
        modal.Close();
        Assert.False(modal.Open);
    }

    [AvaloniaFact]
    public void CyanModal_EscapeKey_Should_Close_When_Keyboard_Enabled()
    {
        var modal = new CyanModal { Open = true, Keyboard = true };
        modal.RaiseEvent(new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Escape
        });

        Assert.False(modal.Open);
    }

    [AvaloniaFact]
    public void CyanModal_EscapeKey_Should_Not_Close_When_Keyboard_Disabled()
    {
        var modal = new CyanModal { Open = true, Keyboard = false };
        modal.RaiseEvent(new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Escape
        });

        Assert.True(modal.Open);
    }
}
