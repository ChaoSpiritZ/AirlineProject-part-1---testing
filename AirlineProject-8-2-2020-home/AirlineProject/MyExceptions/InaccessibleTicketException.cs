using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class InaccessibleTicketException : Exception
    {
        public InaccessibleTicketException()
        {
        }

        public InaccessibleTicketException(string message) : base(message)
        {
        }

        public InaccessibleTicketException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InaccessibleTicketException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
