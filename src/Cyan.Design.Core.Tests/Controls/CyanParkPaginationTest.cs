using Avalonia.Headless.XUnit;
using Cyan.Design.Core.Controls.Common;
using Cyan.Design.Core.Controls.Navigation;

namespace Cyan.Design.Core.Tests.Controls;

public class CyanParkPaginationTest
{
    [AvaloniaFact]
    public void CyanParkPagination_Default_Properties_Should_Be_Expected()
    {
        var p = new CyanParkPagination();
        Assert.Equal(0, p.Count);
        Assert.Equal(1, p.Page);
        Assert.Equal(1, p.DefaultPage);
        Assert.Equal(10, p.PageSize);
        Assert.Equal(10, p.DefaultPageSize);
        Assert.Equal(1, p.SiblingCount);
        Assert.Equal(ControlSize.Middle, p.PaginationSize);
    }

    [AvaloniaFact]
    public void CyanParkPagination_Count_Should_Be_Settable()
    {
        var p = new CyanParkPagination { Count = 100 };
        Assert.Equal(100, p.Count);
    }

    [AvaloniaFact]
    public void CyanParkPagination_Page_Should_Be_Settable()
    {
        var p = new CyanParkPagination { Page = 3 };
        Assert.Equal(3, p.Page);
    }

    [AvaloniaFact]
    public void CyanParkPagination_PageSize_Should_Be_Settable()
    {
        var p = new CyanParkPagination { PageSize = 20 };
        Assert.Equal(20, p.PageSize);
    }

    [AvaloniaFact]
    public void CyanParkPagination_SiblingCount_Should_Be_Settable()
    {
        var p = new CyanParkPagination { SiblingCount = 2 };
        Assert.Equal(2, p.SiblingCount);
    }

    [AvaloniaFact]
    public void CyanParkPagination_PaginationSize_Should_Be_Settable()
    {
        var p = new CyanParkPagination { PaginationSize = ControlSize.Small };
        Assert.Equal(ControlSize.Small, p.PaginationSize);
    }

    [AvaloniaFact]
    public void CyanParkPagination_DefaultPage_Should_Be_Settable()
    {
        var p = new CyanParkPagination { DefaultPage = 2 };
        Assert.Equal(2, p.DefaultPage);
    }

    [AvaloniaFact]
    public void CyanParkPagination_DefaultPageSize_Should_Be_Settable()
    {
        var p = new CyanParkPagination { DefaultPageSize = 50 };
        Assert.Equal(50, p.DefaultPageSize);
    }
}