using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using SharpGIS.AR.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.IO.IsolatedStorage;

namespace Annoying_Mosquito
{
    class Mosquito
    {
        protected static int total = 0;
        protected static int contribution = 0;
        protected string name;
        protected static int count;
        protected static int level;
        protected bool exist;
        protected int life ;
        protected Image appearance;
        protected SoundEffectInstance voice;
        protected DispatcherTimer attackTimer;


        public void shutUp()
        {        
            if (voice.State == SoundState.Playing)
            {
                voice.Pause();
            }

        }

        public static int getTotal()
        {
            return total;
        }

        public static int showBloodAmount()
        {
            return contribution;
        }

        public static void flushHistory()
        {
           // total = 0;
            contribution = 0;
        }

        public void startSuck()
        {
            attackTimer.Start();
        }

        public void stopSuck()
        {
            attackTimer.Stop();
        }
        public void startSing()
        {
            if (voice.State == SoundState.Paused)
            {
                voice.Resume();
            }
            else
            {
                voice.Play();
            }
        }

        public void die()
        {
            exist = false;
        }

        public static void resetTotal()
        {
            total = 0;
        }


        public void setCount() {
            count = 0;
        }

        public int getCount()
        {
            return count;
        }

        public void addLevel()
        {
            level += 10;
        }

        public void addTotal()
        {
            total++;
        }


        public void setlife()
        {
            life = 100;
        }

        public void setLevel()
        {
            level = 10;
        }

      public Image getAppearance(){
          return appearance;
      }
      public void changeAppearance(BitmapImage img)
      {
          this.appearance.Source = img;
      }

    }


}
