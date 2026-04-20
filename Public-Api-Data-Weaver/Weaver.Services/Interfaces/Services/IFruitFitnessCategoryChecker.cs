using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;

namespace Weaver.Services.Interfaces.Services
{
    public interface IFruitFitnessCategoryChecker
    {
        void SetNext(IFruitFitnessCategoryChecker next);
        void SetCategory(Fruit fruit);
    }
}
