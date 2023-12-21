using WhiteLagoon.Application.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _context;

        public AmenityRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void Update(Amenity entity)
        {
            var amenity = _context.Amenities.Find(entity.Id);
            amenity.VillaId = entity.VillaId;
            amenity.Name = entity.Name;
            amenity.Description = entity.Description;
            _context.SaveChanges();
        }
    }
}
