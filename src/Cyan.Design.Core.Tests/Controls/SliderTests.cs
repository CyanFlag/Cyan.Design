using Avalonia.Headless.XUnit;
using Avalonia.Input;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Core.Tests.Controls;

public class SliderTests
{
    [AvaloniaFact]
    public void CyanSlider_Should_Be_Focusable()
    {
        var slider = new CyanSlider();
        Assert.True(slider.Focusable);
    }

    [AvaloniaFact]
    public void CyanSlider_KeyDown_Right_Should_Increase_Value()
    {
        var slider = new CyanSlider { Value = 50, Step = 5 };
        slider.RaiseEvent(new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Right
        });

        Assert.Equal(55, slider.Value);
    }

    [AvaloniaFact]
    public void CyanSlider_KeyDown_Left_Should_Decrease_Value()
    {
        var slider = new CyanSlider { Value = 50, Step = 5 };
        slider.RaiseEvent(new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Left
        });

        Assert.Equal(45, slider.Value);
    }

    [AvaloniaFact]
    public void CyanSlider_KeyDown_Home_Should_Set_Minimum()
    {
        var slider = new CyanSlider { Value = 50, Minimum = 0, Maximum = 100 };
        slider.RaiseEvent(new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Home
        });

        Assert.Equal(0, slider.Value);
    }

    [AvaloniaFact]
    public void CyanSlider_KeyDown_End_Should_Set_Maximum()
    {
        var slider = new CyanSlider { Value = 50, Minimum = 0, Maximum = 100 };
        slider.RaiseEvent(new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.End
        });

        Assert.Equal(100, slider.Value);
    }

    [AvaloniaFact]
    public void CyanSlider_RangeMode_KeyDown_Right_Should_Increase_RangeEnd()
    {
        var slider = new CyanSlider
        {
            Range = true,
            RangeStart = 20,
            RangeEnd = 50,
            Step = 5
        };
        slider.RaiseEvent(new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Right
        });

        Assert.Equal(55, slider.RangeEnd);
    }

    [AvaloniaFact]
    public void CyanSlider_RangeMode_KeyDown_Left_Should_Decrease_RangeStart()
    {
        var slider = new CyanSlider
        {
            Range = true,
            RangeStart = 20,
            RangeEnd = 50,
            Step = 5
        };
        slider.RaiseEvent(new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Left
        });

        Assert.Equal(15, slider.RangeStart);
    }

    [AvaloniaFact]
    public void CyanSlider_Disabled_Should_Not_Respond_To_Keyboard()
    {
        var slider = new CyanSlider { Value = 50, Step = 5, Disabled = true };
        slider.RaiseEvent(new KeyEventArgs
        {
            RoutedEvent = InputElement.KeyDownEvent,
            Key = Key.Right
        });

        Assert.Equal(50, slider.Value);
    }
}
