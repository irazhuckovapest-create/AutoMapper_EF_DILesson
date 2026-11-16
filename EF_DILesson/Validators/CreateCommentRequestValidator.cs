using EF_DILesson.Contracts;
using FluentValidation;

namespace EF_DILesson.Validators
{
    public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentRequestValidator()
        {
            RuleFor(x => x.AuthorName)
                .NotEmpty().WithMessage("Ім'я автора обов'язкове")
                .MinimumLength(2).WithMessage("Ім'я автора має містити щонайменше 2 символи")
                .MaximumLength(50).WithMessage("Ім'я автора не може бути довшим за 50 символів");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Текст коментаря обов'язковий")
                .MaximumLength(500).WithMessage("Коментар не може бути довшим за 500 символів");
        }
    }
}