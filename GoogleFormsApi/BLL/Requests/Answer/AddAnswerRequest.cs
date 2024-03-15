using FluentValidation;
using GoogleFormsApi.Requests.Form;

namespace GoogleFormsApi.Requests.Answer
{
    public class AddAnswerRequest
    {
        public string? Value { get; set; }

        public Guid QuestionId { get; set; }
    }

    public class AddAnswerValidator : AbstractValidator<AddAnswerRequest>
    {
        public AddAnswerValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.QuestionId).NotNull();
        }
    }
}
