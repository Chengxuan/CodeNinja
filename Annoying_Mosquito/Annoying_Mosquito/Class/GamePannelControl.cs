using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGIS.AR.Controls;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Devices;
using Microsoft.Xna.Framework;

namespace Annoying_Mosquito
{
    class GamePannelControl
    {
        private static List<SimpleMosquito> mosList;

        private ARPanel arPanel;

        private Boolean voiceControl;

        private TextBlock txtTotal;

       // private int attackedTimes;

        public GamePannelControl(ARPanel arPanel,TextBlock txtTotal)
        {
            this.arPanel = arPanel;
            this.txtTotal = txtTotal;
           // attackedTimes = 0;
            mosList = new List<SimpleMosquito>();
            voiceControl = true;
        }


        public int getBloodAmount()
        {
            return SimpleMosquito.showBloodAmount();
        }

        public void killAll()
        {
            for (int i = 0; i < mosList.Count; i++)
            {
              //  smo.addTotal();
               // txtTotal.Text = smo.getTotal().ToString();
                //lifeTimer.Stop();
                arPanel.Children.Remove(mosList[i].getAppearance());
                mosList[i].shutUp();
                //sounds[cout].Stop();
                mosList[i].die();
                mosList[i].stopSuck();
            }
            mosList.Clear();
            SimpleMosquito.flushHistory();
        }

        public void shutUp()
        {
            voiceControl = false;
            for (int i = 0; i < mosList.Count; i++)
            {
                mosList[i].shutUp();
            }
        }

       
        public void startSing()
        {
            voiceControl = true;
            for (int i = 0; i < mosList.Count; i++)
            {
                mosList[i].startSing();
            }
        }

        public void putSimpleMosquitos()
        {
            SimpleMosquito smo = new SimpleMosquito();
            mosList.Add(smo);
            //int cout = smo.getCount();
            if (voiceControl)
            {
                smo.startSing();
            }
            smo.startSuck();
           
         
            smo.getAppearance().Tap += (a, b) =>
            {
                smo.addTotal();
                txtTotal.Text = SimpleMosquito.getTotal().ToString();
                smo.shutUp();
                smo.die();
                smo.stopSuck();
                Stream streamS = TitleContainer.OpenStream("Sounds/squash.wav");
                SoundEffect squash = SoundEffect.FromStream(streamS);
                SoundEffectInstance voiS = squash.CreateInstance();
                if(voiceControl) voiS.Play();
                smo.changeAppearance(new BitmapImage(new Uri("/Image/red_paint.png", UriKind.Relative)));
                DispatcherTimer bloodTimer = new DispatcherTimer();

                bloodTimer.Tick += delegate(object s, EventArgs args)
                {
                    bloodTimer.Stop();
                    arPanel.Children.Remove(smo.getAppearance());

                };
                bloodTimer.Interval = TimeSpan.FromSeconds(1); // three seconds delay
                bloodTimer.Start();
                
                //lifeTimer.Stop();
               
               
                //sounds[cout].Stop();
               
                for (int i = 0; i < mosList.Count; i++)
                {
                    if (mosList[i].Equals(smo))
                    {
                        mosList.Remove(smo);
                    }
                }
            };

            Random random = new Random();
            ARPanel.SetDirection(smo.getAppearance(), new System.Windows.Point(random.Next(-5, 5), random.Next(0, 360)));
            arPanel.Children.Add(smo.getAppearance());
           
        }

    }
}
