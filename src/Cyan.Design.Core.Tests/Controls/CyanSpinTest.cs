using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanSpinTest
{
    [AvaloniaFact]
    public void CyanSpin_Default_Properties_Should_Be_Expected()
    {
        var spin = new CyanSpin();
        Assert.True(spin.Spinning);
        Assert.Equal(ControlSize.Middle, spin.Size);
        Assert.Null(spin.Tip);
        Assert.Equal(0, spin.Delay);
        Assert.Null(spin.Content);
    }

    [AvaloniaFact]
    public void CyanSpin_Spinning_Should_Be_Settable()
    {
        var spin = new CyanSpin { Spinning = false };
        Assert.False(spin.Spinning);
    }

    [AvaloniaFact]
    public void CyanSpin_Size_Should_Be_Settable()
    {
        var spin = new CyanSpin { Size = ControlSize.Small };
        Assert.Equal(ControlSize.Small, spin.Size);

        spin.Size = ControlSize.Large;
        Assert.Equal(ControlSize.Large, spin.Size);
    }

    [AvaloniaFact]
    public void CyanSpin_Tip_Should_Be_Settable()
    {
        var spin = new CyanSpin { Tip = "Loading" };
        Assert.Equal("Loading", spin.Tip);
    }

    [AvaloniaFact]
    public void CyanSpin_Delay_Should_Be_Settable()
    {
        var spin = new CyanSpin { Delay = 500 };
        Assert.Equal(500, spin.Delay);
    }

    [AvaloniaFact]
    public void CyanSpin_Content_Should_Be_Settable()
    {
        var spin = new CyanSpin { Content = "content" };
        Assert.Equal("content", spin.Content);
    }
}