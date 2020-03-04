using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class AirlineNameAlreadyExistsException : Exception
    {
        public AirlineNameAlreadyExistsException()
        {
        }

        public AirlineNameAlreadyExistsException(string message) : base(message)
        {
        }

        public AirlineNameAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AirlineNameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
