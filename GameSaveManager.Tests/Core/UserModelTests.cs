namespace GameSaveManager.Tests.Core
{
    using Bogus;

    using FluentAssertions;

    using GameSaveManager.Core.Models;

    using Xunit;

    public class UserModelTests
    {
        private readonly UserModel ModeloUsuario;

        public UserModelTests()
        {
            ModeloUsuario = new Faker<UserModel>()
                .CustomInstantiator(f => new UserModel(f.Lorem.Word(), f.Lorem.Word()));
        }

        [Fact]
        public void UserModelShouldNotBeNull() => _ = ModeloUsuario.Should().NotBeNull();
    }
}