using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain.Criteria
{
    public class GetByIdCriteria
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }

    }
}
