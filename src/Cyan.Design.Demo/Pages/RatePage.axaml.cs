using Avalonia.Controls;
using Avalonia.Interactivity;
using Cyan.Design.Core.Controls.Feedback;

namespace Cyan.Design.Demo.Pages;

public partial class RatePage : UserControl
{
    public RatePage()
    {
        InitializeComponent();
        BasicRate.ValueChanged += (_, _) => BasicValue.Text = BasicRate.Value.ToString();
        HalfRate.ValueChanged += (_, _) => HalfValue.Text = HalfRate.Value.ToString();
    }
}
