using Microsoft.Extensions.Primitives;
using OpenQA.Selenium;
using TestAutomatation.SiteChacara.Entities;
using TestAutomatation.SiteChacara.Support;

namespace TestAutomatation.SiteChacara.Screens
{
    public class Home : PageObjectModel
    {
        public Home(SeleniumHelper helper) : base(helper) { }

        public void GoToHomePage()
        {
            Helper.GoToUrl(Helper.Configuration.SCBaseUrl);
        }


        #region Elements

        #region FirstCard

        public const string iAmInterestedLinkText = "Tem interesse? Entre em contato e agende!";

        public void ClickIAmInterestedLink()
        {
            Helper.LinkClickByText(iAmInterestedLinkText);
        }


        #endregion

        #region SecondCard

        public const string idNameTxt = "name";
        public const string idPhoneTxt = "phone";
        public const string submitUserLinkText = "Prazer em te conhecer! Qual data?";

        public void FillUser(User user)
        {
            Helper.FillTextBoxById(idNameTxt, user.Name);
            Helper.FillTextBoxById(idPhoneTxt, user.Phone);
        }

        public void SubmitUser()
        {
            Helper.LinkClickByText(submitUserLinkText);
        }

        #endregion

        #region ThirdCard

        public const string classNameCurrentMonthDiv = "react-datepicker__current-month";
        public const string classNameNextMonthButton = "react-datepicker__navigation--next";
        public const string classNameDateTimePickerModal = "react-datepicker";
        public const string classNameDateInput = "register_datepicker__giqWh";
        public const string baseClassNameDate = "react-datepicker__day--";
        public const string textProceedBooking = "Reservar datas!";

        public void PickMonth(int month)
        {
            OpenDatePicker();

            string chosedMonth = IdentifyMonth(month);
            string currentMonth = Helper.GetTextElementByClass(classNameCurrentMonthDiv);

            while(!currentMonth.ToUpper().Contains(chosedMonth.ToUpper()))
            {
                ClickNextMonth();
                currentMonth = Helper.GetTextElementByClass(classNameCurrentMonthDiv);
            }

        }

        public void OpenDatePicker()
        {
            if (!Helper.ExistValidationById(classNameDateTimePickerModal))
                Helper.ClickByClassName(classNameDateInput);
        }

        public void ClickNextMonth()
        {
            Helper.ClickByClassName(classNameNextMonthButton);
        }

        public void PickDates(int startDate, int endDate)
        {
            OpenDatePicker();

            Helper.ClickByClassName(baseClassNameDate + startDate.ToString("D3"));
            Helper.ClickByClassName(baseClassNameDate + endDate.ToString("D3"));
        }

        public void ProceedWithBooking()
        {
            try
            {
                Helper.LinkClickByText(textProceedBooking);
            }
            catch (WebDriverTimeoutException) { }
            finally
            {
            }
        }

        #endregion 

        #endregion

        #region Helpers
        private string IdentifyMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "Janeiro";
                case 2:
                    return "Fevereiro";
                case 3:
                    return "Março";
                case 4:
                    return "Abril";
                case 5:
                    return "Maio";
                case 6:
                    return "Junho";
                case 7:
                    return "Julho";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setembro";
                case 10:
                    return "Outubro";
                case 11:
                    return "Novembro";
                case 12:
                    return "Dezembro";
                default:
                    throw new Exception(message: "Invalid month was informed");
            }
        }
        #endregion
    }
}
