using AAMCJand.Data.Base;
using AAMCJand.Models;

namespace AAMCJand.Data.Services
{
    public class StoresService : EntityBaseRepository<Store>, IStoresService
    {
        public StoresService(AppDbContext context) : base(context)
        {

        }
    }
}
