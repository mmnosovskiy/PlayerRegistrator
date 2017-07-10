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

namespace PlayerRegistrator
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Private Fields
        
        private bool IsPlaying { get; set; }
        private int _duration;
        private Uri _videoSource;
        private Match _game;
        private int[][] _tactics1;
        private int[][] _tactics2;
        private RelayCommand<MediaElement> _getDuration;
        private RelayCommand<Grid> _sliderValueChanged;

        #endregion

        #region Commands

        public RelayCommand<MediaElement> PlayPauseCommand { get; set; }
        public RelayCommand<MediaElement> ForwardCommand { get; set; }
        public RelayCommand<MediaElement> BackwardCommand { get; set; }
        public RelayCommand<Grid> HideTacticsCommand { get; set; }
        public RelayCommand<Button> ReColor1 { get; set; }
        public RelayCommand<Button> ReColor2 { get; set; }
        public RelayCommand<MediaElement> GetDuration
        {
            get
            {
                return _getDuration ??
                    (_getDuration = new RelayCommand<MediaElement>(obj =>
                    {
                        MediaElement media = obj as MediaElement;
                        Duration = (int)media.NaturalDuration.TimeSpan.TotalMilliseconds;


                    }));
            }
        }
        public RelayCommand<Grid> SliderValueChanged
        {
            get
            {
                return _sliderValueChanged ??
                    (_sliderValueChanged = new RelayCommand<Grid>(obj =>
                    {

                        Grid grid1 = obj.Children[0] as Grid;
                        Grid grid2 = obj.Children[1] as Grid;
                        bool[][] notAv1;
                        bool[][] notAv2;
                        try
                        {
                            int[][] pos1 = GetPositions(Game.GetCurrent(Game.Team1), out notAv1);
                            Tactics1 = pos1;

                            ColoredPlayers(grid1, 1);


                            if (notAv1[2][0]) (grid1.Children[0] as Button).IsEnabled = false;

                            int k = 1;
                            for (int j = 1; j < 6; j++)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    if (pos1[i][j] != 0)
                                    {
                                        if (notAv1[i][j])
                                        {
                                            (grid1.Children[k] as Button).IsEnabled = false;
                                            (grid1.Children[k] as Button).Background = new SolidColorBrush(Colors.LightGray);
                                        }
                                        if (List1.Contains(pos1[i][j])) (grid1.Children[k] as Button).Background = new SolidColorBrush(ReverseColor(Game.Team1.ShirtColor));

                                    }
                                    else (grid1.Children[k] as Button).Visibility = Visibility.Hidden;
                                    k++;
                                }
                            }
                        }
                        catch
                        {

                        }
                        try
                        {
                            int[][] pos2 = GetPositions(Game.GetCurrent(Game.Team2), out notAv2);
                            Tactics2 = pos2;
                            ColoredPlayers(grid2, 2);
                            int k = 1;
                            int length = grid2.Children.Count;
                            if (notAv2[2][0]) (grid2.Children[length - k] as Button).IsEnabled = false;
                            k++;
                            for (int j = 1; j < 6; j++)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    if (pos2[i][j] != 0)
                                    {
                                        if (notAv2[i][j])
                                        {
                                            (grid2.Children[length - k] as Button).IsEnabled = false;
                                            (grid2.Children[length - k] as Button).Background = new SolidColorBrush(Colors.LightGray);
                                        }
                                        if (List2.Contains(pos2[i][j])) (grid2.Children[length - k] as Button).Background = new SolidColorBrush(ReverseColor(Game.Team2.ShirtColor));

                                    }
                                    else (grid2.Children[length - k] as Button).Visibility = Visibility.Hidden;
                                    k++;
                                }
                            }
                        }
                        catch
                        {

                        }
                    }));
            }
        }

        #endregion

        #region Public Properties

        public TimeSpan GameTimeSpan
        {
            get { return TimeSpan.FromMilliseconds(Game.TimeVideo); }
            set
            {
                Game.TimeVideo = Convert.ToInt32(value.TotalMilliseconds);
                RaisePropertyChanged("GameTime");
                RaisePropertyChanged("GameTimeSpan");
            }
        }
        public int GameTime
        {
            get { return Game.TimeVideo; }
            set
            {
                Game.TimeVideo = value;
                RaisePropertyChanged("GameTime");
                RaisePropertyChanged("GameTimeSpan");
            }
        }
        public int Duration
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
        public Uri VideoSource
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
        public List<int> List1 { get; set; }
        public List<int> List2 { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainPageViewModel class.
        /// </summary>
        public MainPageViewModel()
        {

            //_dataService.GetData(
            //    (item, error) =>
            //    {
            //        if (error != null)
            //        {
            //            // Report error here
            //            return;
            //        }
            //    });
            VideoSource = new Uri(@"C:\Users\Lucky13\Downloads\Загрузки uTorrent\Фильмы и сериалы\Gladiator.2000.Theatrical.Cut_[scarabey.org]");
            List1 = new List<int>();
            List2 = new List<int>();
            Game = new Match()
            {
                Half = 1,
                TimeVideo = 0,
                Team1 = new Team() { NumberColor = Colors.White, ShirtColor = Colors.DarkRed, Tactics = new List<Tactics>() },
                Team2 = new Team() { NumberColor = Colors.White, ShirtColor = Colors.Purple, Tactics = new List<Tactics>() },
                DisabledPlayers = new List<Player>() { new Player() { Name = "PL2", Number = 2 } }
            };
            IsPlaying = true;
            List<Place> p0 = new List<Place>()
            {
                new Place() { Amplua = 0, Position = 2, Player = new Player() { Name = "PL1", Number = 1 } },
                new Place() { Amplua = 1, Position = 0, Player = new Player() { Name = "PL2", Number = 2 } },
                new Place() { Amplua = 1, Position = 2, Player = new Player() { Name = "PL3", Number = 3 } },
                new Place() { Amplua = 1, Position = 3, Player = new Player() { Name = "PL4", Number = 4 } },
                new Place() { Amplua = 1, Position = 4, Player = new Player() { Name = "PL5", Number = 5 } },
                new Place() { Amplua = 3, Position = 1, Player = new Player() { Name = "PL6", Number = 6 } },
                new Place() { Amplua = 2, Position = 2, Player = new Player() { Name = "PL7", Number = 7 } },
                new Place() { Amplua = 3, Position = 3, Player = new Player() { Name = "PL8", Number = 8 } },
                new Place() { Amplua = 4, Position = 1, Player = new Player() { Name = "PL9", Number = 9 } },
                new Place() { Amplua = 5, Position = 2, Player = new Player() { Name = "PL10", Number = 10 } },
                new Place() { Amplua = 4, Position = 3, Player = new Player() { Name = "PL11", Number = 11 } },
            };
            List<Place> p1 = new List<Place>()
            {
                new Place() { Amplua = 0, Position = 2, Player = new Player() { Name = "PL1", Number = 1 } },
                new Place() { Amplua = 1, Position = 0, Player = new Player() { Name = "PL2", Number = 2 } },
                new Place() { Amplua = 1, Position = 1, Player = new Player() { Name = "PL3", Number = 3 } },
                new Place() { Amplua = 1, Position = 2, Player = new Player() { Name = "PL4", Number = 4 } },
                new Place() { Amplua = 1, Position = 3, Player = new Player() { Name = "PL5", Number = 5 } },
                new Place() { Amplua = 2, Position = 1, Player = new Player() { Name = "PL6", Number = 6 } },
                new Place() { Amplua = 2, Position = 2, Player = new Player() { Name = "PL7", Number = 7 } },
                new Place() { Amplua = 2, Position = 3, Player = new Player() { Name = "PL8", Number = 8 } },
                new Place() { Amplua = 3, Position = 1, Player = new Player() { Name = "PL9", Number = 9 } },
                new Place() { Amplua = 3, Position = 2, Player = new Player() { Name = "PL10", Number = 10 } },
                new Place() { Amplua = 3, Position = 3, Player = new Player() { Name = "PL11", Number = 11 } },
            };
            List<Place> p2 = new List<Place>()
            {
                new Place() { Amplua = 0, Position = 2, Player = new Player() { Name = "PL1", Number = 1 } },
                new Place() { Amplua = 1, Position = 0, Player = new Player() { Name = "PL2", Number = 2 } },
                new Place() { Amplua = 1, Position = 1, Player = new Player() { Name = "PL3", Number = 3 } },
                new Place() { Amplua = 1, Position = 2, Player = new Player() { Name = "PL4", Number = 4 } },
                new Place() { Amplua = 1, Position = 3, Player = new Player() { Name = "PL5", Number = 5 } },
                new Place() { Amplua = 2, Position = 1, Player = new Player() { Name = "PL6", Number = 6 } },
                new Place() { Amplua = 2, Position = 2, Player = new Player() { Name = "PL7", Number = 7 } },
                new Place() { Amplua = 2, Position = 3, Player = new Player() { Name = "PL8", Number = 8 } },
                new Place() { Amplua = 2, Position = 4, Player = new Player() { Name = "PL9", Number = 9 } },
                new Place() { Amplua = 3, Position = 2, Player = new Player() { Name = "PL10", Number = 10 } },
                new Place() { Amplua = 3, Position = 3, Player = new Player() { Name = "PL11", Number = 11 } },
            };
            List<Place> p3 = new List<Place>()
            {
                new Place() { Amplua = 0, Position = 2, Player = new Player() { Name = "PL1", Number = 1 } },
                new Place() { Amplua = 1, Position = 0, Player = new Player() { Name = "PL2", Number = 2 } },
                new Place() { Amplua = 1, Position = 1, Player = new Player() { Name = "PL3", Number = 3 } },
                new Place() { Amplua = 1, Position = 2, Player = new Player() { Name = "PL4", Number = 4 } },
                new Place() { Amplua = 1, Position = 3, Player = new Player() { Name = "PL5", Number = 5 } },
                new Place() { Amplua = 2, Position = 1, Player = new Player() { Name = "PL6", Number = 6 } },
                new Place() { Amplua = 2, Position = 2, Player = new Player() { Name = "PL7", Number = 7 } },
                new Place() { Amplua = 3, Position = 0, Player = new Player() { Name = "PL8", Number = 8 } },
                new Place() { Amplua = 3, Position = 1, Player = new Player() { Name = "PL9", Number = 9 } },
                new Place() { Amplua = 3, Position = 2, Player = new Player() { Name = "PL10", Number = 10 } },
                new Place() { Amplua = 3, Position = 3, Player = new Player() { Name = "PL11", Number = 11 } },
            };
            List<Place> p4 = new List<Place>()
            {
                new Place() { Amplua = 0, Position = 2, Player = new Player() { Name = "PL1", Number = 1 } },
                new Place() { Amplua = 1, Position = 0, Player = new Player() { Name = "PL2", Number = 2 } },
                new Place() { Amplua = 1, Position = 2, Player = new Player() { Name = "PL3", Number = 3 } },
                new Place() { Amplua = 1, Position = 3, Player = new Player() { Name = "PL4", Number = 4 } },
                new Place() { Amplua = 1, Position = 4, Player = new Player() { Name = "PL5", Number = 5 } },
                new Place() { Amplua = 3, Position = 1, Player = new Player() { Name = "PL6", Number = 6 } },
                new Place() { Amplua = 2, Position = 2, Player = new Player() { Name = "PL7", Number = 7 } },
                new Place() { Amplua = 3, Position = 3, Player = new Player() { Name = "PL8", Number = 8 } },
                new Place() { Amplua = 4, Position = 1, Player = new Player() { Name = "PL9", Number = 9 } },
                new Place() { Amplua = 5, Position = 2, Player = new Player() { Name = "PL10", Number = 10 } },
                new Place() { Amplua = 4, Position = 3, Player = new Player() { Name = "PL11", Number = 11 } },
            };
            List<Place> p5 = new List<Place>()
            {
                new Place() { Amplua = 0, Position = 2, Player = new Player() { Name = "PL1", Number = 1 } },
                new Place() { Amplua = 1, Position = 0, Player = new Player() { Name = "PL2", Number = 2 } },
                new Place() { Amplua = 1, Position = 1, Player = new Player() { Name = "PL3", Number = 3 } },
                new Place() { Amplua = 1, Position = 2, Player = new Player() { Name = "PL4", Number = 4 } },
                new Place() { Amplua = 1, Position = 3, Player = new Player() { Name = "PL5", Number = 5 } },
                new Place() { Amplua = 2, Position = 1, Player = new Player() { Name = "PL6", Number = 6 } },
                new Place() { Amplua = 2, Position = 2, Player = new Player() { Name = "PL7", Number = 7 } },
                new Place() { Amplua = 2, Position = 3, Player = new Player() { Name = "PL8", Number = 8 } },
                new Place() { Amplua = 3, Position = 1, Player = new Player() { Name = "PL9", Number = 9 } },
                new Place() { Amplua = 3, Position = 2, Player = new Player() { Name = "PL10", Number = 10 } },
                new Place() { Amplua = 3, Position = 3, Player = new Player() { Name = "PL11", Number = 11 } },
            };
            List<Place> p6 = new List<Place>()
            {
                new Place() { Amplua = 0, Position = 2, Player = new Player() { Name = "PL1", Number = 1 } },
                new Place() { Amplua = 1, Position = 0, Player = new Player() { Name = "PL2", Number = 2 } },
                new Place() { Amplua = 1, Position = 1, Player = new Player() { Name = "PL3", Number = 3 } },
                new Place() { Amplua = 1, Position = 2, Player = new Player() { Name = "PL4", Number = 4 } },
                new Place() { Amplua = 1, Position = 3, Player = new Player() { Name = "PL5", Number = 5 } },
                new Place() { Amplua = 2, Position = 1, Player = new Player() { Name = "PL6", Number = 6 } },
                new Place() { Amplua = 2, Position = 2, Player = new Player() { Name = "PL7", Number = 7 } },
                new Place() { Amplua = 2, Position = 3, Player = new Player() { Name = "PL8", Number = 8 } },
                new Place() { Amplua = 2, Position = 4, Player = new Player() { Name = "PL9", Number = 9 } },
                new Place() { Amplua = 3, Position = 2, Player = new Player() { Name = "PL10", Number = 10 } },
                new Place() { Amplua = 3, Position = 3, Player = new Player() { Name = "PL11", Number = 11 } },
            };
            List<Place> p7 = new List<Place>()
            {
                new Place() { Amplua = 0, Position = 2, Player = new Player() { Name = "PL1", Number = 1 } },
                new Place() { Amplua = 1, Position = 0, Player = new Player() { Name = "PL2", Number = 2 } },
                new Place() { Amplua = 1, Position = 1, Player = new Player() { Name = "PL3", Number = 3 } },
                new Place() { Amplua = 1, Position = 2, Player = new Player() { Name = "PL4", Number = 4 } },
                new Place() { Amplua = 1, Position = 3, Player = new Player() { Name = "PL5", Number = 5 } },
                new Place() { Amplua = 2, Position = 1, Player = new Player() { Name = "PL6", Number = 6 } },
                new Place() { Amplua = 2, Position = 2, Player = new Player() { Name = "PL7", Number = 7 } },
                new Place() { Amplua = 3, Position = 0, Player = new Player() { Name = "PL8", Number = 8 } },
                new Place() { Amplua = 3, Position = 1, Player = new Player() { Name = "PL9", Number = 9 } },
                new Place() { Amplua = 3, Position = 2, Player = new Player() { Name = "PL10", Number = 10 } },
                new Place() { Amplua = 3, Position = 3, Player = new Player() { Name = "PL11", Number = 11 } },
            };
            Game.Team1.Tactics.Add(new Tactics(1, 500, p0));
            Game.Team1.Tactics.Add(new Tactics(1, 1000, p1));
            Game.Team1.Tactics.Add(new Tactics(1, 2000, p2));
            Game.Team1.Tactics.Add(new Tactics(1, 20000, p3));
            Game.Team2.Tactics.Add(new Tactics(1, 1000, p5));
            Game.Team2.Tactics.Add(new Tactics(1, 3000, p7));
            Game.Team2.Tactics.Add(new Tactics(1, 5000, p4));
            Game.Team2.Tactics.Add(new Tactics(1, 8000, p6));
            bool[][] temp = new bool[5][] { new bool[6], new bool[6], new bool[6], new bool[6], new bool[6] };
            PlayPauseCommand = new RelayCommand<MediaElement>(x => PlayPauseMethod(x));
            ForwardCommand = new RelayCommand<MediaElement>(x => ForwardMethod(x));
            BackwardCommand = new RelayCommand<MediaElement>(x => BackwardMethod(x));
            HideTacticsCommand = new RelayCommand<Grid>(x => HideTacticsMethod(x));
            ReColor1 = new RelayCommand<Button>(ReColor1Method);
            ReColor2 = new RelayCommand<Button>(ReColor2Method);
        }

        #endregion

        #region Private Methods

        void PlayPauseMethod(MediaElement media)
        {
            if (IsPlaying)
            {
                media.Pause();
                IsPlaying = false;
            }
            else
            {
                media.Play();
                IsPlaying = true;
            }
        }
        void ForwardMethod(MediaElement media)
        {
            TimeSpan ts = media.Position;
            media.Position = new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds + 100);
            if (IsPlaying) media.Play();
            else media.Pause();
        }
        void BackwardMethod(MediaElement media)
        {
            TimeSpan ts = media.Position;
            if (ts.TotalMilliseconds > 100)
                media.Position = new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds - 100);
            else
                media.Position = new TimeSpan(0);
            if (IsPlaying) media.Play();
            else media.Pause();
        }
        void HideTacticsMethod(Grid grid)
        {
            if (grid.Visibility == Visibility.Collapsed)
            {
                grid.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Visibility = Visibility.Collapsed;
            }
        }
        private int[][] GetPositions(Tactics tact, out bool[][] notAv)
        {
            int[][] res = new int[5][];
            notAv = new bool[5][];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new int[6];
                notAv[i] = new bool[6];
            }
            foreach (Place item in tact)
            {
                res[item.Position][item.Amplua] = item.Player.Number;
                if (Game.DisabledPlayers.Contains(item.Player, new PlayerComparer())) notAv[item.Position][item.Amplua] = true;
            }
            return res;
        }
        void ReColor1Method(Button obj)
        {
            if ((obj.Background as SolidColorBrush).Color != new SolidColorBrush(Game.Team1.ShirtColor).Color)
            {
                obj.Background = new SolidColorBrush(Game.Team1.ShirtColor);
                List1.Remove(Convert.ToInt32(obj.Content));
            }
            else
            {
                //foreach (Button item in (obj.Parent as Grid).Children)
                //{
                //    if (item.IsEnabled) item.Background = new SolidColorBrush(Game.Team1.ShirtColor);
                //}
                byte r = (byte)(255 - Game.Team1.ShirtColor.R);
                byte g = (byte)(255 - Game.Team1.ShirtColor.G);
                byte b = (byte)(255 - Game.Team1.ShirtColor.B);
                obj.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
                List1.Add(Convert.ToInt32(obj.Content));
            }
        }
        void ReColor2Method(Button obj)
        {
            if ((obj.Background as SolidColorBrush).Color != new SolidColorBrush(Game.Team2.ShirtColor).Color)
            {
                obj.Background = new SolidColorBrush(Game.Team2.ShirtColor);
                List2.Remove(Convert.ToInt32(obj.Content));
            }
            else
            {
                //foreach (Button item in (obj.Parent as Grid).Children)
                //{
                //    if (item.IsEnabled) item.Background = new SolidColorBrush(Game.Team2.ShirtColor);
                //}
                byte r = (byte)(255 - Game.Team2.ShirtColor.R);
                byte g = (byte)(255 - Game.Team2.ShirtColor.G);
                byte b = (byte)(255 - Game.Team2.ShirtColor.B);
                obj.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
                List2.Add(Convert.ToInt32(obj.Content));
            }
        }        
        Color ReverseColor(Color source)
        {
            byte r = (byte)(255 - source.R);
            byte g = (byte)(255 - source.G);
            byte b = (byte)(255 - source.B);
            return Color.FromRgb(r, g, b);
        }         
        void ColoredPlayers(Grid grid, int team)
        {
            //List<int> res = new List<int>();
            foreach (Button item in grid.Children)
            {
                
                switch (team)
                {
                    case 1:
                        //if ((item.Background as SolidColorBrush).Color != Game.Team1.ShirtColor && (item.Background as SolidColorBrush).Color != Colors.LightGray && item.Visibility == Visibility.Visible)
                        //{
                        //    res.Add(Convert.ToInt32(item.Content));
                        //}
                        item.Background = new SolidColorBrush(Game.Team1.ShirtColor);
                        break;
                    case 2:
                        //if ((item.Background as SolidColorBrush).Color != Game.Team2.ShirtColor && (item.Background as SolidColorBrush).Color != Colors.LightGray && item.Visibility == Visibility.Visible)
                        //{
                        //    res.Add(Convert.ToInt32(item.Content));
                        //}
                        item.Background = new SolidColorBrush(Game.Team2.ShirtColor);
                        break;
                }
                item.IsEnabled = true;
                item.Visibility = Visibility.Visible;
            }
        }

        #endregion
    }
}