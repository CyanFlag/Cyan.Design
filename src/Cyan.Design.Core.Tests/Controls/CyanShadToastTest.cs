using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanShadToastTest
{
    [AvaloniaFact]
    public void CyanShadToast_Default_Properties_Should_Be_Expected()
    {
        var toast = new CyanShadToast();
        Assert.Null(toast.Title);
        Assert.Null(toast.Description);
        Assert.Equal(ShadToastType.Default, toast.Type);
    }

    [AvaloniaFact]
    public void CyanShadToast_Title_Should_Be_Settable()
    {
        var toast = new CyanShadToast { Title = "Hello" };
        Assert.Equal("Hello", toast.Title);
    }

    [AvaloniaFact]
    public void CyanShadToast_Description_Should_Be_Settable()
    {
        var toast = new CyanShadToast { Description = "World" };
        Assert.Equal("World", toast.Description);
    }

    [AvaloniaFact]
    public void CyanShadToast_Type_Should_Be_Settable()
    {
        var toast = new CyanShadToast { Type = ShadToastType.Success };
        Assert.Equal(ShadToastType.Success, toast.Type);
    }

    [AvaloniaFact]
    public void CyanShadToast_All_Types_Should_Be_Settable()
    {
        var toast = new CyanShadToast();
        foreach (var type in new[] { ShadToastType.Default, ShadToastType.Success, ShadToastType.Info, ShadToastType.Warning, ShadToastType.Error, ShadToastType.Loading })
        {
            toast.Type = type;
            Assert.Equal(type, toast.Type);
        }
    }
}