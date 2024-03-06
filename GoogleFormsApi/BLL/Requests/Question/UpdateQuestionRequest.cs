using FluentValidation;

namespace GoogleFormsApi.Requests.Question
{
    public class UpdateQuestionRequest
    {
        public string Description { get; set; }
    }

    public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionRequest>
    {
        public UpdateQuestionValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
