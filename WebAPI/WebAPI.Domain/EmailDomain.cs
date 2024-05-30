#region NameSpace
using System;
#endregion

namespace WebAPI.Domain
{
    #region EmailDomain
    /// <summary>
    /// EmailDomain
    /// </summary>
    public class EmailDomain
    {
        #region FromAddress
        /// <summary>
        /// FromAddress
        /// </summary>
        public string FromAddress { get; set; }
        #endregion

        #region ToAddress
        /// <summary>
        /// ToAddress
        /// </summary>
        public string ToAddress { get; set; }
        #endregion

        #region CC
        /// <summary>
        /// CC
        /// </summary>
        public string CC { get; set; }
        #endregion

        #region BCC
        /// <summary>
        /// BCC
        /// </summary>
        public string BCC { get; set; }
        #endregion

        #region Subject
        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }
        #endregion

        #region Body
        /// <summary>
        /// Body
        /// </summary>
        public string Body { get; set; }
        #endregion
    }
    #endregion
}
