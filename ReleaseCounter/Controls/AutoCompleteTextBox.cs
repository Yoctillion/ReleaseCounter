using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MetroRadiance.UI.Controls;

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
    ///     <MyNamespace:AutoCompleteTextBox/>
    ///
    /// </summary>
    public class AutoCompleteTextBox : PromptTextBox
    {
        static AutoCompleteTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoCompleteTextBox), new FrameworkPropertyMetadata(typeof(AutoCompleteTextBox)));
        }

        //public string Text
        //{
        //    get { return (string)this.GetValue(TextProperty); }
        //    set { this.SetValue(TextProperty, value); }
        //}

        //public static DependencyProperty TextProperty =
        //    DependencyProperty.Register(nameof(Text), typeof(string), typeof(AutoCompleteTextBox), new UIPropertyMetadata("", TextChangedCallBack));

        public IEnumerable<string> TextsSource
        {
            get { return (IEnumerable<string>)this.GetValue(TextsSourceProperty); }
            set { this.SetValue(TextsSourceProperty, value); }
        }

        public static DependencyProperty TextsSourceProperty =
            DependencyProperty.Register(nameof(TextsSource), typeof(IEnumerable<string>), typeof(AutoCompleteTextBox), new UIPropertyMetadata(new string[0], TextChangedCallBack));

        public IEnumerable<string> AvailableTexts
        {
            get { return (IEnumerable<string>)this.GetValue(AvailableTextsProperty); }
            private set { this.SetValue(AvailableTextsProperty, value); }
        }

        public static DependencyProperty AvailableTextsProperty =
            DependencyProperty.Register(nameof(AvailableTexts), typeof(IEnumerable<string>), typeof(AutoCompleteTextBox), new UIPropertyMetadata(new string[0]));

        private static void TextChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (AutoCompleteTextBox)d;
            source.UpdateAvailableTexts();
        }

        private void UpdateAvailableTexts()
        {
            this.AvailableTexts = TextsSource?.Where(t => t.Contains(this.Text ?? ""));
        }

        Popup Popup => this.Template.FindName("PART_Popup", this) as Popup;

        ListBox ItemList => this.Template.FindName("PART_ItemList", this) as ListBox;

        ScrollViewer Host => this.Template.FindName("PART_ContentHost", this) as ScrollViewer;

        UIElement TextBoxView
            => LogicalTreeHelper.GetChildren(Host)
                .Cast<UIElement>()
                .FirstOrDefault();
    }
}
