﻿using System;
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
using ReleaseCounter.ViewModels;

namespace ReleaseCounter.Views
{
    /// <summary>
    /// HorizontalView.xaml 的交互逻辑
    /// </summary>
    public partial class HorizontalView : UserControl
    {
        private MainWindowViewModel Vm => (MainWindowViewModel) this.DataContext;

        public HorizontalView()
        {
            InitializeComponent();
        }
        

        private void GetReleases(object sender, SelectionChangedEventArgs e)
        {
            this.Vm?.GetReleases();
        }
    }
}
