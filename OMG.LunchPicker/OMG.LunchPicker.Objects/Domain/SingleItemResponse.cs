using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain
{
    public class SingleItemResponse<T> : ResponseBase
    {
        public T Result { get; set; }
    }
}
