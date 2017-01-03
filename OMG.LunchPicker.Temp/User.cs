using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Temp
{
    public class User : IAuditable
    {
        #region Ctors
        public User()
        {
            Ratings = new List<Rating>();
        }
        #endregion

        #region Public Properties
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        #endregion

        #region IAuditable
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        #endregion

        #region Navigation Properties (support for lazy loading)
        public virtual ICollection<Rating> Ratings { get; set; }
        #endregion
    }
}
