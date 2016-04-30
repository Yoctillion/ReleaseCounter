using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using Livet;
using Octokit;
using ReleaseCounter.Models;
using ReleaseCounter.Views;

namespace ReleaseCounter.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Title

        private string _title;

        public string Title
        {
            get { return this._title; }
            set
            {
                if (this._title != value)
                {
                    this._title = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Width

        private int _width;

        public int Width
        {
            get { return this._width; }
            set
            {
                if (this._width != value)
                {
                    this._width = value;
                    this.AdjustLayout();
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Height

        private int _height;

        public int Height
        {
            get { return this._height; }
            set
            {
                if (this._height != value)
                {
                    this._height = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        private enum LayoutType
        {
            Horizontal,
            Vertical
        }

        #region WindowState

        private WindowState _windowState;

        public WindowState WindowState
        {
            get { return this._windowState; }
            set
            {
                if (this._windowState != value)
                {
                    this._windowState = value;
                    this.AdjustLayout();
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Layout

        private LayoutType _layout;

        private LayoutType Layout
        {
            get { return this._layout; }
            set
            {
                if (this._layout != value)
                {
                    this._layout = value;
                    this.RaisePropertyChanged();

                    this.SwitchView();
                }
            }
        }

        #endregion

        private readonly UserControl _verticalView;
        private readonly UserControl _horizontalView;

        #region CurrentView

        private UserControl _currentView;

        public UserControl CurrentView
        {
            get { return this._currentView; }
            set
            {
                if (this._currentView != value)
                {
                    this._currentView = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion


        #region UserName

        private string _userName;

        public string UserName
        {
            get { return this._userName; }
            set
            {
                if (this._userName != value)
                {
                    this._userName = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Repositories

        private RepositoryData[] _repositories;

        /// <summary>
        /// public repositories
        /// </summary>
        public RepositoryData[] Repositories
        {
            get { return this._repositories; }
            set
            {
                if (this._repositories != value)
                {
                    this._repositories = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region SelectedRepository

        private RepositoryData _selectedRepository;

        public RepositoryData SelectedRepository
        {
            get { return this._selectedRepository; }
            set
            {
                if (this._selectedRepository != value)
                {
                    this._selectedRepository = value;
                    this.RaisePropertyChanged();

                    this.GetReleases();
                }
            }
        }

        #endregion

        #region CurrentRepository

        private RepositoryData _currentRepository;

        public RepositoryData CurrentRepository
        {
            get { return this._currentRepository; }
            set
            {
                if (this._currentRepository != value)
                {
                    this._currentRepository = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion



        private static readonly RepositoryData[] EmtpyRepositories = new RepositoryData[0];

        private readonly GitHubClient _client;

        public MainWindowViewModel()
        {
            this._client = new GitHubClient(new ProductHeaderValue("ReleaseConter"))
            {
                Credentials = new Credentials("34ff0bf25198ae160e5cf7d7bb5eba06879c6fed")
            };


            this._horizontalView = new HorizontalView { DataContext = this };
            this._verticalView = new VerticalView { DataContext = this };

            this.Title = "Release Viewer";
            this.Width = 600;
            this.Height = 400;

            this.SwitchView();
        }

        public async void GetRepos()
        {
            if (string.IsNullOrEmpty(this.UserName))
            {
                this.Repositories = EmtpyRepositories;
            }
            else
            {
                try
                {
                    var repos = await this._client.GetRepositories(this.UserName);
                    this.Repositories = repos.ToArray();
                }
                catch (ApiException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }

        public async void GetReleases()
        {

            if (this.SelectedRepository == null)
            {
                this.CurrentRepository = null;
                return;
            }

            this.CurrentRepository = await this._client.GetRepository(this.SelectedRepository.Author, this.SelectedRepository.Name);
        }

        private void AdjustLayout()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.Layout = LayoutType.Horizontal;
            }
            else if (this.Width >= 600)
            {
                this.Layout = LayoutType.Horizontal;
            }
            else
            {
                this.Layout = LayoutType.Vertical;
            }
        }

        private void SwitchView()
        {
            switch (this.Layout)
            {
                case LayoutType.Horizontal:
                    this.CurrentView = this._horizontalView;
                    break;

                case LayoutType.Vertical:
                    this.CurrentView = this._verticalView;
                    break;
            }
        }
    }
}
