using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using PCL2.Neo.Helpers;
using PCL2.Neo.Models;
using PCL2.Neo.Utils;

namespace PCL2.Neo.Controls;

public class MyRadioButton : TemplatedControl
{
    private Path? _shapeLogo;
    private TextBlock? _labText;
    
    private bool _isMouseDown = false;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _shapeLogo = e.NameScope.Find<Path>("ShapeLogo")!;
        _labText = e.NameScope.Find<TextBlock>("LabText")!;
        
        this.PointerEntered += (_, _) => RefreshColor();
        this.PointerExited += (_, _) => RefreshColor();
        this.Loaded += (_, _) => RefreshColor();
        
        _shapeLogo.Data = Geometry.Parse(Logo);
        _shapeLogo.RenderTransform = new ScaleTransform { ScaleX = LogoScale, ScaleY = LogoScale };
        _labText.Text = Text;
    }

    public int Uuid = CoreUtils.GetUuid();
    
    public static readonly StyledProperty<string> LogoProperty = AvaloniaProperty.Register<MyRadioButton, string>(
        nameof(Logo));

    public string Logo
    {
        get => GetValue(LogoProperty);
        set
        {
            SetValue(LogoProperty, value);
            if (_shapeLogo != null)
            {
                _shapeLogo.Data = Geometry.Parse(value);
            }
        }
    }

    public static readonly StyledProperty<double> LogoScaleProperty = AvaloniaProperty.Register<MyRadioButton, double>(
        nameof(LogoScale),
        1);

    public double LogoScale
    {
        get => GetValue(LogoScaleProperty);
        set
        {
            SetValue(LogoScaleProperty, value);
            if (_shapeLogo != null)
            {
                _shapeLogo.RenderTransform = new ScaleTransform { ScaleX = value, ScaleY = value };
            }
        }
    }

    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<MyRadioButton, string>(
        nameof(Text),
        string.Empty);

    public string Text
    {
        get => GetValue(TextProperty);
        set
        {
            SetValue(TextProperty, value);
            if (_labText != null)
            {
                _labText.Text = value;
            }
        }
    }

    public enum ColorState
    {
        White,
        HighLight
    }
    
    public static readonly StyledProperty<ColorState> ColorTypeProperty = 
        AvaloniaProperty.Register<MyRadioButton, ColorState>(nameof(ColorType));

    public ColorState ColorType
    {
        get => GetValue(ColorTypeProperty);
        set
        {
            SetValue(ColorTypeProperty, value);
            RefreshColor();
        }
    }

    public static readonly StyledProperty<bool> CheckedProperty = AvaloniaProperty.Register<MyRadioButton, bool>(
        nameof(Checked));

    public bool Checked
    {
        get => GetValue(CheckedProperty);
        set => SetValue(CheckedProperty, value);
    }
    
    private void RefreshColor()
    {
        if (_shapeLogo is null || _labText is null) return;
        if (IsLoaded)
        {
            switch (ColorType)
            {
                case ColorState.White:
                    if (Checked)
                    {
                        _shapeLogo.Fill = (IBrush?)Application.Current!.Resources["ColorBrush3"];
                        _labText.Foreground = (IBrush?)Application.Current!.Resources["ColorBrush3"] ;
                        this.Background = (SolidColorBrush)new MyColor(255, 255, 255);
                    }
                    else if (_isMouseDown)
                    {
                        this.Background = (SolidColorBrush)new MyColor(120, ThemeHelper.Color8);
                    }
                    else if (IsPointerOver)
                    {
                        _shapeLogo.Fill = (SolidColorBrush)new MyColor(255, 255, 255);
                        _labText.Foreground = (SolidColorBrush)new MyColor(255, 255, 255);
                        this.Background = (SolidColorBrush)new MyColor(50, ThemeHelper.Color8);
                    }
                    else
                    {
                        _shapeLogo.Fill = (SolidColorBrush)new MyColor(255, 255, 255);
                        _labText.Foreground = (SolidColorBrush)new MyColor(255, 255, 255);
                        this.Background = (SolidColorBrush)ThemeHelper.ColorSemiTransparent;
                    }
                    break;
                case ColorState.HighLight:
                    if (Checked)
                    {
                        _shapeLogo.Fill = (SolidColorBrush)new MyColor(255, 255, 255);
                        _labText.Foreground = (SolidColorBrush)new MyColor(255, 255, 255);
                        this.Background = (IBrush?)Application.Current!.Resources["ColorBrush3"];
                    }
                    else if (_isMouseDown)
                    {
                        this.Background = (IBrush?)Application.Current!.Resources["ColorBrush6"];
                    }
                    else if (IsPointerOver)
                    {
                        _shapeLogo.Fill = (IBrush?)Application.Current!.Resources["ColorBrush3"];
                        _labText.Foreground = (IBrush?)Application.Current!.Resources["ColorBrush3"];
                        this.Background = (IBrush?)Application.Current!.Resources["ColorBrush7"];
                    }
                    else
                    {
                        _shapeLogo.Fill = (IBrush?)Application.Current!.Resources["ColorBrush3"];
                        _labText.Foreground = (IBrush?)Application.Current!.Resources["ColorBrush3"];
                        this.Background = (SolidColorBrush)ThemeHelper.ColorSemiTransparent;
                    }
                    break;
            }
        }
        else
        {
            switch (ColorType)
            {
                case ColorState.White:
                    if (Checked)
                    {
                        this.Background = (SolidColorBrush)new MyColor(255, 255, 255);
                        _shapeLogo.Fill = (IBrush?)Application.Current!.Resources["ColorBrush3"];
                        _labText.Foreground = (IBrush?)Application.Current!.Resources["ColorBrush3"];
                    }
                    else
                    {
                        this.Background = (SolidColorBrush)ThemeHelper.ColorSemiTransparent;
                        _shapeLogo.Fill = (SolidColorBrush)new MyColor(255, 255, 255);
                        _labText.Foreground = (SolidColorBrush)new MyColor(255, 255, 255);
                    }
                    break;
                case ColorState.HighLight:
                    if (Checked)
                    {
                        this.Background = (IBrush?)Application.Current!.Resources["ColorBrush3"];
                        _shapeLogo.Fill = (SolidColorBrush)new MyColor(255, 255, 255);
                        _labText.Foreground = (SolidColorBrush)new MyColor(255, 255, 255);
                    }
                    else
                    {
                        this.Background = (SolidColorBrush)ThemeHelper.ColorSemiTransparent;
                        _shapeLogo.Fill = (IBrush?)Application.Current!.Resources["ColorBrush3"];
                        _labText.Foreground = (IBrush?)Application.Current!.Resources["ColorBrush3"];
                    }
                    break;
            }
        }
    }
}