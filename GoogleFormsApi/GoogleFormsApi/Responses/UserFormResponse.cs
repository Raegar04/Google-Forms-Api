using Domain.Models;

namespace GoogleFormsApi.Responses
{
    public class UserFormResponse
    {
        public Guid Id { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual Guid FormId { get; set; }
    }
}
