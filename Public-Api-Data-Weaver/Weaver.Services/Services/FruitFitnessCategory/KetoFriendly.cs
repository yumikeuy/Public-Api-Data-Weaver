using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;

namespace Weaver.Services.Services.FruitFitnessCategory
{
    public class KetoFriendly : FruitFitnessCategorySetter
    {
        private const double carbsBound = 10;
        public override void SetCategory(Fruit fruit)
        {
            if (fruit.Nutritions == null) return;

            var n = fruit.Nutritions;

            if (n.Carbohydrates < carbsBound && n.Fat > n.Sugar)
            {
                fruit.FitnessCategories.Add(FitnessCategories.KetoFriendly);
            }

            base.SetCategory(fruit);
        }
    }
}
