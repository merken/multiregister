using System;

namespace multiregister.Models
{
    public abstract class Currency
    {
        public abstract float Rate { get; }
        public abstract string Code { get; }
        public abstract float Vat { get; }
    }

    public class Euro : Currency
    {
        public override float Rate { get { return 25; } }
        public override string Code { get { return "EUR"; } }
        public override float Vat { get { return 0.21f; } }
    }

    public class Dollar : Currency
    {
        public override float Rate { get { return 29.03f; } }
        public override string Code { get { return "USD"; } }
        public override float Vat { get { return 0.0f; } }
    }

    public class Pound : Currency
    {
        public override float Rate { get { return 21.92f; } }
        public override string Code { get { return "GBP"; } }
        public override float Vat { get { return 0.20f; } }
    }

    public interface ICurrencyInvoicingService<T> where T : Currency
    {
        string CreateInvoice(float numberOfHoursPerformed);
    }

    public class CurrencyInvoicingService<T> : ICurrencyInvoicingService<T> where T : Currency
    {
        private T GetCurrency()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public string CreateInvoice(float numberOfHoursPerformed)
        {
            var currency = GetCurrency();
            var value = currency.Rate * numberOfHoursPerformed;
            value += value * currency.Vat;

            return $"{value} {currency.Code} incl VAT ({currency.Vat.ToString("0.00")})";
        }
    }

    public class LowTaxEuroService : ICurrencyInvoicingService<Euro>
    {
        public string CreateInvoice(float numberOfHoursPerformed)
        {
            var taxRate = 0.06f;
            var currency = new Euro();
            var value = currency.Rate * numberOfHoursPerformed;
            value += value * taxRate; //Low tax rate

            return $"{value} {currency.Code} incl LOW VAT ({taxRate.ToString("0.00")})";
        }
    }

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