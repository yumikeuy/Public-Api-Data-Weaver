using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver.Models.Entities;
using Weaver.Services.DTOs;

namespace Weaver.Services.Interfaces.Services
{
    public interface IFruitTransformator
    {
        void Transformate(Fruit fruit);
    }
}
