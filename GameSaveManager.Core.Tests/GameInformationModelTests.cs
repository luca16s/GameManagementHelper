namespace GameSaveManager.Core.Tests
{
    using System;

    using Bogus;

    using FluentAssertions;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Models;
    using GameSaveManager.Core.Services;
    using GameSaveManager.Core.Utils;

    using NUnit.Framework;

    public class GameInformationModelTests
    {
        private GameInformationModel GameInformation;

        [SetUp]
        public void Setup()
        {
            BackupFactory fac = new Faker<BackupFactory>()
                .Generate();

            GameInformation = new Faker<GameInformationModel>()
                .RuleFor(g => g.DefaultSaveName, f => f.Lorem.Word())
                .RuleFor(g => g.SaveBackupExtension, f => fac.Create(f.PickRandom<EBackupSaveType>()).GetFileExtension())
                .RuleFor(g => g.DefaultSaveExtension, f => f.Lorem.Word())
                .Generate();
        }

        [Test]
        public void ShouldReturnNewSaveNameIfDefaultIsOverridden()
        {
            const string generatedName = "DarkSouls-FatBoySlin";

            string newSaveName = GameInformation.BuildSaveName(generatedName);

            string saveName = generatedName + "." + GameInformation.SaveBackupExtension;

            _ = saveName.Should().Be(newSaveName);
        }

        [Test]
        public void ShouldReturnDefaultSaveNameIfNoNewNameIsProvided()
        {
            string newSaveName = GameInformation.BuildSaveName();

            string defaultSaveName = GameInformation.DefaultSaveName + "." + GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Be(newSaveName);
        }

        [Test]
        public void ShouldReturnDefaultSaveNameIfEmptyStringIsProvided()
        {
            string newSaveName = GameInformation.BuildSaveName(string.Empty);

            string defaultSaveName = GameInformation.DefaultSaveName + "." + GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Be(newSaveName);
        }

        [Test]
        public void ShouldReturnDefaultSaveNameIfNullValueIsProvided()
        {
            string newSaveName = GameInformation.BuildSaveName(null);

            string defaultSaveName = GameInformation.DefaultSaveName + "." + GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Be(newSaveName);
        }

        [Test]
        public void ShouldSetDefaultIfSaveExtensionIsNotInBackupSaveTypeEnum()
        {
            const string saveExtension = "rar";

            _ = GameInformation.Invoking(g => g.SetSaveBackupExtension(saveExtension)).Should().Throw<NotSupportedException>().WithMessage("Extensão de arquivo não suportada.");
        }

        [Test]
        public void ShouldSetSaveExtensionIfValidExtensionIsPrivided()
        {
            string saveExtension = new Faker().Make(1, () => new Faker().PickRandom<EBackupSaveType>().Description())[0];

            GameInformation.SetSaveBackupExtension(saveExtension);

            string defaultSaveName = GameInformation.SaveBackupExtension;

            _ = defaultSaveName.Should().Be(saveExtension);
        }

        [Test]
        public void ShouldReturnDefaultSaveName()
        {
            string expected = $"{GameInformation.DefaultSaveName}.{GameInformation.DefaultSaveExtension}";

            string saveName = GameInformation.RestoreSaveName();

            _ = saveName.Should().Be(expected);
        }
    }
}