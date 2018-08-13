using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using WpfApp3.Elements;
using WpfApp3.UDP;
using WpfApp3.Elements.NavigationSub;
using WpfApp3.Elements.SettingSub;
using WpfApp3.Elements.FitnessSub;

namespace WpfApp3
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region declaring
        public bool fitnessAcitve = false;
        private bool isDrivingHAD = false;
        public bool IsDrivingHAD
        {
            get { return isDrivingHAD; }
            set
            {
                if (isDrivingHAD != value)
                {
                    isDrivingHAD = value;
                }
                //do stuff here
            }
        }
        public bool ManuelDrive = true;
        public Stopwatch stopWatchTakeOver;
        public int mainframekey = 3; //default damit DriveScreen angezeigt wird
        public int navisubframekey = 1;
        public int settingsubframekey = 1;
        public int fitnesssubframekey = 1;
        public bool isinMainManue = true;
        public bool isinNaviMenue = false;
        public bool isinSetMenue = false;

        internal static MainWindow mainWindow;
        internal static NavigationScreen navigationScreen = new NavigationScreen();
        internal static SettingScreen settingScreen = new SettingScreen();
        internal static Navi_1 navi_1 = new Navi_1();
        internal static Navi_2 navi_2 = new Navi_2();
        internal static Navi_3 navi_3 = new Navi_3();
        internal static Navi_4 navi_4 = new Navi_4();
        internal static SubSetting1 subset1 = new SubSetting1();
        internal static SubSetting2 subset2 = new SubSetting2();
        internal static SubSetting3 subset3 = new SubSetting3();
        internal static SubSetting4 subset4 = new SubSetting4();
        internal static FitSub1 subfit1 = new FitSub1();
        internal static FitSub2 subfit2 = new FitSub2();
        internal static FitSub3 subfit3 = new FitSub3();
        internal static FitSub4 subfit4 = new FitSub4();

        internal static SettingMenue settingMenue = new SettingMenue();
        internal static NavigationMenue navigationMenue = new NavigationMenue();
        internal static FitnessMenue fitnessMenue = new FitnessMenue();

        public speed Speed = new speed();
        public rpm Rpm = new rpm();

        internal UDP_Sender udp_sender = new UDP_Sender();
        internal PacketSILAB udpSILAB
        {
            set
            {
                IsDrivingHAD = value.SILABhadActive;

                if (value.SILABhadPossible)
                {
                    udp_sender.sendData("1", "150.150.1.16", 5001); //IP belongs to MiniTablet needs to be changed
                }
                else
                {
                    udp_sender.sendData("2", "150.150.1.16", 5001); // IP belongs to MiniTablet needs to be changed
                }

                Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {

                    Speed.Velocity = (int)value.SILABvel;
                    Rpm.RPM = (int)value.SILABrpm;



                }));
            }
        }

        private int maindirection;
        private int navidirection;
        private int settingdirection;
        private int fitnessdirection;
        internal PacketMenueNavigation udpMenueNavigation
        {
            set
            {
                if (isinMainManue)
                {
                    maindirection = value.Data[1]; // is direction of swipe gesture
                    switch (maindirection)
                    {
                        case 1:
                            //set highlight of lower menue bar
                            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                            {
                                Debug.Print("Previous MainFrameKey :" + mainframekey);
                                int prevbuffer = mainframekey;
                                changemainframekey(2);
                                int buffer = mainframekey;
                                mainmenueborderhighlight(buffer);
                                // changemainframeleft(buffer);
                                changemainframeout(prevbuffer, 2);
                                changemainframein(buffer, 2);
                            }), null);
                            break;

                        case 2:
                            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                            {
                                Debug.Print("Previous MainFrameKey :" + mainframekey);
                                int prevbuffer = mainframekey;
                                changemainframekey(1);
                                int buffer = mainframekey;
                                mainmenueborderhighlight(buffer);
                                // changemainframeleft(buffer);
                                changemainframeout(prevbuffer, 1);
                                changemainframein(buffer, 1);
                            }), null);
                            break;


                    }
                }
                if (isinNaviMenue)
                {
                    navidirection = value.Data[0]; // is direction of swipe gesture
                    switch (navidirection)
                    {
                        case 1:
                            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                            {
                                Debug.Print("Previous NaviSubFrameKey :" + navisubframekey);
                                int prevbuffer = navisubframekey;
                                navigationScreen.changesubframekey(1);
                                int buffer = navisubframekey;
                                navigationScreen.changesubframe(navisubframekey, 1);
                            }), null);
                            break;

                        case 2:
                            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                            {
                                Debug.Print("Previous NaviSubFrameKey :" + navisubframekey);
                                int prevbuffer = navisubframekey;
                                navigationScreen.changesubframekey(2);
                                int buffer = navisubframekey;
                                navigationScreen.changesubframe(navisubframekey, 2);
                            }), null);
                            break;
                    }
                }
                if(isinSetMenue)
                {
                    settingdirection = value.Data[0]; // is direction of swipe gesture
                    switch (settingdirection)
                    {
                        case 1:
                            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                            {
                                Debug.Print("Previous NaviSubFrameKey :" + settingsubframekey);
                                int prevbuffer = settingsubframekey;
                                settingScreen.changesubframekey(1);
                                int buffer = settingsubframekey;
                                settingScreen.changesubframe(settingsubframekey, 1);
                            }), null);
                            break;

                        case 2:
                            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                            {
                                Debug.Print("Previous NaviSubFrameKey :" + settingsubframekey);
                                int prevbuffer = settingsubframekey;
                                settingScreen.changesubframekey(2);
                                int buffer = settingsubframekey;
                                settingScreen.changesubframe(settingsubframekey, 2);
                            }), null);
                            break;
                    }
                }
            }
        }
        
        internal PacketAutopilot udpAutopilot
        {
            set
            {
                int buff = value.Data[0];
                switch(buff)
                {
                    case 1:
                    //Dispatcher.Invoke(new Action(() => changeSpeedToHAF()), null);
                    //Dispatcher.Invoke(new Action(() => changeRpmToHAF()), null);
                    break;

                    case 2:
                    //Dispatcher.Invoke(new Action(() => changeRpmToManuel()), null);
                    //Dispatcher.Invoke(new Action(() => changeSpeedToManuel()), null);
                    break;
                }
               
            }
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            loadspeedelement();
            loadrpmelement();
            loadframes();
            ClockFrame.Navigate(new Elements.Time());
            DateFrame.Navigate(new Elements.Date());
            stopWatchTakeOver = new Stopwatch();
            mainWindow = this;
            
            
        }

        public void startup()
        {
            // call startup in Speed and Rpm
            Speed.startup();
        }

        public void changenavisubframe (int subframekey)
        {

            // Add Animation for blending in and out
            switch(subframekey)
            {
                case 1:
                    navigationScreen.NaviSubFrame.Navigate(navi_1);
                    break;

                case 2:
                    navigationScreen.NaviSubFrame.Navigate(navi_2);
                    break;

                case 3:
                    navigationScreen.NaviSubFrame.Navigate(navi_3);
                    break;

                case 4:
                    navigationScreen.NaviSubFrame.Navigate(navi_4);
                    break;

                default:
                    Debug.Print("changenavisubframe did not call any case, subframekey: " + subframekey);
                    break;
            }
        }

        public void loadframes()
        {
            FitnessFrame.Navigate(new Elements.FitnessScreen());
            DriveFrame.Navigate(new Elements.DriveScreen());
            SettingFrame.Navigate(new Elements.SettingScreen());
            NavigationFrame.Navigate(navigationScreen);
            BorderDrive.BorderThickness = new Thickness(10);
        }

        public void changenavisubframe(int subframekey, int direction) // direction 1 is to the right 2 is to the left
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
                        navi_4.BeginAnimation(Frame.MarginProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        navi_2.BeginAnimation(Frame.MarginProperty, blendin);
                    }
                    navi_1.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;


                case 2: //nav2

                    if (direction == 1)
                    {
                        navi_1.BeginAnimation(Frame.MarginProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        navi_3.BeginAnimation(Frame.MarginProperty, blendin);
                    }
                    navi_2.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;

                case 3: //nav3

                    if (direction == 1)
                    {
                        navi_2.BeginAnimation(Frame.MarginProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        navi_4.BeginAnimation(Frame.MarginProperty, blendin);
                    }
                    navi_3.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;

                case 4: //nav4

                    if (direction == 1)
                    {
                        navi_3.BeginAnimation(Frame.MarginProperty, blendin);

                    }
                    else if (direction == 2)
                    {
                        navi_1.BeginAnimation(Frame.MarginProperty, blendin);
                    }
                    navi_4.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;

                default:
                    Debug.Print("no case called");
                    break;

            }
        }

        public void changemainframeout (int mainframekey, int direction) // direction 1 is right 2 is left
        {
            ThicknessAnimation moveoutright = new ThicknessAnimation()
            {
                From = new Thickness(4200, 0, 0, 50),
                To = new Thickness(6200, 0, 0, 50),
                Duration = TimeSpan.FromMilliseconds(500),
                AccelerationRatio = 0.4
            };

            ThicknessAnimation moveoutleft = new ThicknessAnimation()
            {
                From = new Thickness(4200, 0, 0, 50),
                To = new Thickness(2200, 0, 0, 50),
                Duration = TimeSpan.FromMilliseconds(500),
                AccelerationRatio = 0.4
            };

            DoubleAnimation blendout = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300)
            };
            switch(mainframekey)
            {
                case 1: //setting
                    if(direction == 1)
                    {
                        SettingFrame.BeginAnimation(Frame.MarginProperty, moveoutright);
                    }
                    else if (direction == 2)
                    {
                        SettingFrame.BeginAnimation(Frame.MarginProperty, moveoutleft);
                    }
                    SettingFrame.BeginAnimation(Frame.OpacityProperty, blendout);

                    break;


                case 2: //navigation
                    if (direction == 1)
                    {
                        NavigationFrame.BeginAnimation(Frame.MarginProperty, moveoutright);
                    }
                    else if (direction == 2)
                    {
                        NavigationFrame.BeginAnimation(Frame.MarginProperty, moveoutleft);
                    }
                    NavigationFrame.BeginAnimation(Frame.OpacityProperty, blendout);

                    break;

                case 3: //drive
                    if (direction == 1)
                    {
                        DriveFrame.BeginAnimation(Frame.MarginProperty, moveoutright);
                    }
                    else if (direction == 2)
                    {
                        DriveFrame.BeginAnimation(Frame.MarginProperty, moveoutleft);
                    }
                    DriveFrame.BeginAnimation(Frame.OpacityProperty, blendout);

                    break;

                case 4: //fitness
                    if (direction == 1)
                    {
                        FitnessFrame.BeginAnimation(Frame.MarginProperty, moveoutright);
                    }
                    else if (direction == 2)
                    {
                        FitnessFrame.BeginAnimation(Frame.MarginProperty, moveoutleft);
                    }
                    FitnessFrame.BeginAnimation(Frame.OpacityProperty, blendout);
                    break;

                default:
                    Debug.Print("no case called");
                    break;

            }
        }

        public void changemainframein(int mainframekey, int direction) // direction 1 is right 2 is left
        {
            ThicknessAnimation moveinright = new ThicknessAnimation()
            {
                From = new Thickness(6200, 0, 0, 50),
                To = new Thickness(4200, 0, 0, 50),
                Duration = TimeSpan.FromMilliseconds(500),
                AccelerationRatio = 0.4
            };

            ThicknessAnimation moveinleft = new ThicknessAnimation()
            {
                From = new Thickness(2200, 0, 0, 50),
                To = new Thickness(4200, 0, 0, 50),
                Duration = TimeSpan.FromMilliseconds(500),
                AccelerationRatio = 0.4
            };

            DoubleAnimation blendin = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(300)
            };
            switch (mainframekey)
            {
                case 1: //setting
                    if (direction == 1)
                    {
                        SettingFrame.BeginAnimation(Frame.MarginProperty, moveinleft);
                    }
                    else if (direction == 2)
                    {
                        SettingFrame.BeginAnimation(Frame.MarginProperty, moveinright);
                    }
                    SettingFrame.BeginAnimation(Frame.OpacityProperty, blendin);

                    break;


                case 2: //navigation
                    if (direction == 1)
                    {
                        NavigationFrame.BeginAnimation(Frame.MarginProperty, moveinleft);
                    }
                    else if (direction == 2)
                    {
                        NavigationFrame.BeginAnimation(Frame.MarginProperty, moveinright);
                    }
                    NavigationFrame.BeginAnimation(Frame.OpacityProperty, blendin);

                    break;

                case 3: //drive
                    if (direction == 1)
                    {
                        DriveFrame.BeginAnimation(Frame.MarginProperty, moveinleft);
                    }
                    else if (direction == 2)
                    {
                        DriveFrame.BeginAnimation(Frame.MarginProperty, moveinright);
                    }
                    DriveFrame.BeginAnimation(Frame.OpacityProperty, blendin);

                    break;

                case 4: //fitness
                    if (direction == 1)
                    {
                        FitnessFrame.BeginAnimation(Frame.MarginProperty, moveinleft);
                    }
                    else if (direction == 2)
                    {
                        FitnessFrame.BeginAnimation(Frame.MarginProperty, moveinright);
                    }
                    FitnessFrame.BeginAnimation(Frame.OpacityProperty, blendin);
                    break;

                default:
                    Debug.Print("no case called");
                    break;

            }
        }

        public void changemainframeright(int mainframekey) // int direction (1 or 2) 1 is right 2 is left
        {

            var righttransfrommain = new ThicknessAnimation()
            {
                From = new Thickness(4000, 0, 0, 0),
                To = new Thickness(2000, 0, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500)
            };

            var righttranstomain = new ThicknessAnimation()
            {
                From = new Thickness(6000, 0, 0, 0),
                To = new Thickness(4000, 0, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500)
            };

            var rightopacityfrommain = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(500)
            };

            var rightopacitytomain = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(500)
            };

            // mainframekey: 1 is settingscreen, 2 is navigation, 3 is drivescreen, 4 is fitnessscreen

            //right -> nach links gewischt

            switch (mainframekey)
            {
                case 1: //von setting nach navigation
                    SettingFrame.BeginAnimation(Frame.MarginProperty, righttransfrommain);
                    NavigationFrame.BeginAnimation(Frame.MarginProperty, righttranstomain);
                    SettingFrame.BeginAnimation(Frame.OpacityProperty, rightopacityfrommain);
                    NavigationFrame.BeginAnimation(Frame.OpacityProperty, rightopacitytomain);

                    break;


                case 2: //von navigation nach drive
                    NavigationFrame.BeginAnimation(Frame.MarginProperty, righttransfrommain);
                    DriveFrame.BeginAnimation(Frame.MarginProperty, righttranstomain);
                    NavigationFrame.BeginAnimation(Frame.OpacityProperty, rightopacityfrommain);
                    DriveFrame.BeginAnimation(Frame.OpacityProperty, rightopacitytomain);

                    break;

                case 3: // von drive nach fitness
                    DriveFrame.BeginAnimation(Frame.MarginProperty, righttransfrommain);
                    FitnessFrame.BeginAnimation(Frame.MarginProperty, righttranstomain);
                    DriveFrame.BeginAnimation(Frame.OpacityProperty, rightopacityfrommain);
                    FitnessFrame.BeginAnimation(Frame.OpacityProperty, rightopacitytomain);

                    break;

                case 4: // von fitness nach setting
                    FitnessFrame.BeginAnimation(Frame.MarginProperty, righttransfrommain);
                    SettingFrame.BeginAnimation(Frame.MarginProperty, righttranstomain);
                    FitnessFrame.BeginAnimation(Frame.OpacityProperty, rightopacityfrommain);
                    SettingFrame.BeginAnimation(Frame.OpacityProperty, rightopacitytomain);
                    break;




            }
            
        }

        public void changemainframeleft(int mainframekey)
        {

            Debug.Print("changemainframeleft called");
            var lefttransfrommain = new ThicknessAnimation()
            {
                From = new Thickness(4000, 0, 0, 0),
                To = new Thickness(6000, 0, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500),
                
            };
            
            var lefttranstomain = new ThicknessAnimation()
            {
                From = new Thickness(2000, 0, 0, 0),
                To = new Thickness(4000, 0, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500)
            };

            var leftopacityfrommain = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(500)
            };

            var leftopacitytomain = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(500)
            };

            Debug.Print("Animations built");
            // mainframekey: 1 is settingscreen, 2 is navigation, 3 is drivescreen, 4 is fitnessscreen
            switch (mainframekey) // gibt an welcher screen angezeigt wird
            
            {
                case 0:
                    Debug.Print("case 0 called");
                    break;

                case 1: // von setting nach fitness

                    SettingFrame.BeginAnimation(Frame.MarginProperty, lefttransfrommain);
                    FitnessFrame.BeginAnimation(Frame.MarginProperty, lefttranstomain);
                    SettingFrame.BeginAnimation(Frame.OpacityProperty, leftopacityfrommain);
                    FitnessFrame.BeginAnimation(Frame.OpacityProperty, leftopacitytomain);
                    Debug.Print("Case 1");
                    break;

                case 2: // von navigation nach setting

                    NavigationFrame.BeginAnimation(Frame.MarginProperty, lefttransfrommain);
                    SettingFrame.BeginAnimation(Frame.MarginProperty, lefttranstomain);
                    NavigationFrame.BeginAnimation(Frame.OpacityProperty, leftopacityfrommain);
                    SettingFrame.BeginAnimation(Frame.OpacityProperty, leftopacitytomain);
                    Debug.Print("Case 2");
                    break;

                    //case 3: // von drive nach navigation

                    DriveFrame.BeginAnimation(Frame.MarginProperty, lefttransfrommain);
                    NavigationFrame.BeginAnimation(Frame.MarginProperty, lefttranstomain);
                    DriveFrame.BeginAnimation(Frame.OpacityProperty, leftopacityfrommain);
                    NavigationFrame.BeginAnimation(Frame.OpacityProperty, leftopacitytomain);
                    Debug.Print("Case 3");
                    break;

                case 4: // von fitness nach drive

                    FitnessFrame.BeginAnimation(Frame.MarginProperty, lefttransfrommain);
                    DriveFrame.BeginAnimation(Frame.MarginProperty, lefttranstomain);
                    FitnessFrame.BeginAnimation(Frame.OpacityProperty, leftopacityfrommain);
                    DriveFrame.BeginAnimation(Frame.OpacityProperty, leftopacitytomain);
                    Debug.Print("Case 4");
                    break;

                default:
                    Debug.Print("no case called");
                    break;
            }
            
            

        }
   
        public void changemainframekey (int plusminus) //1 is plus, 2 is minus
        {
            switch (plusminus)
            {
                case 1:
                    if(mainframekey == 4)
                    {
                        mainframekey = 1;
                    }
                    else
                    {
                        mainframekey++;
                    }
                    Debug.Print("MainframeKey nach case 1: " + mainframekey);
                    break;


                case 2:
                    if(mainframekey == 1)
                    {
                        mainframekey = 4;
                    }
                    else
                    {
                        mainframekey--;
                    }

                    break;

                default:

                    Debug.Print("default called in changemainframekey");
                    break;

            }
            
            
           
        }

        public void changesubmenue (int key) // Aktuell
        {
            switch (key)
            {
                case 1:
                    SubMenueFrame.Navigate(settingMenue);
                    break;

                case 2:
                    SubMenueFrame.Navigate(navigationMenue);
                    break;

                case 3:
                    SubMenueFrame.Navigate(null);
                    break;

                case 4:
                    SubMenueFrame.Navigate(fitnessMenue);
                    break;

                default:
                    Debug.Print("submenue could not be changed");
                    break;

            }
        }

        public void mainmenueborderhighlight (int mainframekey)
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
            ThicknessAnimation thickout = new ThicknessAnimation()
            {
                From = new Thickness(10),
                To = new Thickness(2),
                Duration = TimeSpan.FromMilliseconds(500)
            };
            ThicknessAnimation thickin = new ThicknessAnimation()
            {
                From = new Thickness(2),
                To = new Thickness(10),
                Duration = TimeSpan.FromMilliseconds(500)
            };
            Debug.Print("Aminations built for Highlighter");
            switch(mainframekey) //mainframekey: 1 is settingscreen, 2 is navigation, 3 is drivescreen, 4 is fitnessscreen
            {
                case 1:  //Setting
                    BorderSetting.BeginAnimation(Border.BorderThicknessProperty, thickin);                   
                    BorderSettingShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opain);
                    
                    if(BorderFitness.BorderThickness == new Thickness(10))
                    {
                        BorderFitness.BeginAnimation(Border.BorderThicknessProperty, thickout);
                        BorderFitnessShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    if (BorderNavigation.BorderThickness == new Thickness(10))
                    {
                        BorderNavigation.BeginAnimation(Border.BorderThicknessProperty, thickout);
                        BorderNavigationShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }

                    Debug.Print("Case 1 called");
                    break;

                case 2: //Navgation
                    BorderNavigation.BeginAnimation(Border.BorderThicknessProperty, thickin);
                    BorderNavigationShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opain);

                    if (BorderDrive.BorderThickness == new Thickness(10))
                    {
                        BorderDrive.BeginAnimation(Border.BorderThicknessProperty, thickout);
                        BorderDriveShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    if (BorderSetting.BorderThickness == new Thickness(10))
                    {
                        BorderSetting.BeginAnimation(Border.BorderThicknessProperty, thickout);
                        BorderSettingShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    Debug.Print("Case 2 called");
                    break;

                case 3: //Drive
                    BorderDrive.BeginAnimation(Border.BorderThicknessProperty, thickin);
                    BorderDriveShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opain);

                    if (BorderNavigation.BorderThickness == new Thickness(10))
                    {
                        BorderNavigation.BeginAnimation(Border.BorderThicknessProperty, thickout);
                        BorderNavigationShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    if (BorderFitness.BorderThickness == new Thickness(10))
                    {
                        BorderFitness.BeginAnimation(Border.BorderThicknessProperty, thickout);
                        BorderFitnessShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    Debug.Print("Case 3 called");
                    break;

                case 4: //Fitness
                    BorderFitness.BeginAnimation(Border.BorderThicknessProperty, thickin);
                    BorderFitnessShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opain);

                    if (BorderDrive.BorderThickness == new Thickness(10))
                    {
                        BorderDrive.BeginAnimation(Border.BorderThicknessProperty, thickout);
                        BorderDriveShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    if (BorderSetting.BorderThickness == new Thickness(10))
                    {
                        BorderSetting.BeginAnimation(Border.BorderThicknessProperty, thickout);
                        BorderSettingShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opaout);
                    }
                    Debug.Print("Case 4 called");
                    break;

                default:
                    Debug.Print("Default called");
                    break;
            }

        } //Aktuell
            
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            new UDP.UDP_Receiver();
        }

        public void changeMode()
        {
            if (ManuelDrive == false)
            {

                //FrameSpeed.NavigationService.Navigate(new Elements.speed());
                //FrameSpeed.NavigationService.Navigate(new Elements.rpm());

                // change to HAF Size
    


            }
            if(ManuelDrive == true)
            {
                // change to ManuelDrive Size


            }
            //ManuelDrive != ManuelDrive; Test
        }

        public void Click(object sender, RoutedEventArgs e)
        {
            //changeSpeedToHAF();
            //changeRpmToHAF();
            isinMainManue = false;
        }
        public void Click2 (object sender, RoutedEventArgs e)
        {
            //changeRpmToManuel();
            //changeSpeedToManuel();
            isinMainManue = true;
        }
        public void left(object sender, RoutedEventArgs e)
        { 
            Debug.Print("left clicked");
            if(isinMainManue)
            {
                Debug.Print("Previous MainFrameKey :" + mainframekey);
                int prevbuffer = mainframekey;
                changemainframekey(2);
                int buffer = mainframekey;
                changesubmenue(buffer);
                mainmenueborderhighlight(buffer);
               // changemainframeleft(buffer);
                changemainframeout(prevbuffer, 2);
                changemainframein(buffer, 2);

            }
            else
            {
                int prevbuffer = navigationScreen.navsubframekey;
                navigationScreen.changesubframekey(2);
                int buffer = navigationScreen.navsubframekey;
                navigationScreen.changesubframe(prevbuffer,2);
                
                navigationScreen.highlightsubmenue(buffer);
            }
            

            Debug.Print("new MainFrameKey: " + mainframekey);
            //Speed.Velocity = 100;
            Rpm.updateRpm(10);
            Speed.updateSpeed(10);
            //Rpm update does not work
            littlerpmframe.Refresh();
            littlespeendframe.Refresh();

            udp_sender.sendData("2", "127.0.0.1", 8002);


            
             
        }
        public void right(object sender, RoutedEventArgs e)
        {
            Debug.Print("right clicked");
            if (isinMainManue)
            {
                Debug.Print("Previous MainFrameKey :" + mainframekey);
                int prevbuffer = mainframekey;
                changemainframekey(1);
                int buffer = mainframekey;
                changesubmenue(buffer);
                mainmenueborderhighlight(buffer);
                changemainframeout(prevbuffer, 1);
                changemainframein(buffer, 1);
                //changemainframeright(buffer);
                Debug.Print("new MainFrameKey: " + mainframekey);
                udp_sender.sendData("1", "127.0.0.1", 8002);
            }else
            {
                int prevbuffer = navigationScreen.navsubframekey;
                navigationScreen.changesubframekey(1);
                int buffer = navigationScreen.navsubframekey;
                navigationScreen.changesubframe(prevbuffer, 1);

                navigationScreen.highlightsubmenue(buffer);
            }

               
            //Speed.Velocity = 0;
            Speed.updateSpeed(50);
            littlespeendframe.Refresh(); //Refresh nicht zwingend notwendig
            Rpm.updateRpm(50);
            littlerpmframe.Refresh();
        }
       
        //public void changeSpeedToHAF()
        //{
        //    /*
        //    Duration durationsth = new Duration(TimeSpan.FromMilliseconds(1000)); // hier sehen bei Bildschrim richtiger Größe, welche Geschwindigkeit angemessen ist
        //    Thickness thicknesssth = new Thickness(0, 0, 0, 0); // hier sehen welche Alignments by default vertreten sind, dementsprechend anpassen
        //    ThicknessAnimation animatespeedtohaf = new ThicknessAnimation(thicknesssth,durationsth);
        //    FrameSpeed.BeginAnimation(Frame.MarginProperty, animatespeedtohaf);
        //    */
        //    double ratio = 150 / 350;
        //    var speedscalex = new DoubleAnimation()
        //    {
        //        From = 1,
        //        To = 0.7,
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    var speedscaley = new DoubleAnimation()
        //    {
        //        From = 1,
        //        To = 0.7,
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    scale1.BeginAnimation(ScaleTransform.ScaleXProperty, speedscalex);
        //    scale1.BeginAnimation(ScaleTransform.ScaleYProperty, speedscaley);
        //    // 400 to left, 150 to bottom 
            
        //    var speedmargin = new ThicknessAnimation()
        //    {
        //        From = new Thickness(400, 150, 0, 0),
        //        To = new Thickness(0, 300, 0, 0),
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    littlespeendframe.BeginAnimation(Frame.MarginProperty, speedmargin); 
        //}

        //public void changeRpmToHAF()
        //{  

        //    var rpmscalex = new DoubleAnimation()
        //    {
        //        From = 1,
        //        To = 0.7,
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    var rpmscaley = new DoubleAnimation()
        //    {
        //        From = 1,
        //        To = 0.7,
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    scale2.BeginAnimation(ScaleTransform.ScaleXProperty, rpmscalex);
        //    scale2.BeginAnimation(ScaleTransform.ScaleYProperty, rpmscaley);
        //    // 400 to left, 150 to bottom 
            
        //    var rpmmargin = new ThicknessAnimation()
        //    {
        //        From = new Thickness(0, 150, 400, 0),
        //        To = new Thickness(400, 300, 0, 0),
        //        Duration = TimeSpan.FromMilliseconds(1000) 
        //    };
        //    littlerpmframe.BeginAnimation(Frame.MarginProperty, rpmmargin);

        //    /* Duration durationrth = new Duration(TimeSpan.FromMilliseconds(1000));
        //     Thickness thicknessrth = new Thickness(0, 0, 0, 0);
        //     ThicknessAnimation animaterpmtohaf = new ThicknessAnimation(thicknessrth, durationrth);
        //     FrameRPM.BeginAnimation(Frame.MarginProperty, animaterpmtohaf);
        //     */
        //    /*
        //     // So mach ma's, nur noch anpassen
        //    var stx = new DoubleAnimation()
        //    {
        //        From = 1,
        //        To = 0.5,
        //        Duration = TimeSpan.FromMilliseconds(1000),
        //    };

        //    var sty = new DoubleAnimation()
        //    {
        //        From = 1,
        //        To = 0.5,
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    scale.BeginAnimation(ScaleTransform.ScaleXProperty, stx);
        //    scale.BeginAnimation(ScaleTransform.ScaleYProperty, sty);

        //    var ami1 = new ThicknessAnimation()
        //    {
        //        From = new Thickness(0, 0, 0, 0),
        //        To = new Thickness(50, 50, 0, 0),
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    frame.BeginAnimation(Frame.MarginProperty, ami1);
        //     */
        //}

        //public void changeSpeedToManuel()
        //{
        //    var speedscalex = new DoubleAnimation()
        //    {
        //        From = 0.7,
        //        To = 1,
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    var speedscaley = new DoubleAnimation()
        //    {
        //        From = 0.7,
        //        To = 1,
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    scale1.BeginAnimation(ScaleTransform.ScaleXProperty, speedscalex);
        //    scale1.BeginAnimation(ScaleTransform.ScaleYProperty, speedscaley);
        //    // 400 to left, 150 to bottom
            
        //    var speedmargin = new ThicknessAnimation()
        //    {
        //        From = new Thickness(0, 300, 0, 0),
        //        To = new Thickness(400, 150, 0, 0),
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    littlespeendframe.BeginAnimation(Frame.MarginProperty, speedmargin); 
        //}

        //public void changeRpmToManuel()
        //{
        //    var rpmscalex = new DoubleAnimation()
        //    {
        //        From = 0.7,
        //        To = 1,
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    var rpmscaley = new DoubleAnimation()
        //    {
        //        From = 0.7,
        //        To = 1,
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    scale2.BeginAnimation(ScaleTransform.ScaleXProperty, rpmscalex);
        //    scale2.BeginAnimation(ScaleTransform.ScaleYProperty, rpmscaley);
        //    // 400 to left, 150 to bottom
            
        //    var rpmmargin = new ThicknessAnimation()
        //    {
        //        From = new Thickness(400, 300, 0, 0),
        //        To = new Thickness(0, 150, 400, 0),
        //        Duration = TimeSpan.FromMilliseconds(1000)
        //    };
        //    littlerpmframe.BeginAnimation(Frame.MarginProperty, rpmmargin); 
        //}

        public void loadspeedelement()
        {

           
                littlespeendframe.Navigate(Speed);
            
        }

        public void loadrpmelement()
        {
            littlerpmframe.Navigate(Rpm);
            // do same here, change MainWindow.xaml respectivly
        }

        public void startTakeoverScenario ()
        {
            if(!ManuelDrive)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    stopWatchTakeOver.Start();
                    //start TakeOverTimer on Screen
                    
                    /*
                     * if (training aktiv -> sofort beenden
                     * Was tun bei Stehposition?
                     * Bei Sitzposition erst nach Abfrage zur Fahrposition fahren?
                     * 
                     * Eventuell Abfrage ob TakeOver bereits angezeigt wird (_grid.Children.Contains(TakeOverElement)) -> (_grid.Children.Add(TakeOverElement))
                     * 
                    */
                }), null);
            }
        } //not done

        public void startAutopilot()
        {
            // Sende an SILAB um autonom zu fahren.
            udp_sender.sendData("1 oder 0", "ip silab", 1); //Werte für SILAB anpassen

            // auf Antwort von SIALB warten das HAD aktiv ist

            isDrivingHAD = true;

        }

        public void startFitness ()
        {
            if(isDrivingHAD)
            {
                FitnessFrame.Navigate(null);
                fitnessAcitve = true;

            }
            else
            {
                Debug.Print("Fitness not available isDrivingHAD: " + isDrivingHAD);
            }
        }

        public void endFitness()
        {
            SubFrame.Navigate(null);
            FitnessFrame.Navigate(new Elements.FitnessScreen()); //to be changed
            fitnessAcitve = false;
        }

        


        /* Timer für Übernahmesituation
         * Übernahmesituation entweder von Autopilotanfrage vom Tablet oder weil muss
         * Hauptmenüs laden und entladen
         * Untermenüs laden und entladen
         * Fitness funktion */





    }
}
