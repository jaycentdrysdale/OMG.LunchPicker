using OMG.LunchPicker.Temp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Temp
{
    public class Rating : IAuditable
    {
        #region Ctors
        public Rating()
        {

        }
        #endregion

        #region Public Properties
        [Key]
        [Column(Order = 1)]
        public int? RestaurantId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int? UserId { get; set; }

        [Required]
        public RatingValue RatingValue { get; set; }
        public bool IsActive { get; set; }
        #endregion

        #region IAuditable
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        #endregion

        #region Navigation Properties (support for lazy loading)
        public virtual User User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        #endregion
    }
}
