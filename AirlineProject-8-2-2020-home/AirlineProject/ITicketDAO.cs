using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public interface ITicketDAO : IBasicDB<Ticket>
    {
        void RemoveTicketsByFlight(Flight flight);
        void RemoveTicketsByCustomer(Customer customer);
        IList<Ticket> GetTicketsByCustomerId(Customer customer);
        IList<Ticket> GetTicketsByAirlineCompany(AirlineCompany airline);
        void ClearTickets();
        void ClearTicketsHistory();
    }
}
