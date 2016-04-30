using System.Windows;
using System.Windows.Controls;
using MetroRadiance.UI;
using MetroRadiance.UI.Controls;
using ReleaseCounter.ViewModels;

namespace ReleaseCounter.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly MainWindowViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            this._vm = new MainWindowViewModel();
            this.DataContext = this._vm;
        }
    }
}
