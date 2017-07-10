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
using GalaSoft.MvvmLight.Ioc;

namespace PlayerRegistrator
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class LoginPageViewModel : ViewModelBase
    {
        #region Private Members
        
        private List<User> _userList;
        private User _currentUser;
        private bool _isLoggedIn;
        private bool _isLoggingIn;
        private bool _isLoginFailed;
        private string _log;
        private RelayCommand<IHavePassword> _loginCommand;
        private RelayCommand _hideAlertCommand;

        #endregion

        #region Public Properties

        public List<User> UserList
        {
            get
            {
                return _userList;
            }

            set
            {
                Set(ref _userList, value);
            }
        }
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }

            set
            {
                Set(ref _currentUser, value);
            }
        }
        public bool IsLoggedIn
        {
            get
            {
                return _isLoggedIn;
            }

            set
            {
                Set(ref _isLoggedIn, value);
            }
        }
        public bool IsLoggingIn
        {
            get
            {
                return _isLoggingIn;
            }

            set
            {
                Set(ref _isLoggingIn, value);
            }
        }
        public string Log
        {
            get
            {
                return _log;
            }

            set
            {
                Set(ref _log, value);
            }
        }
        public bool IsLoginFailed
        {
            get
            {
                return _isLoginFailed;
            }

            set
            {
                Set(ref _isLoginFailed, value);
            }
        }

        #endregion

        #region Commands

        public RelayCommand<IHavePassword> LoginCommand
        {
            get
            {
                return _loginCommand ??
                    (_loginCommand = new RelayCommand<IHavePassword>(async (obj) =>
                    {
                        await Login(obj);
                    }));
            }
        }
        private async Task Login(IHavePassword obj)
        {
            if (IsLoggingIn)
                return;
            Log = string.Empty;
            try
            {
                IsLoggingIn = true;
                IsLoginFailed = false;

                Log += "Идет проверка введенных данных...\n";

                await Task.Run(() =>
                {
                    IsLoggedIn = MsSqlService.Login(CurrentUser, obj.SecurePassword.Unsecure());
                    if (IsLoggedIn)
                    {
                        Log += "Вход выполнен успешно!\n";
                    }
                    else
                    {
                        Log += "Неверный пароль!\n";
                        IsLoginFailed = true;
                    }
                });
            }
            catch (Exception e)
            {
                Log += e.Message + "\n";
                IsLoginFailed = true;
            }
            finally
            {
                IsLoggingIn = false;
                if (IsLoggedIn)
                {
                    var mainViewModelInstance = SimpleIoc.Default.GetInstance<MainViewModel>();
                    mainViewModelInstance.CurrentPage = ApplicationPage.Settings;
                }
            }
        }
        public RelayCommand HideAlertCommand
        {
            get
            {
                return _hideAlertCommand ??
                    (_hideAlertCommand = new RelayCommand(() =>
                    {
                        IsLoginFailed = false;
                    }));
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the LoginPageViewModel class.
        /// </summary>
        public LoginPageViewModel()
        {
            UserList = MsSqlService.GetUserList();
            Log = string.Empty;   
        }

        #endregion


    }
}