using System.Windows;
using Panels.ViewModel;

namespace Panels.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(MainWindowViewModel viewmodel)
            : this()
        {
            DataContext = viewmodel;
        }
    }
}