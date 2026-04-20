using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;
using Weaver.Services.Interfaces.Services;
using Weaver.Services.Services.VitaminsCheckers;

namespace Weaver.Services.Services.FruitFitnessCategory
{
    internal abstract class FruitFitnessCategorySetter : IFruitFitnessCategoryChecker
    {
        protected IFruitFitnessCategoryChecker? _next;

        public void SetNext(IFruitFitnessCategoryChecker fruitFitnessCategorySetter)
        {
            if (_next is null)
            {
                _next = fruitFitnessCategorySetter;
            }
            else
            {
                _next.SetNext(fruitFitnessCategorySetter);
            }

        }

        public virtual void SetCategory(Fruit fruit)
        {
            if (fruit is null) return;

            _next?.SetCategory(fruit);
        }
    }
}
