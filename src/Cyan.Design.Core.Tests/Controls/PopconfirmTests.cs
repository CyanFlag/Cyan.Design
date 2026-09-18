using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Buttons;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Core.Tests.Controls;

public class PopconfirmTests
{
    [AvaloniaFact]
    public void CyanPopconfirm_Default_Properties_Should_Be_Expected()
    {
        var popconfirm = new CyanPopconfirm();
        Assert.False(popconfirm.Open);
        Assert.Null(popconfirm.Title);
        Assert.Null(popconfirm.Description);
        Assert.Equal("确定", popconfirm.OkText);
        Assert.Equal("取消", popconfirm.CancelText);
        Assert.Equal(ButtonType.Primary, popconfirm.OkType);
        Assert.True(popconfirm.ShowCancel);
        Assert.Null(popconfirm.Icon);
        Assert.False(popconfirm.Disabled);
        Assert.Equal(PopconfirmPlacement.Top, popconfirm.Placement);
        Assert.True(popconfirm.Arrow);
        Assert.False(popconfirm.ConfirmLoading);
        Assert.Equal(PopconfirmTrigger.Click, popconfirm.Trigger);
    }

    [AvaloniaFact]
    public void CyanPopconfirm_Open_Should_Be_Settable()
    {
        var popconfirm = new CyanPopconfirm { Open = true };
        Assert.True(popconfirm.Open);
    }

    [AvaloniaFact]
    public void CyanPopconfirm_OkText_Should_Be_Settable()
    {
        var popconfirm = new CyanPopconfirm { OkText = "Yes" };
        Assert.Equal("Yes", popconfirm.OkText);
    }

    [AvaloniaFact]
    public void CyanPopconfirm_CancelText_Should_Be_Settable()
    {
        var popconfirm = new CyanPopconfirm { CancelText = "No" };
        Assert.Equal("No", popconfirm.CancelText);
    }

    [AvaloniaFact]
    public void CyanPopconfirm_OkType_Should_Be_Settable()
    {
        var popconfirm = new CyanPopconfirm { OkType = ButtonType.Dashed };
        Assert.Equal(ButtonType.Dashed, popconfirm.OkType);
    }

    [AvaloniaFact]
    public void CyanPopconfirm_Disabled_Should_Be_Settable()
    {
        var popconfirm = new CyanPopconfirm { Disabled = true };
        Assert.True(popconfirm.Disabled);
    }

    [AvaloniaFact]
    public void CyanPopconfirm_Placement_Should_Be_Settable()
    {
        var popconfirm = new CyanPopconfirm { Placement = PopconfirmPlacement.Bottom };
        Assert.Equal(PopconfirmPlacement.Bottom, popconfirm.Placement);
    }

    [AvaloniaFact]
    public void CyanPopconfirm_Trigger_Should_Be_Settable()
    {
        var popconfirm = new CyanPopconfirm { Trigger = PopconfirmTrigger.Hover };
        Assert.Equal(PopconfirmTrigger.Hover, popconfirm.Trigger);
    }
}
