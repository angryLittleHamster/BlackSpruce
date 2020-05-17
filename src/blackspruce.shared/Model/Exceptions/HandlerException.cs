#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSpruce.Shared.Model.Exceptions
{
    public class HandlerException : Exception
    {
        public HandlerException(string? message)
            :base(message)
        {

        }
        
    }
}
