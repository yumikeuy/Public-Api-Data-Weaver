using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;
using Weaver.Services.Interfaces.Services;

namespace Weaver.Services.Services.FruitFitnessCategory
{
    public class FitnessCategoryComposer : IFruitFitnessCategoryCheckerComposer
    {
        private readonly IFruitFitnessCategoryChecker _checker;

        public FitnessCategoryComposer()
        {
            _checker =       new KetoFriendly();
            _checker.SetNext(new ProteinBoost());
            _checker.SetNext(new SugarBoom());
        }

        public void Check(Fruit fruit)
        {
            _checker.SetCategory(fruit);
        }
    }
}
