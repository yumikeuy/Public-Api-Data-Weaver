using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weaver.Models.Entities
{
    public class Nutritions : BaseEntity
    {
        public double Carbohydrates { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public int Calories { get; set; }
        public double Sugar { get; set; }
        public Fruit Fruit { get; set; } = null!;
        public Guid FruitId { get; set; }
    }
}
