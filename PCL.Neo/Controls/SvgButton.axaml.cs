using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace PCL.Neo.Controls;

/// <summary>
/// 自定义按钮控件，包含SVG图片和文字显示，支持多种状态样式
/// </summary>
public partial class SvgButton : Button
{
    #region 依赖属性定义

    /// <summary>
    /// 定义SvgContent依赖属性
    /// </summary>
    public static readonly StyledProperty<string> SvgContentProperty = AvaloniaProperty.Register<SvgButton, string>(
        nameof(SvgContent), string.Empty);

    /// <summary>
    /// SVG内容
    /// </summary>
    public string SvgContent
    {
        get => GetValue(SvgContentProperty);
        set => SetValue(SvgContentProperty, value);
    }

    /// <summary>
    /// 定义ButtonText依赖属性
    /// </summary>
    public static readonly StyledProperty<string> ButtonTextProperty = AvaloniaProperty.Register<SvgButton, string>(
        nameof(ButtonText), "按钮");

    /// <summary>
    /// 按钮文字
    /// </summary>
    public string ButtonText
    {
        get => GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }

    /// <summary>
    /// 定义NormalBackgroundColor依赖属性
    /// </summary>
    public static readonly StyledProperty<Color> NormalBackgroundColorProperty =
        AvaloniaProperty.Register<SvgButton, Color>(
            nameof(NormalBackgroundColor), Color.Parse("#D9D9D9"));

    /// <summary>
    /// 正常状态背景色
    /// </summary>
    public Color NormalBackgroundColor
    {
        get => GetValue(NormalBackgroundColorProperty);
        set => SetValue(NormalBackgroundColorProperty, value);
    }

    /// <summary>
    /// 定义HoverBackgroundColor依赖属性
    /// </summary>
    public static readonly StyledProperty<Color> HoverBackgroundColorProperty =
        AvaloniaProperty.Register<SvgButton, Color>(
            nameof(HoverBackgroundColor), Color.Parse("#C9C9C9"));

    /// <summary>
    /// 悬停状态背景色
    /// </summary>
    public Color HoverBackgroundColor
    {
        get => GetValue(HoverBackgroundColorProperty);
        set => SetValue(HoverBackgroundColorProperty, value);
    }

    /// <summary>
    /// 定义PressedBackgroundColor依赖属性
    /// </summary>
    public static readonly StyledProperty<Color> PressedBackgroundColorProperty =
        AvaloniaProperty.Register<SvgButton, Color>(
            nameof(PressedBackgroundColor), Color.Parse("#B9B9B9"));

    /// <summary>
    /// 按下状态背景色
    /// </summary>
    public Color PressedBackgroundColor
    {
        get => GetValue(PressedBackgroundColorProperty);
        set => SetValue(PressedBackgroundColorProperty, value);
    }

    /// <summary>
    /// 定义DisabledBackgroundColor依赖属性
    /// </summary>
    public static readonly StyledProperty<Color> DisabledBackgroundColorProperty =
        AvaloniaProperty.Register<SvgButton, Color>(
            nameof(DisabledBackgroundColor), Color.Parse("#E9E9E9"));

    /// <summary>
    /// 禁用状态背景色
    /// </summary>
    public Color DisabledBackgroundColor
    {
        get => GetValue(DisabledBackgroundColorProperty);
        set => SetValue(DisabledBackgroundColorProperty, value);
    }

    /// <summary>
    /// 定义SvgBackgroundColor依赖属性
    /// </summary>
    public static readonly StyledProperty<Color> SvgBackgroundColorProperty =
        AvaloniaProperty.Register<SvgButton, Color>(
            nameof(SvgBackgroundColor), Color.Parse("#464646"));

    /// <summary>
    /// SVG背景色
    /// </summary>
    public Color SvgBackgroundColor
    {
        get => GetValue(SvgBackgroundColorProperty);
        set => SetValue(SvgBackgroundColorProperty, value);
    }

    /// <summary>
    /// 定义TextColor依赖属性
    /// </summary>
    public static readonly StyledProperty<Color> TextColorProperty = AvaloniaProperty.Register<SvgButton, Color>(
        nameof(TextColor), Colors.Black);

    /// <summary>
    /// 文字颜色
    /// </summary>
    public Color TextColor
    {
        get => GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// 定义TextSize依赖属性
    /// </summary>
    public static readonly StyledProperty<double> TextSizeProperty = AvaloniaProperty.Register<SvgButton, double>(
        nameof(TextSize), 25);

    /// <summary>
    /// 文字大小
    /// </summary>
    public double TextSize
    {
        get => GetValue(TextSizeProperty);
        set => SetValue(TextSizeProperty, value);
    }

    #endregion

    /// <summary>
    /// 初始化CustomButton类的新实例
    /// </summary>
    public SvgButton()
    {
        InitializeComponent();

        // 绑定依赖属性到UI元素
        this.GetObservable(SvgContentProperty).Subscribe(UpdateSvgContent);
        this.GetObservable(ButtonTextProperty).Subscribe(UpdateTextContent);
        this.GetObservable(TextColorProperty).Subscribe(UpdateTextColor);
        this.GetObservable(TextSizeProperty).Subscribe(UpdateTextSize);
        this.GetObservable(SvgBackgroundColorProperty).Subscribe(UpdateSvgBackgroundColor);
        this.GetObservable(NormalBackgroundColorProperty).Subscribe(_ => UpdateBackground());
        this.GetObservable(HoverBackgroundColorProperty).Subscribe(_ => UpdateBackground());
        this.GetObservable(PressedBackgroundColorProperty).Subscribe(_ => UpdateBackground());
        this.GetObservable(DisabledBackgroundColorProperty).Subscribe(_ => UpdateBackground());

        // 初始化状态
        UpdateBackground();
    }

    /// <summary>
    /// 初始化组件
    /// </summary>
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// 更新SVG内容
    /// </summary>
    /// <param name="content">SVG内容</param>
    private void UpdateSvgContent(string content)
    {
        var svgPresenter = this.FindControl<TextBlock>("SvgContentPresenter");
        svgPresenter?.Text = content;
    }

    /// <summary>
    /// 更新文字内容
    /// </summary>
    /// <param name="text">文字内容</param>
    private void UpdateTextContent(string text)
    {
        var textBlock = this.FindControl<TextBlock>("TextContent");
        textBlock?.Text = text;
    }

    /// <summary>
    /// 更新文字颜色
    /// </summary>
    /// <param name="color">文字颜色</param>
    private void UpdateTextColor(Color color)
    {
        var textBlock = this.FindControl<TextBlock>("TextContent");
        textBlock?.Foreground = new SolidColorBrush(color);
    }

    /// <summary>
    /// 更新文字大小
    /// </summary>
    /// <param name="size">文字大小</param>
    private void UpdateTextSize(double size)
    {
        var textBlock = this.FindControl<TextBlock>("TextContent");
        textBlock?.FontSize = size;
    }

    /// <summary>
    /// 更新SVG背景色
    /// </summary>
    /// <param name="color">SVG背景色</param>
    private void UpdateSvgBackgroundColor(Color color)
    {
        var svgContainer = this.FindControl<Border>("SvgContainer");
        svgContainer?.Background = new SolidColorBrush(color);
    }

    /// <summary>
    /// 更新背景色
    /// </summary>
    private void UpdateBackground()
    {
        if (!IsEnabled)
        {
            Background = new SolidColorBrush(DisabledBackgroundColor);
        }
        else if (IsPressed)
        {
            Background = new SolidColorBrush(PressedBackgroundColor);
        }
        else if (IsPointerOver)
        {
            Background = new SolidColorBrush(HoverBackgroundColor);
        }
        else
        {
            Background = new SolidColorBrush(NormalBackgroundColor);
        }
    }

    /// <summary>
    /// 处理指针进入事件
    /// </summary>
    /// <param name="e">指针事件参数</param>
    protected override void OnPointerEntered(Avalonia.Input.PointerEventArgs e)
    {
        base.OnPointerEntered(e);
        UpdateBackground();
    }

    /// <summary>
    /// 处理指针离开事件
    /// </summary>
    /// <param name="e">指针事件参数</param>
    protected override void OnPointerExited(Avalonia.Input.PointerEventArgs e)
    {
        base.OnPointerExited(e);
        UpdateBackground();
    }

    /// <summary>
    /// 处理指针按下事件
    /// </summary>
    /// <param name="e">指针事件参数</param>
    protected override void OnPointerPressed(Avalonia.Input.PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        UpdateBackground();
    }

    /// <summary>
    /// 处理指针释放事件
    /// </summary>
    /// <param name="e">指针事件参数</param>
    protected override void OnPointerReleased(Avalonia.Input.PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        UpdateBackground();
    }

    /// <summary>
    /// 处理启用状态变化事件
    /// </summary>
    /// <param name="change">路由事件参数</param>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == IsEnabledProperty)
        {
            UpdateBackground();

            var textBlock = this.FindControl<TextBlock>("TextContent");
            textBlock?.Opacity = IsEnabled ? 1 : 0.5;

            var svgContainer = this.FindControl<Border>("SvgContainer");
            svgContainer?.Opacity = IsEnabled ? 1 : 0.5;
        }
    }
}
