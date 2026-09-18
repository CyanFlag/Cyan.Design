using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Dropdowns;
using Cyan.Design.Core.Controls.Inputs;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanSelectTest
{
    [AvaloniaFact]
    public void CyanSelect_Default_Properties_Should_Be_Expected()
    {
        var select = new CyanSelect();
        Assert.Equal(ControlSize.Middle, select.InputSize);
        Assert.Equal(InputStatus.Default, select.InputStatus);
    }

    [AvaloniaFact]
    public void CyanSelect_InputSize_Should_Be_Settable()
    {
        var select = new CyanSelect { InputSize = ControlSize.Large };
        Assert.Equal(ControlSize.Large, select.InputSize);

        select.InputSize = ControlSize.Small;
        Assert.Equal(ControlSize.Small, select.InputSize);
    }

    [AvaloniaFact]
    public void CyanSelect_InputStatus_Should_Be_Settable()
    {
        var select = new CyanSelect { InputStatus = InputStatus.Error };
        Assert.Equal(InputStatus.Error, select.InputStatus);

        select.InputStatus = InputStatus.Warning;
        Assert.Equal(InputStatus.Warning, select.InputStatus);
    }

    [AvaloniaFact]
    public void CyanSelect_Should_Be_ComboBox()
    {
        var select = new CyanSelect();
        Assert.IsAssignableFrom<ComboBox>(select);
    }
}