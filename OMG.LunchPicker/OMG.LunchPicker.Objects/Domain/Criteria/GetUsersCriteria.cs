using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain.Criteria
{
    public class GetUsersCriteria : PagableCriteriaBase
    {
        public string PartialUserNameOrEmail { get; set; }
    }
}
