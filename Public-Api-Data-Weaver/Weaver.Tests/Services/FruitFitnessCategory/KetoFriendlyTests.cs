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
    public class KetoFriendlyTests
    {
        [Theory]
        [InlineData(7, 5, 8, false)]
        [InlineData(11, 5, 3, false)]
        [InlineData(11, 3, 5, false)]
        [InlineData(7, 8, 5, true)]
        public void SetCategory_ShouldHandleCarbsAndFatConditions(double carbs, double fat, double sugar, bool shouldAdd)
        {
            // Arrange
            var checker = new KetoFriendly();
            var fruit = new Fruit { Nutritions = new Nutritions { Carbohydrates = carbs, Sugar = sugar, Fat = fat } };

            // Act
            checker.SetCategory(fruit);

            // Assert
            if (shouldAdd)
            {
                fruit.FitnessCategories.Should().Contain(FitnessCategories.KetoFriendly);
            }
            else
            {
                fruit.FitnessCategories.Should().NotContain(FitnessCategories.KetoFriendly);
            }

        }

        [Theory]
        [InlineData(7, 5, 8)]
        [InlineData(11, 5, 3)]
        [InlineData(11, 3, 5)]
        [InlineData(7, 8, 5)]
        public void SetCategory_ShouldCallNextInChain(double carbs, double fat, double sugar)
        {
            // Arrange
            var checker = new KetoFriendly();
            var nextMock = Substitute.For<IFruitFitnessCategoryChecker>();
            checker.SetNext(nextMock);

            var fruit = new Fruit { Nutritions = new Nutritions { Carbohydrates = carbs, Sugar = sugar, Fat = fat } };

            // Act
            checker.SetCategory(fruit);

            // Assert
            nextMock.Received(1).SetCategory(fruit);
        }
    }
}
