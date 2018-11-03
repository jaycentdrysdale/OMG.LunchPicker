using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain.Criteria
{
    public class GetCuisinesCriteria : PagableCriteriaBase
    {
        #region Ctors
        public string PartialName { get; set; }
        public bool IsActive { get; set; }
        #endregion
    }
}
