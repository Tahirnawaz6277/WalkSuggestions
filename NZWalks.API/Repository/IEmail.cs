using NZWalks.API.Models.DTO.Email;

namespace NZWalks.API.Repository
{
    public interface IEmail
    {
        public void SendEmail(EmailDTO email);
    }
}
