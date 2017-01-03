using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace OMG.LunchPicker.Objects.Entities
{
    public partial class Cuisine : Entity, IAuditable
    {
        public Cuisine()
        {
            this.Restaurants = new List<Restaurant>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
