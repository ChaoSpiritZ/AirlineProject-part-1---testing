using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public static class POCOValidator
    {
        public static void AirlineCompanyValidator(AirlineCompany airline , bool checkId)
        {
            if (checkId)
            {
                if (airline.ID <= 0)
                    throw new InvalidPOCOException($"illegal id [{airline.ID}] for airline [{airline}]");
            }

            if (airline.AirlineName.Trim() == "")
                throw new InvalidPOCOException($"empty airline name [{airline.ID}] for airline [{airline}]");

            if (airline.CountryCode <= 0)
                throw new InvalidPOCOException($"illegal country code [{airline.CountryCode}] for airline [{airline}]");

            if (airline.Password.Trim() == "")
                throw new InvalidPOCOException($"empty password [{airline.Password}] for airline [{airline}]");

            if (airline.UserName.Trim() == "")
                throw new InvalidPOCOException($"empty username [{airline.UserName}] for airline [{airline}]");
        }

        public static void CountryValidator(Country country, bool checkId)
        {
            if (checkId)
            {
                if (country.ID <= 0)
                    throw new InvalidPOCOException($"illegal id [{country.ID}] for country [{country}]");
            }

            if (country.CountryName.Trim() == "")
                throw new InvalidPOCOException($"empty country name [{country.ID}] for country [{country}]");
        }

        public static void CustomerValidator(Customer customer, bool checkId)
        {
            if (checkId)
            {
                if (customer.ID <= 0)
                    throw new InvalidPOCOException($"illegal id [{customer.ID}] for customer [{customer}]");
            }

            if (customer.Address.Trim() == "")
                throw new InvalidPOCOException($"empty address [{customer.Address}] for customer [{customer}]");

            if (customer.CreditCardNumber.Trim() == "")
                throw new InvalidPOCOException($"empty credit card number [{customer.CreditCardNumber}] for customer [{customer}]");

            if (customer.FirstName.Trim() == "")
                throw new InvalidPOCOException($"empty first name [{customer.FirstName}] for customer [{customer}]");

            if (customer.LastName.Trim() == "")
                throw new InvalidPOCOException($"empty last name [{customer.LastName}] for customer [{customer}]");

            if (customer.Password.Trim() == "")
                throw new InvalidPOCOException($"empty password [{customer.Password}] for customer [{customer}]");

            if (customer.PhoneNo.Trim() == "")
                throw new InvalidPOCOException($"empty phone number [{customer.PhoneNo}] for customer [{customer}]");

            if (customer.UserName.Trim() == "")
                throw new InvalidPOCOException($"empty username [{customer.UserName}] for customer [{customer}]");
        }

        public static void FlightValidator(Flight flight, bool checkId)
        {
            if (checkId)
            {
                if (flight.ID <= 0)
                    throw new InvalidPOCOException($"illegal id [{flight.ID}] for flight [{flight}]");
            }

            if (flight.AirlineCompanyId < 0) //once was <=
                throw new InvalidPOCOException($"illegal airline company id [{flight.AirlineCompanyId}] for flight [{flight}]");

            if (flight.DepartureTime == null)
                throw new InvalidPOCOException($"empty departure time [{flight.DepartureTime}] for flight [{flight}]");

            if (flight.DestinationCountryCode <= 0)
                throw new InvalidPOCOException($"illegal destination country code [{flight.DestinationCountryCode}] for flight [{flight}]");

            if (flight.LandingTime == null)
                throw new InvalidPOCOException($"empty landing time [{flight.LandingTime}] for flight [{flight}]");

            if (flight.OriginCountryCode <= 0)
                throw new InvalidPOCOException($"illegal origin country code [{flight.OriginCountryCode}] for flight [{flight}]");

            if (flight.RemainingTickets < 0)
                throw new InvalidPOCOException($"illegal remaining tickets [{flight.RemainingTickets}] for flight [{flight}]");
        }

        public static void TicketValidator(Ticket ticket, bool checkId)
        {
            if (checkId)
            {
                if (ticket.ID <= 0)
                    throw new InvalidPOCOException($"illegal id [{ticket.ID}] for ticket [{ticket}]");
            }

            if (ticket.CustomerId <= 0)
                throw new InvalidPOCOException($"illegal customer id [{ticket.CustomerId}] for ticket [{ticket}]");

            if (ticket.FlightId <= 0)
                throw new InvalidPOCOException($"illegal flight id [{ticket.FlightId}] for ticket [{ticket}]");
        }
    }
}
