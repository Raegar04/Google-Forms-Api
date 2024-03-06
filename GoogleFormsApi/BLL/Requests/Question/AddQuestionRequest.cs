using FluentValidation;

namespace GoogleFormsApi.Requests.Question
{
    public class AddQuestionRequest
    {
        public string Description { get; set; }
    }

    public class AddQuestionValidator : AbstractValidator<AddQuestionRequest>
    {
        public AddQuestionValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
