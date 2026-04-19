using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weaver.Services.DTOs
{
    public class ExternalFruitDto
    {
        public int Id { get; set; }
        public string Genus { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Family { get; set; } = null!;
        public string Order { get; set; } = null!;
        public ExternalNutritionsDto Nutritions { get; set; } = null!;
    }
}
