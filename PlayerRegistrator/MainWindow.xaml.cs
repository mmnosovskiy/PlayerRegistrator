﻿using System.Windows;
using PlayerRegistrator.ViewModel;

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
            media.Focus();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}