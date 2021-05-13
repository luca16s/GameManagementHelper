namespace GameSaveManager.Core.Validations
{
    using FluentValidation;

    using GameSaveManager.Core.Models;
    using GameSaveManager.Core.Utils;

    public class GameInformationModelValidatons : AbstractValidator<GameInformationModel>
    {
        public GameInformationModelValidatons()
        {
            RuleSet("Saves", () =>
            {
                _ = RuleFor(gameModel => gameModel.UserDefinedSaveName)
                .MinimumLength(5)
                .MaximumLength(150)
                .NotNull()
                .NotEmpty()
                .WithMessage(SystemMessages.UserDefinedSaveNameErrorMessage);
            });
        }
    }
}