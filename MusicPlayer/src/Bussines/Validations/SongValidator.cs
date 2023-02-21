using Data.DTO_s;
using FluentValidation;

namespace MusicPlayer.Bussines.Validations;

public class SongValidator : AbstractValidator<SongCreateDto>
{
	public SongValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Song name required");
	}
}
