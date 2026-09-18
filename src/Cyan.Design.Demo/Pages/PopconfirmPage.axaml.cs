using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Demo.Pages;

public partial class PopconfirmPage : UserControl
{
    public PopconfirmPage()
    {
        InitializeComponent();
        AsyncPopconfirm.Confirm += OnAsyncConfirm;
    }

    private void OnAsyncConfirm(object? sender, RoutedEventArgs e)
    {
        AsyncPopconfirm.ConfirmLoading = true;
        DispatcherTimer.RunOnce(() =>
        {
            AsyncPopconfirm.ConfirmLoading = false;
            AsyncPopconfirm.Open = false;
        }, TimeSpan.FromSeconds(2));
    }
}
