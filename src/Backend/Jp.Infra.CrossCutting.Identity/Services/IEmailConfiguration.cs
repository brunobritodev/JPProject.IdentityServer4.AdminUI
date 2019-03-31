
using MailKit.Security;

namespace Jp.Infra.CrossCutting.Identity.Services
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; }
        int SmtpPort { get; }
        string SmtpUsername { get; set; }
        string SmtpPassword { get; set; }
        string FromName { get; set; }
        string FromAddress { get; set; }
        bool SendEmail { get; set; }
        bool UseSsl { get; set; }
    }
}
