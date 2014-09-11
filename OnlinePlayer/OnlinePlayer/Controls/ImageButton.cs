using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OnlinePlayer.Controls
{
    public class ImageButton : Button
    {
        public static readonly DependencyProperty SourceProperty =
             DependencyProperty.Register("Source", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata());
        public ImageSource Source
        {
            get { return (ImageSource)this.GetValue(SourceProperty); }
            set { this.SetValue(SourceProperty, value); }
        }
    }
}
