using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PlayerRegistrator.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PlayerRegistrator.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;



        
        private double _duration;
        public double Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                Set(ref _duration, value);
            }
        }
        private string _videoSource;
        public string VideoSource
        {
            get
            {
                return _videoSource;
            }
            set
            {
                Set(ref _videoSource, value);
            }
        }
        private Match _game;
        public Match Game
        {
            get
            {
                return _game;
            }
            set
            {
                Set(ref _game, value);
            }
        }
        private SolidColorBrush _brush;
        public SolidColorBrush Brush
        {
            get
            {
                return _brush;
            }
            set
            {
                Set(ref _brush, value);
            }
        }
        private int[][] _tactics1;
        public int[][] Tactics1
        {
            get
            {
                return _tactics1;
            }
            set
            {
                Set(ref _tactics1, value);
            }
        }
        private int[][] _tactics2;
        public int[][] Tactics2
        {
            get
            {
                return _tactics2;
            }
            set
            {
                Set(ref _tactics2, value);
            }
        }
        private Color _color1;
        public Color Color1
        {
            get
            {
                return _color1;
            }
            set
            {
                Set(ref _color1, value);
            }
        }




        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;

            //_dataService.GetData(
            //    (item, error) =>
            //    {
            //        if (error != null)
            //        {
            //            // Report error here
            //            return;
            //        }

            //        CurrentTime = 0;
            //    });
            Game = new Match()
            {
                Half = 1,
                TimeVideo = 10,
                Col = Colors.Red,
                Team1 = new Team() { NumberColor = Colors.Red, ShirtColor = Colors.DarkRed, Tactics = new List<Tactics>() },
                Team2 = new Team() { NumberColor = Colors.DarkOrchid, ShirtColor = Colors.BlueViolet }
            };
            Color1 = Colors.PowderBlue;
            //Tactics1 = GetPositions(Game.GetCurrent(Game.Team1));
            //Tactics2 = GetPositions(Game.GetCurrent(Game.Team2));
        }
        private int[][] GetPositions(Tactics tact)
        {
            int[][] res = new int[5][];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new int[6];
            }
            foreach (Place item in tact)
            {
                res[item.Position][item.Amplua] = item.Player.Number;
            }
            return res;
        }

        private RelayCommand<Ellipse> _recolor;
        public RelayCommand<Ellipse> Recolor
        {
            get
            {
                return _recolor ??
                    (_recolor = new RelayCommand<Ellipse>(obj =>
                    {
                        obj.Fill = new SolidColorBrush(Color.Subtract(((SolidColorBrush)obj.Fill).Color, Colors.Black));
                    }));
            }
        }
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}