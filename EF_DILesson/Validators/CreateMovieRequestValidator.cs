using EF_DILesson.Contracts;
using FluentValidation;

namespace EF_DILesson.Validators
{
    public class CreateMovieRequestValidator : AbstractValidator<CreateMovieRequest>
    {
        public CreateMovieRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Назва фільму обов'язкова")
                .MaximumLength(100).WithMessage("Назва не може бути довшою за 100 символів");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Опис не може бути довшим за 1000 символів");

            RuleFor(x => x.PosterUrl)
                .NotEmpty().WithMessage("Посилання на постер обов'язкове")
                .MaximumLength(500).WithMessage("Посилання на постер не може бути довшим за 500 символів")
                .Must(IsValidUrl).WithMessage("Посилання на постер має починатися з http:// або https://");

            RuleFor(x => x.MovieUrl)
                .NotEmpty().WithMessage("Посилання на фільм обов'язкове")
                .MaximumLength(500).WithMessage("Посилання на фільм не може бути довшим за 500 символів")
                .Must(IsValidUrl).WithMessage("Посилання на фільм має починатися з http:// або https://");

            RuleFor(x => x.ReleaseYear)
                .InclusiveBetween(1888, DateTime.Now.Year + 1)
                .WithMessage("Рік випуску має бути від 1888 до " + (DateTime.Now.Year + 1));

            RuleFor(x => x.Rating)
                .InclusiveBetween(0.0, 10.0)
                .WithMessage("Рейтинг має бути від 0 до 10");
        }

        private bool IsValidUrl(string url)
        {
            Uri result;
            bool ok = Uri.TryCreate(url, UriKind.Absolute, out result);
            if (!ok) return false;
            return result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps;
        }
    }
}