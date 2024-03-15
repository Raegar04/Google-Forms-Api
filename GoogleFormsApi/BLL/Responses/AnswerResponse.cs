using Domain.Models;

namespace GoogleFormsApi.Responses
{
    public class AnswerResponse
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public QuestionResponse Question { get; set; }
    }
}
