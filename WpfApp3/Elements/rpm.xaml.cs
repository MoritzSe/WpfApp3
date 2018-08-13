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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp3.UDP;

namespace WpfApp3.Elements
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    
    
    public partial class rpm : UserControl
    {

        private float currentRpm = 0;

        #region
        internal PacketSILAB packetSILABrpm
        {
            set
            {
                currentRpm = value.SILABrpm;

                Dispatcher.Invoke(DispatcherPriority.Normal,(Action)(() => updateRpm(currentRpm)));
            }
        }

        #endregion

        public double RPM
        {
            get { return (double)GetValue(RPMProperty); }

            set
            {
                SetValue(RPMProperty, value);
                Debug.Print("RPMProperty set to: " + value);
                Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    ArcRpm.EndAngle = value;
                    RpmVelNumber.Text = value.ToString();
                    Debug.Print("ArcRpm update invoked with value: " + value);
                }));

            }
        }
        public static readonly DependencyProperty RPMProperty =
            DependencyProperty.Register("RPM", typeof(double), typeof(rpm), new UIPropertyMetadata(null));

        public rpm()
        {
            InitializeComponent();
        }
        //public or private?
        public void updateRpm (float rpm)
        {
            Debug.Print("updaterpm called with value rpm: "+rpm);
            float rpmmax = 100; // check this
            float rpmmin = 0;
            // set textvalue
            RpmVelNumber.Text = rpm.ToString();
            // set anglevalue

            double angle;
            angle = -120 + 2.4 * (double)rpm; // maximale rpm rausfinden, übernehmen, bisher selbes wie bei speed
            ArcRpm.EndAngle = angle;
            Debug.Print("new angle: " + angle);
        }
    }
}
