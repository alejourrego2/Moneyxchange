using Business.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers
{
    public class BaseManager
    {
        public readonly ExchangeContext context;
        public BaseManager(ExchangeContext context) {
            this.context = context;
        }
    }
}
