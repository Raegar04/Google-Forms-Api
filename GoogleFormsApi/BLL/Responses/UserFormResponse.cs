using Domain.Models;

namespace GoogleFormsApi.Responses
{
    public class UserFormResponse
    {
        public Guid Id { get; set; }

        public ICollection<AnswerResponse> Responses { get; set; }

        public Guid UserId { get; set; }

        public FormResponse Form { get; set; }
    }
}
