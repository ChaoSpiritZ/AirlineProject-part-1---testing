using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class EmptyTokenException : Exception
    {
        public EmptyTokenException()
        {
        }

        public EmptyTokenException(string message) : base(message)
        {
        }

        public EmptyTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
