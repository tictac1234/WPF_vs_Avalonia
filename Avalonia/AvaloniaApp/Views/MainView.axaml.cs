using Avalonia.Controls;
using Avalonia.Input;
using System.Diagnostics;

namespace AvaloniaApp.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();


        //_titlebar.AddHandler(PointerPressedEvent, OnMouseDown, handledEventsToo: true);
    }

    protected virtual void OnMouseDown(object sender, PointerPressedEventArgs e)
    {
        var p = e.GetPosition(this);

    }


}
