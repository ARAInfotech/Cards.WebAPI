#region Namespace
using WebAPI.Domain;
#endregion

namespace CommunicationManager.Interface
{
    #region IEmailManager
    /// <summary>
    /// IEmailManager
    /// </summary>
    public interface IEmailManager
    {
        #region SendMail
        /// <summary>
        /// SendMail
        /// </summary>
        /// <param name="emailDomain"></param>
        /// <returns></returns>
        bool SendMail(EmailDomain emailDomain);
        #endregion
    }
    #endregion
}
