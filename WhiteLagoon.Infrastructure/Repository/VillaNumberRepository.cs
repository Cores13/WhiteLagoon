using WhiteLagoon.Application.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaNumberRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void Update(VillaNumber entity)
        {
            var villaNumber = _context.VillaNumbers.Find(entity.Villa_Number);
            villaNumber.VillaId = entity.VillaId;
            villaNumber.SpecialDetails = entity.SpecialDetails;
            _context.SaveChanges();
        }
    }
}
