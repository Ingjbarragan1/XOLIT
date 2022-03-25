using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XOLIT.API.Common
{
    public class Result
    {        public int StatusResult { get; set; }
        public string StatusMessage { get; set; }
        public object Data { get; set; }
    }
}
