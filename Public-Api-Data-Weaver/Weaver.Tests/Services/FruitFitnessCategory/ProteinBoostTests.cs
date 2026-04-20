using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Weaver.Models.Entities;
using Weaver.Services.Interfaces.Services;
using Weaver.Services.Services.FruitFitnessCategory;

namespace Weaver.Tests.Services.FruitFitnessCategory
{
    public class ProteinBoostTests
    {
        [Theory]
        [InlineData(0.03, 3, false)]
        [InlineData(0.03, 5, true)]
        [InlineData(0.06, 3, true)]
        [InlineData(0.06, 6, true)]
        public void SetCategory_ShouldHandleProteinConditions(double ppc, double protein, bool shouldAdd)
        {
            // Arrange
            var checker = new ProteinBoost();
            var fruit = new Fruit { ProteinPerCalorie = ppc, Nutritions = new Nutritions { Protein = protein } };

            // Act
            checker.SetCategory(fruit);

            // Assert
            if (shouldAdd)
            {
                fruit.FitnessCategories.Should().Contain(FitnessCategories.ProteinBoost);
            }
            else
            {
                fruit.FitnessCategories.Should().NotContain(FitnessCategories.ProteinBoost);
            }

        }

        [Theory]
        [InlineData(0.03, 3)]
        [InlineData(0.03, 5)]
        [InlineData(0.05, 3)]
        public void SetCategory_ShouldCallNextInChain(double ppc, int protein)
        {
            // Arrange
            var checker = new ProteinBoost();
            var nextMock = Substitute.For<IFruitFitnessCategoryChecker>();
            checker.SetNext(nextMock);

            var fruit = new Fruit { ProteinPerCalorie = ppc, Nutritions = new Nutritions { Protein = protein } };

            // Act
            checker.SetCategory(fruit);

            // Assert
            nextMock.Received(1).SetCategory(fruit);
        }
    }
}
