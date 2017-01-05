using OMG.LunchPicker.Objects.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain.Criteria
{
    public class GetRestaurantsCriteria : PagableCriteriaBase
    {
        public string PartialName { get; set; }
        public double? RatingValue { get; set; }
        public string Cuisine { get; set; }
    }
}
