using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;
using Weaver.Services.DTOs;
using Weaver.Services.Interfaces.Services;

namespace Weaver.Services.Services
{
    public class FruitTransformator : IFruitTransformator
    {
        private readonly IVitaminsCheckerComposer _vitaminsCheckerComposer;
        private readonly IFruitFitnessCategoryCheckerComposer _fitnessCategoryComposer;

        public FruitTransformator(IVitaminsCheckerComposer vitaminsCheckerComposer, IFruitFitnessCategoryCheckerComposer fitnessCategoryComposer)
        {
            _vitaminsCheckerComposer = vitaminsCheckerComposer;
            _fitnessCategoryComposer = fitnessCategoryComposer;
        }

        public void Transformate(Fruit fruit)
        {
            var n = fruit.Nutritions;
            fruit.ProteinPerCalorie = n.Calories > 0 ? n.Protein / n.Calories : 0;

            _vitaminsCheckerComposer.Check(fruit);
            _fitnessCategoryComposer.Check(fruit);
        }
    }
}
