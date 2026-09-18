using System.Collections.ObjectModel;
using Cyan.Design.Demo.Models;
using Cyan.Design.Demo.Pages;

namespace Cyan.Design.Demo.ViewModels;

public sealed class NavigationViewModel
{
    public ObservableCollection<NavGroupItem> Groups { get; }

    public NavigationViewModel()
    {
        Groups = new ObservableCollection<NavGroupItem>(BuildGroups());
    }

    private static IEnumerable<NavGroupItem> BuildGroups()
    {
        yield return new NavGroupItem
        {
            GroupName = "Buttons",
            IconKey = "home",
            Items =
            [
                new NavItem { Header = "Button 按钮", IconKey = "home", PageFactory = static () => new ButtonPage() }
            ]
        };

        yield return new NavGroupItem
        {
            GroupName = "Typography",
            IconKey = "pen",
            Items =
            [
                new NavItem { Header = "Typography 文字", IconKey = "pen", PageFactory = static () => new TypographyPage() }
            ]
        };

        yield return new NavGroupItem
        {
            GroupName = "Inputs",
            IconKey = "list",
            Items =
            [
                new NavItem { Header = "Input 输入框", IconKey = "list", PageFactory = static () => new InputPage() },
                new NavItem { Header = "ComboBox 下拉选择", IconKey = "select", PageFactory = static () => new ComboBoxPage() },
                new NavItem { Header = "TimePicker 时间选择", IconKey = "select", PageFactory = static () => new TimePickerPage() },
                new NavItem { Header = "Editable 可编辑文本", IconKey = "list", PageFactory = static () => new EditablePage() }
            ]
        };

        yield return new NavGroupItem
        {
            GroupName = "Selections",
            IconKey = "select",
            Items =
            [
                new NavItem { Header = "选择控件", IconKey = "select", PageFactory = static () => new SelectionPage() },
                new NavItem { Header = "Segmented 分段控制", IconKey = "select", PageFactory = static () => new SegmentedPage() },
                new NavItem { Header = "Switch 开关", IconKey = "check", PageFactory = static () => new SwitchPage() }
            ]
        };

        yield return new NavGroupItem
        {
            GroupName = "Layout",
            IconKey = "folder",
            Items =
            [
                new NavItem { Header = "布局容器", IconKey = "folder", PageFactory = static () => new LayoutPage() },
                new NavItem { Header = "Collapse 折叠面板", IconKey = "list", PageFactory = static () => new CollapsePage() },
                new NavItem { Header = "Tab 标签页", IconKey = "list", PageFactory = static () => new TabPage() }
            ]
        };

        yield return new NavGroupItem
        {
            GroupName = "Dropdowns",
            IconKey = "list",
            Items =
            [
                new NavItem { Header = "Dropdown 下拉菜单", IconKey = "list", PageFactory = static () => new DropdownPage() },
                new NavItem { Header = "Select 选择器", IconKey = "check", PageFactory = static () => new SelectPage() },
                new NavItem { Header = "ShadContextMenu 右键菜单", IconKey = "list", PageFactory = static () => new ShadContextMenuPage() },
                new NavItem { Header = "ShadCommand 命令面板", IconKey = "list", PageFactory = static () => new ShadCommandPage() }
            ]
        };

        yield return new NavGroupItem
        {
            GroupName = "Feedback",
            IconKey = "message",
            Items =
            [
                new NavItem { Header = "Drawer 抽屉", IconKey = "list", PageFactory = static () => new DrawerPage() },
                new NavItem { Header = "Modal 对话框", IconKey = "message", PageFactory = static () => new ModalPage() },
                new NavItem { Header = "HoverCard 悬停卡片", IconKey = "message", PageFactory = static () => new HoverCardPage() },
                new NavItem { Header = "Popconfirm 气泡确认", IconKey = "check", PageFactory = static () => new PopconfirmPage() },
                new NavItem { Header = "Progress 进度条", IconKey = "list", PageFactory = static () => new ProgressPage() },
                new NavItem { Header = "Rate 评分", IconKey = "check", PageFactory = static () => new RatePage() },
                new NavItem { Header = "Slider 滑动输入条", IconKey = "select", PageFactory = static () => new SliderPage() },
                new NavItem { Header = "Spin 加载中", IconKey = "refresh", PageFactory = static () => new SpinPage() },
                new NavItem { Header = "ShadToast 提示", IconKey = "message", PageFactory = static () => new ShadToastPage() },
                new NavItem { Header = "ParkSpinner 加载", IconKey = "refresh", PageFactory = static () => new ParkSpinnerPage() },
                new NavItem { Header = "全局反馈", IconKey = "message", PageFactory = static () => new FeedbackPage() }
            ]
        };

        yield return new NavGroupItem
        {
            GroupName = "Navigation",
            IconKey = "people",
            Items =
            [
                new NavItem { Header = "NavigationView", IconKey = "people", PageFactory = static () => new NavigationPage() },
                new NavItem { Header = "Menubar 菜单栏", IconKey = "list", PageFactory = static () => new MenubarPage() },
                new NavItem { Header = "ParkTabs 标签页", IconKey = "list", PageFactory = static () => new ParkTabsPage() },
                new NavItem { Header = "ParkPagination 分页", IconKey = "list", PageFactory = static () => new ParkPaginationPage() },
                new NavItem { Header = "ShadMessage 消息", IconKey = "message", PageFactory = static () => new ShadMessagePage() }
            ]
        };

        yield return new NavGroupItem
        {
            GroupName = "Display",
            IconKey = "people",
            Items =
            [
                new NavItem { Header = "Avatar 头像", IconKey = "people", PageFactory = static () => new AvatarPage() },
                new NavItem { Header = "Badge 徽标数", IconKey = "message", PageFactory = static () => new BadgePage() },
                new NavItem { Header = "DataGrid 数据表格", IconKey = "list", PageFactory = static () => new DataGridPage() }
            ]
        };

        yield return new NavGroupItem
        {
            GroupName = "Windowing",
            IconKey = "folder",
            Items =
            [
                new NavItem { Header = "AppWindow 应用窗口", IconKey = "folder", PageFactory = static () => new AppWindowPage() }
            ]
        };

        yield return new NavGroupItem
        {
            GroupName = "Icons",
            IconKey = "brush",
            Items =
            [
                new NavItem { Header = "Icon 图标", IconKey = "brush", PageFactory = static () => new IconPage() }
            ]
        };
    }
}
