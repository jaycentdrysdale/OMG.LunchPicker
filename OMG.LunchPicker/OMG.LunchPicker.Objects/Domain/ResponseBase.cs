using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain
{
    public class ResponseBase
    {
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; }
    }
}
