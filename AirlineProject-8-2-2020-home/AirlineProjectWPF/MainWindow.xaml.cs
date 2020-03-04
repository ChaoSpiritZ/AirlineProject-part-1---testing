using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using AirlineProject;
using Prism.Commands;

namespace AirlineProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public FlyingCenterSystem fcs = FlyingCenterSystem.GetInstance();
        public AnonymousUserFacade anonymousUserFacade = new AnonymousUserFacade();
        public IList<Flight> flights;
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

        public DelegateCommand BuyTicketCommand { get; set; }

        public MainWindow()
        {
            BuyTicketCommand = new DelegateCommand(BuyTicket, IsBuyTicketCommandEnabled);
            InitializeComponent();
            flights = anonymousUserFacade.GetAllFlights();
            listBoxFlights.ItemsSource = flights;
            this.DataContext = this;
            Task.Run(() =>
            {
                while (true)
                {
                    BuyTicketCommand.RaiseCanExecuteChanged();
                    Thread.Sleep(500);
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (fcs.Login(UserTxtBx.Text, PwdTxtBx.Text, out ILoginToken lt) == null)
                {
                    BorderLogin.BorderBrush = Brushes.Red;
                }
                else
                {
                    if(lt is null)
                    {
                        BorderLogin.BorderBrush = Brushes.Gold;
                    }
                    else
                    {
                        BorderLogin.BorderBrush = Brushes.Green;
                    }
                }
            }
            catch(WrongPasswordException)
            {
                BorderLogin.BorderBrush = Brushes.Red;
            }
        }

        private void Show_Flight_Button_Click(object sender, RoutedEventArgs e)
        {
            long flightId = 0;
            long.TryParse(ShowFlightTxtBx.Text, out flightId);
            ShownFlight = anonymousUserFacade.GetFlight(flightId);
        }

        public void BuyTicket()
        {
            MessageBox.Show("Ticket Bought! just kidding - no tickets until part 2 of the project");
            return;
        }

        public bool IsBuyTicketCommandEnabled()
        {
            if(ShownFlight is null)
            {
                return false;
            }
            if(ShownFlight.RemainingTickets > 0)
            {
                return true;
            }
            return false;
        }
    }
}
