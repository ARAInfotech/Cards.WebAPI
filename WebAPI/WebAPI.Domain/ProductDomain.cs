namespace WebAPI.Domain
{
    #region ProductDomain
    /// <summary>
    /// UserDomain
    /// </summary>
    public class ProductDomain
    {
        #region ProductId
        /// <summary>
        /// ProductId
        /// </summary>
        public long ProductId { get; set; }
        #endregion

        #region Name
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        #endregion

        #region Description
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
        #endregion

        #region Url
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; } = string.Empty;
        #endregion

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

        #region Rate
        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate { get; set; }
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
        public string CreatedByName { get; set; }
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
        public string ModifiedByName { get; set; }
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
