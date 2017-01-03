using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain.Criteria
{
    public class PagableCriteriaBase
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string SortField { get; set; }
        public bool Reverse { get; set; }
        public string GetSortExpression()
        {
            if (string.IsNullOrWhiteSpace(SortField))
                return "Id desc";
            return SortField + (Reverse ? " desc" : "");
        }
    }
}
