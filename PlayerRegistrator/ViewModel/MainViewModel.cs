using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PlayerRegistrator.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using System.Windows.Threading;

namespace PlayerRegistrator
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Members

        private readonly IDataService _dataService;
        private ApplicationPage _currentPage;

        #endregion

        #region Public Properties
        /// <summary>
        /// The currrent page of the application
        /// </summary>
        public ApplicationPage CurrentPage
        {
            get { return _currentPage; }
            set
            {
                Set(ref _currentPage, value);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            CurrentPage = ApplicationPage.Login;
        }

        #endregion
               
    }
}