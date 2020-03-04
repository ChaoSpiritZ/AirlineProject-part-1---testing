using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AirlineProject
{
    public class AirlineDAOMSSQL : IAirlineDAO //supposed to be internal
    {
        /// <summary>
        /// adds an airline company
        /// </summary>
        /// <param name="t">ID is generated on creation, leave it at 0</param>
        public void Add(AirlineCompany t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("ADD_AIRLINE_COMPANY", con))
                {
                    cmd.Parameters.AddWithValue("@airlineName", t.AirlineName);
                    cmd.Parameters.AddWithValue("@userName", t.UserName);
                    cmd.Parameters.AddWithValue("@password", t.Password);
                    cmd.Parameters.AddWithValue("@countryCode", t.CountryCode);
                    cmd.CommandType = CommandType.StoredProcedure;

                    t.ID = (long)(decimal)cmd.ExecuteScalar();
                }
            }
        }

        public void ClearAirlineCompanies()
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("CLEAR_AIRLINE_COMPANIES", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
        }

        /// <summary>
        /// gets an airline company by its ID
        /// </summary>
        /// <param name="id">the ID of the airline company you are looking for</param>
        /// <returns></returns>
        public AirlineCompany Get(long id)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_AIRLINE_COMPANY", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AirlineCompany airlineCompany = new AirlineCompany()
                            {
                                ID = (long)reader["ID"],
                                AirlineName = (string)reader["AIRLINE_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                CountryCode = (long)reader["COUNTRY_CODE"]
                            };
                            return airlineCompany;
                        }
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// gets an airline company by its name
        /// </summary>
        /// <param name="name">the name of the airline company you are looking for</param>
        /// <returns></returns>
        public AirlineCompany GetAirlineByAirlineName(string name)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_AIRLINE_BY_AIRLINE_NAME", con))
                {
                    cmd.Parameters.AddWithValue("@airlineName", name);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AirlineCompany airlineCompany = new AirlineCompany()
                            {
                                ID = (long)reader["ID"],
                                AirlineName = (string)reader["AIRLINE_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                CountryCode = (long)reader["COUNTRY_CODE"]
                            };
                            return airlineCompany;
                        }
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// gets an airline company by its username
        /// </summary>
        /// <param name="name">the username of the airline company you are looking for</param>
        /// <returns></returns>
        public AirlineCompany GetAirlineByUsername(string name)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_AIRLINE_BY_USERNAME", con))
                {
                    cmd.Parameters.AddWithValue("@userName", name);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AirlineCompany airlineCompany = new AirlineCompany()
                            {
                                ID = (long)reader["ID"],
                                AirlineName = (string)reader["AIRLINE_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                CountryCode = (long)reader["COUNTRY_CODE"]
                            };
                            return airlineCompany;
                        }
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// gets all the airline companies
        /// </summary>
        /// <returns></returns>
        public IList<AirlineCompany> GetAll()
        {
            List<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_ALL_AIRLINE_COMPANIES", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AirlineCompany airlineCompany = new AirlineCompany()
                            {
                                ID = (long)reader["ID"],
                                AirlineName = (string)reader["AIRLINE_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                CountryCode = (long)reader["COUNTRY_CODE"]
                            };
                            airlineCompanies.Add(airlineCompany);
                        }
                    }
                }
            }
            return airlineCompanies;
        }

        /// <summary>
        /// gets all of the airline companies that were created by the given country
        /// </summary>
        /// <param name="countryId">the id of the country you are trying to get all the airline companies from</param>
        /// <returns></returns>
        public IList<AirlineCompany> GetAllAirlinesByCountry(long countryId)
        {
            List<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_ALL_AIRLINES_BY_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@countryId", countryId);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AirlineCompany airlineCompany = new AirlineCompany()
                            {
                                ID = (long)reader["ID"],
                                AirlineName = (string)reader["AIRLINE_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                CountryCode = (long)reader["COUNTRY_CODE"]
                            };
                            airlineCompanies.Add(airlineCompany);
                        }
                    }
                }
            }
            return airlineCompanies;
        }

        /// <summary>
        /// removes an airline company
        /// </summary>
        /// <param name="t">removes the airline with the matching ID</param>
        public void Remove(AirlineCompany t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("REMOVE_AIRLINE_COMPANY", con))
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
        /// updates an airline company
        /// </summary>
        /// <param name="t">updates the airline with the matching id, updates all fields</param>
        public void Update(AirlineCompany t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE_AIRLINE_COMPANY", con))
                {
                    cmd.Parameters.AddWithValue("@airlineName", t.AirlineName);
                    cmd.Parameters.AddWithValue("@userName", t.UserName);
                    cmd.Parameters.AddWithValue("@password", t.Password);
                    cmd.Parameters.AddWithValue("@countryCode", t.CountryCode);
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
