﻿using System;
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

namespace PlayerRegistrator
{
    /// <summary>
    /// Логика взаимодействия для MediaElementExt.xaml
    /// </summary>
    public partial class MediaElementExt : UserControl
    {
        public static DependencyProperty VideoPositionProperty;
        public int VideoPosition
        {
            get { return (int)GetValue(VideoPositionProperty); }
            set { SetValue(VideoPositionProperty, value); }
        }
        public static DependencyProperty LoadedBehaviorProperty;
        public MediaState LoadedBehavior
        {
            get { return (MediaState)GetValue(LoadedBehaviorProperty); }
            set { SetValue(LoadedBehaviorProperty, value); }
        }
        public static DependencyProperty ScrubbingEnabledProperty;
        public bool ScrubbingEnabled
        {
            get { return (bool)GetValue(ScrubbingEnabledProperty); }
            set { SetValue(ScrubbingEnabledProperty, value); }
        }
        public static DependencyProperty SourceProperty;
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static DependencyProperty DurationProperty;
        public int Duration
        {
            get { return (int)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }
        public MediaElementExt()
        {
            InitializeComponent();
        }
        static MediaElementExt()
        {
            VideoPositionProperty = DependencyProperty.Register("VideoPosition", typeof(int), typeof(MediaElementExt),
                new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnVideoPositionChanged)));
            LoadedBehaviorProperty = DependencyProperty.Register("LoadedBehavior", typeof(MediaState), typeof(MediaElementExt),
                new FrameworkPropertyMetadata(MediaState.Play, new PropertyChangedCallback(OnVideoPositionChanged)));
            ScrubbingEnabledProperty = DependencyProperty.Register("ScrubbingEnabled", typeof(bool), typeof(MediaElementExt),
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnScrubbingEnabledChanged)));
            SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(MediaElementExt),
                new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnSourceChanged)));
            DurationProperty = DependencyProperty.Register("Duration", typeof(int), typeof(MediaElementExt),
                new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnVideoPositionChanged)));
        }
        private static void OnVideoPositionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            int newVideoPosition = (int)e.NewValue;
            MediaElementExt media = (MediaElementExt)sender;
            media.VideoPosition = newVideoPosition;
        }
        private static void OnLoadedBehaviorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MediaState newMediaState = (MediaState)e.NewValue;
            MediaElementExt media = (MediaElementExt)sender;
            media.LoadedBehavior = newMediaState;
        }
        private static void OnScrubbingEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            bool newScrubbingEnabled = (bool)e.NewValue;
            MediaElementExt media = (MediaElementExt)sender;
            media.ScrubbingEnabled = newScrubbingEnabled;
        }
        private static void OnSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            string newSource = (string)e.NewValue;
            MediaElementExt media = (MediaElementExt)sender;
            media.Source = newSource;
        }
        private static void OnDurationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            int newDuration = (int)e.NewValue;
            MediaElementExt media = (MediaElementExt)sender;
            media.Duration = newDuration;
        }
    }
}
