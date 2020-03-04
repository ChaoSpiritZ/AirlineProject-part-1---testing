using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class InaccessibleAirlineCompanyException : Exception
    {
        public InaccessibleAirlineCompanyException()
        {
        }

        public InaccessibleAirlineCompanyException(string message) : base(message)
        {
        }

        public InaccessibleAirlineCompanyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InaccessibleAirlineCompanyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
