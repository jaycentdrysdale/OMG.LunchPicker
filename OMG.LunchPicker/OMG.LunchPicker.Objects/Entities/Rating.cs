using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace OMG.LunchPicker.Objects.Entities
{
    public partial class Rating : Entity, IAuditable
    {
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public int RatingValue { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual User User { get; set; }
    }
}
