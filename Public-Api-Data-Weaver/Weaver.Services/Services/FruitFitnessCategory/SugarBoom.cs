using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;

namespace Weaver.Services.Services.FruitFitnessCategory
{
    internal class SugarBoom : FruitFitnessCategorySetter
    {
        private const double sugarBound = 15;
        private const double caloriesBound = 60;
        public override void SetCategory(Fruit fruit)
        {
            if (fruit.Nutritions == null) return;

            var n = fruit.Nutritions;
            
            if (n.Sugar > sugarBound || n.Calories > caloriesBound)
            {
                fruit.FitnessCategories.Add(FitnessCategories.SugarBoom);
            }

            base.SetCategory(fruit);
        }
    }
}
