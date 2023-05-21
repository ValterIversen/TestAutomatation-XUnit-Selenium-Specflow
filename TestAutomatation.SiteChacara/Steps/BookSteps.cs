using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestAutomatation.SiteChacara.Config;
using TestAutomatation.SiteChacara.Screens;
using TestAutomatation.SiteChacara.Support;

namespace TestAutomatation.SiteChacara.Steps
{
    [Binding]
    public class BookSteps
    {
        private readonly SCFixture _scFixture;
        private readonly Home _homePage;

        public BookSteps(SCFixture scFixture)
        {
            _scFixture = scFixture;
            _homePage = new Home(scFixture.BrowserHelper);
        }

        #region Given

        [Given(@"that we are on the home page")]
        public void GivenThatWeAreOnTheHomePage()
        {
            try
            {
                _homePage.GoToHomePage();
            }
            catch (Exception ex)
            {
                _scFixture.AddLog(ex.Message, $"{GetType().Name}.{_scFixture.GetActualMethodName()}", false);

                throw;
            }

        }

        [Given(@"the user is interested")]
        public void GivenTheUserIsInterested()
        {
            try
            {
                _homePage.ClickIAmInterestedLink();
            }
            catch (Exception ex)
            {
                _scFixture.AddLog(ex.Message, $"{GetType().Name}.{_scFixture.GetActualMethodName()}", false);

                throw;
            }
        }

        [Given(@"that a random user is informed")]
        public void GivenThatARandomUserIsInformed()
        {
            try
            {
                _scFixture.User = MockupDataGenerator.MockupUser();
                _homePage.FillUser(_scFixture.User);
                _homePage.SubmitUser();

            }
            catch (Exception ex)
            {
                _scFixture.AddLog(ex.Message, $"{GetType().Name}.{_scFixture.GetActualMethodName()}", false);

                throw;
            }
        }
        [Given(@"that a random period of month (.*) with an interval of (.*) days is informed")]
        public void GivenThatARandomPeriodOfMonthWithAnIntervalOfDaysIsInformed(int month, int interval)
        {
            try
            {
                _scFixture.Period = MockupDataGenerator.MockupPeriod(interval, month);

                _homePage.PickMonth(month);

                _homePage.PickDates(_scFixture.Period.StartDate.Day, _scFixture.Period.FinalDate.Day);

            }
            catch (Exception ex)
            {
                _scFixture.AddLog(ex.Message, $"{GetType().Name}.{_scFixture.GetActualMethodName()}", false);

                throw;
            }
        }


        [Given(@"that random not-booked dates are informed with an interval of (.*) days")]
        public void GivenThatRandomNot_BookedDatesAreInformedWithAnIntervalOfDays(int p0)
        {
            try
            {
                _homePage.PickMonth(p0);
            }
            catch (Exception ex)
            {
                _scFixture.AddLog(ex.Message, $"{GetType().Name}.{_scFixture.GetActualMethodName()}", false);

                throw;
            }
        }

        #endregion

        #region When

        [When(@"proceeding with the booking")]
        public void WhenProceedingWithTheBooking()
        {
            try
            {
                _homePage.ProceedWithBooking();
            }
            catch (Exception ex)
            {
                _scFixture.AddLog(ex.Message, $"{GetType().Name}.{_scFixture.GetActualMethodName()}", false);

                throw;
            }
        }

        #endregion


    }
}
