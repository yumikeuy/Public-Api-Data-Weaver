using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;
using Weaver.Services.Services.VitaminsCheckers;

namespace Weaver.Services.Interfaces.Services
{
    internal interface IVitaminsChecker
    {
        void SetNext(VitaminsChecker vitaminsChecker);
        void CheckForVitamins(Fruit fruit);
    }
}
