using Avalonia.Controls;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Demo.Pages;

public partial class FeedbackPage : UserControl
{
    public FeedbackPage()
    {
        InitializeComponent();
    }

    private void OnSuccess(object? s, Avalonia.Interactivity.RoutedEventArgs e) => CyanMessage.Show(FeedbackHost, "操作成功", MessageType.Success);
    private void OnInfo(object? s, Avalonia.Interactivity.RoutedEventArgs e) => CyanMessage.Show(FeedbackHost, "这是一条普通消息", MessageType.Info);
    private void OnWarning(object? s, Avalonia.Interactivity.RoutedEventArgs e) => CyanMessage.Show(FeedbackHost, "请注意风险", MessageType.Warning);
    private void OnError(object? s, Avalonia.Interactivity.RoutedEventArgs e) => CyanMessage.Show(FeedbackHost, "操作失败", MessageType.Error);

    private void OnNotiSuccess(object? s, Avalonia.Interactivity.RoutedEventArgs e) => CyanNotification.Show(FeedbackHost, "任务完成", "数据已成功保存到云端。", NotificationType.Success);
    private void OnNotiInfo(object? s, Avalonia.Interactivity.RoutedEventArgs e) => CyanNotification.Show(FeedbackHost, "系统提示", "有新版本可用，建议更新。", NotificationType.Info);
    private void OnNotiWarning(object? s, Avalonia.Interactivity.RoutedEventArgs e) => CyanNotification.Show(FeedbackHost, "存储不足", "剩余空间低于 10%，请及时清理。", NotificationType.Warning);
    private void OnNotiError(object? s, Avalonia.Interactivity.RoutedEventArgs e) => CyanNotification.Show(FeedbackHost, "网络异常", "无法连接到服务器，请检查网络。", NotificationType.Error);
}
