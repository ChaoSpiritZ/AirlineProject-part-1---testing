using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
    {

        /// <summary>
        /// creates a new airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline">id is generated upon creation, leave it at 0</param>
        public void CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.AirlineCompanyValidator(airline, false);
            if (_airlineDAO.GetAirlineByAirlineName(airline.AirlineName) != null)
                throw new AirlineNameAlreadyExistsException($"failed to create airline! there is already an airline with the name '{airline.AirlineName}'");
            if (_airlineDAO.GetAirlineByUsername(airline.UserName) != null || _customerDAO.GetCustomerByUsername(airline.UserName) != null || airline.UserName == "admin")
                throw new UsernameAlreadyExistsException($"failed to create airline! Username '{airline.UserName}' is already taken!");
            if (_countryDAO.Get(airline.CountryCode) == null)
                throw new CountryNotFoundException($"failed to create airline! there is no country with id [{airline.CountryCode}]");
            _airlineDAO.Add(airline);
        }

        /// <summary>
        /// creates a new customer
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer">id is generated upon creation, leave it at 0</param>
        public void CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.CustomerValidator(customer, false);
            if (_airlineDAO.GetAirlineByUsername(customer.UserName) != null || _customerDAO.GetCustomerByUsername(customer.UserName) != null || customer.UserName == "admin")
                throw new UsernameAlreadyExistsException($"failed to create customer! Username '{customer.UserName}' is already taken!");
            _customerDAO.Add(customer);
        }

        /// <summary>
        /// removes an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline">removes an airline company that has this parameter's ID</param>
        public void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.AirlineCompanyValidator(airline, true);
            if (_airlineDAO.Get(airline.ID) == null)
                throw new UserNotFoundException($"failed to remove airline! airline with username [{airline.UserName}] was not found!");
            IList<Flight> flights = _flightDAO.GetFlightsByAirlineCompanyId(airline);
            flights.ToList().ForEach(f => _ticketDAO.RemoveTicketsByFlight(f));
            flights.ToList().ForEach(f => _flightDAO.Remove(f));
            _airlineDAO.Remove(airline);
        }

        /// <summary>
        /// removes a customer
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer">removes a customer that has this parameter's ID</param>
        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.CustomerValidator(customer, true);
            if (_customerDAO.Get(customer.ID) == null)
                throw new UserNotFoundException($"failed to remove customer! customer with username [{customer.UserName}] was not found!");
            IList<Flight> flights = _flightDAO.GetFlightsByCustomer(customer);
            flights.ToList().ForEach(f => f.RemainingTickets++);
            flights.ToList().ForEach(f => _flightDAO.Update(f));
            _ticketDAO.RemoveTicketsByCustomer(customer);
            _customerDAO.Remove(customer);
        }

        /// <summary>
        /// updates an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline">updates the airline company with this parameter's ID</param>
        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.AirlineCompanyValidator(airline, true);
            if (_airlineDAO.Get(airline.ID) == null)
                throw new UserNotFoundException($"failed to update airline! airline with username [{airline.UserName}] was not found!");
            if (_countryDAO.Get(airline.CountryCode) == null)
                throw new CountryNotFoundException($"failed to update airline! there is no country with id [{airline.CountryCode}]");
            _airlineDAO.Update(airline);
        }

        /// <summary>
        /// updates a customer
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer">updates the customer with this parameter's ID</param>
        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.CustomerValidator(customer, true);
            if (_customerDAO.Get(customer.ID) == null)
                throw new UserNotFoundException($"failed to update customer! customer with username [{customer.UserName}] was not found!");
            _customerDAO.Update(customer);
        }
    }
}
