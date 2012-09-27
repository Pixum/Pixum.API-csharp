using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixum.API
{
    public class PSIException : Exception
    {
        public int Code { get; set; }

        public PSIException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
