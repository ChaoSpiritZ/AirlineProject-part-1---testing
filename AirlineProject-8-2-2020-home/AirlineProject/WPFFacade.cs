using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class WPFFacade : FacadeBase
    {
        public AirlineCompany GetAirlineCompanyByName(string name)
        {
            return _airlineDAO.GetAirlineByAirlineName(name);
        }

        public IList<Flight> GetFlightsByAirlineCompanyId(AirlineCompany airline)
        {
            if (airline is null)
                return null;
            return _flightDAO.GetFlightsByAirlineCompanyId(airline);
        }
    }
}
