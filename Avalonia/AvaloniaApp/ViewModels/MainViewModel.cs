using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Forms;

namespace AvaloniaApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public Action action { get; set; }
    public ICommand MyCommand { get; set; }


    public MainViewModel()
    {
        action += ExecuteMyCommand;
        MyCommand = new RelayCommand(action);
    }

    private void ExecuteMyCommand()
    {
        var dlg = new OpenFileDialog();
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            // ファイルを選択した後の処理
            string selectedFilePath = dlg.FileName;
            // 選択されたファイルのパスを利用するなど
        }
        Debug.WriteLine("Buttonがクリックされました！"); 
    }


}
