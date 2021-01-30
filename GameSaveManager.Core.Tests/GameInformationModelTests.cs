namespace GameSaveManager.Core.Tests
{
    using System;

    using AutoFixture.Xunit2;

    using Bogus;

    using FluentAssertions;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Models;
    using GameSaveManager.Core.Services;
    using GameSaveManager.Core.Utils;

    using Xunit;

    public class GameInformationModelTests
    {
        private readonly GameInformationModel GameInformation;

        public GameInformationModelTests()
        {
            BackupFactory factory = new Faker<BackupFactory>()
                .Generate();

            GameInformation = new Faker<GameInformationModel>()
                .RuleFor(g => g.DefaultSaveName, f => f.Lorem.Word())
                .RuleFor(g => g.SaveBackupExtension, f => factory.Create(f.PickRandom<EBackupSaveType>()).GetFileExtension())
                .RuleFor(g => g.DefaultSaveExtension, f => f.Lorem.Word())
                .Generate();
        }

        [Fact]
        public void ShouldReturnNewSaveNameIfDefaultIsOverridden()
        {
            const string generatedName = "DarkSouls-FatBoySlin";

            string newSaveName = GameInformation.BuildSaveName(generatedName);

            string saveName = generatedName + GameInformation.SaveBackupExtension;

            _ = saveName.Should().Be(newSaveName);
        }

        [Fact]
        public void ShouldReturnDefaultSaveNameIfNoNewNameIsProvided()
        {
            GameInformation.UserDefinedSaveName = GameInformation.DefaultSaveName;

            string newSaveName = GameInformation.BuildSaveName();

            string defaultSaveName = GameInformation.DefaultSaveName + GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Be(newSaveName);
        }

        [Fact]
        public void ShouldReturnDefaultSaveNameIfEmptyStringIsProvided()
        {
            GameInformation.UserDefinedSaveName = GameInformation.DefaultSaveName;

            string newSaveName = GameInformation.BuildSaveName(string.Empty);

            string defaultSaveName = GameInformation.DefaultSaveName + GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Be(newSaveName);
        }

        [Fact]
        public void ShouldReturnDefaultSaveNameIfNullValueIsProvided()
        {
            GameInformation.UserDefinedSaveName = GameInformation.DefaultSaveName;

            string newSaveName = GameInformation.BuildSaveName(null);

            string defaultSaveName = GameInformation.DefaultSaveName + GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Be(newSaveName);
        }

        [Fact]
        public void ShouldReturnDefaultSaveNameIfWhiteSpaceValueIsProvided()
        {
            GameInformation.UserDefinedSaveName = GameInformation.DefaultSaveName;

            string newSaveName = GameInformation.BuildSaveName("      ");

            string defaultSaveName = GameInformation.DefaultSaveName + GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Be(newSaveName);
        }

        [Fact]
        public void ShouldSetDefaultIfSaveExtensionIsNotInBackupSaveTypeEnum()
        {
            const string saveExtension = "rar";

            _ = GameInformation.Invoking(g => g.SetSaveBackupExtension(saveExtension)).Should().Throw<NotSupportedException>().WithMessage("Extensão de arquivo não suportada.");
        }

        [Fact]
        public void ShouldSetSaveExtensionIfValidExtensionIsPrivided()
        {
            string saveExtension = new Faker().Make(1, () => new Faker().PickRandom<EBackupSaveType>().Description())[0];

            GameInformation.SetSaveBackupExtension(saveExtension);

            string defaultSaveName = GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Contain(saveExtension);
        }

        [Fact]
        public void ShouldReturnDefaultSaveName()
        {
            string expected = $"{GameInformation.DefaultSaveName}.{GameInformation.DefaultSaveExtension}";

            string saveName = GameInformation.RestoreSaveName();

            _ = saveName.Should().Be(expected);
        }

        [Theory, AutoData]
        public void PropertieUserDefinedSaveNameShouldReturnSettedText(string expected, GameInformationModel game)
        {
            game.DefaultGameSaveFolder = expected;

            _ = game.DefaultGameSaveFolder.Should().Be(expected);
        }

        [Theory, AutoData]
        public void PropertieNameShouldReturnSettedText(string expected, GameInformationModel game)
        {
            game.Name = expected;

            _ = game.Name.Should().Be(expected);
        }

        [Theory, AutoData]
        public void PropertieTitleShouldReturnSettedText(string expected, GameInformationModel game)
        {
            game.Title = expected;

            _ = game.Title.Should().Be(expected);
        }

        [Theory, AutoData]
        public void PropertieDeveloperShouldReturnSettedText(string expected, GameInformationModel game)
        {
            game.Developer = expected;

            _ = game.Developer.Should().Be(expected);
        }

        [Theory, AutoData]
        public void PropertiePublisherShouldReturnSettedText(string expected, GameInformationModel game)
        {
            game.Publisher = expected;

            _ = game.Publisher.Should().Be(expected);
        }

        [Theory, AutoData]
        public void PropertieOnlineSaveFolderShouldReturnSettedText(string expected, GameInformationModel game)
        {
            game.OnlineSaveFolder = expected;

            _ = game.OnlineSaveFolder.Should().Be(expected);
        }

        [Theory, AutoData]
        public void PropertieDefaultGameSaveFolderShouldReturnSettedText(string expected, GameInformationModel game)
        {
            game.DefaultGameSaveFolder = expected;

            _ = game.DefaultGameSaveFolder.Should().Be(expected);
        }

        [Theory, AutoData]
        public void PropertieCoverPathShouldReturnSettedText(string expected, GameInformationModel game)
        {
            game.CoverPath = expected;

            _ = game.CoverPath.Should().Be(expected);
        }
    }
}
