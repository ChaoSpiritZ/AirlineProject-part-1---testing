using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {
        /// <summary>
        /// cancels one of your flights
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight">deletes using this parameter's ID</param>
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {

            LoginHelper.CheckToken<AirlineCompany>(token);
            POCOValidator.FlightValidator(flight, true);
            if (_flightDAO.Get(flight.ID) == null)
                throw new FlightNotFoundException($"failed to cancel flight! there is no flight with id [{flight.ID}]");
            if (flight.AirlineCompanyId != token.User.ID)
                throw new InaccessibleFlightException($"failed to cancel flight! you do not own flight [{flight}]");
            if (_flightDAO.Get(flight.ID).DepartureTime < DateTime.Now) //was sql current date supposed to be involved?
                throw new FlightAlreadyTookOffException($"failed to cancel flight! flight [{flight}] already took off at [{flight.DepartureTime}]");
            _ticketDAO.RemoveTicketsByFlight(flight);
            _flightDAO.Remove(flight);


            //need to add functions to to remove Poco lists:

            // to delete a poco ==> you need to delete those
            //4 airlineCompany ==> flights ==> what if it's flying? please ignore
            //5 country ==> airlineCompanies, flights ==> umm... bermuda triangle stuff right here? please ignore
            //3 customer ==> tickets
            //2 flight ==> tickets
            //1 ticket ==> none
        }


        /// <summary>
        /// changing your password requires you to enter both your old password and a new password
        /// </summary>
        /// <param name="token"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            LoginHelper.CheckToken<AirlineCompany>(token);
            if (newPassword.Trim() == "")
                throw new EmptyPasswordException($"failed to change password! new password is empty!");
            if (token.User.Password != oldPassword)
                throw new WrongPasswordException($"failed to change password! old password doesn't match!");
            token.User.Password = newPassword;
            _airlineDAO.Update(token.User);
        }

        /// <summary>
        /// creates a flight
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight">id and airline company id will be generated upon creation, leave them at 0</param>
        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            LoginHelper.CheckToken<AirlineCompany>(token);
            POCOValidator.FlightValidator(flight, false);
            //if (flight.AirlineCompanyId != token.User.ID)
            //    throw new InaccessibleFlightException($"failed to create flight [{flight}], you do not own this flight!"); //probably won't happen unless something goes wrong
            if (DateTime.Compare(flight.DepartureTime, flight.LandingTime) > 0)
                throw new InvalidFlightDateException($"failed to create flight [{flight}], cannot fly back in time from [{flight.DepartureTime}] to [{flight.LandingTime}]");
            if (DateTime.Compare(flight.DepartureTime, flight.LandingTime) == 0)
                throw new InvalidFlightDateException($"failed to create flight [{flight}], departure time and landing time are the same [{flight.DepartureTime}], and as you know, teleportation isn't invented yet");
            if (_countryDAO.Get(flight.OriginCountryCode) == null)
                throw new CountryNotFoundException($"failed to create flight [{flight}], origin country with id [{flight.OriginCountryCode}] was not found!");
            if (_countryDAO.Get(flight.DestinationCountryCode) == null)
                throw new CountryNotFoundException($"failed to create flight [{flight}], destination country with id [{flight.DestinationCountryCode}] was not found!");
            flight.AirlineCompanyId = token.User.ID;
            _flightDAO.Add(flight);
        }

        /// <summary>
        /// gets all the flights belonging to this airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            LoginHelper.CheckToken<AirlineCompany>(token);
            return _flightDAO.GetFlightsByAirlineCompanyId(token.User);
        }

        /// <summary>
        /// gets all the tickets of all the flights belonging to this airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            return _ticketDAO.GetTicketsByAirlineCompany(token.User);
        }

        /// <summary>
        /// can change all the airline's details except ID and Password
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        public void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            LoginHelper.CheckToken<AirlineCompany>(token);
            POCOValidator.AirlineCompanyValidator(airline, true);
            if (airline.ID != token.User.ID)
                throw new InaccessibleAirlineCompanyException($"failed to modify details! this is not your account!"); //will it ever happen? who knows...
            if (_airlineDAO.GetAirlineByAirlineName(airline.AirlineName) != null)
                if (_airlineDAO.GetAirlineByAirlineName(airline.AirlineName) != token.User)
                    throw new AirlineNameAlreadyExistsException($"failed to modify details! there is already and airline with the name [{airline.AirlineName}]");
            if (_airlineDAO.GetAirlineByUsername(airline.UserName) != null)
                if (_airlineDAO.GetAirlineByUsername(airline.UserName) != token.User)
                    throw new UsernameAlreadyExistsException($"failed to modify details! username [{airline.UserName}] is already taken!");
            airline.Password = _airlineDAO.Get(airline.ID).Password; // i guess..
            _airlineDAO.Update(airline);
        }

        /// <summary>
        /// update one of your airline company's flights
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight">make sure it has the same id as the flight you want to update</param>
        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            LoginHelper.CheckToken<AirlineCompany>(token);
            POCOValidator.FlightValidator(flight, true);
            if (_flightDAO.Get(flight.ID) == null)
                throw new FlightNotFoundException($"failed to update flight! flight with id of [{flight.ID}] was not found!");
            if (flight.AirlineCompanyId != token.User.ID)
                throw new InaccessibleFlightException($"failed to update flight! you do not own flight [{flight}]");
            if (DateTime.Compare(flight.DepartureTime, flight.LandingTime) > 0)
                throw new InvalidFlightDateException($"failed to update flight [{flight}], cannot fly back in time from [{flight.DepartureTime}] to [{flight.LandingTime}]");
            if (DateTime.Compare(flight.DepartureTime, flight.LandingTime) == 0)
                throw new InvalidFlightDateException($"failed to update flight [{flight}], departure time and landing time are the same [{flight.DepartureTime}], and as you know, teleportation isn't invented yet");
            if (_countryDAO.Get(flight.OriginCountryCode) == null)
                throw new CountryNotFoundException($"failed to update flight [{flight}], origin country with id [{flight.OriginCountryCode}] was not found!");
            if (_countryDAO.Get(flight.DestinationCountryCode) == null)
                throw new CountryNotFoundException($"failed to update flight [{flight}], destination country with id [{flight.DestinationCountryCode}] was not found!");
            _flightDAO.Update(flight);
        }
    }
}
