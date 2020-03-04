using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class FlightAlreadyTookOffException : Exception
    {
        public FlightAlreadyTookOffException()
        {
        }

        public FlightAlreadyTookOffException(string message) : base(message)
        {
        }

        public FlightAlreadyTookOffException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightAlreadyTookOffException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
