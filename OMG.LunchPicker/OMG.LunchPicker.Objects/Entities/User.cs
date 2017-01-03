using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace OMG.LunchPicker.Objects.Entities
{
    public partial class User : Entity, IAuditable
    {
        public User()
        {
            this.Ratings = new List<Rating>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
