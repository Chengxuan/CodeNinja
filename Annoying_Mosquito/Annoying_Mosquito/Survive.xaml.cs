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
using System.IO.IsolatedStorage;


namespace Annoying_Mosquito
{
    public partial class Survive : PhoneApplicationPage
    {
        //set a photocamera
        private Microsoft.Devices.PhotoCamera m_camera;
       // public SoundEffectInstance[] sounds = new SoundEffectInstance[100];
        private ImageBrush[] background = new ImageBrush[2];
        private DispatcherTimer lifeTimer;
        private GamePannelControl gpc;
        public Survive()
        {
            InitializeComponent();
            recLife.Height = 300;
            //SimpleMosquito simpleMosquito00 = new SimpleMosquito();
           // simpleMosquito00.setCount();
            //simpleMosquito00.setlife();
           // simpleMosquito00.setTotal();
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
             gpc = new GamePannelControl(arPanel, txtTotal);
            lifeTimer = new DispatcherTimer();
            lifeTimer.Tick += delegate(object s, EventArgs args)
            {
                gpc.putSimpleMosquitos();
                if (gpc.getBloodAmount()<100)
                {
                    VibrateController testVibrateController = VibrateController.Default;
                    testVibrateController.Start(TimeSpan.FromMilliseconds(5));
                    recLife.Height = 300 - (gpc.getBloodAmount() < 300 ? gpc.getBloodAmount() * 3 : 300);
                }
                else
                {
                    IsolatedStorageSettings appset = IsolatedStorageSettings.ApplicationSettings;
                    if (appset.Contains("best"))
                    {
                        if (Convert.ToInt16(appset["best"].ToString()) < SimpleMosquito.getTotal())
                        {
                            appset["best"] = SimpleMosquito.getTotal().ToString();
                        }
                    }
                    else
                    {
                        appset.Add("best", SimpleMosquito.getTotal().ToString());
                    }
                    SimpleMosquito.resetTotal();
                    recLife.Height = 0;
                    gpc.killAll();
                    arPanel.Children.Clear();
                    lifeTimer.Stop();
                    txtOver.Visibility = Visibility.Visible;
                }
            };
            lifeTimer.Interval = TimeSpan.FromSeconds(3); // three seconds delay
            lifeTimer.Start();
        }

       

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            lifeTimer.Stop();
            gpc.killAll();
            arPanel.Children.Clear();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            txtTotal.Text = SimpleMosquito.getTotal().ToString();
        }

        private void btnMute_Click(object sender, RoutedEventArgs e)
        {
           
            switch (btnMute.Tag.ToString())
            {

                case "On":
                    btnMute.Tag = "Off";
                    btnMute.Background = background[0];
                    //Mute the mosquitos
                    gpc.shutUp();
                    break;
                case "Off":
                    btnMute.Tag = "On";
                    btnMute.Background = background[1];
                    //Let the mosquitos singing
                    gpc.startSing();
                    break;
                default:
                    break;
            }
        }

    }
}