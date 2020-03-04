using AirlineProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace AirlineProjectWPF
{
    /// <summary>
    /// Interaction logic for WPF53Window.xaml
    /// </summary>
    public partial class WPF53Window : Window, INotifyPropertyChanged
    {
        private Flight _randomFlight;
        public Flight RandomFlight
        {
            get
            {
                return _randomFlight;
            }
            set
            {
                _randomFlight = value;
                OnPropertyChanged("RandomFlight");
            }
        }

        public WPF53Window()
        {
            RandomFlight = new Flight(42, 69, 360, 420, DateTime.Today, DateTime.Now, 999);
            InitializeComponent();
            this.DataContext = this;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
