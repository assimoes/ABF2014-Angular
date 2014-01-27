using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABFAPI.Models
{
    public class BillingResponse :IResponse
    {

        public Guid AssociatedToken
        {
            get;
            set;
        }

        public string Response
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }

        public int Phase
        { get; set; }
    }
}