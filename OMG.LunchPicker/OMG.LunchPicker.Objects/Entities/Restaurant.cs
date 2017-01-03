using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace OMG.LunchPicker.Objects.Entities
{
    public partial class Restaurant : Entity, IAuditable
    {
        public Restaurant()
        {
            this.Ratings = new List<Rating>();
            this.Cuisines = new List<Cuisine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Cuisine> Cuisines { get; set; }
    }
}
