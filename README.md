# Cyan.Design

参考 **ant.design** 设计语言的 Avalonia UI 控件库，灵感来自 Huskui.Avalonia 与 FluentAvalonia。

## 技术栈

- .NET 8.0
- Avalonia 11.3.0
- C# 12（Nullable 启用、编译绑定启用）

## 项目结构

```
Cyan.Design.slnx
└── src/
    ├── Cyan.Design.Core/           控件逻辑（类库，无样式）
    │   └── Controls/
    │       ├── Buttons/            CyanButton
    │       ├── Typography/         CyanTitle / CyanText / CyanParagraph
    │       ├── Inputs/             CyanInput / CyanTextArea / CyanPassword / CyanSearch / CyanInputNumber / CyanTimeInput
    │       ├── Selections/         CyanCheckbox / CyanRadio / CyanRadioGroup / CyanSwitch / CyanSegmented / CyanSegmentedItem
    │       ├── Layout/             CyanCard / CyanDivider / CyanSpace
    │       ├── Dropdowns/          CyanSelect / CyanDatePicker / CyanDropdown / CyanDropdownButton / CyanDropdownMenu / CyanDropdownItem
    │       ├── Display/            CyanAvatar / CyanAvatarGroup / CyanBadge / CyanCollapse / CyanCollapsePanel
    │       ├── Feedback/           CyanDrawer / CyanModal / CyanPopconfirm / CyanProgress / CyanRate / CyanSlider / CyanSpin / CyanMessage / CyanNotification / CyanShadToast
    │       ├── Navigation/         CyanNavigationView / CyanNavigationViewItem / CyanTabItem
    │       ├── Icons/              CyanIcon / CyanIcons
    │       └── Common/            ControlSize / ControlSizeExtensions / CountTextHelper / DropdownHoverHandler
    ├── Cyan.Design.Themes/         样式资源
    │   ├── AntDesign/
    │   │   ├── Light.axaml         亮色色板 + 设计 token
    │   │   └── Dark.axaml          暗色色板 + 设计 token
    │   ├── Shadcn/
    │   │   ├── Light.axaml         Shadcn 亮色色板
    │   │   └── Dark.axaml          Shadcn 暗色色板
    │   ├── Styles/Controls/*.axaml 各控件样式
    │   ├── Styles/Controls.axaml   控件样式聚合
    │   └── CyanDesignTheme.axaml   主题入口（ThemeDictionaries）
    ├── Cyan.Design.Core.Tests/     单元测试项目（xUnit + Avalonia.Headless）
    │   ├── Controls/               控件 API 测试
    │   └── Themes/                 Token 对称性测试
    └── Cyan.Design.Demo/           桌面演示应用
        ├── Models/                 导航数据模型（NavItem / NavGroupItem）
        ├── ViewModels/             NavigationViewModel（数据驱动导航）
        ├── Controls/               DemoSection 共享布局控件
        └── Pages/                  各控件展示页
```

## 控件清单

### Buttons
| 控件 | 命名空间 | 主要属性 |
|------|----------|----------|
| CyanButton | Core.Controls.Buttons | ButtonType / ButtonSize / Shape / Danger / Block / Loading / Ghost |

### Typography
| 控件 | 命名空间 | 主要属性 |
|------|----------|----------|
| CyanTitle | Core.Controls.Typography | Level(H1-H5) / Mark / Code |
| CyanText | Core.Controls.Typography | Type / Strong / Mark / Code / Italic / Delete / Underline |
| CyanParagraph | Core.Controls.Typography | Type / Strong / Mark |

### Inputs
| 控件 | 命名空间 | 主要属性 |
|------|----------|----------|
| CyanInput | Core.Controls.Inputs | InputSize / InputStatus / InputVariant / AllowClear / Prefix / Suffix / ShowCount |
| CyanTextArea | Core.Controls.Inputs | InputSize / InputStatus / InputVariant / AutoSize / MinRows / MaxRows / ShowCount |
| CyanPassword | Core.Controls.Inputs | 继承 CyanInput / VisibilityToggle / IsPasswordVisible |
| CyanSearch | Core.Controls.Inputs | 继承 CyanInput / Loading / EnterButton / SearchButtonBackground / Searched 事件 |
| CyanInputNumber | Core.Controls.Inputs | Value / Min / Max / Step / Precision / ShowControls / KeyboardNavigation / ValueChanged 事件 |
| CyanTimeInput | Core.Controls.Inputs | Value(TimeSpan?) / HourString / MinuteString / SecondString / ClockIdentifier / UseSeconds |

### Selections
| 控件 | 命名空间 | 主要属性 |
|------|----------|----------|
| CyanCheckbox | Core.Controls.Selections | （继承 CheckBox） |
| CyanRadio | Core.Controls.Selections | （继承 RadioButton） |
| CyanRadioGroup | Core.Controls.Selections | Value / Options / ButtonStyle |
| CyanSwitch | Core.Controls.Selections | （继承 ToggleButton） |
| CyanSegmented | Core.Controls.Selections | Value / Options / Disabled |
| CyanSegmentedItem | Core.Controls.Selections | Label / Value / Icon / Disabled |

### Layout
| 控件 | 命名空间 | 主要属性 |
|------|----------|----------|
| CyanCard | Core.Controls.Layout | Header / Extra / Bordered / Hoverable |
| CyanDivider | Core.Controls.Layout | Orientation / Dashed / Align |
| CyanSpace | Core.Controls.Layout | Direction / SpaceSize |

### Dropdowns
| 控件 | 命名空间 | 主要属性 |
|------|----------|----------|
| CyanSelect | Core.Controls.Dropdowns | InputSize / InputStatus |
| CyanDatePicker | Core.Controls.Dropdowns | InputSize / InputStatus |
| CyanDropdown | Core.Controls.Dropdowns | Overlay / Trigger / Placement / Arrow / Disabled / Open |
| CyanDropdownButton | Core.Controls.Dropdowns | Overlay / Trigger / Placement / ButtonType |
| CyanDropdownMenu | Core.Controls.Dropdowns | Items |
| CyanDropdownItem | Core.Controls.Dropdowns | Icon / Danger / Disabled |

### Display
| 控件 | 命名空间 | 主要属性 |
|------|----------|----------|
| CyanAvatar | Core.Controls.Display | Source / Text / Icon / Shape / Size / Gap / BadgeText / BadgeDot |
| CyanAvatarGroup | Core.Controls.Display | MaxCount |
| CyanBadge | Core.Controls.Display | Count / OverflowCount / ShowZero / Dot / Offset / Size / Status / Text / Color |
| CyanCollapse | Core.Controls.Display | Accordion / Bordered / Size / IconPlacement / Collapsible |
| CyanCollapsePanel | Core.Controls.Display | Header / Active / Disabled / ShowArrow |

### Feedback
| 控件 | 命名空间 | 主要属性 |
|------|----------|----------|
| CyanDrawer | Core.Controls.Feedback | Open / Placement / Width / Height / Closable / MaskClosable |
| CyanModal | Core.Controls.Feedback | Open / Title / Width / Closable / MaskClosable / ConfirmLoading |
| CyanPopconfirm | Core.Controls.Feedback | Open / Title / Description / Icon / Trigger / Placement / ConfirmLoading / ShowCancel |
| CyanProgress | Core.Controls.Feedback | Type / Percent / ShowInfo / Status / StrokeColor / RailColor / Size / StrokeWidth / Steps |
| CyanRate | Core.Controls.Feedback | Count / Value / AllowHalf / Disabled / ReadOnly / Character |
| CyanSlider | Core.Controls.Feedback | Value / Min / Max / Step / Marks / Disabled / Vertical |
| CyanSpin | Core.Controls.Feedback | Spinning / Size / Delay |

### Feedback（静态服务）
| 控件 | 命名空间 | 主要属性 |
|------|----------|----------|
| CyanMessage | Core.Controls.Feedback | 静态 Show(host, text, type) |
| CyanNotification | Core.Controls.Feedback | 静态 Show(host, title, desc, type) |

### Navigation
| 控件 | 命名空间 | 主要属性 |
|------|----------|----------|
| CyanNavigationView | Core.Controls.Navigation | Items / SelectedItem / PaneDisplayMode |
| CyanNavigationViewItem | Core.Controls.Navigation | Header / Icon / PageFactory |
| CyanTabItem | Core.Controls.Navigation | Header / Content / Icon |

### Icons
| 控件 | 命名空间 | 主要属性 |
|------|----------|----------|
| CyanIcon | Core.Controls.Icons | IconKey / Size |
| CyanIcons | Core.Controls.Icons | 图标资源字典 |

## 主题系统

采用 Avalonia `ResourceDictionary.ThemeDictionaries` 机制，定义 Light / Dark 两组资源，运行时通过 `RequestedThemeVariant` 切换自动生效。

资源命名规范：
- 画刷：`ColorXxxBrush`（如 `ColorPrimaryBrush`、`ColorTextBrush`、`ColorBgContainerBrush`）
- 圆角：`BorderRadius` / `BorderRadiusSM` / `BorderRadiusLG`
- 字号：`FontSize` / `FontSizeLG` / `FontSizeH1` …
- 间距：`Spacing` / `SpacingSM` / `SpacingLG`
- 控件高度：`ControlHeight` / `ControlHeightSM` / `ControlHeightLG`

颜色格式统一 `#AARRGGBB`（alpha 前置，与 CSS 的 `#RRGGBBAA` 相反）。

## 使用方法

在应用 `App.axaml` 中接入主题：

```xml
<Application xmlns:fluent="using:Avalonia.Themes.Fluent">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceInclude Source="avares://Cyan.Design.Themes/CyanDesignTheme.axaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
    <Application.Styles>
        <fluent:FluentTheme />
        <StyleInclude Source="avares://Cyan.Design.Themes/Styles/Controls.axaml" />
    </Application.Styles>
</Application>
```

在 XAML 中使用控件：

```xml
<Window xmlns:core="using:Cyan.Design.Core.Controls.Buttons">
    <core:CyanButton Content="Primary" ButtonType="Primary" />
</Window>
```

切换主题：

```csharp
Application.Current.RequestedThemeVariant = ThemeVariant.Dark;
```

## 开发范式（新增控件步骤）

1. **Core**：在 `Controls/<分类>/` 创建控件类，继承 Avalonia 基类，用 `StyledProperty` 暴露 API
2. **Themes**：在 `Styles/Controls/<控件>.axaml` 编写样式（`Selector` + `DynamicResource` + `ControlTemplate`）
3. **聚合**：在 `Controls.axaml` 添加 `StyleInclude`
4. **Demo**：在 `Pages/` 创建展示页，加入 `NavigationViewModel` 导航分组
5. **测试**：在 `Core.Tests/Controls/` 新增测试文件，覆盖默认值与关键行为
6. **构建验证**：`dotnet build Cyan.Design.slnx`

## 工程配置

| 文件 | 作用 |
|------|------|
| `Directory.Build.props` | 公共编译属性（TargetFramework、Nullable、LangVersion 等），子项目继承 |
| `global.json` | 固定 .NET SDK 版本，确保团队构建一致 |
| `.editorconfig` | 代码风格规则，`dotnet format` 自动执行 |
| `nuget.config` | NuGet 包缓存路径配置 |

## 构建与测试

```bash
# 还原依赖
dotnet restore

# 构建全部项目（零警告零错误）
dotnet build Cyan.Design.slnx

# 运行全部测试（xUnit + Avalonia.Headless）
dotnet test src\Cyan.Design.Core.Tests\Cyan.Design.Core.Tests.csproj

# 代码格式化检查（零差异）
dotnet format Cyan.Design.slnx --verify-no-changes

# 自动格式化修复
dotnet format Cyan.Design.slnx
```

## 所需 SDK

- .NET 8.0 SDK（对应 `global.json` 指定版本）
- Avalonia 11.3.0

## 测试项目

测试项目 `Cyan.Design.Core.Tests` 使用：
- **xUnit 2.9** 测试框架
- **Avalonia.Headless + Avalonia.Headless.XUnit** 无头测试环境
- `[AvaloniaFact]` 特性标记需要在 Avalonia 上下文运行的测试

测试覆盖：
- `Themes/TokenSymmetryTests` - 四份色板 Token 键集合对称性校验
- `Controls/*Tests` - 各控件公共 API 默认值、属性变更、键盘可访问性测试

## 运行 Demo

```bash
dotnet run --project src\Cyan.Design.Demo\Cyan.Design.Demo.csproj
```

左侧导航切换各控件展示页，左下角 Light / Dark 按钮切换主题。

## 设计参考

- **ant.design 5.x**：色板、设计 token、组件视觉规范
- **FluentAvalonia**：主题字典机制、NavigationView 形态
- **Huskui.Avalonia**：控件分类与封装方式