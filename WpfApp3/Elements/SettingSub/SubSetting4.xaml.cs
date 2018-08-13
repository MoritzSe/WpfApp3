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
    /// Interaktionslogik für SubSetting4.xaml
    /// </summary>
    public partial class SubSetting4 : UserControl
    {
        public bool is_on = false;
            

        public SubSetting4()
        {
            InitializeComponent();
        }
    

    public void lighton()
    {
            DoubleAnimation lighton = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            bordereffect.BeginAnimation(DropShadowEffect.OpacityProperty, lighton);
        
    }

    public void lightoff()
    {
            DoubleAnimation lighton = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            bordereffect.BeginAnimation(DropShadowEffect.OpacityProperty, lighton);
        }

    }
}
