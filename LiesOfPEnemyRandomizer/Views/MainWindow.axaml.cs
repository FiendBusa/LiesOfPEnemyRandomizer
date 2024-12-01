using Avalonia.Controls;
using Avalonia.Interactivity;
using LiesOfPEnemyRandomizer.ViewModels;
using System.ComponentModel;




namespace LiesOfPEnemyRandomizer.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            _viewModel = (MainWindowViewModel)DataContext;

        }

        private void Binding(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        }
    }

        
}



    