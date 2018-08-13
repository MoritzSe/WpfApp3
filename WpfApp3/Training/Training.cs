using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.UDP;

namespace WpfApp3.Training
{
    class Training : INotifyPropertyChanged
    {
        private ObservableCollection<Exercise> exerciseList;

        public ObservableCollection<Exercise> ExerciseList
        {
            get { return exerciseList;}

            set
            {
                if (exerciseList == value) return;
                exerciseList = value;
                if (this.PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ItemSource"));
            }
        }

        //Erstellen der Übungen
        private Exercise rowing = new Exercise("Rudern", 3, 4, "rudern_final_mp4", false);
        private Exercise side_pull = new Exercise("Seitziehen", 3, 4, "seitziehen_final.mp4", false);
        private Exercise latpull = new Exercise("Lattzug", 3, 4, "lattziehen_final.mp4" , true);
        // es gibt weitere Übungen, die aber bisher nicht betrachtet worden sind.

        // ErgometerTraining noch miteinbauen?

        private Exercise activteExercise;
        public Exercise ActiveExercise
        {
            get { return activteExercise; }
            
        }

        private bool isActive = false;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public Training()
        {
            exerciseList = new ObservableCollection<Exercise>();
            exerciseList.Add(rowing);
            exerciseList.Add(side_pull);
            exerciseList.Add(latpull);
            //exerciselist.Add(ergo);

            rowing.PropertyChanged += Exercise_PropertyChanged;
            side_pull.PropertyChanged += Exercise_PropertyChanged;
            latpull.PropertyChanged += Exercise_PropertyChanged;

        }






        private void Exercise_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Exercise sent = (Exercise)sender;
            if(e.PropertyName.Equals("exercise_finished"))
            {

            

            }
        }


    }
}
