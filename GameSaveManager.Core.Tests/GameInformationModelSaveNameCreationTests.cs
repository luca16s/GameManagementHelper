namespace GameSaveManager.Core.Tests
{
    using Bogus;

    using FluentAssertions;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Models;
    using GameSaveManager.Core.Services;

    using NUnit.Framework;

    public class GameInformationModelSaveNameCreationTests
    {
        private GameInformationModel GameInformation;

        [SetUp]
        public void Setup()
        {
            BackupFactory fac = new Faker<BackupFactory>()
                .Generate();

            GameInformation = new Faker<GameInformationModel>()
                .RuleFor(g => g.DefaultSaveName, f => f.Lorem.Word())
                .RuleFor(g => g.SaveBackupExtension, f => fac.Create(f.PickRandom<EBackupSaveType>())
                .GetFileExtension())
                .Generate();
        }

        [Test]
        public void ShouldReturnNewSaveNameIfDefaultIsOverridden()
        {
            const string generatedName = "DarkSouls-FatBoySlin";

            string newSaveName = GameInformation.BuildSaveName(generatedName);

            string saveName = generatedName + GameInformation.SaveBackupExtension;

            _ = saveName.Should().Be(newSaveName);
        }

        [Test]
        public void ShouldReturnDefaultSaveNameIfNoNewNameIsProvided()
        {
            string newSaveName = GameInformation.BuildSaveName();

            string defaultSaveName = GameInformation.DefaultSaveName + GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Be(newSaveName);
        }

        [Test]
        public void ShouldReturnDefaultSaveNameIfEmptyStringIsProvided()
        {
            string newSaveName = GameInformation.BuildSaveName(string.Empty);

            string defaultSaveName = GameInformation.DefaultSaveName + GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Be(newSaveName);
        }

        [Test]
        public void ShouldReturnDefaultSaveNameIfNullValueIsProvided()
        {
            string newSaveName = GameInformation.BuildSaveName(null);

            string defaultSaveName = GameInformation.DefaultSaveName + GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Be(newSaveName);
        }
    }
}