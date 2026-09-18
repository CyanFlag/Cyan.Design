using Avalonia.Headless.XUnit;
using Avalonia.Input;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Inputs;

namespace Cyan.Design.Core.Tests.Controls;

public class SearchTests
{
    [AvaloniaFact]
    public void CyanSearch_Default_Properties_Should_Be_Expected()
    {
        var search = new CyanSearch();
        Assert.False(search.Loading);
        Assert.Null(search.EnterButton);
        Assert.Null(search.SearchButtonForeground);
        Assert.Null(search.SearchButtonBackground);
    }

    [AvaloniaFact]
    public void CyanSearch_Loading_Should_Be_Settable()
    {
        var search = new CyanSearch { Loading = true };
        Assert.True(search.Loading);
    }

    [AvaloniaFact]
    public void CyanSearch_EnterButton_Should_Be_Settable()
    {
        var search = new CyanSearch { EnterButton = "Search" };
        Assert.Equal("Search", search.EnterButton);
    }

    [AvaloniaFact]
    public void CyanSearch_Searched_Event_Should_Be_Raisable()
    {
        var search = new CyanSearch();
        var fired = false;
        search.Searched += (_, _) => fired = true;
        search.RaiseEvent(new RoutedEventArgs(CyanSearch.SearchedEvent));
        Assert.True(fired);
    }

    [AvaloniaFact]
    public void CyanSearch_Should_Inherit_CyanInput_Properties()
    {
        var search = new CyanSearch();
        Assert.Equal(Cyan.Design.Core.Controls.Common.ControlSize.Middle, search.InputSize);
        Assert.Equal(InputStatus.Default, search.InputStatus);
    }
}