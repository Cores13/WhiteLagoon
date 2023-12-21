using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Application.Interfaces
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
        void Update(Amenity entity);
    }
}
