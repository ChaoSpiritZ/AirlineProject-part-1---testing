using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class CustomerDAOMSSQL : ICustomerDAO //supposed to be internal
    {
        /// <summary>
        /// adds a customer
        /// </summary>
        /// <param name="t">ID is generated on creation, leave it at 0</param>
        public void Add(Customer t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("ADD_CUSTOMER", con))
                {
                    cmd.Parameters.AddWithValue("@firstName", t.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", t.LastName);
                    cmd.Parameters.AddWithValue("@userName", t.UserName);
                    cmd.Parameters.AddWithValue("@password", t.Password);
                    cmd.Parameters.AddWithValue("@address", t.Address);
                    cmd.Parameters.AddWithValue("@phoneNo", t.PhoneNo);
                    cmd.Parameters.AddWithValue("@creditCardNumber", t.CreditCardNumber);
                    cmd.CommandType = CommandType.StoredProcedure;

                    t.ID = (long)(decimal)cmd.ExecuteScalar();
                }
            }
        }

        public void ClearCustomers()
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("CLEAR_CUSTOMERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
        }

        /// <summary>
        /// gets a customer with the same ID
        /// </summary>
        /// <param name="id">the ID of the customer you are looking for</param>
        /// <returns></returns>
        public Customer Get(long id)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_CUSTOMER", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer()
                            {
                                ID = (long)reader["ID"],
                                FirstName = (string)reader["FIRST_NAME"],
                                LastName = (string)reader["LAST_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                Address = (string)reader["ADDRESS"],
                                PhoneNo = (string)reader["PHONE_NO"],
                                CreditCardNumber = (string)reader["CREDIT_CARD_NUMBER"]
                            };
                            return customer;
                        }
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// gets all the customers
        /// </summary>
        /// <returns></returns>
        public IList<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_ALL_CUSTOMERS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer()
                            {
                                ID = (long)reader["ID"],
                                FirstName = (string)reader["FIRST_NAME"],
                                LastName = (string)reader["LAST_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                Address = (string)reader["ADDRESS"],
                                PhoneNo = (string)reader["PHONE_NO"],
                                CreditCardNumber = (string)reader["CREDIT_CARD_NUMBER"]
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }
            return customers;
        }

        /// <summary>
        /// gets a customer by its username
        /// </summary>
        /// <param name="name">the username of the customer you are looking for</param>
        /// <returns></returns>
        public Customer GetCustomerByUsername(string name)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_CUSTOMER_BY_USERNAME", con))
                {
                    cmd.Parameters.AddWithValue("@userName", name);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer()
                            {
                                ID = (long)reader["ID"],
                                FirstName = (string)reader["FIRST_NAME"],
                                LastName = (string)reader["LAST_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                Address = (string)reader["ADDRESS"],
                                PhoneNo = (string)reader["PHONE_NO"],
                                CreditCardNumber = (string)reader["CREDIT_CARD_NUMBER"]
                            };
                            return customer;
                        }
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// removes a customer
        /// </summary>
        /// <param name="t">removes the customer with the same ID</param>
        public void Remove(Customer t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("REMOVE_CUSTOMER", con))
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
        /// updates a customer
        /// </summary>
        /// <param name="t">updates the customer with the same ID, updates all fields</param>
        public void Update(Customer t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE_CUSTOMER", con))
                {
                    cmd.Parameters.AddWithValue("@firstName", t.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", t.LastName);
                    cmd.Parameters.AddWithValue("@userName", t.UserName);
                    cmd.Parameters.AddWithValue("@password", t.Password);
                    cmd.Parameters.AddWithValue("@address", t.Address);
                    cmd.Parameters.AddWithValue("@phoneNo", t.PhoneNo);
                    cmd.Parameters.AddWithValue("@creditCardNumber", t.CreditCardNumber);
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
