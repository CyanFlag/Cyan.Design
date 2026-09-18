using Avalonia.Headless.XUnit;
using Avalonia.Input;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Inputs;

namespace Cyan.Design.Core.Tests.Controls;

public class InputTests
{
    [AvaloniaFact]
    public void CyanInput_Should_Have_Submitted_Event()
    {
        var input = new CyanInput();
        bool submitted = false;
        input.Submitted += (_, _) => submitted = true;

        input.RaiseEvent(new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Enter
        });

        Assert.True(submitted);
    }

    [AvaloniaFact]
    public void CyanInput_Default_Properties_Should_Be_Expected()
    {
        var input = new CyanInput();
        Assert.Equal(ControlSize.Middle, input.InputSize);
        Assert.Equal(InputStatus.Default, input.InputStatus);
        Assert.Equal(InputVariant.Outlined, input.InputVariant);
        Assert.False(input.AllowClear);
        Assert.False(input.ShowCount);
    }
}
