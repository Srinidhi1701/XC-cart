using System.Text.RegularExpressions;
using XCKartApi.Models;

namespace XCKartApi.Repositories
{
    public class VerificationService 
    {
        public int CustomerVerification(CustomerModel addCustomerModel)
        {
            int verify = 0;
            string Pattern = @"^[a-zA-Z]+$";
            Regex regex = new Regex(Pattern);
            string patternPhone = @"^[0-9]+$";
            Regex regex2 = new Regex(patternPhone);
            string patternEmail = @"^[a-z0-9][-a-z0-9._]+@([-a-z]+.)+[a-z]{2,5}$";
            Regex regex3 = new Regex(patternEmail);

            if (regex.IsMatch(addCustomerModel.customer_name) && regex2.IsMatch(addCustomerModel.phone_no) && regex3.IsMatch(addCustomerModel.email))
            {
                verify = 3;
            }

            return verify;
        }
        public int CashAuthorize(string cname, string sname)
        {
            if (cname.Equals(sname))
            {
                return 1;
            }
            return 0;
        }
        public int UpiAuthorize(string phoneNo)
        {
            try
            {

                string paymentPattern = @"^[0-9]+$";
                Regex reg = new Regex(paymentPattern);
                if (reg.IsMatch(phoneNo))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return 0;
        }
        public int CreditAuthorize(string cardNumber, string cardService, int expiryMonth, int expiryYear, int cvv)
        {

            try
            {
                string card_pattern = @"^[0-9]{12}$";
                Regex regexcard = new Regex(card_pattern);
                if (regexcard.IsMatch(cardNumber) && (cardService == "Rupay" || cardService == "Visa" || cardService == "AmericanExpress") && expiryMonth > 0 && expiryMonth <= 12 && expiryYear < 2030 && expiryYear > 2022 && cvv > 100 && cvv < 1000)
                {
                    return 1;
                }
                return 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }
    }
}

