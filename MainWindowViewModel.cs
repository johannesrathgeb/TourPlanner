using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner
{
    public class MainWindowViewModel : INotifyPropertyChanged, INotifyPropertyChanging
    {

        string selected; 

        public string Selected
        {
            get => selected;
            set
            {
                //if (selected != value)
                //{
                    selected = value;
                    NotifyPropertyChanging(nameof(Output));
                    NotifyPropertyChanged(nameof(Output));
                //}
            }
        }

        public string Output => Selected;



        public MainWindowViewModel()
        {
            Selected = "";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            /*
            if(!string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));
            }
            */

            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        protected void NotifyPropertyChanging([CallerMemberName] string propertyName = "")
        {
            /*
            if (!string.IsNullOrEmpty(propertyName))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(propertyName)));
            }
            */

            var handler = PropertyChanging;
            if(handler != null)
            {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }

        }


        /*
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        */


    }
}
