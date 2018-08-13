using Microsoft.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp3.UDP;

namespace WpfApp3.Elements
{
    /// <summary>
    /// Interaktionslogik für speed.xaml
    /// </summary>
    public partial class speed : UserControl
    {
        private float currentVel = 0;
        #region

        internal PacketSILAB udpSILABspeed
        {
            set
            {
                currentVel = value.SILABvel;

                Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() => updateSpeed(currentVel)));
            }
        }

        public double Velocity
        {
            get { return (double)GetValue(VelocityProperty); }

            set
            {
                SetValue(VelocityProperty, value);
                Debug.Print("VelocityProperty set to: " + value);
                Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    ArcSpeed.EndAngle = value;
                    SpeedVelNumber.Text = value.ToString();
                    Debug.Print("ArcSpeed update invoked with value: " + value);
                }));

            }
        }

        public static readonly DependencyProperty VelocityProperty =
                DependencyProperty.Register("Velocity", typeof(double), typeof(speed), new UIPropertyMetadata(null));

        
        #endregion

        public speed()
        {
            
            InitializeComponent();
            
        }

        public void updateSpeed(float speed)
        {
            float speedmax = 100; // check this
            float speedmin = 0;
            // set textvalue
            SpeedVelNumber.Text = speed.ToString();
            // set anglevalue

            double angle;
            angle = -120 + 2.4 * (double)speed; // -120° bis 120° -> 240° entspricht max km/h -> 2.4 entspricht 1 km/h 
            ArcSpeed.EndAngle = angle;

         
        }



        public void startup()
        {
            Debug.Print("startupcalled");
            DoubleAnimation startupspeedup = new DoubleAnimation
            {
                From = -120,
                To = 120,
                Duration = TimeSpan.FromMilliseconds(500)
            };

            DoubleAnimation startupspeeddown = new DoubleAnimation
            {
                From = 120,
                To = -120,
                Duration = TimeSpan.FromMilliseconds(500)

            };

            ArcSpeed.BeginAnimation(Arc.EndAngleProperty, startupspeedup);
            ArcSpeed.BeginAnimation(Arc.EndAngleProperty, startupspeeddown);

            
            Debug.Print("Animation startup called");
        }
    }
}
