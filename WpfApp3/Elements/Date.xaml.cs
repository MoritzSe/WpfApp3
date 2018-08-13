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
    /// Interaktionslogik für Date.xaml
    /// </summary>
    public partial class Date : UserControl
    {
        public Date()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1),
                DispatcherPriority.Normal, delegate
                {
                    double day = DateTime.Now.Day;
                    double month = DateTime.Now.Month;

                    this.dateText.Text = day + "  " + month;
                }, this.Dispatcher);
        }
    }
}
