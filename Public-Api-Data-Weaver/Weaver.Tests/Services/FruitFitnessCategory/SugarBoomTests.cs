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
    public class SugarBoomTests
    {
        [Theory]
        [InlineData(10, 50, false)]
        [InlineData(20, 50, true)]
        [InlineData(10, 70, true)]
        public void SetCategory_ShouldHandleSugarAndCaloriesConditions(double sugar, int calories, bool shouldAdd)
        {
            // Arrange
            var checker = new SugarBoom();
            var fruit = new Fruit { Nutritions = new Nutritions { Sugar = sugar, Calories = calories } }; 

            // Act
            checker.SetCategory(fruit);

            // Assert
            if (shouldAdd)
            {
                fruit.FitnessCategories.Should().Contain(FitnessCategories.SugarBoom);
            }
            else
            {
                fruit.FitnessCategories.Should().NotContain(FitnessCategories.SugarBoom);
            }
            
        }

        [Theory]
        [InlineData(10, 50)]
        [InlineData(15, 50)]
        [InlineData(10, 70)]
        public void SetCategory_ShouldCallNextInChain(double sugar, int calories)
        {
            // Arrange
            var checker = new SugarBoom();
            var nextMock = Substitute.For<IFruitFitnessCategoryChecker>();
            checker.SetNext(nextMock);

            var fruit = new Fruit { Nutritions = new Nutritions { Sugar = sugar, Calories = calories } };

            // Act
            checker.SetCategory(fruit);

            // Assert
            nextMock.Received(1).SetCategory(fruit);
        }
    }
}
