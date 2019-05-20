using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class ExchangeRequest
    {
        public decimal value { get; set; }
        public string fromCurrency { get; set; }

        public string targetCurrency { get; set; }
    }
}
