using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public interface ILoggedInAirlineFacade
    {
        void CancelFlight(LoginToken<AirlineCompany> token, Flight flight);
        void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword);
        void CreateFlight(LoginToken<AirlineCompany> token, Flight flight);
        IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token);
        IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token);
        void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline);
        void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight);
    }
}
