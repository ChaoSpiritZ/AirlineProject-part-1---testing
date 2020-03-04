using AirlineProject;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AirlineProjectWPF
{
    public class WPF53WindowViewModel : INotifyPropertyChanged
    {
        public WPFFacade wpfFacade = new WPFFacade();
        public IAnonymousUserFacade anonymousFacade = new AnonymousUserFacade();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Flight> Flights { get; set; }
        public AirlineCompany SearchedAirlineCompany { get; set; }

        private Flight _shownFlight;
        public Flight ShownFlight
        {
            get
            {
                return _shownFlight;
            }
            set
            {
                _shownFlight = value;
                OnPropertyChanged("ShownFlight");
            }
        }

        public DelegateCommand<string> SearchCommand { get; set; }

        public DelegateCommand<string> ShowCommand { get; set; }

        //public Flight randomFlight = new Flight(42, 69, 360, 420, DateTime.Today, DateTime.Now, 999);

        public WPF53WindowViewModel()
        {
            Flights = new ObservableCollection<Flight>();
            SearchedAirlineCompany = new AirlineCompany();
            ShownFlight = new Flight();
            SearchCommand = new DelegateCommand<string>(SearchCommandExecute);
            ShowCommand = new DelegateCommand<string>(ShowCommandExecute);
        }

        public void SearchCommandExecute(string text)
        {
            SearchedAirlineCompany = wpfFacade.GetAirlineCompanyByName(text);
            Flights.Clear();
            if (SearchedAirlineCompany is null == false)
                Flights.AddRange(wpfFacade.GetFlightsByAirlineCompanyId(SearchedAirlineCompany));
        }

        public void ShowCommandExecute(string text)
        {
            long.TryParse(text, out long flightId);
            ShownFlight = anonymousFacade.GetFlight(flightId);
        }

        public void OnPropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
