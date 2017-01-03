using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Temp
{

    public class RestaurantCuisine : IAuditable
    {
        #region Ctors
        public RestaurantCuisine(){}
        #endregion

        #region Public Properties
        [Key]
        [Column(Order = 1)]
        public int RestaurantId { get; set; }
        [Key]
        [Column(Order=2)]
        public int CuisineId { get; set; }
        #endregion

        #region Navigation Properties (support for lazy loading)
        public virtual Cuisine Cuisine { get; set; }
        #endregion

        #region IAuditable
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        #endregion
    }
}
