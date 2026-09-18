using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Inputs;

namespace Cyan.Design.Core.Tests.Controls;

public class PasswordTests
{
    [AvaloniaFact]
    public void CyanPassword_Default_Properties_Should_Be_Expected()
    {
        var password = new CyanPassword();
        Assert.True(password.VisibilityToggle);
        Assert.False(password.IsPasswordVisible);
    }

    [AvaloniaFact]
    public void CyanPassword_Toggle_To_Visible_Should_Remove_Mask()
    {
        var password = new CyanPassword();
        password.IsPasswordVisible = true;
        Assert.True(password.IsPasswordVisible);
        Assert.Equal('\0', password.PasswordChar);
    }

    [AvaloniaFact]
    public void CyanPassword_Toggle_Back_To_Hidden_Should_Apply_Mask()
    {
        var password = new CyanPassword();
        password.IsPasswordVisible = true;
        password.IsPasswordVisible = false;
        Assert.False(password.IsPasswordVisible);
        Assert.Equal('●', password.PasswordChar);
    }

    [AvaloniaFact]
    public void CyanPassword_VisibilityToggle_Can_Be_Disabled()
    {
        var password = new CyanPassword { VisibilityToggle = false };
        Assert.False(password.VisibilityToggle);
    }
}
