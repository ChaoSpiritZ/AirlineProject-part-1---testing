using AirlineProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProjectTest
{
    public static class TestData
    {
        internal static FlyingCenterSystem fcs = FlyingCenterSystem.GetInstance();
        internal static TestingFacade testFacade = new TestingFacade();

        //datetimes
        internal static DateTime pastDate1 = new DateTime(2000, 1, 1);
        internal static DateTime pastDate2 = new DateTime(2000, 2, 2);
        internal static DateTime futureDate1 = new DateTime(2030, 1, 1);
        internal static DateTime futureDate2 = new DateTime(2030, 2, 2);
        internal static DateTime futureDate3 = new DateTime(2030, 3, 3);

        //countries
        internal static int argentinaID = 109;
        internal static int barbadosID = 110;
        internal static int chadID = 111;
        internal static int denmarkID = 112;
        internal static int egyptID = 113;
        internal static int franceID = 114;
        internal static int guatemalaID = 115;
        internal static int hondurasID = 116;
        internal static int israelID = 117;
        internal static int jamaicaID = 118;

        //test airlines
        internal static AirlineCompany airline1 = new AirlineCompany(0, "Alpha", "AlphaUser", "AlphaPass", argentinaID);
        internal static AirlineCompany airline2 = new AirlineCompany(0, "Beta", "BetaUser", "BetaPass", barbadosID);

        //test customers
        internal static Customer customer1 = new Customer(0, "Ahron", "Ahri", "AhronUser", "AhronPass", "AAddress", "052-aaaaaaa", "creditaaa");
        internal static Customer customer2 = new Customer(0, "Bernard", "Bravo", "BernardUser", "BernardPass", "BAddress", "052-bbbbbbb", "creditbbb");
        internal static Customer customer3 = new Customer(0, "Charlie", "Chan", "CharlieUser", "CharliePass", "CAddress", "052-ccccccc", "creditccc");
    }
}
