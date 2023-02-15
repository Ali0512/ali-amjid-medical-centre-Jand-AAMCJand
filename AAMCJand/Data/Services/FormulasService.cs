using AAMCJand.Data.Base;
using AAMCJand.Models;

namespace AAMCJand.Data.Services
{
    public class FormulasService : EntityBaseRepository<Formula>, IFormulasService
    {
        public FormulasService(AppDbContext context) : base(context) { }
    }
}
