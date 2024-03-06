using FluentValidation;

namespace GoogleFormsApi.Requests.Form
{
    public class UpdateFormRequest
    {
        public string? Title { get; set; }

        public bool Closed { get; set; }

        public string? Description { get; set; }
    }

    public class UpdateFormValidator : AbstractValidator<UpdateFormRequest>
    {
        public UpdateFormValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
