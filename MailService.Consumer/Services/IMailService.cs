namespace MailService.Consumer.Services
{
    using System.Threading.Tasks;

    using global::MailService.Consumer.Models;

    public interface IMailService
    {
        Task<MailResponseModel> Send(MailModel mail);
    }
}