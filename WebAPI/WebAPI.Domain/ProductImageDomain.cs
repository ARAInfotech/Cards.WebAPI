#region NameSpace
using System;
#endregion

namespace WebAPI.Domain
{
    #region ProductImageDomain
    /// <summary>
    /// ProductImageDomain
    /// </summary>
    public class ProductImageDomain
    {
        #region URL
        /// <summary>
        /// URL
        /// </summary>
        public string URL { get; set; }
        #endregion

        #region Title
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region IsImage
        /// <summary>
        /// IsImage
        /// </summary>
        public bool IsImage { get; set; }
        #endregion
    }
    #endregion
}
