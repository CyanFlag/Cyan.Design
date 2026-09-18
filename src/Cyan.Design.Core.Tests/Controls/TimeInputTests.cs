using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Inputs;

namespace Cyan.Design.Core.Tests.Controls;

public class TimeInputTests
{
    [AvaloniaFact]
    public void CyanTimeInput_Default_Properties_Should_Be_Expected()
    {
        var input = new CyanTimeInput();
        Assert.Null(input.Value);
        Assert.Equal("00", input.HourString);
        Assert.Equal("00", input.MinuteString);
        Assert.Equal("00", input.SecondString);
        Assert.Equal("12HourClock", input.ClockIdentifier);
        Assert.False(input.UseSeconds);
        Assert.False(input.InputFocus);
    }

    [AvaloniaFact]
    public void CyanTimeInput_ClockIdentifier_Should_Support_24Hour()
    {
        var input = new CyanTimeInput { ClockIdentifier = "24HourClock" };
        Assert.Equal("24HourClock", input.ClockIdentifier);
    }

    [AvaloniaFact]
    public void CyanTimeInput_ClockIdentifier_Should_Support_12Hour()
    {
        var input = new CyanTimeInput { ClockIdentifier = "12HourClock" };
        Assert.Equal("12HourClock", input.ClockIdentifier);
    }

    [AvaloniaFact]
    public void CyanTimeInput_ClockIdentifier_Empty_Should_Be_Allowed()
    {
        var input = new CyanTimeInput { ClockIdentifier = "" };
        Assert.Equal("", input.ClockIdentifier);
    }

    [AvaloniaFact]
    public void CyanTimeInput_UseSeconds_Should_Be_Settable()
    {
        var input = new CyanTimeInput { UseSeconds = true };
        Assert.True(input.UseSeconds);
    }

    [AvaloniaFact]
    public void CyanTimeInput_HourString_Should_Be_Settable()
    {
        var input = new CyanTimeInput { HourString = "09" };
        Assert.Equal("09", input.HourString);
    }

    [AvaloniaFact]
    public void CyanTimeInput_MinuteString_Should_Be_Settable()
    {
        var input = new CyanTimeInput { MinuteString = "30" };
        Assert.Equal("30", input.MinuteString);
    }

    [AvaloniaFact]
    public void CyanTimeInput_InputFocus_Should_Be_Settable()
    {
        var input = new CyanTimeInput { InputFocus = true };
        Assert.True(input.InputFocus);
    }
}
