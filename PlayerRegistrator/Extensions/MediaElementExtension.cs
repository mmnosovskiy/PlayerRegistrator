using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PlayerRegistrator
{
    public class MediaElementExtension
    {
        public static TimeSpan GetBindablePosition(DependencyObject obj)
        {
            return (TimeSpan)obj.GetValue(BindablePositionProperty);
        }

        public static void SetBindablePosition(DependencyObject obj, double value)
        {
            obj.SetValue(BindablePositionProperty, value);
        }

        // Using a DependencyProperty as the backing store for BindablePosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindablePositionProperty =
            DependencyProperty.RegisterAttached("BindablePosition", typeof(TimeSpan), typeof(MediaElementExtension), new PropertyMetadata(new TimeSpan(), BindablePositionChangedCallback));

        private static void BindablePositionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MediaElement mediaElement = d as MediaElement;
            if (mediaElement == null) return;

            mediaElement.Position = (TimeSpan)e.NewValue;
        }
    }
}
