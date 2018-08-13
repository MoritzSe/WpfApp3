using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfApp3.Elements
{
    /// <summary>
    /// Interaktionslogik für SettingMenue.xaml
    /// </summary>
    public partial class SettingMenue : UserControl
    {
        public SettingMenue()
        {
            InitializeComponent();
        }

        public void Highlight(int key) // key gibt an welcher Teil gehighlighted werden soll - entscheidet selbst, welcher ausgeblendet werden muss
        {
            DoubleAnimation fadeout = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(200)
            };

            DoubleAnimation fadein = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(200)
            };

            switch (key)
            {
                case 1: //Sitz

                    if (LichtEffekt.Opacity == 1)
                    {
                        LichtEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    if (LenkradEffekt.Opacity == 1)
                    {
                        LenkradEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    SitzEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadein);

                    break;

                case 2: //Lenksäule

                    if (SitzEffekt.Opacity == 1)
                    {
                        SitzEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    if (ACEffekt.Opacity == 1)
                    {
                        ACEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    LenkradEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadein);

                    break;
                case 3: //Klima

                    if (LenkradEffekt.Opacity == 1)
                    {
                        LenkradEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    if (LichtEffekt.Opacity == 1)
                    {
                        LichtEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    ACEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadein);

                    break;
                case 4: //Licht

                    if (ACEffekt.Opacity == 1)
                    {
                        ACEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    if (SitzEffekt.Opacity == 1)
                    {
                        SitzEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    LichtEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadein);

                    break;

                default:

                    Debug.Print("Highlight in NavigationMenue: no case called");

                    break;
            }

        }
    }
}
