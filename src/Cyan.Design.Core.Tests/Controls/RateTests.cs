using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Core.Tests.Controls;

public class RateTests
{
    [AvaloniaFact]
    public void CyanRate_Default_Properties_Should_Be_Expected()
    {
        var rate = new CyanRate();
        Assert.Equal(5, rate.Count);
        Assert.Equal(0, rate.Value);
        Assert.False(rate.AllowHalf);
        Assert.True(rate.AllowClear);
        Assert.False(rate.Disabled);
        Assert.False(rate.ReadOnly);
        Assert.Equal("★", rate.Character);
        Assert.Equal(20, rate.StarFontSize);
    }

    [AvaloniaFact]
    public void CyanRate_Count_Should_Be_Settable()
    {
        var rate = new CyanRate { Count = 10 };
        Assert.Equal(10, rate.Count);
    }

    [AvaloniaFact]
    public void CyanRate_Value_Should_Be_Settable()
    {
        var rate = new CyanRate { Value = 3.5 };
        Assert.Equal(3.5, rate.Value);
    }

    [AvaloniaFact]
    public void CyanRate_AllowHalf_Should_Be_Settable()
    {
        var rate = new CyanRate { AllowHalf = true };
        Assert.True(rate.AllowHalf);
    }

    [AvaloniaFact]
    public void CyanRate_AllowClear_Should_Be_Settable()
    {
        var rate = new CyanRate { AllowClear = false };
        Assert.False(rate.AllowClear);
    }

    [AvaloniaFact]
    public void CyanRate_Disabled_Should_Be_Settable()
    {
        var rate = new CyanRate { Disabled = true };
        Assert.True(rate.Disabled);
    }

    [AvaloniaFact]
    public void CyanRate_ReadOnly_Should_Be_Settable()
    {
        var rate = new CyanRate { ReadOnly = true };
        Assert.True(rate.ReadOnly);
    }

    [AvaloniaFact]
    public void CyanRate_Character_Should_Be_Settable()
    {
        var rate = new CyanRate { Character = "♥" };
        Assert.Equal("♥", rate.Character);
    }
}
