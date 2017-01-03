using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain
{
    public class MultiItemsResponse<T> : ResponseBase
    {
        public int Total { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
