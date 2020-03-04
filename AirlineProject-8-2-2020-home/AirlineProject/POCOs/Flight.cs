using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class Flight : IPoco
    {
        public long ID { get; set; }
        public long AirlineCompanyId { get; set; }
        public long OriginCountryCode { get; set; }
        public long DestinationCountryCode { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public int RemainingTickets { get; set; }

        public Flight()
        {
        }

        public Flight(long iD, long airlineCompanyId, long originCountryCode, long destinationCountryCode, DateTime departureTime, DateTime landingTime, int remainingTickets)
        {
            ID = iD;
            AirlineCompanyId = airlineCompanyId;
            OriginCountryCode = originCountryCode;
            DestinationCountryCode = destinationCountryCode;
            DepartureTime = departureTime;
            LandingTime = landingTime;
            RemainingTickets = remainingTickets;
        }

        public static bool operator ==(Flight item1, Flight item2)
        {
            if (ReferenceEquals(item1, null) && ReferenceEquals(item2, null))
                return true;
            if (ReferenceEquals(item1, null) || ReferenceEquals(item2, null))
                return false;
            return item1.ID == item2.ID;
        }

        public static bool operator !=(Flight item1, Flight item2)
        {
            return !(item1 == item2);
        }

        public override bool Equals(object obj)
        {
            Flight other = (Flight)obj;
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }

        public override string ToString()
        {
            //return $"Flight --- ID: {ID}, Airline Company ID: {AirlineCompanyId}, Origin Country Code: {OriginCountryCode}, Destination Country Code: {DestinationCountryCode}, Departure Time: {DepartureTime}, Landing Time: {LandingTime}, Remaining Tickets: {RemainingTickets}";
            return JsonConvert.SerializeObject(this);
        }
    }
}
