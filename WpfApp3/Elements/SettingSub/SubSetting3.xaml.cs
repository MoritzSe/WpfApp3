using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3.Elements.SettingSub
{
    /// <summary>
    /// Interaktionslogik für SubSetting3.xaml
    /// </summary>
    public partial class SubSetting3 : UserControl
    {
        public int currentTempInt;
        public string currentTemp;
        public string Value;

        public SubSetting3()
        {
            InitializeComponent();
            currentTemp = "21.5";
            Value = "21.5";
        }

        public void changeTemp()
        {
            // do something here to check temperature and adjust the value
        }

        public void setOnAC ()
        {
            DoubleAnimation seton = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            shadowactop.BeginAnimation(DropShadowEffect.OpacityProperty,seton);
            shadowacbottom.BeginAnimation(DropShadowEffect.OpacityProperty, seton);
            shadowacarc.BeginAnimation(DropShadowEffect.OpacityProperty, seton);
        }

        public void setOffAC()
        {
            DoubleAnimation setoff = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            shadowactop.BeginAnimation(DropShadowEffect.OpacityProperty, setoff);
            shadowacbottom.BeginAnimation(DropShadowEffect.OpacityProperty, setoff);
            shadowacarc.BeginAnimation(DropShadowEffect.OpacityProperty, setoff);
        }

        public void setOnTemp()
        {
            DoubleAnimation seton = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            shadowtemptop.BeginAnimation(DropShadowEffect.OpacityProperty, seton);
            shadowtempbottom.BeginAnimation(DropShadowEffect.OpacityProperty, seton);
            shadowtemparc.BeginAnimation(DropShadowEffect.OpacityProperty, seton);
        }

        public void setOffTemp()
        {
            DoubleAnimation setoff = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(100)
            };

            shadowtemptop.BeginAnimation(DropShadowEffect.OpacityProperty, setoff);
            shadowtempbottom.BeginAnimation(DropShadowEffect.OpacityProperty, setoff);
            shadowtemparc.BeginAnimation(DropShadowEffect.OpacityProperty, setoff);
        }

        public void acOFF ()
        {
            ACoff.Foreground = Brushes.Green;
            ACauto.Foreground = Brushes.White;
            AC1.Foreground = Brushes.White;
            AC2.Foreground = Brushes.White;
            AC3.Foreground = Brushes.White;
            AC4.Foreground = Brushes.White;
        }
        public void acAuto()
        {
            ACoff.Foreground = Brushes.White;
            ACauto.Foreground = Brushes.Green;
            AC1.Foreground = Brushes.White;
            AC2.Foreground = Brushes.White;
            AC3.Foreground = Brushes.White;
            AC4.Foreground = Brushes.White;
        }
        public void ac1()
        {
            ACoff.Foreground = Brushes.White;
            ACauto.Foreground = Brushes.White;
            AC1.Foreground = Brushes.Green;
            AC2.Foreground = Brushes.White;
            AC3.Foreground = Brushes.White;
            AC4.Foreground = Brushes.White;
        }
        public void ac2()
        {
            ACoff.Foreground = Brushes.White;
            ACauto.Foreground = Brushes.White;
            AC1.Foreground = Brushes.White;
            AC2.Foreground = Brushes.Green;
            AC3.Foreground = Brushes.White;
            AC4.Foreground = Brushes.White;
        }
        public void ac3()
        {
            ACoff.Foreground = Brushes.White;
            ACauto.Foreground = Brushes.White;
            AC1.Foreground = Brushes.White;
            AC2.Foreground = Brushes.White;
            AC3.Foreground = Brushes.Green;
            AC4.Foreground = Brushes.White;
        }
        public void ac4()
        {
            ACoff.Foreground = Brushes.White;
            ACauto.Foreground = Brushes.White;
            AC1.Foreground = Brushes.White;
            AC2.Foreground = Brushes.White;
            AC3.Foreground = Brushes.White;
            AC4.Foreground = Brushes.Green;
        }
    }
}
