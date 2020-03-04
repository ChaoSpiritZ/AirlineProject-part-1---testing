using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class FlightDAOMSSQL : IFlightDAO //supposed to be internal
    {
        /// <summary>
        /// adds a flight
        /// </summary>
        /// <param name="t">ID is generated on creation, leave it at 0</param>
        public void Add(Flight t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("ADD_FLIGHT", con))
                {
                    cmd.Parameters.AddWithValue("@airlineCompanyId", t.AirlineCompanyId);
                    cmd.Parameters.AddWithValue("@originCountryCode", t.OriginCountryCode);
                    cmd.Parameters.AddWithValue("@destinationCountryCode", t.DestinationCountryCode);
                    cmd.Parameters.AddWithValue("@departureTime", t.DepartureTime);
                    cmd.Parameters.AddWithValue("@landingTime", t.LandingTime);
                    cmd.Parameters.AddWithValue("@remainingTickets", t.RemainingTickets);
                    cmd.CommandType = CommandType.StoredProcedure;

                    t.ID = (long)(decimal)cmd.ExecuteScalar();
                }
            }
        }

        public void ClearFlights()
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("CLEAR_FLIGHTS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
        }

        public void ClearFlightsHistory()
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("CLEAR_FLIGHTS_HISTORY", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
        }

        /// <summary>
        /// gets a flight by its ID
        /// </summary>
        /// <param name="id">the ID of the flight you are looking for</param>
        /// <returns></returns>
        public Flight Get(long id)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHT", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            return flight;
                        }
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// gets all flights
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetAll()
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_ALL_FLIGHTS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        /// <summary>
        /// gets all flights along with their remaining available tickets
        /// </summary>
        /// <returns></returns>
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flights = new Dictionary<Flight, int>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_ALL_FLIGHTS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight, flight.RemainingTickets);
                        }
                    }
                }
            }
            return flights;
        }

        /// <summary>
        /// gets all flights that belong to a certain airline company
        /// </summary>
        /// <param name="airline">the airline company that owns the flights you are looking for</param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByAirlineCompanyId(AirlineCompany airline)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_AIRLINE_COMPANY_ID", con))
                {
                    cmd.Parameters.AddWithValue("@airlineCompanyId", airline.ID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        /// <summary>
        /// gets all flights that a customer bought tickets for
        /// </summary>
        /// <param name="customer">the customer that owns the tickets of the flights you are looking for</param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_CUSTOMER", con))
                {
                    cmd.Parameters.AddWithValue("@customerId", customer.ID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        /// <summary>
        /// gets all the flights the depart at the inserted date
        /// </summary>
        /// <param name="departureDate">the date of the department of the flights you are looking for, must be precise</param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_DEPARTURE_DATE", con))
                {
                    cmd.Parameters.AddWithValue("@departureDate", departureDate);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        /// <summary>
        /// gets all the flights that land at a certain country
        /// </summary>
        /// <param name="countryCode">the ID of the destination country of the flights you are looking for</param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDestinationCountry(long countryCode)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_DESTINATION_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@countryCode", countryCode);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        /// <summary>
        /// gets all the flights the land at the inserted date
        /// </summary>
        /// <param name="landingDate">the date of the landing time of the flights you are looking for, must be precise</param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_LANDING_DATE", con))
                {
                    cmd.Parameters.AddWithValue("@landingDate", landingDate);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        /// <summary>
        /// gets all the flights that depart from a certain country
        /// </summary>
        /// <param name="countryCode">the ID of the departed country of the flights you are looking for</param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByOriginCountry(long countryCode)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_ORIGIN_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@countryCode", countryCode);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        public void MoveTicketsAndFlightsToHistory()
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("MOVE_TICKETS_AND_FLIGHTS_TO_HISTORY", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
        }

        /// <summary>
        /// removes a flight
        /// </summary>
        /// <param name="t">removes the flight with the same ID</param>
        public void Remove(Flight t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("REMOVE_FLIGHT", con))
                {
                    cmd.Parameters.AddWithValue("@id", t.ID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        public void Update(Flight t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE_FLIGHT", con))
                {
                    cmd.Parameters.AddWithValue("@airlineCompanyId", t.AirlineCompanyId);
                    cmd.Parameters.AddWithValue("@originCountryCode", t.OriginCountryCode);
                    cmd.Parameters.AddWithValue("@destinationCountryCode", t.DestinationCountryCode);
                    cmd.Parameters.AddWithValue("@departureTime", t.DepartureTime);
                    cmd.Parameters.AddWithValue("@landingTime", t.LandingTime);
                    cmd.Parameters.AddWithValue("@remainingTickets", t.RemainingTickets);
                    cmd.Parameters.AddWithValue("@id", t.ID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
        }
    }
}
