using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        /// <summary>
        /// cancels one of your tickets
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ticket">removes a ticket based on this parameter's ID</param>
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            LoginHelper.CheckToken<Customer>(token);
            POCOValidator.TicketValidator(ticket, true);
            if (_ticketDAO.Get(ticket.ID) == null)
                throw new TicketNotFoundException($"failed to cancel ticket [{ticket}], ticket with id of [{ticket.ID}] was not found!");
            if (ticket.CustomerId != token.User.ID)
                throw new InaccessibleTicketException($"failed to cancel ticket , you do not own ticket [{ticket}]");
            Flight updatedFlight = _flightDAO.Get(ticket.FlightId);
            updatedFlight.RemainingTickets++;
            _flightDAO.Update(updatedFlight);
            _ticketDAO.Remove(ticket);
        }

        /// <summary>
        /// gets all of your flights
        /// </summary>
        /// <param name="token">gets flights based on the user inside this token</param>
        /// <returns></returns>
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            LoginHelper.CheckToken<Customer>(token);
            return _flightDAO.GetFlightsByCustomer(token.User);
        }

        /// <summary>
        /// purchases a ticket to a flight
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight">the flight you want to purchase a ticket for</param>
        /// <returns></returns>
        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            LoginHelper.CheckToken<Customer>(token);
            POCOValidator.FlightValidator(flight, false);
            if (_flightDAO.Get(flight.ID) == null)
                throw new FlightNotFoundException($"failed to purchase ticket, there is no flight with id of [{flight.ID}]");
            IList<Ticket> tickets = _ticketDAO.GetTicketsByCustomerId(token.User);
            if (tickets.Any(item => item.FlightId == flight.ID)) //boolean
                throw new TicketAlreadyExistsException($"failed to purchase ticket, you already purchased a ticket to flight [{flight}]");
            if (_flightDAO.Get(flight.ID).RemainingTickets == 0)
                throw new NoMoreTicketsException($"failed to purchase ticket to flight [{flight}], there are no more tickets left!");
            Ticket newTicket = new Ticket(0, flight.ID, token.User.ID);
            _ticketDAO.Add(newTicket);
            flight.RemainingTickets--;
            _flightDAO.Update(flight);
            return newTicket;
        }
    }
}
