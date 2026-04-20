using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;
using Weaver.Services.DTOs;
using Weaver.Services.Interfaces.Services;

namespace Weaver.Services.Services.VitaminsCheckers
{
    internal class VitaminsChecker(char vitamin, HashSet<string> genuses) : IVitaminsChecker
    {
        protected VitaminsChecker? _next;
        public void SetNext(VitaminsChecker vitaminsChecker)
        {
            if (_next is null)
            {
                _next = vitaminsChecker;
            }
            else
            {
                _next.SetNext(vitaminsChecker);
            }
                
        }

        public void CheckForVitamins(Fruit fruit)
        {
            if (fruit is null) return;

            if (genuses.Contains(fruit.Genus))
            {
                fruit.HighVitamins.Add(vitamin);
            }

            _next?.CheckForVitamins(fruit);
        }
    }
}
