using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReleaseCounter.Models;

namespace ReleaseCounter.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:ReleaseCounter.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:ReleaseCounter.Controls;assembly=ReleaseCounter.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:RepositoryList/>
    ///
    /// </summary>
    public class RepositoryList : Control
    {
        static RepositoryList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RepositoryList), new FrameworkPropertyMetadata(typeof(RepositoryList)));
        }

        public IEnumerable<RepositoryData> Repositories
        {
            get { return (IEnumerable<RepositoryData>)this.GetValue(RepositoriesProperty); }
            set { this.SetValue(RepositoriesProperty, value); }
        }

        public static readonly DependencyProperty RepositoriesProperty =
            DependencyProperty.Register(nameof(Repositories), typeof(IEnumerable<RepositoryData>), typeof(RepositoryList),
                new PropertyMetadata(new RepositoryData[0], AvailableUpdatedCallBack));


        public IEnumerable<RepositoryData> AvailableRepositories
        {
            get { return (IEnumerable<RepositoryData>)this.GetValue(AvailableRepositoriesProperty); }
            private set { this.SetValue(AvailableRepositoriesProperty, value); }
        }

        public static readonly DependencyProperty AvailableRepositoriesProperty =
            DependencyProperty.Register(nameof(AvailableRepositories), typeof(IEnumerable<RepositoryData>), typeof(RepositoryList),
                new PropertyMetadata(new RepositoryData[0]));


        public RepositoryData Selected
        {
            get { return (RepositoryData)this.GetValue(SelectedProperty); }
            set { this.SetValue(SelectedProperty, value); }
        }

        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.Register(nameof(Selected), typeof(RepositoryData), typeof(RepositoryList), new PropertyMetadata(null));

        public string Filter
        {
            get { return (string)this.GetValue(FilterProperty); }
            set { this.SetValue(FilterProperty, value); }
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register(nameof(Filter), typeof(string), typeof(RepositoryList),
                new PropertyMetadata("", AvailableUpdatedCallBack));


        private static void AvailableUpdatedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (RepositoryList)d;
            source.UpdateAvailable();
        }

        private void UpdateAvailable()
        {
            this.AvailableRepositories = this.Repositories?
                .Where(r => r.Name.IndexOf(this.Filter?.Trim() ?? "", StringComparison.OrdinalIgnoreCase) >= 0)
                .ToArray();
        }
    }
}
