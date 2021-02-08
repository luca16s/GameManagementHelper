namespace GameSaveManager.Tests.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using FluentAssertions;

    using GameSaveManager.Core.Models;
    using GameSaveManager.Core.Utils;

    using Xunit;

    public class EnumExtensionsTests
    {
        [Fact]
        public void DescriptionShouldReturnValueOfDescriptionAnnotation()
        {
            string description = ETesteEnum.TESTE_1.Description();

            _ = description.Should().Be("TESTE 1");
        }

        [Fact]
        public void DescriptionShouldReturnEnumWithUnderscoreReplaced()
        {
            string description = ETesteEnum.TESTE_3.Description();

            _ = description.Should().Be("Teste 3");
        }

        [Fact]
        public void GetAllValuesAndDescriptionsShouldThrowAnExceptionWhenTypeIsNotEnum()
        {
            object objeto = new();

            Action act = () => objeto?.GetType().GetAllValuesAndDescriptions();

            _ = act.Should().Throw<ArgumentException>()
                .And.Message.Should().Be("Tipo esperado: Enum. Tipo recebido: Object.");
        }

        [Fact]
        public void GetAllValuesAndDescriptionsShouldReturnListOfItems()
        {
            var listaModelo = new EnumModel[]
            {
                new EnumModel { Value = ETesteEnum.TESTE_1, Description ="TESTE 1" },
                new EnumModel { Value = ETesteEnum.TESTE_2, Description ="TESTE 2" },
                new EnumModel { Value = ETesteEnum.TESTE_3, Description ="Teste 3" },
            };

            IEnumerable<EnumModel> retorno = ETesteEnum.TESTE_1.GetType().GetAllValuesAndDescriptions();

            _ = retorno.Should().NotBeEmpty()
                .And.HaveCount(3)
                .And.OnlyHaveUniqueItems()
                .And.BeEquivalentTo(listaModelo);
        }
    }

    public enum ETesteEnum
    {
        [Description("TESTE 1")]
        TESTE_1,

        [Description("TESTE 2")]
        TESTE_2,

        TESTE_3
    }
}