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
        protected string name;
        protected static int count = 0;
        protected static int level = 10;
        protected bool exist = true;
        protected int life = 100;

        public void setCount() { count = 0; }

        public int addLevel(int levelinput)
        {
            level += levelinput;
            return level;
        }

        public void setTotal()
        {
            total = 0;
        }

        public int getTotal()
        {
            return total;
        }

        public void setlife()
        {
            life = 100;
        }

        public void setLevel()
        {
            level = 10;
        }

        public void start(SharpGIS.AR.Controls.ARPanel arPanel, Button btnMute, SoundEffectInstance[] sounds, TextBlock txtTotal,System.Windows.Shapes.Rectangle recLife,TextBlock txtOver)
        {
            int cout = count;
            exist = true;
            Stream stream = TitleContainer.OpenStream("Sounds/" + name + ".wav");
            SoundEffect annoying = SoundEffect.FromStream(stream);
            sounds[cout] = annoying.CreateInstance();
            sounds[cout].IsLooped = true;
            sounds[cout].Volume = (float)0.2;
            if (btnMute.Tag.ToString() == "On")
            {
                sounds[cout].Play();
            }

            Image img = new Image();
            img.Source = new BitmapImage(new Uri("/Image/" + name + ".png", UriKind.Relative));
            img.Width = 100;
            img.Height = 100;
            btnMute.Click += (c, d) =>
            {

                if (sounds[cout] != null)
                {
                    if (btnMute.Tag.ToString() == "On")
                    {
                        if (exist)
                        {
                            sounds[cout].Play();
                        }

                    }
                    else
                    {
                        sounds[cout].Pause();
                    }
                }
            };
            DispatcherTimer lifeTimer = new DispatcherTimer();
            lifeTimer.Tick += delegate(object s, EventArgs args)
                {
                    if (recLife.Height > 3)
                    {
                        recLife.Height -= 3;
                    }
                    else
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (sounds[i] != null)
                            {
                                sounds[i].Dispose();
                            }
                        }
                        arPanel.Children.Clear();
                        lifeTimer.Stop();
                        txtOver.Visibility = Visibility.Visible;
                    }
                };
            lifeTimer.Interval = new TimeSpan(0, 0, 2); // one seconds delay
            lifeTimer.Start();
            img.Tap += (a, b) =>
            {
                total += 1;
                txtTotal.Text = total.ToString();
                lifeTimer.Stop();
                arPanel.Children.Remove(img);
                sounds[cout].Stop();
                this.exist = false;
            };
            Random random = new Random();
            ARPanel.SetDirection(img, new System.Windows.Point(random.Next(-30, 30), random.Next(0, 360)));
            arPanel.Children.Add(img);
            if (count < 100)
            {
                ++count;
            }
            else
            {
                count = 0; 
            }
        }

        public void start(TextBlock txtNext, SharpGIS.AR.Controls.ARPanel arPanel, Button btnReplay, Button btnMute, SoundEffectInstance[] sounds)
        {
            int cout = count;
            exist = true;
            Stream stream = TitleContainer.OpenStream("Sounds/" + name + ".wav");
            SoundEffect annoying = SoundEffect.FromStream(stream);
            sounds[cout] = annoying.CreateInstance();
            sounds[cout].IsLooped = true;
            sounds[cout].Volume = (float)0.2;
            if (btnMute.Tag.ToString() == "On")
            {
                sounds[cout].Play();
            }
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("/Image/" + name + ".png", UriKind.Relative));
            img.Width = 500 / (int)(level / 10);
            img.Height = 500 / (int)(level / 10);
            btnMute.Click += (c, d) =>
            {

                if (sounds[cout] != null)
                {
                    if (btnMute.Tag.ToString() == "On")
                    {
                        if (exist)
                        {
                            sounds[cout].Play();
                        }

                    }
                    else
                    {
                        sounds[cout].Pause();
                    }
                }
            };
            img.Tap += (a, b) =>
            {
                
                if (--count == 0)    //start next round if no mosquito left
                {

                    if (level < 50)
                    {
                        txtNext.Visibility = Visibility.Visible;
                        DispatcherTimer timer = new DispatcherTimer();
                        timer.Tick +=
                delegate(object s, EventArgs args)
                {
                    timer.Stop();
                    txtNext.Visibility = Visibility.Collapsed;
                    level += 10;
                    for (int i = 0; i < 5; i++)
                    {
                        SimpleMosquito simpleMosquito1 = new SimpleMosquito();
                        simpleMosquito1.start(txtNext, arPanel, btnReplay, btnMute, sounds);
                    }

                };
                        timer.Interval = new TimeSpan(0, 0, 2); // one seconds delay
                        timer.Start();
                       
                        //create next round's mosquitos
                    }
                    else
                    {
                        txtNext.Text = "Finish!";
                        txtNext.Visibility = Visibility.Visible;
                        DispatcherTimer timer = new DispatcherTimer();
                        timer.Tick +=
                delegate(object s, EventArgs args)
                {
                    timer.Stop();
                    txtNext.Visibility = Visibility.Collapsed;
                };
                        timer.Interval = new TimeSpan(0, 0, 2); // one seconds delay
                        timer.Start();                      
                        level = 10;
                        btnReplay.Visibility = Visibility.Visible;
                    }

                }

               
                arPanel.Children.Remove(img);
                sounds[cout].Stop();
                this.exist = false;

            };
            Random random = new Random();
            ARPanel.SetDirection(img, new System.Windows.Point(random.Next(-30, 30), random.Next(0, 360)));
            arPanel.Children.Add(img);
            ++count;
        }


    }
    class SimpleMosquito : Mosquito
    {

        public SimpleMosquito()
        {
            this.name = "mosquito";

        }

    }

    class MrSucker : Mosquito
    {
        public MrSucker()
        {
            this.name = "mrsucker";
        }

    }

    class MsSucker : Mosquito
    {
        private MsSucker()
        {
            this.name = "mssucker";
        }

    }

}
