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
        public void Transformate(Fruit fruit)
        {
            fruit.ProteinPerCalorie = fruit.Nutritions.Protein / fruit.Nutritions.Calories;
        }
    }
}
