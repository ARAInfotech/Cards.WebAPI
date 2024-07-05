using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    #region CategoryDomain
    /// <summary>
    /// CategoryDomain
    /// </summary>
    public class CategoryDomain
    {
        #region CategoryId
        /// <summary>
        /// CategoryId
        /// </summary>
        public long CategoryId { get; set; }
        #endregion

        #region CategoryName
        /// <summary>
        /// CategoryName
        /// </summary>
        public string CategoryName { get; set; }
        #endregion

        #region Description
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
        #endregion

        #region CreatedBy
        /// <summary>
        /// CreatedBy
        /// </summary>
        public long CreatedBy { get; set; }
        #endregion

        #region CreatedByName
        /// <summary>
        /// CreatedByName
        /// </summary>
        public string? CreatedByName { get; set; }
        #endregion

        #region CreatedDate
        /// <summary>
        /// CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }
        #endregion

        #region ModifiedBy
        /// <summary>
        /// ModifiedBy
        /// </summary>
        public long ModifiedBy { get; set; }
        #endregion

        #region ModifiedByName
        /// <summary>
        /// ModifiedByName
        /// </summary>
        public string? ModifiedByName { get; set; }
        #endregion

        #region ModifiedDate
        /// <summary>
        /// ModifiedDate
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        #endregion

    }
    #endregion
}
