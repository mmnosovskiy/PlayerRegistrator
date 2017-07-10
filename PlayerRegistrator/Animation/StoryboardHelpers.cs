using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace PlayerRegistrator
{
    public static class StoryboardHelpers
    {
        public static async Task AddFadeIn(this Storyboard storyboard, float seconds, float decelerationRatio = 0.9f)
        {
            var fadeAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                DecelerationRatio = decelerationRatio
            };
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(fadeAnimation);
        }
        public static async Task AddFadeOut(this Storyboard storyboard, float seconds, float decelerationRatio = 0.9f)
        {
            var fadeAnimation = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                DecelerationRatio = decelerationRatio
            };
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(fadeAnimation);
        }
        public static async Task AddSlideToLeft(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f)
        {
            var slideAnimation = new ThicknessAnimation()
            {
                From = new Thickness(0),
                To = new Thickness(-offset, 0, offset, 0),
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                DecelerationRatio = decelerationRatio
            };
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            storyboard.Children.Add(slideAnimation);
        }
    }
}
