using Domain.Models;

namespace GoogleFormsApi.Responses
{
    public class FormResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool Closed { get; set; }

        public string Description { get; set; }

        public Guid HolderId { get; set; }

        public DateTime LastUpdated { get; set; }

        public Guid UpdatedByUserWithId { get; set; }
    }
}
