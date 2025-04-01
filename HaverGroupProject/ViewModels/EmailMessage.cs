using System.Net.Mail;

namespace HaverGroupProject.ViewModels
{
    public class EmailMessage
    {
        public List<EmailAddress> ToAddresses { get; set; } = [];
        public string Subject { get; set; } = "";
        public string Content { get; set; } = "";
    }
}
