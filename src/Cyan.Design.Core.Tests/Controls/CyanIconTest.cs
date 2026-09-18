using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Icons;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanIconTest
{
    [AvaloniaFact]
    public void CyanIcon_Default_Properties_Should_Be_Expected()
    {
        var icon = new CyanIcon();
        Assert.Null(icon.IconKey);
        Assert.Null(icon.Text);
    }

    [AvaloniaFact]
    public void CyanIcon_IconKey_Should_Be_Settable()
    {
        var icon = new CyanIcon { IconKey = "shangyi" };
        Assert.Equal("shangyi", icon.IconKey);
    }

    [AvaloniaFact]
    public void CyanIcon_Valid_Key_Should_Set_Text()
    {
        var icon = new CyanIcon { IconKey = "shangyi" };
        Assert.NotNull(icon.Text);
        Assert.NotEmpty(icon.Text);
    }

    [AvaloniaFact]
    public void CyanIcon_Invalid_Key_Should_Set_Null_Text()
    {
        var icon = new CyanIcon { IconKey = "non-existent-key-xyz" };
        Assert.Null(icon.Text);
    }

    [AvaloniaFact]
    public void CyanIcon_Empty_Key_Should_Set_Null_Text()
    {
        var icon = new CyanIcon { IconKey = "" };
        Assert.Null(icon.Text);
    }

    [AvaloniaFact]
    public void CyanIcon_Null_Key_Should_Set_Null_Text()
    {
        var icon = new CyanIcon { IconKey = null };
        Assert.Null(icon.Text);
    }
}