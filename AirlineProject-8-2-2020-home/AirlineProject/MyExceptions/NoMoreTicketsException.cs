using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class NoMoreTicketsException : Exception
    {
        public NoMoreTicketsException()
        {
        }

        public NoMoreTicketsException(string message) : base(message)
        {
        }

        public NoMoreTicketsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoMoreTicketsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
