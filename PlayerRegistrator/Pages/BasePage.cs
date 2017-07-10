using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;

namespace PlayerRegistrator
{
    /// <summary>
    /// Base page for all pages to gain base functionallity
    /// </summary>
    public class BasePage<VM> : Page
        where VM : ViewModelBase, new()
    {

        #region Private Members

        private VM mViewModel;

        #endregion

        #region Public Properties

        /// <summary>
        /// Animation when the page appears
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.FadeIn;
        /// <summary>
        /// Animation when the page disappears
        /// </summary>
        public PageAnimation PageUnLoadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;
        /// <summary>
        /// Duration of animation
        /// </summary>
        public float SlideSeconds { get; set; } = 1.5f;

        public VM ViewModel
        {
            get { return mViewModel; }
            set
            {
                if (mViewModel == value)
                    return;

                mViewModel = value;

                this.ViewModel = mViewModel;
            }
        }

        #endregion

        #region Constructor

        public BasePage()
        {
            if (this.PageLoadAnimation != PageAnimation.None)
                this.Visibility = Visibility.Collapsed;

            this.Loaded += BasePage_Loaded;

            this.DataContext = ServiceLocator.Current.GetInstance<VM>();
        }

        #endregion

        #region Animation Load / Unload

        private async void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await AnimateIn();
        }

        public async Task AnimateIn()
        {
            if (this.PageLoadAnimation == PageAnimation.None)
                return;

            switch (this.PageLoadAnimation)
            {
                case PageAnimation.FadeIn:

                    await this.FadeIn(this.SlideSeconds);
                    
                    break;
            }
        }

        public async Task AnimateOut()
        {
            if (this.PageUnLoadAnimation == PageAnimation.None)
                return;

            switch (this.PageUnLoadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:

                    await this.SlideToLeftAndFadeOut(this.SlideSeconds);

                    break;
            }
        }

        #endregion
    }
}
