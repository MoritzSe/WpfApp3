using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Training
{
    class Exercise : INotifyPropertyChanged
    {
        /* Eine Übung hat einen Namen, einen Satzcounter, einen Wiederholungscounter, ein Bild, ein hinterlegtes Tutorialvideo, 
         * einen Verweis auf die zugehörigen Sensoren (UDP Receive)
        */
        private string name;
        private string videopath;
        private int amount_sets; // Anzahl der Sets für die jeweilige Übung
        private int amount_per_set; // Anzahl der Ausführungen pro Set für die jeweilige Übung
        private int amount_sets_done; // Anzahl der geschafften Sets
        private int amount_exe_done; // Anzahl der geschafften Übungen
        private bool isStandingExe; // Übung erfordert stehende Position
        private bool isRunning = false; // Übung läuft gerade

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged("name"); }
        }

        public string Videopath
        {
            get { return videopath; }
            //set { videopath = value; NotifyPropertyChanged("videopath"); }
        }

        public int Amount_sets
        {
            get { return amount_sets; }
            set { amount_sets = value; NotifyPropertyChanged("amount_sets"); }
        }

        public int Amount_per_set
        {
            get { return amount_per_set; }
            set { amount_per_set = value; NotifyPropertyChanged("amount_per_set"); }
        }

        public int Amount_sets_done
        {
            get { return amount_sets_done; }
            set { amount_sets_done = value; NotifyPropertyChanged("amount_sets_done"); }
        }

        public int Amount_exe_done
        {
            get { return amount_exe_done; }
            set { amount_exe_done = value; NotifyPropertyChanged("amount_exe_done"); }
        }

        public bool IsStandingExe
        {
            get { return isStandingExe; }
            set { isStandingExe = value; NotifyPropertyChanged("isStandingExe"); }
        }

        public bool IsRunning
        {
            get { return IsRunning; }
            set { isRunning = value; NotifyPropertyChanged("isRunning"); }
        }

        public Exercise(string name, int amount_set, int amount, string videopath, bool isStandingExe)
        {
            this.name = name;
            this.videopath = videopath;
            this.isStandingExe = isStandingExe;
        }

        public void finishedExe()
        {
            NotifyPropertyChanged("exercise_finished");
        }

        public void setDone()
        {
            if(amount_sets_done < amount_sets)
            {
                Amount_sets_done++;
                Amount_exe_done = 1; // was macht das?
            } else
            {
                finishedExe();
            }

        }

        public void exeDone()
        {
            if(amount_exe_done < amount_per_set)
            {
                Amount_exe_done++;
            }
            else
            {
                setDone();
            }
        }


    }
}
