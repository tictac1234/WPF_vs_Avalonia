using Avalonia;
using Avalonia.Media;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using System.Diagnostics;
using System;
using System.Threading;
using System.Windows.Threading;
using Avalonia.Controls.Shapes;
using System.Reflection;

namespace AvaloniaApp.Views;

public partial class MyCanvas : Control
{
    Rect _rect;
    Rect _rectBack;

    Point _rectPoint;
    Point _clickPt;

    Brush _brush;
    Brush _brushBack;

    DispatcherTimer _disTimer;
    Timer _timer;

    bool _isHit = false;


    public MyCanvas()
    {
        InitializeComponent();
        InitializeShapes();

        this.PointerPressed += OnPointerPressed;
        this.PointerReleased += OnPointerReleased;
        this.PointerMoved += OnPointerMoved;
        this.ClipToBounds = true;

        this.Focusable = true;

        this.Width = 640;
        this.Height = 480;
        _rectBack = new Rect(0, 0, Width, Height);
        //InitializeTimer();
    }


    //void OnTimerElapsed(object state)
    //{
    //    InvalidateVisual();
    //}

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }


    void InitializeShapes()
    {
        _rect = new Rect(0, 0, 64, 64);

        _brush = new SolidColorBrush(Colors.Blue);
        _brushBack = new SolidColorBrush(Colors.Gray);
    }


    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _clickPt = e.GetPosition(this);
        _rectPoint = new Point(_rect.X, _rect.Y);

        if (_rect.Contains(_clickPt))
        {
            _isHit = true;
        }
    }

    // After moving the rectangle with the mouse, Redraw this instance.
    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (_isHit)
        {
            var pos = e.GetPosition(this);
            var add = pos - _clickPt;
            _rect = new Rect(_rectPoint.X + add.X, _rectPoint.Y + add.Y, _rect.Width, _rect.Height);

            this.InvalidateVisual();
        }
    }

    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isHit = false;
    }


    public override void Render(DrawingContext context)
    {
        base.Render(context);
        context.DrawRectangle(_brushBack, null, _rectBack);
        context.DrawRectangle(_brush, null, _rect);
    }


}
