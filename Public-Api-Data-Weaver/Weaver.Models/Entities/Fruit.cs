using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weaver.Models.Entities
{
    public class Fruit : BaseEntity
    {
        public string Genus { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string Order { get; set; } = string.Empty;
        public Nutritions Nutritions { get; set; } = null!;
        public HashSet<string> FitnessCategories { get; set; } = [];
        public HashSet<char> HighVitamins { get; set; } = [];
        public double ProteinPerCalorie { get; set; }
    }
}
