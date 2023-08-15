using Bogus;
using Microsoft.VisualStudio.TestPlatform.Common.ExtensionFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomatation.SiteChacara.Entities;

namespace TestAutomatation.SiteChacara.Support
{
    static public class MockupDataGenerator
    {

        public static User MockupUser()
        {
            var faker = new Faker("pt_BR");

            User user = new()
            {
                Name = faker.Person.FullName,
                Phone = RemoveCountryCode(faker.Person.Phone)
            };

            return user;
        }

        public static Period MockupPeriod(int interval)
        {
            var faker = new Faker("pt_BR");

            Period period = new();

            period.StartDate = faker.Date.Between(DateTime.Now, DateTime.Now.AddDays(30));

            period.FinalDate = faker.Date.Between(period.StartDate, period.StartDate.AddDays(interval));

            return period;
        }
        public static Period MockupPeriod(int interval, int month)
        {
            var faker = new Faker("pt_BR");

            Period period = new();
            DateTime limitInitialDate = EndOfMonth(month).AddDays(interval * -1);

            period.StartDate = faker.Date.Between(StartOfMonth(month), limitInitialDate);

            period.FinalDate = faker.Date.Between(period.StartDate, period.StartDate.AddDays(interval));

            if(month < DateTime.Now.Month)
            {
                period.StartDate = period.StartDate.AddYears(1);
                period.FinalDate = period.FinalDate.AddYears(1);
            }

            return period;
        }

        public static Book MockupBook()
        {
            var faker = new Faker("pt_BR");

            Book book = new Book
            {
                User = MockupUser(),
                Period = MockupPeriod(5)
            };

            return book;
        }

        private static string RemoveCountryCode(string phone)
        {
            return phone.Replace("+55 ", "");
        }

        private static DateTime StartOfMonth(int month)
        {
            return new DateTime(DateTime.Now.Year, month, 1, 0, 0, 0);
        }
        private static DateTime EndOfMonth(int month)
        {
            return StartOfMonth(month).AddMonths(1).AddSeconds(-1);
        }


    }
}
