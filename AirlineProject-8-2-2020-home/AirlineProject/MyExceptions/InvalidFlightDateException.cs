using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class InvalidFlightDateException : Exception
    {
        public InvalidFlightDateException()
        {
        }

        public InvalidFlightDateException(string message) : base(message)
        {
        }

        public InvalidFlightDateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidFlightDateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
