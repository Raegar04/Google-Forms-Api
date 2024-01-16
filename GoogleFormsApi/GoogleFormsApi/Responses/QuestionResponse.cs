using Domain.Models;

namespace GoogleFormsApi.Responses
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }

        public int Index { get; set; }

        public string Description { get; set; }

        public virtual Guid FormId { get; set; }
    }
}
