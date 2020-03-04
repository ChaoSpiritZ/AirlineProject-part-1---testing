using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    class SystemFacade : FacadeBase
    {
        public void MoveTicketsAndFlightsToHistory()
        {
            _flightDAO.MoveTicketsAndFlightsToHistory();
        }
    }
}
