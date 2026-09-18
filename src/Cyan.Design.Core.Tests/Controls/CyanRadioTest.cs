using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Selections;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanRadioTest
{
    [AvaloniaFact]
    public void CyanRadio_Default_Properties_Should_Be_Expected()
    {
        var radio = new CyanRadio();
        Assert.Null(radio.Value);
        Assert.Equal(RadioOptionType.Default, radio.OptionType);
        Assert.Equal(ControlSize.Middle, radio.RadioSize);
        Assert.Equal(RadioButtonStyle.Outline, radio.ButtonStyle);
    }

    [AvaloniaFact]
    public void CyanRadio_Value_Should_Be_Settable()
    {
        var radio = new CyanRadio { Value = "A" };
        Assert.Equal("A", radio.Value);
    }

    [AvaloniaFact]
    public void CyanRadio_OptionType_Should_Be_Settable()
    {
        var radio = new CyanRadio { OptionType = RadioOptionType.Button };
        Assert.Equal(RadioOptionType.Button, radio.OptionType);
    }

    [AvaloniaFact]
    public void CyanRadio_RadioSize_Should_Be_Settable()
    {
        var radio = new CyanRadio { RadioSize = ControlSize.Large };
        Assert.Equal(ControlSize.Large, radio.RadioSize);
    }

    [AvaloniaFact]
    public void CyanRadio_ButtonStyle_Should_Be_Settable()
    {
        var radio = new CyanRadio { ButtonStyle = RadioButtonStyle.Solid };
        Assert.Equal(RadioButtonStyle.Solid, radio.ButtonStyle);
    }

    [AvaloniaFact]
    public void CyanRadio_IsChecked_Should_Be_Settable()
    {
        var radio = new CyanRadio { IsChecked = true };
        Assert.True(radio.IsChecked);
    }
}