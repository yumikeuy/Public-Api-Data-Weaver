using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;
using Weaver.Services.Interfaces.Services;

namespace Weaver.Services.Services.VitaminsCheckers
{
    internal class DefaultVitaminsCheckerComposer : IVitaminsCheckerComposer
    {
        private IVitaminsChecker _vitaminsChecker;

        public DefaultVitaminsCheckerComposer() 
        {
            _vitaminsChecker =       new VitaminsChecker('A', ["Elaeis", "Psidium", "Cucurbita", "Mangifera", "Prunus"]);
            _vitaminsChecker.SetNext(new VitaminsChecker('B', ["Adansonia", "Persea", "Fragaria", "Musa", "Citrus"]));
            _vitaminsChecker.SetNext(new VitaminsChecker('C', ["Myrciaria", "Malpighia", "Phyllanthus", "Psidium", "Actinidia", "Rosa", "Citrus"]));
            _vitaminsChecker.SetNext(new VitaminsChecker('D', ["Saccharomyces"]));
            _vitaminsChecker.SetNext(new VitaminsChecker('E', ["Hippophae", "Persea", "Actinidia", "Elaeis", "Rubus"]));
            _vitaminsChecker.SetNext(new VitaminsChecker('K', ["Persea", "Actinidia", "Ficus", "Prunus", "Vitis"]));
        }

        public void Check(Fruit fruit)
        {
            if (fruit is null) return;

            _vitaminsChecker.CheckForVitamins(fruit);
        }
    }
}
