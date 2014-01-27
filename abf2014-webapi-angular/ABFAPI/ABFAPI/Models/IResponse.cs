using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABFAPI.Models
{
    public interface IResponse
    {
        Guid AssociatedToken { get; set; }
        string Response { get; set; }
        int Status { get; set; }
        int Phase { get; set; }
    }
}