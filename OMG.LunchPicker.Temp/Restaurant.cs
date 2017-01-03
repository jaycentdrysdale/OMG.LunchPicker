using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Temp
{
    public class Restaurant : IAuditable
    {
        #region Ctors
        public Restaurant()
        {
            Cuisines = new List<RestaurantCuisine>();
            Ratings = new List<Rating>();
        }
        #endregion

        #region Public Properties
        public int Id { get; set; }

        [StringLength(80)]
        [Required]
        public string Name { get; set; }

        [StringLength(80)]
        [Required]
        public string Street { get; set; }

        [StringLength(80)]
        public string City { get; set; }

        [StringLength(3)]
        public string State { get; set; }

        [StringLength(10)]
        [Required]
        public string Zip { get; set; }

        public bool IsActive { get; set; }
        #endregion

        #region IAuditable
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        #endregion

        #region Navigation Properties (support for lazy loading)
        public virtual ICollection<RestaurantCuisine> Cuisines { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        #endregion
    }
}
