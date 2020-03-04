using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class OldTestingFunctionsFacade : FacadeBase
    {
        ///////////////////////////AirlineCompanies//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddAirlineCompany(AirlineCompany airlineCompany)
        {
            _airlineDAO.Add(airlineCompany);
        }

        public AirlineCompany GetAirlineCompany(long id)
        {
            AirlineCompany airlineCompany = _airlineDAO.Get(id);
            return airlineCompany;
        }

        public AirlineCompany GetAirlineByAirlineName(string name)
        {
            AirlineCompany airlineCompany = _airlineDAO.GetAirlineByAirlineName(name);
            return airlineCompany;
        }

        public AirlineCompany GetAirlineByUsername(string name)
        {
            AirlineCompany airlineCompany = _airlineDAO.GetAirlineByUsername(name);
            return airlineCompany;
        }

        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            IList<AirlineCompany> airlineCompanies = _airlineDAO.GetAll();
            return airlineCompanies;
        }

        public IList<AirlineCompany> GetAllAirlinesByCountry(long countryId)
        {
            IList<AirlineCompany> airlineCompanies = _airlineDAO.GetAllAirlinesByCountry(countryId);
            return airlineCompanies;
        }

        public void RemoveAirlineCompany(AirlineCompany t)
        {
            _airlineDAO.Remove(t);
        }

        public void UpdateAirlineCompany(AirlineCompany t)
        {
            _airlineDAO.Update(t);
        }

        ///////////////////////////COUNTRIES//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddCountry(Country country)
        {
            _countryDAO.Add(country);
        }

        public Country GetCountry(long id)
        {
            Country country = _countryDAO.Get(id);
            return country;
        }

        public IList<Country> GetAllCountries()
        {
            IList<Country> countries = _countryDAO.GetAll();
            return countries;
        }

        public void RemoveCountry(Country country)
        {
            _countryDAO.Remove(country);
        }

        public void UpdateCountry(Country country)
        {
            _countryDAO.Update(country);
        }

        ///////////////////////////CUSTOMERS//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddCustomer(Customer customer)
        {
            _customerDAO.Add(customer);
        }

        public Customer GetCustomer(long id)
        {
            Customer customer = _customerDAO.Get(id);
            return customer;
        }

        public IList<Customer> GetAllCustomers()
        {
            IList<Customer> customers = _customerDAO.GetAll();
            return customers;
        }

        public void RemoveCustomer(Customer customer)
        {
            _customerDAO.Remove(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerDAO.Update(customer);
        }

        public Customer GetCustomerByUsername(string name)
        {
            Customer customer = _customerDAO.GetCustomerByUsername(name);
            return customer;
        }

        ///////////////////////////FLIGHTS//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddFlight(Flight flight)
        {
            _flightDAO.Add(flight);
        }

        public Flight GetFlight(long id)
        {
            Flight flight = _flightDAO.Get(id);
            return flight;
        }

        public IList<Flight> GetAllFlights()
        {
            IList<Flight> flights = _flightDAO.GetAll();
            return flights;
        }

        public void RemoveFlight(Flight flight)
        {
            _flightDAO.Remove(flight);
        }

        public void UpdateFlight(Flight flight)
        {
            _flightDAO.Update(flight);
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flightsVacancy = _flightDAO.GetAllFlightsVacancy();
            return flightsVacancy;
        }

        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            IList<Flight> flights = _flightDAO.GetFlightsByCustomer(customer);
            return flights;
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

        ///////////////////////////TICKETS//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddTicket(Ticket ticket)
        {
            _ticketDAO.Add(ticket);
        }

        public Ticket GetTicket(long id)
        {
            Ticket ticket = _ticketDAO.Get(id);
            return ticket;
        }

        public IList<Ticket> GetAllTickets()
        {
            IList<Ticket> tickets = _ticketDAO.GetAll();
            return tickets;
        }

        public void RemoveTicket(Ticket ticket)
        {
            _ticketDAO.Remove(ticket);
        }

        public void UpdateTicket(Ticket ticket)
        {
            _ticketDAO.Update(ticket);
        }
    }
}
