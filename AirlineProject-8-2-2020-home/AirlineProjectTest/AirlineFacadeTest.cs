using System;
using System.Collections.Generic;
using AirlineProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirlineProjectTest
{
    [TestClass]
    public class AirlineFacadeTest
    {
        //[ExpectedException(typeof(WrongPasswordException))]
        //[TestMethod]
        //public void AirlineFacade_INSERT_GET_BY_ID_FLIGHT()
        //{

        //    // Arrange
        //    // LOGIN FOR ADMIN

        //    // ACT
        //    // INSERT AIRLINE -- return company id
        //    // CREATE FLIGHT -- return flight id (x)
        //    // GET FLIGHT by id (x)

        //    // Assert
        //    int x = 5;
        //    int y = x * 2 ;
        //    Assert.AreEqual(10, y);

        //    //AirlineCompany get_result = null;
        //    //Assert.AreEqual(TestData.Airline_1.AirlineName, get_result.AirlineName);

        //    Console.WriteLine("");
        //    throw new WrongPasswordException();

        //}

        [TestInitialize] //goes to this after every test
        public void Initialize()
        {
            AirlineProjectConfig.CONNECTION_STRING = @"Data Source=DESKTOP-JJ6DFK2;Initial Catalog=TESTINGAirlineProject;Integrated Security=True";
            TestData.testFacade.ClearAllTables();
        }

        [TestCleanup]
        public void Cleanup()
        {
            AirlineProjectConfig.CONNECTION_STRING = @"Data Source=DESKTOP-JJ6DFK2;Initial Catalog=AirlineProject;Integrated Security=True";
        }

        //--------------------------ANONYMOUS-FACADE-----------------------------------------

        [TestMethod]
        public void AnonymousFacadeGetAirlineCompanyByIdMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            AirlineCompany actualAirline = anonymousFacade.GetAirlineCompanyById(TestData.airline1.ID);

            Assert.AreEqual(TestData.airline1.ID, actualAirline.ID);
            Assert.AreEqual(TestData.airline1.AirlineName, actualAirline.AirlineName);
            Assert.AreEqual(TestData.airline1.UserName, actualAirline.UserName);
            Assert.AreEqual(TestData.airline1.Password, actualAirline.Password);
            Assert.AreEqual(TestData.airline1.CountryCode, actualAirline.CountryCode);

            //pretty much a AdminFacadeCreateNewAirlineMethod clone
        }

        [TestMethod]
        public void AnonymousFacadeGetAllAirlineCompaniesMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline2);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            IList<AirlineCompany> airlineCompanies = anonymousFacade.GetAllAirlineCompanies();

            Assert.AreEqual(TestData.airline1, airlineCompanies[0]);
            Assert.AreEqual(TestData.airline2, airlineCompanies[1]);
        }

        [TestMethod]
        public void AnonymousFacadeGetAllFlightsMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken);
            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            Flight flight2 = new Flight(0, 0, TestData.chadID, TestData.denmarkID, TestData.futureDate1, TestData.futureDate2, 190);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight1);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight2);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            IList<Flight> flights = anonymousFacade.GetAllFlights();

            Assert.AreEqual(flight1, flights[0]);
            Assert.AreEqual(flight2, flights[1]);
        }

        [TestMethod]
        public void AnonymousFacadeGetAllFlightsVacancyMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken);
            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            Flight flight2 = new Flight(0, 0, TestData.chadID, TestData.denmarkID, TestData.futureDate1, TestData.futureDate2, 190);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight1);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight2);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            Dictionary<Flight, int> flightsVacancy = anonymousFacade.GetAllFlightsVacancy();

            Assert.AreEqual(flight1.RemainingTickets, flightsVacancy[flight1]);
            Assert.AreEqual(flight2.RemainingTickets, flightsVacancy[flight2]);
        }

        [TestMethod]
        public void AnonymousFacadeGetFlightMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken);
            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight1);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            Flight actualFlight = anonymousFacade.GetFlight(flight1.ID);

            Assert.AreEqual(flight1.ID, actualFlight.ID);
            Assert.AreEqual(flight1.AirlineCompanyId, actualFlight.AirlineCompanyId);
            Assert.AreEqual(flight1.OriginCountryCode, actualFlight.OriginCountryCode);
            Assert.AreEqual(flight1.DestinationCountryCode, actualFlight.DestinationCountryCode);
            Assert.AreEqual(flight1.DepartureTime, actualFlight.DepartureTime);
            Assert.AreEqual(flight1.LandingTime, actualFlight.LandingTime);
            Assert.AreEqual(flight1.RemainingTickets, actualFlight.RemainingTickets);
        }

        [TestMethod]
        public void AnonymousFacadeGetFlightsByDepartureDateMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken);
            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            Flight flight2 = new Flight(0, 0, TestData.chadID, TestData.denmarkID, TestData.futureDate1, TestData.futureDate3, 190);
            Flight flight3 = new Flight(0, 0, TestData.egyptID, TestData.franceID, TestData.futureDate2, TestData.futureDate3, 180);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight1);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight2);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight3);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            IList<Flight> flights = anonymousFacade.GetFlightsByDepartureDate(TestData.futureDate1);

            Assert.AreEqual(flight1, flights[0]);
            Assert.AreEqual(flight2, flights[1]);
            Assert.AreEqual(2, flights.Count);
        }

        [TestMethod]
        public void AnonymousFacadeGetFlightsByDestinationCountryMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken);
            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            Flight flight2 = new Flight(0, 0, TestData.argentinaID, TestData.chadID, TestData.futureDate1, TestData.futureDate3, 190);
            Flight flight3 = new Flight(0, 0, TestData.barbadosID, TestData.chadID, TestData.futureDate2, TestData.futureDate3, 180);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight1);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight2);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight3);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            IList<Flight> flights = anonymousFacade.GetFlightsByDestinationCountry(TestData.chadID);

            Assert.AreEqual(flight2, flights[0]);
            Assert.AreEqual(flight3, flights[1]);
            Assert.AreEqual(2, flights.Count);
        }

        [TestMethod]
        public void AnonymousFacadeGetFlightsByLandingDateMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken);
            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            Flight flight2 = new Flight(0, 0, TestData.chadID, TestData.denmarkID, TestData.futureDate1, TestData.futureDate3, 190);
            Flight flight3 = new Flight(0, 0, TestData.egyptID, TestData.franceID, TestData.futureDate2, TestData.futureDate3, 180);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight1);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight2);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight3);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            IList<Flight> flights = anonymousFacade.GetFlightsByLandingDate(TestData.futureDate3);

            Assert.AreEqual(flight2, flights[0]);
            Assert.AreEqual(flight3, flights[1]);
            Assert.AreEqual(2, flights.Count);
        }

        [TestMethod]
        public void AnonymousFacadeGetFlightsByOriginCountryMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken);
            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            Flight flight2 = new Flight(0, 0, TestData.argentinaID, TestData.chadID, TestData.futureDate1, TestData.futureDate3, 190);
            Flight flight3 = new Flight(0, 0, TestData.barbadosID, TestData.chadID, TestData.futureDate2, TestData.futureDate3, 180);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight1);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight2);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, flight3);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            IList<Flight> flights = anonymousFacade.GetFlightsByOriginCountry(TestData.argentinaID);

            Assert.AreEqual(flight1, flights[0]);
            Assert.AreEqual(flight2, flights[1]);
            Assert.AreEqual(2, flights.Count);
        }

        //----------------------ADMIN-FACADE------------------------------------------

        [TestMethod]
        public void AdminFacadeCreateNewAirlineMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            AirlineCompany actualAirline = anonymousFacade.GetAirlineCompanyById(TestData.airline1.ID);

            Assert.AreEqual(TestData.airline1.ID, actualAirline.ID);
            Assert.AreEqual(TestData.airline1.AirlineName, actualAirline.AirlineName);
            Assert.AreEqual(TestData.airline1.UserName, actualAirline.UserName);
            Assert.AreEqual(TestData.airline1.Password, actualAirline.Password);
            Assert.AreEqual(TestData.airline1.CountryCode, actualAirline.CountryCode);
        }

        [TestMethod]
        public void AdminFacadeCreateNewCustomerMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);

            Customer actualCustomer = TestData.testFacade.GetCustomerById(TestData.customer1.ID);

            Assert.AreEqual(TestData.customer1.ID, actualCustomer.ID);
            Assert.AreEqual(TestData.customer1.FirstName, actualCustomer.FirstName);
            Assert.AreEqual(TestData.customer1.LastName, actualCustomer.LastName);
            Assert.AreEqual(TestData.customer1.UserName, actualCustomer.UserName);
            Assert.AreEqual(TestData.customer1.Password, actualCustomer.Password);
            Assert.AreEqual(TestData.customer1.Address, actualCustomer.Address);
            Assert.AreEqual(TestData.customer1.PhoneNo, actualCustomer.PhoneNo);
            Assert.AreEqual(TestData.customer1.CreditCardNumber, actualCustomer.CreditCardNumber);
        }

        [TestMethod]
        public void AdminFacadeRemoveAirlineMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.RemoveAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);

            Assert.AreEqual(null, anonymousFacade.GetAirlineCompanyById(TestData.airline1.ID));
        }

        [TestMethod]
        public void AdminFacadeRemoveCustomerMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);
            adminFacade.RemoveCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);

            Assert.AreEqual(null, TestData.testFacade.GetCustomerById(TestData.customer1.ID));
        }

        [TestMethod]
        public void AdminFacadeUpdateAirlineDetailsMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            AirlineCompany updatedAirline = new AirlineCompany(TestData.airline1.ID, "Alpho", "AlphoUser", "AlphoPass", TestData.argentinaID);
            adminFacade.UpdateAirlineDetails((LoginToken<Administrator>)adminToken, updatedAirline);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            AirlineCompany actualAirline = anonymousFacade.GetAirlineCompanyById(TestData.airline1.ID);

            Assert.AreEqual(updatedAirline.ID, actualAirline.ID);
            Assert.AreEqual(updatedAirline.AirlineName, actualAirline.AirlineName);
            Assert.AreEqual(updatedAirline.UserName, actualAirline.UserName);
            Assert.AreEqual(updatedAirline.Password, actualAirline.Password);
            Assert.AreEqual(updatedAirline.CountryCode, actualAirline.CountryCode);
        }

        [TestMethod]
        public void AdminFacadeUpdateCustomerDetailsMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);
            Customer updatedCustomer = new Customer(TestData.customer1.ID, "Alphonse", "Alric", "AlphonseUser", "AlphonsePass", "AlphonseAddress", "050-aaaaaaa", "creditaaa");
            adminFacade.UpdateCustomerDetails((LoginToken<Administrator>)adminToken, updatedCustomer);

            Customer actualCustomer = TestData.testFacade.GetCustomerById(TestData.customer1.ID);

            Assert.AreEqual(updatedCustomer.ID, actualCustomer.ID);
            Assert.AreEqual(updatedCustomer.FirstName, actualCustomer.FirstName);
            Assert.AreEqual(updatedCustomer.LastName, actualCustomer.LastName);
            Assert.AreEqual(updatedCustomer.UserName, actualCustomer.UserName);
            Assert.AreEqual(updatedCustomer.Password, actualCustomer.Password);
            Assert.AreEqual(updatedCustomer.Address, actualCustomer.Address);
            Assert.AreEqual(updatedCustomer.PhoneNo, actualCustomer.PhoneNo);
            Assert.AreEqual(updatedCustomer.CreditCardNumber, actualCustomer.CreditCardNumber);
        }

        //-----------------------------------AIRLINE-FACADE----------------------------------

        [TestMethod]
        public void AirlineFacadeCancelFlightMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken);
            Flight newFlight = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, newFlight);
            airlineFacade.CancelFlight((LoginToken<AirlineCompany>)airlineToken, newFlight);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);

            Assert.AreEqual(null, anonymousFacade.GetFlight(newFlight.ID));
        }

        [TestMethod]
        public void AirlineFacadeChangeMyPasswordMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken);
            airlineFacade.ChangeMyPassword((LoginToken<AirlineCompany>)airlineToken, TestData.airline1.Password, "54321");

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);

            Assert.AreEqual("54321", anonymousFacade.GetAirlineCompanyById(TestData.airline1.ID).Password);
        }

        [TestMethod]
        public void AirlineFacadeCreateFlightMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken);
            Flight newFlight = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            airlineFacade.CreateFlight((LoginToken<AirlineCompany>)airlineToken, newFlight);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            Flight actualFlight = anonymousFacade.GetFlight(newFlight.ID);

            Assert.AreEqual(newFlight.ID, actualFlight.ID);
            Assert.AreEqual(newFlight.AirlineCompanyId, actualFlight.AirlineCompanyId);
            Assert.AreEqual(newFlight.OriginCountryCode, actualFlight.OriginCountryCode);
            Assert.AreEqual(newFlight.DestinationCountryCode, actualFlight.DestinationCountryCode);
            Assert.AreEqual(newFlight.DepartureTime, actualFlight.DepartureTime);
            Assert.AreEqual(newFlight.LandingTime, actualFlight.LandingTime);
            Assert.AreEqual(newFlight.RemainingTickets, actualFlight.RemainingTickets);
        }

        [TestMethod]
        public void AirlineFacadeGetAllFlightsMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline2);

            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            Flight flight2 = new Flight(0, 0, TestData.chadID, TestData.denmarkID, TestData.futureDate1, TestData.futureDate3, 200);
            Flight flight3 = new Flight(0, 0, TestData.egyptID, TestData.franceID, TestData.futureDate2, TestData.futureDate3, 200);
            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            ILoggedInAirlineFacade airlineFacade2 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline2.UserName, TestData.airline2.Password, out ILoginToken airlineToken2);
            airlineFacade1.CreateFlight((LoginToken<AirlineCompany>)airlineToken1, flight1);
            airlineFacade1.CreateFlight((LoginToken<AirlineCompany>)airlineToken1, flight2);
            airlineFacade2.CreateFlight((LoginToken<AirlineCompany>)airlineToken2, flight3);

            IList<Flight> flights1 = airlineFacade1.GetAllFlights((LoginToken<AirlineCompany>)airlineToken1);

            Assert.AreEqual(flight1, flights1[0]);
            Assert.AreEqual(flight2, flights1[1]);
            Assert.AreEqual(2, flights1.Count);
        }

        [TestMethod]
        public void AirlineFacadeGetAllTicketsMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline2);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer2);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer3);

            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            Flight flight2 = new Flight(0, 0, TestData.chadID, TestData.denmarkID, TestData.futureDate1, TestData.futureDate3, 200);
            Flight flight3 = new Flight(0, 0, TestData.egyptID, TestData.franceID, TestData.futureDate2, TestData.futureDate3, 200);
            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            ILoggedInAirlineFacade airlineFacade2 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline2.UserName, TestData.airline2.Password, out ILoginToken airlineToken2);
            airlineFacade1.CreateFlight((LoginToken<AirlineCompany>)airlineToken1, flight1);
            airlineFacade1.CreateFlight((LoginToken<AirlineCompany>)airlineToken1, flight2);
            airlineFacade2.CreateFlight((LoginToken<AirlineCompany>)airlineToken2, flight3);

            ILoggedInCustomerFacade customerFacade1 = (ILoggedInCustomerFacade)TestData.fcs.Login(TestData.customer1.UserName, TestData.customer1.Password, out ILoginToken customerToken1);
            ILoggedInCustomerFacade customerFacade2 = (ILoggedInCustomerFacade)TestData.fcs.Login(TestData.customer2.UserName, TestData.customer2.Password, out ILoginToken customerToken2);
            ILoggedInCustomerFacade customerFacade3 = (ILoggedInCustomerFacade)TestData.fcs.Login(TestData.customer3.UserName, TestData.customer3.Password, out ILoginToken customerToken3);
            Ticket ticket1 = customerFacade1.PurchaseTicket((LoginToken<Customer>)customerToken1, flight1);
            Ticket ticket2 = customerFacade2.PurchaseTicket((LoginToken<Customer>)customerToken2, flight2);
            Ticket ticket3 = customerFacade3.PurchaseTicket((LoginToken<Customer>)customerToken3, flight3);

            IList<Ticket> tickets1 = airlineFacade1.GetAllTickets((LoginToken<AirlineCompany>)airlineToken1);

            Assert.AreEqual(ticket1, tickets1[0]);
            Assert.AreEqual(ticket2, tickets1[1]);
            Assert.AreEqual(2, tickets1.Count);
        }

        [TestMethod]
        public void AirlineFacadeModifyAirlineDetailsMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            AirlineCompany updatedAirline = new AirlineCompany(TestData.airline1.ID, "Alpho", "AlphoUser", "AlphoPass", TestData.argentinaID);
            airlineFacade1.ModifyAirlineDetails((LoginToken<AirlineCompany>)airlineToken1, updatedAirline);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            AirlineCompany actualAirline = anonymousFacade.GetAirlineCompanyById(TestData.airline1.ID);

            Assert.AreEqual(updatedAirline.ID, actualAirline.ID);
            Assert.AreEqual(updatedAirline.AirlineName, actualAirline.AirlineName);
            Assert.AreEqual(updatedAirline.UserName, actualAirline.UserName);
            Assert.AreEqual(updatedAirline.Password, actualAirline.Password);
            Assert.AreEqual(updatedAirline.CountryCode, actualAirline.CountryCode);
        }

        [TestMethod]
        public void AirlineFacadeUpdateFlightMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            
            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            airlineFacade1.CreateFlight((LoginToken<AirlineCompany>)airlineToken1, flight1);
            Flight updatedFlight = new Flight(flight1.ID, flight1.AirlineCompanyId, TestData.chadID, TestData.denmarkID, TestData.futureDate1, TestData.futureDate2, 199);
            airlineFacade1.UpdateFlight((LoginToken<AirlineCompany>)airlineToken1, updatedFlight);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);
            Flight actualFlight = anonymousFacade.GetFlight(flight1.ID);

            Assert.AreEqual(updatedFlight.ID, actualFlight.ID);
            Assert.AreEqual(updatedFlight.AirlineCompanyId, actualFlight.AirlineCompanyId);
            Assert.AreEqual(updatedFlight.OriginCountryCode, actualFlight.OriginCountryCode);
            Assert.AreEqual(updatedFlight.DestinationCountryCode, actualFlight.DestinationCountryCode);
            Assert.AreEqual(updatedFlight.DepartureTime, actualFlight.DepartureTime);
            Assert.AreEqual(updatedFlight.LandingTime, actualFlight.LandingTime);
            Assert.AreEqual(updatedFlight.RemainingTickets, actualFlight.RemainingTickets);
        }

        //-----------------------------------CUSTOMER-FACADE----------------------------------

        [TestMethod]
        public void CustomerFacadeCancelTicketMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);

            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            airlineFacade1.CreateFlight((LoginToken<AirlineCompany>)airlineToken1, flight1);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);

            //Assert.AreEqual(200, anonymousFacade.GetFlight(flight1.ID).RemainingTickets);

            ILoggedInCustomerFacade customerFacade1 = (ILoggedInCustomerFacade)TestData.fcs.Login(TestData.customer1.UserName, TestData.customer1.Password, out ILoginToken customerToken1);
            Ticket ticket1 = customerFacade1.PurchaseTicket((LoginToken<Customer>)customerToken1, flight1);

            Assert.AreEqual(199, anonymousFacade.GetFlight(flight1.ID).RemainingTickets);

            customerFacade1.CancelTicket((LoginToken<Customer>)customerToken1, ticket1);

            Assert.AreEqual(200, anonymousFacade.GetFlight(flight1.ID).RemainingTickets);
            Assert.AreEqual(0, customerFacade1.GetAllMyFlights((LoginToken<Customer>)customerToken1).Count);
        }

        [TestMethod]
        public void CustomerFacadeGetAllMyFlightsMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);

            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            Flight flight2 = new Flight(0, 0, TestData.chadID, TestData.denmarkID, TestData.futureDate1, TestData.futureDate3, 190);
            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            airlineFacade1.CreateFlight((LoginToken<AirlineCompany>)airlineToken1, flight1);
            airlineFacade1.CreateFlight((LoginToken<AirlineCompany>)airlineToken1, flight2);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);

            ILoggedInCustomerFacade customerFacade1 = (ILoggedInCustomerFacade)TestData.fcs.Login(TestData.customer1.UserName, TestData.customer1.Password, out ILoginToken customerToken1);

            Assert.AreEqual(0, customerFacade1.GetAllMyFlights((LoginToken<Customer>)customerToken1).Count);

            Ticket ticket1 = customerFacade1.PurchaseTicket((LoginToken<Customer>)customerToken1, flight1);
            Ticket ticket2 = customerFacade1.PurchaseTicket((LoginToken<Customer>)customerToken1, flight2);

            IList<Flight> actualflights = customerFacade1.GetAllMyFlights((LoginToken<Customer>)customerToken1);

            Assert.AreEqual(2, customerFacade1.GetAllMyFlights((LoginToken<Customer>)customerToken1).Count);
            Assert.AreEqual(flight1, actualflights[0]);
            Assert.AreEqual(flight2, actualflights[1]);
        }

        [TestMethod]
        public void CustomerFacadePurchaseTicketMethod()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);

            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            airlineFacade1.CreateFlight((LoginToken<AirlineCompany>)airlineToken1, flight1);

            IAnonymousUserFacade anonymousFacade = (IAnonymousUserFacade)TestData.fcs.Login("", "", out ILoginToken anonymousToken);

            Assert.AreEqual(200, anonymousFacade.GetFlight(flight1.ID).RemainingTickets);

            ILoggedInCustomerFacade customerFacade1 = (ILoggedInCustomerFacade)TestData.fcs.Login(TestData.customer1.UserName, TestData.customer1.Password, out ILoginToken customerToken1);
            Ticket ticket1 = customerFacade1.PurchaseTicket((LoginToken<Customer>)customerToken1, flight1);

            Assert.AreEqual(199, anonymousFacade.GetFlight(flight1.ID).RemainingTickets);
            Assert.AreEqual(1, customerFacade1.GetAllMyFlights((LoginToken<Customer>)customerToken1).Count);
            Assert.AreEqual(ticket1.FlightId, anonymousFacade.GetFlight(flight1.ID).ID);
            Assert.AreEqual(ticket1.CustomerId, TestData.customer1.ID);
        }

        //----------------------expected-exceptions-----------------------------

        [TestMethod]
        [ExpectedException(typeof(AirlineNameAlreadyExistsException))]

        public void AdminFacadeCreateNewAirlineAirlineNameAlreadyExistsException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
        }

        [TestMethod]
        [ExpectedException(typeof(AirlineNameAlreadyExistsException))]
        public void AirlineFacadeModifyAirlineDetailsMethodAirlineNameAlreadyExistsException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline2);

            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            airlineFacade1.ModifyAirlineDetails((LoginToken<AirlineCompany>)airlineToken1, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade2 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline2.UserName, TestData.airline2.Password, out ILoginToken airlineToken2);
            AirlineCompany modifiedAirline = new AirlineCompany(TestData.airline2.ID, TestData.airline1.AirlineName, TestData.airline2.UserName, TestData.airline2.Password, TestData.airline2.CountryCode);
            modifiedAirline.AirlineName = TestData.airline1.AirlineName;
            airlineFacade2.ModifyAirlineDetails((LoginToken<AirlineCompany>)airlineToken2, modifiedAirline);
        }

        [TestMethod]
        [ExpectedException(typeof(UsernameAlreadyExistsException))]
        public void AirlineFacadeModifyAirlineDetailsMethodUsernameAlreadyExistsException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline2);

            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            airlineFacade1.ModifyAirlineDetails((LoginToken<AirlineCompany>)airlineToken1, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade2 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline2.UserName, TestData.airline2.Password, out ILoginToken airlineToken2);
            AirlineCompany modifiedAirline = new AirlineCompany(TestData.airline2.ID, TestData.airline2.AirlineName, TestData.airline1.UserName, TestData.airline2.Password, TestData.airline2.CountryCode);
            modifiedAirline.UserName = TestData.airline1.UserName;
            airlineFacade2.ModifyAirlineDetails((LoginToken<AirlineCompany>)airlineToken2, modifiedAirline);
        }

        [TestMethod]
        [ExpectedException(typeof(CountryNotFoundException))]
        public void AdminFacadeCreateNewAirlineMethodCountryNotFoundException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, new AirlineCompany(0, "omega", "omegauser", "omegapass", 15));
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyPasswordException))]

        public void AirlineFacadeModifyAirlineDetailsMethodEmptyPasswordException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            airlineFacade1.ChangeMyPassword((LoginToken<AirlineCompany>)airlineToken1, TestData.airline1.Password, "");

        }

        [TestMethod]
        [ExpectedException(typeof(NoMoreTicketsException))]
        public void CustomerFacadePurchaseTicketMethodNoMoreTicketsException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);

            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 0);
            airlineFacade1.CreateFlight((LoginToken<AirlineCompany>)airlineToken1, flight1);

            ILoggedInCustomerFacade customerFacade1 = (ILoggedInCustomerFacade)TestData.fcs.Login(TestData.customer1.UserName, TestData.customer1.Password, out ILoginToken customerToken1);
            customerFacade1.PurchaseTicket((LoginToken<Customer>)customerToken1, flight1);
        }

        [TestMethod]
        [ExpectedException(typeof(TicketAlreadyExistsException))]
        public void CustomerFacadePurchaseTicketMethodTicketAlreadyExistsException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);

            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, TestData.airline1.Password, out ILoginToken airlineToken1);
            Flight flight1 = new Flight(0, 0, TestData.argentinaID, TestData.barbadosID, TestData.futureDate1, TestData.futureDate2, 200);
            airlineFacade1.CreateFlight((LoginToken<AirlineCompany>)airlineToken1, flight1);

            ILoggedInCustomerFacade customerFacade1 = (ILoggedInCustomerFacade)TestData.fcs.Login(TestData.customer1.UserName, TestData.customer1.Password, out ILoginToken customerToken1);
            customerFacade1.PurchaseTicket((LoginToken<Customer>)customerToken1, flight1);
            customerFacade1.PurchaseTicket((LoginToken<Customer>)customerToken1, flight1);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void AdminFacadeRemoveAirlineMethodUserNotFoundException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.RemoveAirline((LoginToken<Administrator>)adminToken, TestData.airline1);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void AdminFacadeRemoveCustomerMethodUserNotFoundException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.RemoveCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void AdminLoginMethodWrongPasswordException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, "wrongpass", out ILoginToken adminToken);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void AirlineLoginMethodWrongPasswordException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewAirline((LoginToken<Administrator>)adminToken, TestData.airline1);

            ILoggedInAirlineFacade airlineFacade1 = (ILoggedInAirlineFacade)TestData.fcs.Login(TestData.airline1.UserName, "wrongpass", out ILoginToken airlineToken1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void CustomerLoginMethodWrongPasswordException()
        {
            ILoggedInAdministratorFacade adminFacade = (ILoggedInAdministratorFacade)TestData.fcs.Login(AirlineProjectConfig.ADMIN_USERNAME, AirlineProjectConfig.ADMIN_PASSWORD, out ILoginToken adminToken);
            adminFacade.CreateNewCustomer((LoginToken<Administrator>)adminToken, TestData.customer1);

            ILoggedInCustomerFacade customerFacade1 = (ILoggedInCustomerFacade)TestData.fcs.Login(TestData.customer1.UserName, "wrongpass", out ILoginToken customerToken1);

        }
    }
}
