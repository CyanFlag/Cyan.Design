using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Selections;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanRadioGroupTest
{
    [AvaloniaFact]
    public void CyanRadioGroup_Default_Properties_Should_Be_Expected()
    {
        var group = new CyanRadioGroup();
        Assert.Null(group.Value);
        Assert.Null(group.DefaultValue);
        Assert.False(group.Disabled);
        Assert.Equal(RadioOptionType.Default, group.OptionType);
        Assert.Equal(RadioButtonStyle.Outline, group.ButtonStyle);
        Assert.Equal(ControlSize.Middle, group.Size);
        Assert.False(group.Block);
    }

    [AvaloniaFact]
    public void CyanRadioGroup_Disabled_Should_Be_Settable()
    {
        var group = new CyanRadioGroup { Disabled = true };
        Assert.True(group.Disabled);
    }

    [AvaloniaFact]
    public void CyanRadioGroup_OptionType_Should_Be_Settable()
    {
        var group = new CyanRadioGroup { OptionType = RadioOptionType.Button };
        Assert.Equal(RadioOptionType.Button, group.OptionType);
    }

    [AvaloniaFact]
    public void CyanRadioGroup_ButtonStyle_Should_Be_Settable()
    {
        var group = new CyanRadioGroup { ButtonStyle = RadioButtonStyle.Solid };
        Assert.Equal(RadioButtonStyle.Solid, group.ButtonStyle);
    }

    [AvaloniaFact]
    public void CyanRadioGroup_Size_Should_Be_Settable()
    {
        var group = new CyanRadioGroup { Size = ControlSize.Small };
        Assert.Equal(ControlSize.Small, group.Size);
    }

    [AvaloniaFact]
    public void CyanRadioGroup_Block_Should_Be_Settable()
    {
        var group = new CyanRadioGroup { Block = true };
        Assert.True(group.Block);
    }

    [AvaloniaFact]
    public void CyanRadioGroup_Value_Should_Be_Settable()
    {
        var group = new CyanRadioGroup { Value = "X" };
        Assert.Equal("X", group.Value);
    }

    [AvaloniaFact]
    public void CyanRadioGroup_DefaultValue_Should_Be_Settable()
    {
        var group = new CyanRadioGroup { DefaultValue = "Default" };
        Assert.Equal("Default", group.DefaultValue);
    }
}