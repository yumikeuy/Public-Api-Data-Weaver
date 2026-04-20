using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;

namespace Weaver.Services.Interfaces.Services
{
    public interface IFruitFitnessCategoryCheckerComposer
    {
        public void Check(Fruit fruit);
    }
}
