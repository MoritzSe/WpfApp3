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
    /// Interaktionslogik für NavigationManue.xaml
    /// </summary>
    public partial class NavigationMenue : UserControl
    {
        public NavigationMenue()
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
                case 1: //Ziel auswählen

                    if (TankstelleEffekt.Opacity == 1)
                    {
                        TankstelleEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    if(LetzteEffekt.Opacity == 1)
                    {
                        LetzteEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    ZielEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadein);

                    break;

                case 2: //Letzte Ziele

                    if (ZielEffekt.Opacity == 1)
                    {
                        ZielEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    if (POIEffekt.Opacity == 1)
                    {
                        POIEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    LetzteEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadein);

                    break;
                case 3: //Points of Interest

                    if (LetzteEffekt.Opacity == 1)
                    {
                        LetzteEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    if (TankstelleEffekt.Opacity == 1)
                    {
                        TankstelleEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    POIEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadein);

                    break;
                case 4: //Tankstelle

                    if (POIEffekt.Opacity == 1)
                    {
                        POIEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    if (ZielEffekt.Opacity == 1)
                    {
                        ZielEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadeout);
                    }

                    TankstelleEffekt.BeginAnimation(DropShadowEffect.OpacityProperty, fadein);

                    break;

                default:

                    Debug.Print("Highlight in NavigationMenue: no case called");

                    break;
            }

        }
    }
}
