using System;
using System.Collections.Generic;

namespace Business.Entities
{
    public class ExchangeInfo
    {
        public bool Success { get; set; }

        public string Timestamp { get; set; }

        public string Base { get; set; }

        public DateTime Date { get; set; }

        public Dictionary<string, decimal> Rates { get; set; }
    }
}
