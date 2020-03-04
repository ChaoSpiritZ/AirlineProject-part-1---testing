using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public interface ILoggedInAdministratorFacade
    {
        void CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline);
        void CreateNewCustomer(LoginToken<Administrator> token, Customer customer);
        void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline);
        void RemoveCustomer(LoginToken<Administrator> token, Customer customer);
        void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline);
        void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer);
    }
}
