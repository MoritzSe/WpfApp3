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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp3.Elements
{
    /// <summary>
    /// Interaktionslogik für Time.xaml
    /// </summary>
    public partial class Time : UserControl
    {
        public Time()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1),
                DispatcherPriority.Normal, delegate
                {
                    double hour = DateTime.Now.Hour;
                    double minute = DateTime.Now.Minute;

                    this.dateText.Text = hour + ":" +minute;
                }, this.Dispatcher);
        }
    }
}
