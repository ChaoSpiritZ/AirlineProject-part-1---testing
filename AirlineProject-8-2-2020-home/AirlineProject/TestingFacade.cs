using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class TestingFacade : FacadeBase
    {
        //public void AddCountry(Country country)
        //{
        //    _countryDAO.Add(country);
        //}


        // to delete a poco ==> you need to delete those
        //4 airlineCompany ==> flights ==> what if it's flying? please ignore
        //5 country ==> airlineCompanies, flights ==> umm... bermuda triangle stuff right here? please ignore
        //3 customer ==> tickets
        //2 flight ==> tickets
        //1 ticket ==> none
        //6 ticketHistory ==> none
        //7 flightHistory ==> none

            /// <summary>
            /// clears all tables EXCEPT countries because why would i do that?
            /// </summary>
        public void ClearAllTables()
        {
            ClearTickets();
            ClearTicketsHistory();
            ClearFlights();
            ClearFlightsHistory();
            ClearCustomers();
            ClearAirlineCompanies();
            //ClearCountries();
        }

        public void ClearAirlineCompanies()
        {
            _airlineDAO.ClearAirlineCompanies();
        }

        //public void ClearCountries()
        //{
        //    _countryDAO.ClearCountries();
        //}

        public void ClearCustomers()
        {
            _customerDAO.ClearCustomers();
        }

        public void ClearFlights()
        {
            _flightDAO.ClearFlights();
        }

        public void ClearFlightsHistory()
        {
            _flightDAO.ClearFlightsHistory();
        }

        public void ClearTickets()
        {
            _ticketDAO.ClearTickets();
        }

        public void ClearTicketsHistory()
        {
            _ticketDAO.ClearTicketsHistory();
        }

        public Customer GetCustomerById(long id)
        {
            Customer customer = _customerDAO.Get(id);
            return customer;
        }
    }
}
