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
using System.Windows.Threading;

namespace PlayerRegistrator
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : BasePage<MainPageViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
            //media.Source = new Uri(@"C:\Users\Lucky13\Downloads\Урок 1. Введение в WPF и XAML");
            media.Play();     
        }
        
        public DispatcherTimer timerVideoTime { get; private set; }
        TimeSpan TotalTime;

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            TotalTime = media.NaturalDuration.TimeSpan;

            timerVideoTime = new DispatcherTimer();
            timerVideoTime.Interval = TimeSpan.FromSeconds(1);
            timerVideoTime.Tick += new EventHandler(timer_Tick);
            timerVideoTime.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (media.NaturalDuration.TimeSpan.TotalSeconds > 0)
            {
                if (TotalTime.TotalSeconds > 0)
                {
                    Slider1.Value = media.Position.TotalMilliseconds;
                }
            }
        }
    }
}
