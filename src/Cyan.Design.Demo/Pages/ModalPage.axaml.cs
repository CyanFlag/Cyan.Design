using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Demo.Pages;

public partial class ModalPage : UserControl
{
    public ModalPage()
    {
        InitializeComponent();
    }

    private void OnOpenBasic(object? sender, RoutedEventArgs e) => BasicModal.Open = true;
    private void OnOpenCustomFooter(object? sender, RoutedEventArgs e) => CustomFooterModal.Open = true;
    private void OnOpenCentered(object? sender, RoutedEventArgs e) => CenteredModal.Open = true;
    private void OnOpenNarrow(object? sender, RoutedEventArgs e) => NarrowModal.Open = true;
    private void OnOpenWide(object? sender, RoutedEventArgs e) => WideModal.Open = true;
    private void OnOpenNoMask(object? sender, RoutedEventArgs e) => NoMaskModal.Open = true;
    private void OnOpenNoClosable(object? sender, RoutedEventArgs e) => NoClosableModal.Open = true;

    private void OnCloseCustomFooter(object? sender, RoutedEventArgs e) => CustomFooterModal.Open = false;

    private void OnOpenConfirmLoading(object? sender, RoutedEventArgs e)
    {
        ConfirmLoadingModal.Open = true;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        ConfirmLoadingModal.Ok += OnConfirmLoadingOk;
    }

    private void OnConfirmLoadingOk(object? sender, RoutedEventArgs e)
    {
        ConfirmLoadingModal.ConfirmLoading = true;
        DispatcherTimer.RunOnce(() =>
        {
            ConfirmLoadingModal.ConfirmLoading = false;
            ConfirmLoadingModal.Close();
        }, TimeSpan.FromSeconds(2));
    }
}
