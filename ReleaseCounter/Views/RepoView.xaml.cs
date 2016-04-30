using System.Windows.Controls;
using System.Windows.Navigation;

namespace ReleaseCounter.Views
{
    /// <summary>
    /// RepoView.xaml 的交互逻辑
    /// </summary>
    public partial class RepoView : UserControl
    {
        public RepoView()
        {
            InitializeComponent();
        }

        private void AccessLink(object s, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
