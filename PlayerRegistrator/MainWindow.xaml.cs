using System.Windows;
using PlayerRegistrator.ViewModel;
using System.Windows.Media;
using System.Windows.Shapes;
using System;
using System.Windows.Threading;

namespace PlayerRegistrator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            media.Play();
            Closing += (s, e) => ViewModelLocator.Cleanup();
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