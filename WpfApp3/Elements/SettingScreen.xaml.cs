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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.Elements.SettingSub;

namespace WpfApp3.Elements
{
    /// <summary>
    /// Interaktionslogik für SettingScreen.xaml
    /// </summary>
    public partial class SettingScreen : UserControl
    {

        public int setsubframekey = 1;
        public static SubSetting1 subset1 = new SubSetting1();
        public static SubSetting2 subset2 = new SubSetting2();
        public static SubSetting3 subset3 = new SubSetting3();
        public static SubSetting4 subset4 = new SubSetting4();

        public SettingScreen()
        {
            InitializeComponent();
        }

        public void changesubframe(int subframekey, int direction) // direction 1 is to the right 2 is to the left
        {

            DoubleAnimation blendout = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300)
            };
            DoubleAnimation blendin = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(300)
            };
            switch (subframekey)
            {
                case 1: //nav1


                    if (direction == 1)
                    {
                        subset4.BeginAnimation(Frame.MarginProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        subset2.BeginAnimation(Frame.MarginProperty, blendin);
                    }
                    subset1.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;


                case 2: //nav2

                    if (direction == 1)
                    {
                        subset1.BeginAnimation(Frame.MarginProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        subset3.BeginAnimation(Frame.MarginProperty, blendin);
                    }
                    subset2.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;

                case 3: //nav3

                    if (direction == 1)
                    {
                        subset2.BeginAnimation(Frame.MarginProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        subset4.BeginAnimation(Frame.MarginProperty, blendin);
                    }
                    subset3.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;

                case 4: //nav4

                    if (direction == 1)
                    {
                        subset3.BeginAnimation(Frame.MarginProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        subset1.BeginAnimation(Frame.MarginProperty, blendin);
                    }
                    subset4.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;

                default:
                    Debug.Print("no case called");
                    break;

            }
        }

        public void changesubframekey(int plusminus) //1 is plus, 2 is minus
        {
            switch (plusminus)
            {
                case 1:
                    if (setsubframekey == 4)
                    {
                        setsubframekey = 1;
                    }
                    else
                    {
                        setsubframekey++;
                    }
                    Debug.Print("SubframeKey nach case 1: " + setsubframekey);
                    break;


                case 2:
                    if (setsubframekey == 1)
                    {
                        setsubframekey = 4;
                    }
                    else
                    {
                        setsubframekey--;
                    }

                    break;

                default:

                    Debug.Print("default called in changesubframekey");
                    break;

            }
        }
    }
}
