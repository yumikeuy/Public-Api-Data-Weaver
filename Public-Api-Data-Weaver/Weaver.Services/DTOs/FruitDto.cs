using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;

namespace Weaver.API.DTOs
{
    public class FruitDto
    {
        public Guid Id { get; set; }
        public string Genus { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Family { get; set; } = null!;
        public string Order { get; set; } = null!;
        public double ProteinPerCalorie { get; set; }
        public string FitnessCategory { get; set; } = null!;
        public NutritionsDto Nutritions { get; set; } = null!;
    }
}
