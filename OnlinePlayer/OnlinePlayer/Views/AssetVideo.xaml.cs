using System;
using System.Collections.Generic;
using System.IO;
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

namespace OnlinePlayer.Views
{
    /// <summary>
    /// Interaction logic for AssetVideo.xaml
    /// </summary>
    public partial class AssetVideo : UserControl
    {
        private bool IsMoved = false;
        private bool IsCapched = false;
        private bool IsPaused = false;
        public AssetVideo()
        {
            InitializeComponent();

        }

        // Play the media. 
        void OnMouseDownPlayMedia(object sender, MouseButtonEventArgs args)
        {

            // The Play method will begin the media if it is not currently active or  
            // resume media if it is paused. This has no effect if the media is 
            // already running.
            meAssetVideo.Play();
            IsPaused = true;

            // Initialize the MediaElement property values.
            InitializePropertyValues();

        }

        // Pause the media. 
        void OnMouseDownPauseMedia(object sender, MouseButtonEventArgs args)
        {

            // The Pause method pauses the media if it is currently running. 
            // The Play method can be used to resume.
            meAssetVideo.Pause();
            IsPaused = true;

        }

        // Stop the media. 
        void OnMouseDownStopMedia(object sender, MouseButtonEventArgs args)
        {

            // The Stop method stops and resets the media to be played from 
            // the beginning.
            timelineSlider.Value = 0;
            meAssetVideo.Stop();

        }

        // Change the volume of the media. 
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            meAssetVideo.Volume = (double)volumeSlider.Value;
        }


        // When the media opens, initialize the "Seek To" slider maximum value 
        // to the total number of miliseconds in the length of the media clip. 
        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = meAssetVideo.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        // When the media playback is finished. Stop() the media to seek to media start. 
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, 0);
            meAssetVideo.Position = ts;
            timelineSlider.Value = 0;
            meAssetVideo.Stop();
        }

        // Jump to different parts of the media (seek to).  
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            int SliderValue = (int)timelineSlider.Value;

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds. 
            // Create a TimeSpan with miliseconds equal to the slider value.
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            meAssetVideo.Position = ts;
        }

        void InitializePropertyValues()
        {
            // Set the media's starting Volume and SpeedRatio to the current value of the 
            // their respective slider controls.
            meAssetVideo.Volume = (double)volumeSlider.Value;
        }

        private void meAssetVideo_MouseEnter(object sender, MouseEventArgs e)
        {
            spControls.Visibility = Visibility.Visible;
        }

        private void meAssetVideo_MouseLeave(object sender, MouseEventArgs e)
        {
            spControls.Visibility = Visibility.Collapsed;
        }

        private void meAssetVideo_Drop(object sender, DragEventArgs e)
        {
            String[] VideoPath = (String[])e.Data.GetData(DataFormats.FileDrop);
            meAssetVideo.Source = new Uri(VideoPath[0]);
            meAssetVideo.Play();
        }

        private void meAssetVideo_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsCapched)
                IsMoved = true;
        }

        private void meAssetVideo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsMoved)
            {
                if (!IsPaused)
                {
                    meAssetVideo.Pause();
                    IsPaused = true;
                }
                else
                {
                    meAssetVideo.Play();
                    IsPaused = false;
                }
            }
            IsCapched = false;
            IsMoved = false;

        }

        private void meAssetVideo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsCapched = true;
        }

        private void meAssetVideo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (!IsPaused)
                {
                    meAssetVideo.Pause();
                    IsPaused = true;
                }
                else
                {
                    meAssetVideo.Play();
                    IsPaused = false;
                }
            }
        }
    }
}
