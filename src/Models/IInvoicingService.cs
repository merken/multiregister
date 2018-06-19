namespace multiregister.Models
{
    public interface IInvoicingService
    {
        string CreateInvoice(float numberOfHoursPerformed);
    }

    public class EuroInvoicingService : IInvoicingService
    {
        private float rate = 25;
        private string currency = "EUR";
        private float vat = 0.21f;

        public string CreateInvoice(float numberOfHoursPerformed)
        {
            var value = rate * numberOfHoursPerformed;
            value += value * vat;

            return $"{value} {currency} incl VAT ({vat.ToString("0.00")})";
        }
    }

    public class DollarInvoicingService : IInvoicingService
    {
        private float rate = 29.03f;
        private string currency = "USD";

        public string CreateInvoice(float numberOfHoursPerformed)
        {
            var value = rate * numberOfHoursPerformed;

            return $"{value} {currency} excl VAT";
        }
    }

    public class PoundInvoicingService : IInvoicingService
    {
        private float rate = 21.92f;
        private string currency = "GBP";
        private float vat = 0.20f;

        public string CreateInvoice(float numberOfHoursPerformed)
        {
            var value = rate * numberOfHoursPerformed;
            value += value * vat;

            return $"{value} {currency} incl VAT ({vat.ToString("0.00")})";
        }
    }
}