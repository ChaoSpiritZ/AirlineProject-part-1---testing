using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public static class LoginHelper
    {
        public static void CheckToken<T>(LoginToken<T> token) where T : IUser
        {
            if(token == null)
                throw new EmptyTokenException();
            if(token.User == null)
                throw new EmptyTokenException();
        }
    }
}
