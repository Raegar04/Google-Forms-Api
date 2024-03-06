using FluentValidation;

namespace GoogleFormsApi.Requests.Form
{
    public class GetFormFilterRequest
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public Guid? HolderId { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
