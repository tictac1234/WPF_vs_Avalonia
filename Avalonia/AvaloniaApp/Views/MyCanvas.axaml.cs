using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using System.Diagnostics;
using System;
using System.Threading;
using System.Windows.Threading;
using Avalonia.Controls.Shapes;
using System.Reflection;
using Avalonia.VisualTree;
using Avalonia.Interactivity;

using FenrirEngine;

using Rect = Avalonia.Rect;


namespace AvaloniaApp.Views;

public partial class MyCanvas : Control
{
    private Bitmap? _sprite;       // スプライト画像
    private Point _spritePosition; // スプライトの位置


    Rect _rect;
    Rect _rectBack;

    Point _rectPoint;
    Point _clickPt;

    Brush _brush;
    Brush _brushBack;

    DispatcherTimer _redrawTimer;
    Timer _timer;

    bool _isHit = false;
    private bool _isDraggingSprite; // スプライトがドラッグ中か

    FenrirEngine.Engine _engine;

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

        InitializeSprite();

        
    }


    private void InitializeSprite()
    {
        // スプライト画像を読み込む
        _sprite = new Bitmap("C:/Main/Develop/GitHub/Fenrir/SharpApp/Resources/enemy_01.png");
        _spritePosition = new Point(100, 100); // 初期位置
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        RaiseEvent(e);

        var visual = this.GetVisualRoot() as TopLevel;
        if (visual != null && visual.PlatformImpl != null)
        {
            var platformHandle = visual.TryGetPlatformHandle();
            if (platformHandle.HandleDescriptor == "HWND")
            {
                // Controlのネイティブ親ハンドル (HWND)
                var hwnd = platformHandle.Handle;

                //InitializeEngine(hwnd);
            }
        }
    }


    //private void InitializeEngine(IntPtr handle)
    //{
    //    _engine = Engine.getEngine(handle, (int)this.Width, (int)this.Height, 1);
    //}



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

        // 矩形のクリック判定
        if (_rect.Contains(_clickPt))
        {
            _isHit = true;
            _rectPoint = new Point(_rect.X, _rect.Y);
        }
        else if (_sprite != null)
        {
            // スプライトのクリック判定
            var spriteBounds = new Rect(_spritePosition, _sprite.Size);
            if (spriteBounds.Contains(_clickPt))
            {
                _isDraggingSprite = true;
            }
        }
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (_isHit)
        {
            // 矩形をドラッグ
            var pos = e.GetPosition(this);
            var add = pos - _clickPt;
            _rect = new Rect(_rectPoint.X + add.X, _rectPoint.Y + add.Y, _rect.Width, _rect.Height);

            this.InvalidateVisual();
        }
        else if (_isDraggingSprite)
        {
            // スプライトをドラッグ
            var pos = e.GetPosition(this);
            var delta = pos - _clickPt;
            _spritePosition = new Point(_spritePosition.X + delta.X, _spritePosition.Y + delta.Y);
            _clickPt = pos;

            this.InvalidateVisual();
        }
    }

    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        // ドラッグを終了
        _isHit = false;
        _isDraggingSprite = false;
    }


    public override void Render(DrawingContext context)
    {
        base.Render(context);
        context.DrawRectangle(_brushBack, null, _rectBack);
        context.DrawRectangle(_brush, null, _rect);

        if (_sprite != null)
        {
            for (int i = 0; i < 100; i++)
            {
                // スプライトを描画
                context.DrawImage(_sprite, new Rect(_sprite.Size), new Rect(_spritePosition + new Point(i, i), _sprite.Size));
            }
            
        }
    }


}
