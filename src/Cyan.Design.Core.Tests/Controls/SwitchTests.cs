using Avalonia.Headless.XUnit;
using Avalonia.Input;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Selections;

namespace Cyan.Design.Core.Tests.Controls;

public class SwitchTests
{
    [AvaloniaFact]
    public void CyanSwitch_Default_Properties_Should_Be_Expected()
    {
        var sw = new CyanSwitch();
        Assert.False(sw.Loading);
        Assert.Equal(ControlSize.Middle, sw.Size);
    }

    [AvaloniaFact]
    public void CyanSwitch_Loading_Should_Not_Toggle_Via_SpaceKey()
    {
        var sw = new CyanSwitch { Loading = true };
        var args = new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Space
        };
        sw.RaiseEvent(args);
        Assert.True(args.Handled);
        Assert.False(sw.IsChecked);
    }

    [AvaloniaFact]
    public void CyanSwitch_Loading_Should_Handle_SpaceKey()
    {
        var sw = new CyanSwitch { Loading = true };
        var args = new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Space
        };
        sw.RaiseEvent(args);
        Assert.True(args.Handled);
    }

    [AvaloniaFact]
    public void CyanSwitch_Loading_Should_Handle_EnterKey()
    {
        var sw = new CyanSwitch { Loading = true };
        var args = new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Enter
        };
        sw.RaiseEvent(args);
        Assert.True(args.Handled);
    }
}
