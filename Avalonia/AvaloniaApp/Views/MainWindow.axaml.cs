using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using System;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System.Diagnostics;

namespace AvaloniaApp.Views;

public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();

        // タイトルバーを非表示にする
        //ExtendClientAreaToDecorationsHint = true;

        // NoChrome を指定してボタンを消す
        //ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.NoChrome;


        this.KeyDown += MainWindow_KeyDown;
        this.AddHandler(PointerPressedEvent, OnMouseDown, handledEventsToo: true);
        this.AddHandler(PointerReleasedEvent, OnMouseUp, handledEventsToo: true);

        //IsHitTestVisible = true;
    }

    private void MainWindow_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        if(e.Key == Key.Escape)
        {
            this.Close();
        }
    }

    protected virtual void OnMouseDownTitleBar(object sender, PointerPressedEventArgs e)
    {
        this.BeginMoveDrag(e);
    }

    protected virtual void OnMouseDown(object sender, PointerPressedEventArgs e)
    {
        var p = e.GetPosition(this);
        //Debug.WriteLine("Mouse Down : " + p.X + " " + p.Y);
    }



    protected virtual void OnMouseUp(object sender, PointerReleasedEventArgs e)
    {
        var p = e.GetPosition(this);
        //Debug.WriteLine("Mouse Up : " + p.X + " " + p.Y);
    }


    //private void InitializeComponent()
    //{
    //    //AvaloniaXamlLoader.Load(this);
    //}
}
