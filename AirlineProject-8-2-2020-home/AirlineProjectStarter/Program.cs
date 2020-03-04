using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineProject;

namespace AirlineProjectStarter
{
    class Program
    {

        static void Main(string[] args)
        {
            OldTestingFunctionsFacade TFF = new OldTestingFunctionsFacade();
            FlyingCenterSystem FCS = FlyingCenterSystem.GetInstance();

            Console.WriteLine("testing functions facade is turned off");
            
            ///////////////////////////AirlineCompanies//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            AirlineCompany AC = new AirlineCompany(0, "Cruiser", "CrusCrus", "45678", 2);
            

            Console.WriteLine("getting all airline companies: ");
            IList<AirlineCompany> airlineCompanies = TFF.GetAllAirlineCompanies();
            airlineCompanies.ToList().ForEach(ac => Console.WriteLine(ac));
            Console.WriteLine("");

            Console.WriteLine("adding airline company with: Name: Cruiser, Username: CrusCrus, Password: 45678, Country Code: 2");
            TFF.AddAirlineCompany(AC);
            Console.WriteLine("");

            Console.WriteLine($"Getting airline company with id of {AC.ID}:");
            Console.WriteLine(TFF.GetAirlineCompany(AC.ID));
            Console.WriteLine("");

            Console.WriteLine("getting all airline companies:");
            airlineCompanies = TFF.GetAllAirlineCompanies();
            airlineCompanies.ToList().ForEach(ac => Console.WriteLine(ac));
            Console.WriteLine("");

            AirlineCompany AC2 = new AirlineCompany(AC.ID, "Cruiser", "CrusCrus", "45678123", 2);

            Console.WriteLine($"updating airline company with id - {AC.ID}, password - 45678 => 45678123");
            TFF.UpdateAirlineCompany(AC2);
            Console.WriteLine("");

            Console.WriteLine($"Getting airline company with id of {AC.ID}:");
            Console.WriteLine(TFF.GetAirlineCompany(AC.ID));
            Console.WriteLine("");

            Console.WriteLine("getting all airline companies:");
            airlineCompanies = TFF.GetAllAirlineCompanies();
            airlineCompanies.ToList().ForEach(ac => Console.WriteLine(ac));
            Console.WriteLine("");

            Console.WriteLine($"removing airline company with id - {AC.ID}:");
            TFF.RemoveAirlineCompany(AC2);
            Console.WriteLine("");

            Console.WriteLine("getting all airline companies:");
            airlineCompanies = TFF.GetAllAirlineCompanies();
            airlineCompanies.ToList().ForEach(ac => Console.WriteLine(ac));
            Console.WriteLine("");

            Console.WriteLine("getting airline company with username - CCCRASH");
            Console.WriteLine(TFF.GetAirlineByUsername("CCCRASH"));
            Console.WriteLine("");

            Console.WriteLine("getting airline company with username - CCCRASh");
            Console.WriteLine(TFF.GetAirlineByUsername("CCCRASh"));
            Console.WriteLine("");

            Console.WriteLine("getting airline company with name - delta");
            Console.WriteLine(TFF.GetAirlineByAirlineName("delta"));
            Console.WriteLine("");

            Console.WriteLine("getting all airline companies by country id 3:");
            airlineCompanies = TFF.GetAllAirlinesByCountry(3);
            airlineCompanies.ToList().ForEach(ac => Console.WriteLine(ac));
            Console.WriteLine("");

            ///////////////////////////COUNTRIES//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Country ctr = new Country(0, "Antarctica");
            

            Console.WriteLine("getting all countries:");
            IList<Country> countries = TFF.GetAllCountries();
            countries.ToList().ForEach(c => Console.WriteLine(c));
            Console.WriteLine("");

            Console.WriteLine("adding country with country name - Antarctica");
            TFF.AddCountry(ctr);
            Console.WriteLine("");

            Console.WriteLine($"Getting country with id of {ctr.ID}:");
            Console.WriteLine(TFF.GetCountry(ctr.ID));
            Console.WriteLine("");

            Console.WriteLine("getting all countries:");
            countries = TFF.GetAllCountries();
            countries.ToList().ForEach(c => Console.WriteLine(c));
            Console.WriteLine("");

            Country ctr2 = new Country(ctr.ID, "Arctica");

            Console.WriteLine($"updating country with id - {ctr.ID}, country name - Antarctica => Arctica");
            TFF.UpdateCountry(ctr2);
            Console.WriteLine("");

            Console.WriteLine($"Getting country with id of {ctr.ID}:");
            Console.WriteLine(TFF.GetCountry(ctr.ID));
            Console.WriteLine("");

            Console.WriteLine("getting all countries:");
            countries = TFF.GetAllCountries();
            countries.ToList().ForEach(c => Console.WriteLine(c));
            Console.WriteLine("");

            Console.WriteLine($"removing country with id - {ctr.ID}:");
            TFF.RemoveCountry(ctr2);
            Console.WriteLine("");

            Console.WriteLine("getting all countries:");
            countries = TFF.GetAllCountries();
            countries.ToList().ForEach(c => Console.WriteLine(c));
            Console.WriteLine("");

            ///////////////////////////CUSTOMERS//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Customer cst = new Customer(0, "Harry", "Potter", "HP127", "Happy127", "Hogwarts", "no phone", "68629472");
            

            Console.WriteLine("getting all customers:");
            IList<Customer> customers = TFF.GetAllCustomers();
            customers.ToList().ForEach(c => Console.WriteLine(c));
            Console.WriteLine("");

            Console.WriteLine("adding customer with harry potter, HP127, Happy127, Hogwarts, no phone, 68629472");
            TFF.AddCustomer(cst);
            Console.WriteLine("");

            Console.WriteLine($"Getting customer with id of {cst.ID}:");
            Console.WriteLine(TFF.GetCustomer(cst.ID));
            Console.WriteLine("");

            Console.WriteLine("getting all customers:");
            customers = TFF.GetAllCustomers();
            customers.ToList().ForEach(c => Console.WriteLine(c));
            Console.WriteLine("");

            Customer cst2 = new Customer(cst.ID, "Harry", "Potter", "HP127", "Happy127", "Hogwarts", "055-5555555", "68629472");

            Console.WriteLine($"updating customer with id - {cst.ID}, phone number - no phone => 055-5555555");
            TFF.UpdateCustomer(cst2);
            Console.WriteLine("");

            Console.WriteLine($"Getting customer with id of {cst.ID}:");
            Console.WriteLine(TFF.GetCustomer(cst.ID));
            Console.WriteLine("");

            Console.WriteLine("getting all customers:");
            customers = TFF.GetAllCustomers();
            customers.ToList().ForEach(c => Console.WriteLine(c));
            Console.WriteLine("");

            Console.WriteLine($"removing customer with id - {cst.ID}:");
            TFF.RemoveCustomer(cst2);
            Console.WriteLine("");

            Console.WriteLine("getting all customers:");
            customers = TFF.GetAllCustomers();
            customers.ToList().ForEach(c => Console.WriteLine(c));
            Console.WriteLine("");

            Console.WriteLine("getting customer by username - glmaN:");
            Console.WriteLine(TFF.GetCustomerByUsername("glmaN"));
            Console.WriteLine("");

            ///////////////////////////FLIGHTS//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Flight fl = new Flight(0, 3, 6, 4, new DateTime(2020, 4, 20), new DateTime(2020, 4, 21), 6);
            

            Console.WriteLine("getting all flights:");
            IList<Flight> flights = TFF.GetAllFlights();
            flights.ToList().ForEach(f => Console.WriteLine(f));
            Console.WriteLine("");

            Console.WriteLine("adding flight with 3, 6, 4, 20-4-2020, 21-4-2020, 6");
            TFF.AddFlight(fl);
            Console.WriteLine("");

            Console.WriteLine($"Getting flight with id of {fl.ID}:");
            Console.WriteLine(TFF.GetFlight(fl.ID));
            Console.WriteLine("");

            Console.WriteLine("getting all flights:");
            flights = TFF.GetAllFlights();
            flights.ToList().ForEach(f => Console.WriteLine(f));
            Console.WriteLine("");

            Flight fl2 = new Flight(fl.ID, 3, 6, 4, new DateTime(2020, 4, 20), new DateTime(2020, 5, 20), 6);

            Console.WriteLine($"updating flight with id - {fl.ID}, landing time - 21-4-2020 => 20-5-2020");
            TFF.UpdateFlight(fl2);
            Console.WriteLine("");

            Console.WriteLine($"Getting flight with id of {fl.ID}:");
            Console.WriteLine(TFF.GetFlight(fl.ID));
            Console.WriteLine("");

            Console.WriteLine("getting all flights:");
            flights = TFF.GetAllFlights();
            flights.ToList().ForEach(f => Console.WriteLine(f));
            Console.WriteLine("");

            Console.WriteLine("getting all flights' vacancy:");
            Dictionary<Flight, int> flightsVacancy = TFF.GetAllFlightsVacancy();
            flightsVacancy.ToList().ForEach(fv => Console.WriteLine($"Flight number [{fv.Key.ID}] has [{fv.Value}] remaining tickets"));
            Console.WriteLine("");

            Console.WriteLine($"removing flight with id - {fl.ID}:");
            TFF.RemoveFlight(fl2);
            Console.WriteLine("");

            Console.WriteLine("getting all flights:");
            flights = TFF.GetAllFlights();
            flights.ToList().ForEach(f => Console.WriteLine(f));
            Console.WriteLine("");

            Console.WriteLine("getting all flights' vacancy:");
            flightsVacancy = TFF.GetAllFlightsVacancy();
            flightsVacancy.ToList().ForEach(fv => Console.WriteLine($"Flight number [{fv.Key.ID}] has [{fv.Value}] remaining tickets"));
            Console.WriteLine("");

            Console.WriteLine("getting all flights by customer - Yoav Levi");
            IList<Flight> flightsByCustomer = TFF.GetFlightsByCustomer(TFF.GetCustomer(1));
            flightsByCustomer.ToList().ForEach(fbc => Console.WriteLine(fbc));
            Console.WriteLine("");

            Console.WriteLine("getting all flights by departure time - 23-4-2019");
            IList<Flight> flightsByDepartureTime = TFF.GetFlightsByDepartureDate(new DateTime(2019, 4, 23)); // only gets flights in that date that depart at 00:00:00.000
            flightsByDepartureTime.ToList().ForEach(fbdt => Console.WriteLine(fbdt));
            Console.WriteLine("");

            Console.WriteLine("getting all flights by destination country id - 9");
            IList<Flight> flightsByDestinationCountry = TFF.GetFlightsByDestinationCountry(9);
            flightsByDestinationCountry.ToList().ForEach(fbdc => Console.WriteLine(fbdc));
            Console.WriteLine("");

            Console.WriteLine("getting all flights by landing time - 20-02-2020");
            IList<Flight> flightsByLandingTime = TFF.GetFlightsByLandingDate(new DateTime(2020, 2, 20)); // only gets flights in that date that land at 00:00:00.000
            flightsByLandingTime.ToList().ForEach(fblt => Console.WriteLine(fblt));
            Console.WriteLine("");

            Console.WriteLine("getting all flights by origin country id - 1");
            IList<Flight> flightsByOriginCountry = TFF.GetFlightsByOriginCountry(1);
            flightsByOriginCountry.ToList().ForEach(fboc => Console.WriteLine(fboc));
            Console.WriteLine("");

            ///////////////////////////TICKETS//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Ticket tk = new Ticket(0, 2, 1);
            

            Console.WriteLine("getting all tickets:");
            IList<Ticket> tickets = TFF.GetAllTickets();
            tickets.ToList().ForEach(t => Console.WriteLine(t));
            Console.WriteLine("");

            Console.WriteLine("adding ticket with flight id - 2, customer id - 1");
            TFF.AddTicket(tk);
            Console.WriteLine("");

            Console.WriteLine($"Getting ticket with id of {tk.ID}:");
            Console.WriteLine(TFF.GetTicket(tk.ID));
            Console.WriteLine("");

            Console.WriteLine("getting all tickets:");
            tickets = TFF.GetAllTickets();
            tickets.ToList().ForEach(t => Console.WriteLine(t));
            Console.WriteLine("");

            Ticket tk2 = new Ticket(tk.ID, 1, 1);

            Console.WriteLine($"updating ticket with id - {tk.ID}, flight id - 2 => 1");
            TFF.UpdateTicket(tk2);
            Console.WriteLine("");

            Console.WriteLine($"Getting ticket with id of {tk.ID}:");
            Console.WriteLine(TFF.GetTicket(tk.ID));
            Console.WriteLine("");

            Console.WriteLine("getting all tickets:");
            tickets = TFF.GetAllTickets();
            tickets.ToList().ForEach(t => Console.WriteLine(t));
            Console.WriteLine("");

            Console.WriteLine($"removing ticket with id - {tk.ID}:");
            TFF.RemoveTicket(tk2);
            Console.WriteLine("");

            Console.WriteLine("getting all tickets:");
            tickets = TFF.GetAllTickets();
            tickets.ToList().ForEach(t => Console.WriteLine(t));
            Console.WriteLine("");
            
        }
    }
}
