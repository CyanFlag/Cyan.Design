using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Core.Tests.Controls;

public class ProgressTests
{
    [AvaloniaFact]
    public void CyanProgress_Default_Properties_Should_Be_Expected()
    {
        var progress = new CyanProgress();
        Assert.Equal(ProgressType.Line, progress.Type);
        Assert.Equal(0, progress.Percent);
        Assert.True(progress.ShowInfo);
        Assert.Equal(ProgressStatus.Normal, progress.Status);
        Assert.Equal(ControlSize.Middle, progress.Size);
        Assert.Equal(6, progress.StrokeWidth);
        Assert.Equal(75, progress.GapDegree);
        Assert.Equal(ProgressStrokeLinecap.Round, progress.StrokeLinecap);
        Assert.Equal(0, progress.SuccessPercent);
        Assert.Equal(120, progress.CircleWidth);
        Assert.Equal(0, progress.Steps);
        Assert.Equal(2, progress.Gap);
        Assert.Equal(ProgressInfoPosition.Right, progress.InfoPosition);
    }

    [AvaloniaFact]
    public void CyanProgress_Type_Should_Be_Settable()
    {
        var progress = new CyanProgress();
        progress.Type = ProgressType.Circle;
        Assert.Equal(ProgressType.Circle, progress.Type);

        progress.Type = ProgressType.Dashboard;
        Assert.Equal(ProgressType.Dashboard, progress.Type);
    }

    [AvaloniaFact]
    public void CyanProgress_Percent_Should_Be_Settable()
    {
        var progress = new CyanProgress { Percent = 65.5 };
        Assert.Equal(65.5, progress.Percent);
    }

    [AvaloniaFact]
    public void CyanProgress_Status_Should_Be_Settable()
    {
        var progress = new CyanProgress();
        progress.Status = ProgressStatus.Success;
        Assert.Equal(ProgressStatus.Success, progress.Status);

        progress.Status = ProgressStatus.Exception;
        Assert.Equal(ProgressStatus.Exception, progress.Status);

        progress.Status = ProgressStatus.Active;
        Assert.Equal(ProgressStatus.Active, progress.Status);
    }

    [AvaloniaFact]
    public void CyanProgress_Size_Should_Be_Settable()
    {
        var progress = new CyanProgress();
        progress.Size = ControlSize.Large;
        Assert.Equal(ControlSize.Large, progress.Size);

        progress.Size = ControlSize.Small;
        Assert.Equal(ControlSize.Small, progress.Size);
    }

    [AvaloniaFact]
    public void CyanProgress_ShowInfo_Should_Be_Settable()
    {
        var progress = new CyanProgress { ShowInfo = false };
        Assert.False(progress.ShowInfo);
    }

    [AvaloniaFact]
    public void CyanProgress_StrokeLinecap_Should_Be_Settable()
    {
        var progress = new CyanProgress { StrokeLinecap = ProgressStrokeLinecap.Butt };
        Assert.Equal(ProgressStrokeLinecap.Butt, progress.StrokeLinecap);
    }

    [AvaloniaFact]
    public void CyanProgress_InfoPosition_Should_Be_Settable()
    {
        var progress = new CyanProgress { InfoPosition = ProgressInfoPosition.Top };
        Assert.Equal(ProgressInfoPosition.Top, progress.InfoPosition);
    }
}
