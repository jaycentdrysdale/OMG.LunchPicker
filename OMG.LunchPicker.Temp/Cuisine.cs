using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Temp
{
    public class Cuisine : IAuditable
    {
        #region Public Properties
        public int Id { get; set; }

        [StringLength(80)]
        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; }
        #endregion

        #region IAuditable
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        #endregion
    }
}
