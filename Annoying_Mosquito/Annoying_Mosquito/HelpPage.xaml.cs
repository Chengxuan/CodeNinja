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
            txtHelp.Text = "How to play:\nIn this game, mosquitos will come out every 3 second, and they will suck your blood every 2 seconds. When your blood bar become zero, game over!\n\n\nThe mosquitos will appear around -5 ~ 5  Horizontal Degree and 0 ~ 360 Vertical Degree!\nMove your Mobile to find them!\nHope you like this game!";
        }
    }
}