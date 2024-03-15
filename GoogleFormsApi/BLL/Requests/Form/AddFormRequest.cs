using FluentValidation;

namespace GoogleFormsApi.Requests.Form
{
    public class AddFormRequest
    {
        public string Title { get; set; }

        public bool Closed { get; set; }

        public string? Description { get; set; }
    }

    public class AddFormValidator : AbstractValidator<AddFormRequest>
    {
        public AddFormValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
