using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class Ticket : IPoco
    {
        public long ID { get; set; }
        public long FlightId { get; set; }
        public long CustomerId { get; set; }

        public Ticket()
        {
        }

        public Ticket(long iD, long flightId, long customerId)
        {
            ID = iD;
            FlightId = flightId;
            CustomerId = customerId;
        }

        public static bool operator ==(Ticket item1, Ticket item2)
        {
            if (ReferenceEquals(item1, null) && ReferenceEquals(item2, null))
                return true;
            if (ReferenceEquals(item1, null) || ReferenceEquals(item2, null))
                return false;
            return item1.ID == item2.ID;
        }

        public static bool operator !=(Ticket item1, Ticket item2)
        {
            return !(item1 == item2);
        }

        public override bool Equals(object obj)
        {
            Ticket other = (Ticket)obj;
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }

        public override string ToString()
        {
            //return $"Ticket --- ID: {ID}, Flight ID: {FlightId}, Customer ID: {CustomerId}";
            return JsonConvert.SerializeObject(this);
        }
    }
}
