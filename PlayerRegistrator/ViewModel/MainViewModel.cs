﻿using Apex.MVVM;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PlayerRegistrator.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        bool IsPlaying { get; set; }
        public RelayCommand<MediaElement> PlayPauseCommand { get; set; }
        public RelayCommand<MediaElement> ForwardCommand { get; set; }
        public RelayCommand<MediaElement> BackwardCommand { get; set; }


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
        private Stopwatch _watch;
        public Stopwatch Watch
        {
            get
            {
                return _watch;
            }
            set
            {
                Set(ref _watch, value);
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
                Team1 = new Team() { NumberColor = Colors.White, ShirtColor = Colors.DarkRed, Tactics = new List<Tactics>() },
                Team2 = new Team() { NumberColor = Colors.White, ShirtColor = Colors.Purple }
            };
            IsPlaying = true;
            Watch = new Stopwatch();
            Watch.Start();
            Tactics1 = GetPositions(new Tactics());
            Tactics2 = GetPositions(new Tactics());
            //_recolor1 = new RelayCommand<Ellipse>(DoRecolor1);
            PlayPauseCommand = new RelayCommand<MediaElement>(x => PlayPauseMethod(x));
            ForwardCommand = new RelayCommand<MediaElement>(x => ForwardMethod(x));
            BackwardCommand = new RelayCommand<MediaElement>(x => BackwardMethod(x));
        }
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
        //private RelayCommand<Ellipse> _recolor1;
        //public RelayCommand<Ellipse> Recolor1
        //{
        //    get { return _recolor1; }
        //}
        //public void DoRecolor1(Ellipse ell)
        //{
        //    //foreach (Ellipse item in (ell.Parent as Grid).Children)
        //    //{
        //    //    item.Fill = new SolidColorBrush(Game.Team1.ShirtColor);
        //    //}
        //    //ell.Fill = new SolidColorBrush(Color.Subtract(((SolidColorBrush)ell.Fill).Color, Colors.Black));
        //    ell.Fill = new SolidColorBrush(Colors.Black);
        //}
        private RelayCommand<Button> _recolor1;
        public RelayCommand<Button> Recolor1
        {
            get
            {
                return _recolor1 ??
                    (_recolor1 = new RelayCommand<Button>(obj =>
                    {
                        foreach (Button item in (obj.Parent as Grid).Children)
                        {
                            item.Background = new SolidColorBrush(Game.Team1.ShirtColor);
                        }
                        byte r = (byte)(255 - Game.Team1.ShirtColor.R);
                        byte g = (byte)(255 - Game.Team1.ShirtColor.G);
                        byte b = (byte)(255 - Game.Team1.ShirtColor.B);
                        obj.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
                    }));
            }
        }
        private RelayCommand<Button> _recolor2;
        public RelayCommand<Button> Recolor2
        {
            get
            {
                return _recolor2 ??
                    (_recolor2 = new RelayCommand<Button>(obj =>
                    {
                        foreach (Button item in (obj.Parent as Grid).Children)
                        {
                            item.Background = new SolidColorBrush(Game.Team2.ShirtColor);
                        }
                        byte r = (byte)(255 - Game.Team2.ShirtColor.R);
                        byte g = (byte)(255 - Game.Team2.ShirtColor.G);
                        byte b = (byte)(255 - Game.Team2.ShirtColor.B);
                        obj.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
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