using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;

namespace Weaver.Services.Services.FruitFitnessCategory
{
    public class ProteinBoost : FruitFitnessCategorySetter
    {
        private const double ppcBound = 0.05;
        private const double proteinBound = 4;
        public override void SetCategory(Fruit fruit)
        {
            if (fruit.Nutritions == null) return;

            var n = fruit.Nutritions;

            if (fruit.ProteinPerCalorie > ppcBound || n.Protein > proteinBound)
            {
                fruit.FitnessCategories.Add(FitnessCategories.ProteinBoost);
            }

            base.SetCategory(fruit);
        }
    }
}
