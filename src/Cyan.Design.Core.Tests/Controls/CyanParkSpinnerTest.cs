using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanParkSpinnerTest
{
    [AvaloniaFact]
    public void CyanParkSpinner_Default_Properties_Should_Be_Expected()
    {
        var spinner = new CyanParkSpinner();
        Assert.Equal(ParkSpinnerSize.MD, spinner.SpinnerSize);
        Assert.Equal(1000, spinner.Speed);
        Assert.Equal(2.0, spinner.Thickness);
        Assert.Null(spinner.Foreground);
        Assert.Null(spinner.TrackColor);
    }

    [AvaloniaFact]
    public void CyanParkSpinner_Default_Size_Should_Be_20()
    {
        var spinner = new CyanParkSpinner();
        Assert.Equal(20.0, spinner.Width);
        Assert.Equal(20.0, spinner.Height);
    }

    [AvaloniaFact]
    public void CyanParkSpinner_SpinnerSize_LG_Should_Update_Dimensions()
    {
        var spinner = new CyanParkSpinner { SpinnerSize = ParkSpinnerSize.LG };
        Assert.Equal(24.0, spinner.Width);
        Assert.Equal(24.0, spinner.Height);
    }

    [AvaloniaFact]
    public void CyanParkSpinner_SpinnerSize_XS_Should_Update_Dimensions()
    {
        var spinner = new CyanParkSpinner { SpinnerSize = ParkSpinnerSize.XS };
        Assert.Equal(12.0, spinner.Width);
        Assert.Equal(12.0, spinner.Height);
    }

    [AvaloniaFact]
    public void CyanParkSpinner_SpinnerSize_SM_Should_Update_Dimensions()
    {
        var spinner = new CyanParkSpinner { SpinnerSize = ParkSpinnerSize.SM };
        Assert.Equal(16.0, spinner.Width);
        Assert.Equal(16.0, spinner.Height);
    }

    [AvaloniaFact]
    public void CyanParkSpinner_SpinnerSize_XL_Should_Update_Dimensions()
    {
        var spinner = new CyanParkSpinner { SpinnerSize = ParkSpinnerSize.XL };
        Assert.Equal(28.0, spinner.Width);
        Assert.Equal(28.0, spinner.Height);
    }

    [AvaloniaFact]
    public void CyanParkSpinner_SpinnerSize_XXL_Should_Update_Dimensions()
    {
        var spinner = new CyanParkSpinner { SpinnerSize = ParkSpinnerSize.XXL };
        Assert.Equal(32.0, spinner.Width);
        Assert.Equal(32.0, spinner.Height);
    }

    [AvaloniaFact]
    public void CyanParkSpinner_Speed_Should_Be_Settable()
    {
        var spinner = new CyanParkSpinner { Speed = 500 };
        Assert.Equal(500, spinner.Speed);
    }

    [AvaloniaFact]
    public void CyanParkSpinner_Thickness_Should_Be_Settable()
    {
        var spinner = new CyanParkSpinner { Thickness = 4.0 };
        Assert.Equal(4.0, spinner.Thickness);
    }
}