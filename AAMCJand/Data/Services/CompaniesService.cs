using AAMCJand.Data.Base;
using AAMCJand.Models;

namespace AAMCJand.Data.Services
{
    public class CompaniesService : EntityBaseRepository<Company>, ICompaniesService
    {
        public CompaniesService(AppDbContext context) : base(context)
        {

        }
    }
}
