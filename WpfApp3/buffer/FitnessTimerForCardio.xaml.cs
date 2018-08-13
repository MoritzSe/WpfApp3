using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp3.buffer
{
    /// <summary>
    /// Interaktionslogik für FitnessTimerForCardio.xaml
    /// </summary>
    public partial class FitnessTimerForCardio : UserControl
    {

        public float Distance
        {
            get { return Distance; }
            set { SetValue(DistanceProperty, value); }
        }

        public int CountdownTime // in Sekunden
        {
            get { return CountdownTime; }
            set { CountdownTime = value; }
        }

        DispatcherTimer timer;
        TimeSpan timeSpan;


        public static readonly DependencyProperty TimerProperty =
            DependencyProperty.Register("Timer", typeof(string), typeof(FitnessTimerForCardio), new UIPropertyMetadata(null));
        public static readonly DependencyProperty DistanceProperty =
            DependencyProperty.Register("Distance", typeof(float), typeof(FitnessTimerForCardio), new UIPropertyMetadata(null));

        public FitnessTimerForCardio()
        {
            InitializeComponent();
            SetValue(DistanceProperty, 10);
        }

        public void setTimer(int count, TimeSpan interval)
        {
            countdown(count, interval, cur => tb.Text = cur.ToString());
        }

      //  public void setTimer()
      //  {
      //      timeSpan = TimeSpan.FromSeconds(CountdownTime);
      //      timer = new DispatcherTimer(timeSpan, DispatcherPriority.Normal, delegate
      //      {
                
      //          timer.Text = timeSpan.ToString("c");
      //          if(timeSpan == TimeSpan.Zero)
      //          {
      //              timer.Stop();
      //          }
      //          timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
      //      },Application.Current.Dispatcher);
      //      timer.Start();
      //  }

        private void countdown(int count, TimeSpan interval, Action<int> ts)
        {
            var dt = new System.Windows.Threading.DispatcherTimer();
            dt.Interval = interval;
            dt.Tick += (_, a) =>
            {
                if (count == 0)
                    dt.Stop();
                else
                    ts(count);
            };
            ts(count);
            dt.Start();
        }

        
    }
}
