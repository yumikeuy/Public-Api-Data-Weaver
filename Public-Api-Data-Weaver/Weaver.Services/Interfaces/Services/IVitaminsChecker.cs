using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;
using Weaver.Services.Services.VitaminsCheckers;

namespace Weaver.Services.Interfaces.Services
{
    public interface IVitaminsChecker
    {
        public void SetNext(VitaminsChecker vitaminsChecker);
        public void CheckForVitamins(Fruit fruit);
    }
}
