namespace Cyan.Design.Core.Controls.Common;

/// <summary>
/// 抽取 CyanInput/CyanTextArea/CyanSearch 中重复的计数文本构建与清除按钮可见性判断逻辑。
/// </summary>
public static class CountTextHelper
{
    /// <summary>
    /// 构建计数文本。当 maxLength 大于 0 时返回 "len/maxLength" 格式，否则仅返回长度。
    /// </summary>
    /// <param name="text">当前文本。</param>
    /// <param name="maxLength">最大长度，0 表示不限制。</param>
    /// <returns>计数文本字符串。</returns>
    public static string BuildCountText(string? text, int maxLength)
    {
        var len = text?.Length ?? 0;
        return maxLength > 0 ? $"{len}/{maxLength}" : len.ToString();
    }

    /// <summary>
    /// 判断清除按钮是否应显示。
    /// </summary>
    /// <param name="allowClear">是否允许清除。</param>
    /// <param name="text">当前文本。</param>
    /// <param name="isReadOnly">是否只读。</param>
    /// <param name="isEnabled">是否启用。</param>
    /// <returns>清除按钮应显示时返回 true。</returns>
    public static bool ShouldShowClearButton(bool allowClear, string? text, bool isReadOnly, bool isEnabled)
        => allowClear && !string.IsNullOrEmpty(text) && !isReadOnly && isEnabled;
}
