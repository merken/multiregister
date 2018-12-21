using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using multiregister.Models;

namespace multiregister.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IEnumerable<IInvoicingService> invoicingServices;
        private readonly IEnumerable<ICurrencyInvoicingService<Euro>> euroServices;

        public InvoicesController(IEnumerable<IInvoicingService> invoicingServices, IEnumerable<ICurrencyInvoicingService<Euro>> euroServices)
        {
            this.invoicingServices = invoicingServices;
            this.euroServices = euroServices;
        }

        [HttpGet]
        public ActionResult<string> Get(float hoursPerformed)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Invoice {DateTime.Now.ToShortDateString()}");
            builder.AppendLine($"");
            builder.AppendLine($"Hours performed : {hoursPerformed}");
            builder.AppendLine($"");

            foreach (var service in invoicingServices)
            {
                builder.AppendLine($"{service.CreateInvoice(hoursPerformed)}");
            }

            foreach (var service in euroServices)
            {
                builder.AppendLine($"{service.CreateInvoice(hoursPerformed)}");
            }

            builder.AppendLine($"---------------------");

            return builder.ToString();
        }
    }
}
