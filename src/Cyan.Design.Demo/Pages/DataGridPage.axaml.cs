using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cyan.Design.Demo.Pages;

public partial class DataGridPage : UserControl
{
    public ObservableCollection<UserInfo> Users { get; }
    public ObservableCollection<UserInfo> EditableUsers { get; }

    public DataGridPage()
    {
        Users = new ObservableCollection<UserInfo>
        {
            new("张三", 28, "zhangsan@example.com", "技术部", true),
            new("李四", 32, "lisi@example.com", "产品部", true),
            new("王五", 25, "wangwu@example.com", "设计部", true),
            new("赵六", 35, "zhaoliu@example.com", "技术部", false),
            new("孙七", 29, "sunqi@example.com", "市场部", true),
            new("周八", 41, "zhouba@example.com", "管理部", true),
            new("吴九", 27, "wujiu@example.com", "技术部", true),
            new("郑十", 33, "zhengshi@example.com", "运营部", false)
        };

        EditableUsers = new ObservableCollection<UserInfo>
        {
            new("张三", 28, "zhangsan@example.com", "技术部", true),
            new("李四", 32, "lisi@example.com", "产品部", true),
            new("王五", 25, "wangwu@example.com", "设计部", true),
            new("赵六", 35, "zhaoliu@example.com", "技术部", false)
        };

        InitializeComponent();
        DataContext = this;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}

public sealed class UserInfo
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }
    public bool IsActive { get; set; }

    public UserInfo(string name, int age, string email, string department, bool isActive)
    {
        Name = name;
        Age = age;
        Email = email;
        Department = department;
        IsActive = isActive;
    }
}