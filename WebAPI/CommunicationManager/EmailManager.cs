#region Namespace
using System.Net;
using System.Net.Mail;
using CommunicationManager.Interface;
using ConfigManager.Interfaces;
using WebAPI.Domain;
#endregion

namespace CommunicationManager
{
    #region EmailManager
    /// <summary>
    /// EmailManager
    /// </summary>
    public class EmailManager : IEmailManager
    {
        #region Variables

        #region _configurationManager
        /// <summary>
        /// _configurationManager
        /// </summary>
        public IConfigurationManager _config;
        #endregion

        #endregion

        #region Constructor
        public EmailManager(IConfigurationManager configuration)
        {
            _config = configuration;
        }
        #endregion

        #region Public Methods

        #region SendMail
        /// <summary>
        /// SendMail
        /// </summary>
        /// <param name="emailDomain"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool SendMail(EmailDomain emailDomain)
        {
            return Send(emailDomain);
        }

        #endregion

        #endregion

        #region Private Methods

        #region Send
        /// <summary>
        /// Send
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        private bool Send(EmailDomain dom)
        {
            bool status = false;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(dom.FromAddress);  
            mailMessage.Subject = dom.Subject;
            mailMessage.Body = dom.Body;
            mailMessage.IsBodyHtml = true;

            string[] ToMuliId = dom.ToAddress.Split(',');
            foreach (string ToEMailId in ToMuliId)
            {
                if (!string.IsNullOrEmpty(ToEMailId))
                {
                    mailMessage.To.Add(new MailAddress(ToEMailId));
                }
            }

            if (!string.IsNullOrEmpty(dom.CC))
            {
                string[] CCId = dom.CC.Split(',');
                foreach (string CCEmail in CCId)
                {
                    if (!string.IsNullOrEmpty(CCEmail))
                    {
                        mailMessage.CC.Add(new MailAddress(CCEmail));
                    }
                }
            }

            if (!string.IsNullOrEmpty(dom.BCC))
            {
                string[] bccid = dom.BCC.Split(',');
                foreach (string bccEmailId in bccid)
                {
                    if (!string.IsNullOrEmpty(bccEmailId))
                    {
                        mailMessage.Bcc.Add(new MailAddress(bccEmailId));
                    }
                }
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Host = _config.GetEmailConfig("Host"); 
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = _config.GetEmailConfig("Username");
            NetworkCred.Password = _config.GetEmailConfig("Password");
            smtp.Credentials = NetworkCred;
            smtp.Port = Convert.ToInt32(_config.GetEmailConfig("Port"));

            try
            {
                smtp.Send(mailMessage);

                status = true;
            }
            catch (Exception)
            {
                status = false;
                throw;
            }

            return status;
        }
        #endregion

        #endregion
    }
    #endregion
}
