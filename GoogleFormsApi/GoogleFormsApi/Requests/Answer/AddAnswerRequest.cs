namespace GoogleFormsApi.Requests.Answer
{
    public class AddAnswerRequest
    {
        public string? Value { get; set; }

        public Guid QuestionId { get; set; }
    }
}
