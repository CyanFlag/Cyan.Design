using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Buttons;
using Cyan.Design.Core.Controls.Common;

namespace Cyan.Design.Core.Tests.Controls;

public class ButtonTests
{
    [AvaloniaFact]
    public void CyanButton_Default_Properties_Should_Be_Expected()
    {
        var button = new CyanButton();
        Assert.Equal(ButtonType.Default, button.ButtonType);
        Assert.Equal(ControlSize.Middle, button.ButtonSize);
        Assert.Equal(ButtonShape.Default, button.Shape);
        Assert.False(button.Danger);
        Assert.False(button.Block);
        Assert.False(button.Loading);
        Assert.False(button.Ghost);
    }

    [AvaloniaFact]
    public void CyanButton_ButtonType_Should_Be_Settable()
    {
        var button = new CyanButton();
        button.ButtonType = ButtonType.Primary;
        Assert.Equal(ButtonType.Primary, button.ButtonType);

        button.ButtonType = ButtonType.Dashed;
        Assert.Equal(ButtonType.Dashed, button.ButtonType);

        button.ButtonType = ButtonType.Text;
        Assert.Equal(ButtonType.Text, button.ButtonType);

        button.ButtonType = ButtonType.Link;
        Assert.Equal(ButtonType.Link, button.ButtonType);
    }

    [AvaloniaFact]
    public void CyanButton_ButtonSize_Should_Be_Settable()
    {
        var button = new CyanButton();
        button.ButtonSize = ControlSize.Large;
        Assert.Equal(ControlSize.Large, button.ButtonSize);

        button.ButtonSize = ControlSize.Small;
        Assert.Equal(ControlSize.Small, button.ButtonSize);
    }

    [AvaloniaFact]
    public void CyanButton_Shape_Should_Be_Settable()
    {
        var button = new CyanButton();
        button.Shape = ButtonShape.Circle;
        Assert.Equal(ButtonShape.Circle, button.Shape);

        button.Shape = ButtonShape.Round;
        Assert.Equal(ButtonShape.Round, button.Shape);
    }

    [AvaloniaFact]
    public void CyanButton_Danger_Should_Be_Settable()
    {
        var button = new CyanButton { Danger = true };
        Assert.True(button.Danger);
    }

    [AvaloniaFact]
    public void CyanButton_Block_Should_Be_Settable()
    {
        var button = new CyanButton { Block = true };
        Assert.True(button.Block);
    }

    [AvaloniaFact]
    public void CyanButton_Loading_Should_Be_Settable()
    {
        var button = new CyanButton { Loading = true };
        Assert.True(button.Loading);
    }

    [AvaloniaFact]
    public void CyanButton_Ghost_Should_Be_Settable()
    {
        var button = new CyanButton { Ghost = true };
        Assert.True(button.Ghost);
    }
}
