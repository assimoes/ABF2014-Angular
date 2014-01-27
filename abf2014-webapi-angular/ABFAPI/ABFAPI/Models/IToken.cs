using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABFAPI.Models
{
    public interface IToken
    {
        Guid Token { get; set; }
        DateTime AcquiredAt { get; set; }
    }
}
