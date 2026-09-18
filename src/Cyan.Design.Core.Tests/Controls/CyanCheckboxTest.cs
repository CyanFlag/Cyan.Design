using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Selections;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanCheckboxTest
{
    [AvaloniaFact]
    public void CyanCheckbox_Default_Properties_Should_Be_Expected()
    {
        var cb = new CyanCheckbox();
        Assert.False(cb.IsChecked);
        Assert.True(cb.IsEnabled);
    }

    [AvaloniaFact]
    public void CyanCheckbox_IsChecked_Should_Be_Settable()
    {
        var cb = new CyanCheckbox { IsChecked = true };
        Assert.True(cb.IsChecked);

        cb.IsChecked = false;
        Assert.False(cb.IsChecked);
    }

    [AvaloniaFact]
    public void CyanCheckbox_Should_Be_CheckBox()
    {
        var cb = new CyanCheckbox();
        Assert.IsAssignableFrom<CheckBox>(cb);
    }

    [AvaloniaFact]
    public void CyanCheckbox_Content_Should_Be_Settable()
    {
        var cb = new CyanCheckbox { Content = "Agree" };
        Assert.Equal("Agree", cb.Content);
    }
}