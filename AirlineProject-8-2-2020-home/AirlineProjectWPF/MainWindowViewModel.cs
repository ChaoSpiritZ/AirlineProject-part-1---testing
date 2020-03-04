using AirlineProject;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AirlineProjectWPF
{
    public class MainWindowViewModel
    {
        public FlyingCenterSystem fcs;

        public DelegateCommand BuyTicketCommand { get; set; }

        public MainWindowViewModel()
        {
            fcs = FlyingCenterSystem.GetInstance();
            BuyTicketCommand = new DelegateCommand(BuyTicket, IsBuyTicketCommandEnabled);
        }

        public void BuyTicket()
        {
            MessageBox.Show("WORKS!");
            return;
        }

        public bool IsBuyTicketCommandEnabled()
        {
            return true;
        }
    }
}
