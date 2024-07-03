namespace WebAPI.Model
{

    #region CategoryModel
    /// <summary>
    /// CategoryModel
    /// </summary>
    public class CategoryModel
    {
        #region CategoryId
        /// <summary>
        /// CategoryId
        /// </summary>
        public string CategoryId { get; set; }
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
        public string? CreatedBy { get; set; }
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
        public string? ModifiedBy { get; set; }
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
