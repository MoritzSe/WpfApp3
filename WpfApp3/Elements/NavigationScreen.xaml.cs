using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
using WpfApp3.Elements.NavigationSub;

namespace WpfApp3.Elements
{
    /// <summary>
    /// Interaktionslogik für Navigation.xaml
    /// </summary>
    public partial class NavigationScreen : UserControl
    {
        private Navi_1 nav1 = new Navi_1();
        private Navi_2 nav2 = new Navi_2();
        private Navi_3 nav3 = new Navi_3();
        private Navi_4 nav4 = new Navi_4();


        public int navsubframekey = 1;
        public NavigationScreen()
        {
            InitializeComponent();
            NaviSubFrame.Navigate(nav1);
        }

        public void changesubframe(int subframekey, int direction) // direction 1 is to the right 2 is to the left
        {
            
            DoubleAnimation blendout = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            DoubleAnimation blendin = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            switch (subframekey)
            {
                case 1: //nav1
                                        
                    if (direction == 1)
                    {
                        NaviSubFrame.Navigate(nav4);

                        nav4.BeginAnimation(Frame.OpacityProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        NaviSubFrame.Navigate(nav2);

                        nav2.BeginAnimation(Frame.OpacityProperty, blendin);
                    }
                    nav1.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;


                case 2: //nav2

                    if (direction == 1)
                    {
                        NaviSubFrame.Navigate(nav1);

                        nav1.BeginAnimation(Frame.OpacityProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        NaviSubFrame.Navigate(nav3);

                        nav3.BeginAnimation(Frame.OpacityProperty, blendin);
                    }
                    nav2.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;

                case 3: //nav3

                    if (direction == 1)
                    {
                        NaviSubFrame.Navigate(nav2);

                        nav2.BeginAnimation(Frame.OpacityProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        NaviSubFrame.Navigate(nav4);

                        nav4.BeginAnimation(Frame.OpacityProperty, blendin);
                    }
                    nav3.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;

                case 4: //nav4

                    if (direction == 1)
                    {
                        NaviSubFrame.Navigate(nav3);

                        nav3.BeginAnimation(Frame.OpacityProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        NaviSubFrame.Navigate(nav1);

                        nav1.BeginAnimation(Frame.OpacityProperty, blendin);
                    }
                    nav4.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;

                default:
                    Debug.Print("no case called");
                    break;

            }
        }

        public void changesubframe(int subframekey)
        {
            Debug.Print("changesubframe in Navigation called with subframekey: " + navsubframekey);
            switch (subframekey)
            {
                case 1:

                    NaviSubFrame.Navigate(nav1);
                    break;
                case 2:
                    NaviSubFrame.Navigate(nav2);
                    break;
                case 3:
                    NaviSubFrame.Navigate(nav3);
                    break;
                case 4:
                    NaviSubFrame.Navigate(nav4);
                    break;
                default:
                    Debug.Print("no case called in NaviSubFrame");// ruf den falschen key auf beim welchsel von links auf rechts oder anderhern wegen der reihenfolge! nachshcaun wie bei main gemacht und übernehmen!
                    break;

            }
        }

        public void changesubframekey(int plusminus) //1 is plus, 2 is minus
        {
            switch (plusminus)
            {
                case 1:
                    if (navsubframekey == 4)
                    {
                        navsubframekey = 1;
                    }
                    else
                    {
                        navsubframekey++;
                    }
                    Debug.Print("SubframeKey nach case 1: " + navsubframekey);
                    break;


                case 2:
                    if (navsubframekey == 1)
                    {
                        navsubframekey = 4;
                    }
                    else
                    {
                        navsubframekey--;
                    }

                    break;

                default:

                    Debug.Print("default called in changesubframekey");
                    break;

            }



        }

        public void highlightsubmenue (int navsubframekey)
        {
            DoubleAnimation opaout = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(500)
            };
            DoubleAnimation opain = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(500)
            };
            
            Debug.Print("Aminations built for Navigation Highlighter");
            switch (navsubframekey) //mainframekey: 1 is ziel, 2 is letzte ziele, 3 is POI, 4 is MapTanken
            {
                case 1:  //Ziel
                   
                    Address.BeginAnimation(DropShadowEffect.OpacityProperty, opain);

                    if (FuelMap.Opacity == 0)
                    {
                        FuelMap.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                        Debug.Print("nav highlighter if 1 called");
                    }
                    if (Recent.Opacity == 1)
                    {
                        Recent.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                        Debug.Print("nav highligther if 2 called");
                    }

                    Debug.Print("Case 1 called");
                    break;

                case 2: //Recent
                    Recent.BeginAnimation(DropShadowEffect.OpacityProperty, opain);

                    if (Address.Opacity == 1)
                    {
                        Address.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    if (POI.Opacity == 0)
                    {
                        POI.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    Debug.Print("Case 2 called");
                    break;

                case 3: //POI
                    POI.BeginAnimation(DropShadowEffect.OpacityProperty, opain);

                    if (Recent.Opacity == 0)
                    {
                        Recent.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    if (FuelMap.Opacity == 0)
                    {
                        FuelMap.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    Debug.Print("Case 3 called");
                    break;

                case 4: //FuelMap
                    FuelMap.BeginAnimation(DropShadowEffect.OpacityProperty, opain);

                    if (POI.Opacity == 0)
                    {
                        POI.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    if (Address.Opacity == 0)
                    {
                        Address.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    Debug.Print("Case 4 called");
                    break;

                default:
                    Debug.Print("Default called");
                    break;
            }

        }
    }
    
}
