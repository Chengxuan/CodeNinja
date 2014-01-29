using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Annoying_Mosquito
{
    public partial class HelpPage : PhoneApplicationPage
    {
        public HelpPage()
        {
            InitializeComponent();
            txtHelp.Text = "Trainning Mode:\nIn this mode, you have 5 seconds to read a simple introduction about how to play this game. In first round you will see very big mosquitos, and then their size will decrease round by round. Each round will have 5 mosquitos!\n\n\nSurvive Mode:\nIn this mode, mosquitos will increase every 1 second, and they will suck your blood every 2 seconds. When your blood bar become zero, game over!\n\n\nThe mosquitos will appear around -30 ~ 30  Horizontal Degree and 0 ~ 360 Vertical Degree!\nMove your Mobile to find them!\nHope you like this game!";
        }
    }
}