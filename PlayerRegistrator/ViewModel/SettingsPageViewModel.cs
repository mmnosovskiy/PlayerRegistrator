using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using System.Windows.Threading;
using System.Security;
using System.Threading.Tasks;
using Uniso.InStat.Web;
using Uniso.InStat;
using Microsoft.Win32;
using System.IO;
using Microsoft.Practices.ServiceLocation;

namespace PlayerRegistrator
{

    public class SettingsPageViewModel : ViewModelBase
    {
        #region Private Members

        private string _videoPath;
        private bool _isReady;
        private int _videoId;
        private string _gamePart;
        private RelayCommand _getVideoPath;
        private RelayCommand _nextCommand;

        #endregion

        #region Public Properties

        //public List<string> GameParts { get; set; } = new List<string>() { "Half 1", "Half 2", "Ex half 1", "Ex half 2", "Penalty" };
        public string VideoPath
        {
            get { return _videoPath; }
            set
            {
                Set(ref _videoPath, value);
                RaisePropertyChanged("IsReady");
            }
        }
        public bool IsReady
        {
            get { return !string.IsNullOrWhiteSpace(VideoPath) && !string.IsNullOrWhiteSpace(GamePart); }
            set
            {
                Set(ref _isReady, value);                
            }
        }
        public int VideoId
        {
            get { return _videoId; }
            set
            {
                Set(ref _videoId, value);                
            }
        }
        public string GamePart
        {
            get { return _gamePart; }
            set
            {
                Set(ref _gamePart, value);
                RaisePropertyChanged("IsReady");
            }
        }

        #endregion

        #region Commands

        public RelayCommand GetVideoPath
        {
            get
            {
                return _getVideoPath ??
                    (_getVideoPath = new RelayCommand(() =>
                    {
                        OpenFileDialog fileDialog = new OpenFileDialog();
                        fileDialog.Multiselect = false;
                        fileDialog.Filter = "Видео файлы (*.MKV;*.MP4;*.AVI)|*.MKV;*.MP4;*.AVI";
                        if ((bool)fileDialog.ShowDialog())
                        {
                            VideoPath = fileDialog.FileName;
                        } 
                        
                    }));
            }
        }
        public RelayCommand NextCommand
        {
            get
            {
                return _nextCommand ??
                    (_nextCommand = new RelayCommand(() =>
                    {
                        if (File.Exists(VideoPath))
                        {
                            var mainViewModelInstance = ServiceLocator.Current.GetInstance<MainViewModel>();

                            var mainPageViewModelInstance = ServiceLocator.Current.GetInstance<MainPageViewModel>();
                            mainPageViewModelInstance.VideoSource = new Uri(VideoPath);
                            mainViewModelInstance.CurrentPage = ApplicationPage.Main;
                        }
                        else MessageBox.Show("Файл не найден!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }));
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the SettingsPageViewModel class.
        /// </summary>
        public SettingsPageViewModel()
        {

        }

        #endregion


    }
}