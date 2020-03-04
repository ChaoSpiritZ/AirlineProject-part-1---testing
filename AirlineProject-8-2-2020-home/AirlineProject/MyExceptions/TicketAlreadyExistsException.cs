using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class TicketAlreadyExistsException : Exception
    {
        public TicketAlreadyExistsException()
        {
        }

        public TicketAlreadyExistsException(string message) : base(message)
        {
        }

        public TicketAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TicketAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
