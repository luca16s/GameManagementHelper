namespace GameSaveManager.Core.Tests.Core
{
    using Bogus;

    using FluentAssertions;

    using GameSaveManager.Core.Models;

    using Xunit;

    public class EnumModelTests
    {
        public EnumModel EnumModelo;

        public EnumModelTests()
        {
            EnumModelo = new Faker<EnumModel>()
                .RuleFor(g => g.Value, f => f.Lorem.Word())
                .RuleFor(g => g.Description, f => f.Lorem.Word())
                .Generate();
        }

        [Fact]
        public void EnumModelShouldNotBeNull() => _ = EnumModelo.Should().NotBeNull();

        [Fact]
        public void EnumValueShouldNotReturnNull() => _ = EnumModelo.Value.Should().NotBeNull();

        [Fact]
        public void EnumDescriptionShouldNotReturnNull() => _ = EnumModelo.Description.Should().NotBeNull();
    }
}