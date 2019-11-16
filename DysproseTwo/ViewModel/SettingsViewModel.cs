using DysproseTwo.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.ViewModel
{
    class SettingsViewModel : Notifier
    {

        private int _fadeIntervalValue;

        public int FadeIntervalValue
        {
            get { return _fadeIntervalValue; }
            set
            {
                if (_fadeIntervalValue != value)
                {
                    _fadeIntervalValue = value;
                    NotifyPropertyChanged();
                }
                
            }
        }


        private TimeUnit _selectedTimeUnit;

        public TimeUnit SelectedTimeUnit
        {
            get { return _selectedTimeUnit; }
            set
            {
                if (_selectedTimeUnit != value)
                {
                    _selectedTimeUnit = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private double _fontSize;

        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List<double> FontList { get; set; } = new List<double>()
        {
            8,9,10,11,12,14,15,18,24,30,36,48,60,72,96
        };

        public List<TimeUnit> TimeUnits { get; set; } = new List<TimeUnit>()
        {
            TimeUnit.Seconds, TimeUnit.Minutes, TimeUnit.Hours
        };
    }
}
