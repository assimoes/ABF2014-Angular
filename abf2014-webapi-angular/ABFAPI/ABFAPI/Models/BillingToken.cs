using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABFAPI.Models
{
    public class BillingToken : IToken
    {
        public Guid Token { get; set; }
        public DateTime AcquiredAt { get; set; }
    }
}