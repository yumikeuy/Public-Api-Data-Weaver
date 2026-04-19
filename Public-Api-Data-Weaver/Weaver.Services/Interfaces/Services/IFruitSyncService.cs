using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weaver.Services.Interfaces.Services
{
    public interface IFruitSyncService
    {
        Task<int> SyncFruitsAsync();
    }
}
