using EF_DILesson.Contracts;
using FluentValidation;

namespace EF_DILesson.Validators
{
    public class UpdateCommentRequestValidator : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentRequestValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Текст коментаря обов'язковий")
                .MaximumLength(500).WithMessage("Коментар не може бути довшим за 500 символів");
        }
    }
}