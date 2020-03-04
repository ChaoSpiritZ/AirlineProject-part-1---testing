using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class Customer : IPoco, IUser
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string CreditCardNumber { get; set; }

        public Customer()
        {
        }

        public Customer(long iD, string firstName, string lastName, string userName, string password, string address, string phoneNo, string creditCardNumber)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            Address = address;
            PhoneNo = phoneNo;
            CreditCardNumber = creditCardNumber;
        }

        public static bool operator ==(Customer item1, Customer item2)
        {
            if (ReferenceEquals(item1, null) && ReferenceEquals(item2, null))
                return true;
            if (ReferenceEquals(item1, null) || ReferenceEquals(item2, null))
                return false;
            return item1.ID == item2.ID;
        }

        public static bool operator !=(Customer item1, Customer item2)
        {
            return !(item1 == item2);
        }

        public override bool Equals(object obj)
        {
            Customer other = (Customer)obj;
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }

        public override string ToString()
        {
            //return $"Customer --- ID: {ID}, First Name: {FirstName}, Last Name: {LastName}, UserName: {UserName}, Password: {Password}, Address: {Address}, Phone Number: {PhoneNo}, Credit Card Number: {CreditCardNumber}";
            return JsonConvert.SerializeObject(this);
        }
    }
}
