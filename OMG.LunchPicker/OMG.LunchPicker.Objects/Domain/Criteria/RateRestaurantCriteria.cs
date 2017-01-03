using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain.Criteria
{
    public class RateRestaurantCriteria
    {
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public int RatingValue { get; set; }
    }
}
