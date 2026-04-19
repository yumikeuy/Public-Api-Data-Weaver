using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Services.DTOs;

namespace Weaver.Services.Services.VitaminsCheckers
{
    public abstract class VitaminsChecker
    {
        protected VitaminsChecker? next;
        public void SetNext(VitaminsChecker vitaminsChecker)
        {
            if (next is null)
                next = vitaminsChecker;
            else
                next.SetNext(vitaminsChecker);
        }

        public virtual ICollection<char> CheckForVitamins(ExternalFruitDto fruitDto)
        {
            return next?.CheckForVitamins(fruitDto) ?? [];
        }
    }
}
