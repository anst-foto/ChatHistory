using Avalonia.Controls;
using Avalonia.Interactivity;
using ChatHistory.Core.Models;
using ChatHistory.UI.ViewModels;

namespace ChatHistory.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ButtonUserInfo_OnClick(object? sender, RoutedEventArgs e)
    {
        (this.DataContext as MainWindowViewModel)?.CommandUserInfo.Execute(); //FIXME
    }
}
