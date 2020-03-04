using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class Country : IPoco
    {
        public long ID { get; set; }
        public string CountryName { get; set; }

        public Country()
        {
        }

        public Country(long iD, string countryName)
        {
            ID = iD;
            CountryName = countryName;
        }

        public static bool operator ==(Country item1, Country item2)
        {
            if (ReferenceEquals(item1, null) && ReferenceEquals(item2, null))
                return true;
            if (ReferenceEquals(item1, null) || ReferenceEquals(item2, null))
                return false;
            return item1.ID == item2.ID;
        }

        public static bool operator !=(Country item1, Country item2)
        {
            return !(item1 == item2);
        }

        public override bool Equals(object obj)
        {
            Country other = (Country)obj;
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)ID;
        }

        public override string ToString()
        {
            //return $"Country --- ID: {ID}, Country Name: {CountryName}";
            return JsonConvert.SerializeObject(this);
        }
    }
}
