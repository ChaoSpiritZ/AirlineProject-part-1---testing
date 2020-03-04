using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AirlineProject
{
    public class CountryDAOMSSQL : ICountryDAO //supposed to be internal
    {
        /// <summary>
        /// adds a country
        /// </summary>
        /// <param name="t">ID is generated on creation, leave it at 0</param>
        public void Add(Country t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("ADD_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@countryName", t.CountryName);
                    cmd.CommandType = CommandType.StoredProcedure;

                    t.ID = (long)(decimal)cmd.ExecuteScalar();
                }
            }
        }

        public void ClearCountries()
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("CLEAR_COUNTRIES", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
        }

        /// <summary>
        /// gets a country by its ID
        /// </summary>
        /// <param name="id">the ID of the country you are looking for</param>
        /// <returns></returns>
        public Country Get(long id)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Country country = new Country()
                            {
                                ID = (long)reader["ID"],
                                CountryName = (string)reader["COUNTRY_NAME"]
                            };
                            return country;
                        }
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// gets all the countries
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAll()
        {
            List<Country> countries = new List<Country>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_ALL_COUNTRIES", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Country country = new Country()
                            {
                                ID = (long)reader["ID"],
                                CountryName = (string)reader["COUNTRY_NAME"]
                            };
                            countries.Add(country);
                        }
                    }
                }
            }
            return countries;
        }

        /// <summary>
        /// removes a country
        /// </summary>
        /// <param name="t">removes the country with the same ID</param>
        public void Remove(Country t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("REMOVE_COUNTRY", con))
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
        /// updates a country
        /// </summary>
        /// <param name="t">updates the country with the same ID, updates all fields</param>
        public void Update(Country t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@countryName", t.CountryName);
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
