using Microsoft.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Annoying_Mosquito
{
    class SimpleMosquito : Mosquito
    {

        public SimpleMosquito()
        {
            count = 0; level = 10; life = 100;
            exist = true;
            this.name = "mosquito";
            appearance = new Image();
            appearance.Source = new BitmapImage(new Uri("/Image/" + name + ".png", UriKind.Relative));
            appearance.Width = 100;
            appearance.Height = 100;
            Stream stream = TitleContainer.OpenStream("Sounds/" + name + ".wav");
            SoundEffect annoying = SoundEffect.FromStream(stream);

            voice = annoying.CreateInstance();
            voice.IsLooped = true;
            voice.Volume = (float)0.2;
            this.attackTimer = new DispatcherTimer();
            attackTimer.Interval = TimeSpan.FromSeconds(1);
            attackTimer.Tick += delegate(object s, EventArgs args)
            {
                contribution++;
            };
        }


    }

    

   
}
