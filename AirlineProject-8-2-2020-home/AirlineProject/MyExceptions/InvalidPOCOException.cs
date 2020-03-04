using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class InvalidPOCOException : Exception
    {
        public InvalidPOCOException()
        {
        }

        public InvalidPOCOException(string message) : base(message)
        {
        }

        public InvalidPOCOException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPOCOException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
