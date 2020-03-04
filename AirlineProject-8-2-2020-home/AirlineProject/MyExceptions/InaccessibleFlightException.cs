using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class InaccessibleFlightException : Exception
    {
        public InaccessibleFlightException()
        {
        }

        public InaccessibleFlightException(string message) : base(message)
        {
        }

        public InaccessibleFlightException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InaccessibleFlightException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
