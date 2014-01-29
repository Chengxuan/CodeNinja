using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices;
using SharpGIS.AR.Controls;
using System.Threading;
using Microsoft.Xna.Framework.Audio;
using System.Windows.Threading;

namespace Annoying_Mosquito
{
    public partial class Train : PhoneApplicationPage
    {
        public Microsoft.Devices.PhotoCamera m_camera;
        public SoundEffectInstance[] sounds = new SoundEffectInstance[20];
        public ImageBrush[] background = new ImageBrush[2];
        public DispatcherTimer timer = new DispatcherTimer();

        public Train()
        {
            InitializeComponent();
            SimpleMosquito simpleMosquito00 = new SimpleMosquito();
            simpleMosquito00.setCount();
            simpleMosquito00.setLevel();
            simpleMosquito00.setTotal();
            //initial camera
            m_camera = new Microsoft.Devices.PhotoCamera();
            viewfinderBrush.SetSource(m_camera); // this function should using Microsoft.Devices
            background[0] = new ImageBrush();
            background[1] = new ImageBrush();
            background[0].ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Image/soundoff.png", UriKind.Relative));
            background[1].ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Image/soundon.png", UriKind.Relative));
            startGame();
        }



        private void ARPanel_Loaded(object sender, RoutedEventArgs e)
        {
            var panel = sender as SharpGIS.AR.Controls.ARPanel;
            //For motion to be supported, device needs at least a compass and
            //an accelerometer. A gyro as well makes the experience much better though
            if (Microsoft.Devices.Sensors.Motion.IsSupported)
            {
                //Start the AR PAnel
                panel.Start();
            }
            else //Bummer! 
            {
                panel.Visibility = System.Windows.Visibility.Collapsed;
                //MessageBox.Show("Sorry - Motion sensor is not supported on this device. App cannot run!!!");
            }
        }

        private void ARPanel_Unloaded(object sender, RoutedEventArgs e)
        {
            //Remember to stop the motion sensor when leaving
            (sender as SharpGIS.AR.Controls.ARPanel).Stop();
        }



        //over ride the orientation function
        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            switch (e.Orientation)
            {
                case PageOrientation.Landscape:
                case PageOrientation.LandscapeLeft:
                    videoBrushTransform.Rotation = 0;
                    break;
                case PageOrientation.LandscapeRight:
                    videoBrushTransform.Rotation = 180;
                    break;
                default: break;
            }

            base.OnOrientationChanged(e);
        }

        public void startGame()
        {
            timer.Tick +=
    delegate(object s, EventArgs args)
    {
        txtIntroduction.Visibility = System.Windows.Visibility.Collapsed;
        timer.Stop();
        for (int i = 0; i < 5; i++)
        {
            SimpleMosquito simpleMosquito1 = new SimpleMosquito();
            simpleMosquito1.start(txtNext, arPanel, btnReplay, btnMute, sounds);
        }

    };
            timer.Interval = new TimeSpan(0, 0,5); // one seconds delay
            timer.Start();
                       
           
            btnReplay.Click += (sender1, e1) =>
            {
                
                for (int i = 0; i < 5; i++)
                {
                    SimpleMosquito simpleMosquito1 = new SimpleMosquito();
                    simpleMosquito1.start(txtNext, arPanel, btnReplay, btnMute, sounds);
                }
               
            };


        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            timer.Stop();            
            for (int i = 0; i < 20; i++)
            {
                if (sounds[i] != null)
                {
                    sounds[i].Dispose();
                }
            }
            this.arPanel.Children.Clear();
        }

        private void btnMute_Click(object sender, RoutedEventArgs e)
        {
            switch (btnMute.Tag.ToString())
            {

                case "On":
                    btnMute.Tag = "Off";
                    btnMute.Background = background[0];
                    break;
                case "Off":
                    btnMute.Tag = "On";
                    btnMute.Background = background[1];
                    break;
                default:
                    break;
            }
        }
    }
}