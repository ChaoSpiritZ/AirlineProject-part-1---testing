using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade
    {
        public AirlineCompany GetAirlineCompanyById(long id)
        {
            AirlineCompany airline = _airlineDAO.Get(id);
            return airline;
        }

        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            IList<AirlineCompany> airlineCompanies = _airlineDAO.GetAll();
            return airlineCompanies;
        }

        public IList<Flight> GetAllFlights()
        {
            IList<Flight> flights = _flightDAO.GetAll();
            return flights;
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flightsVacancy = _flightDAO.GetAllFlightsVacancy();
            return flightsVacancy;
        }

        public Flight GetFlight(long id)
        {
            Flight flight = _flightDAO.Get(id);
            return flight;
        }

        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            IList<Flight> flights = _flightDAO.GetFlightsByDepartureDate(departureDate);
            return flights;
        }

        public IList<Flight> GetFlightsByDestinationCountry(long countryCode)
        {
            IList<Flight> flights = _flightDAO.GetFlightsByDestinationCountry(countryCode);
            return flights;
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            IList<Flight> flights = _flightDAO.GetFlightsByLandingDate(landingDate);
            return flights;
        }

        public IList<Flight> GetFlightsByOriginCountry(long countryCode)
        {
            IList<Flight> flights = _flightDAO.GetFlightsByOriginCountry(countryCode);
            return flights;
        }
    }
}
