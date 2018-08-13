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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3.Elements.SettingSub
{
    /// <summary>
    /// Interaktionslogik für SupSetting.xaml
    /// </summary>
    public partial class SubSetting1 : UserControl
    {
        public SubSetting1()
        {
            InitializeComponent();
        }

        ColorAnimation active = new ColorAnimation
        {
            From = Colors.Transparent,
            To = Colors.Green,
            Duration = TimeSpan.FromMilliseconds(100)
        };

        ColorAnimation inactive = new ColorAnimation
        {
            From = Colors.Green,
            To = Colors.Transparent,
            Duration = TimeSpan.FromMilliseconds(100)
        };

        public void seatupon()
        {
            TriUp.BeginAnimation(Polygon.FillProperty, active);
        }

        public void seatupoff()
        {
            TriUp.BeginAnimation(Polygon.FillProperty, inactive);
        }

        public void seatbackon()
        {
            TriBack.BeginAnimation(Polygon.FillProperty, active);
        }

        public void seatbackoff()
        {
            TriBack.BeginAnimation(Polygon.FillProperty, inactive);
        }

        public void seatcwon()
        {
            TriCW.BeginAnimation(Polygon.FillProperty, active);
        }

        public void seatcwoff()
        {
            TriCW.BeginAnimation(Polygon.FillProperty, inactive);
        }

        public void seatccwon()
        {
            TriCCW.BeginAnimation(Polygon.FillProperty, active);
        }

        public void seatccwoff()
        {
            TriCCW.BeginAnimation(Polygon.FillProperty, inactive);
        }

    }

}
