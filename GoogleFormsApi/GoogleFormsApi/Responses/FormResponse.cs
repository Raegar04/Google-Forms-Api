using Domain.Models;

namespace GoogleFormsApi.Responses
{
    public class FormResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Guid HolderId { get; set; }
    }
}
