using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Application.Interfaces
{
    public interface IVillaNumberRepository: IRepository<VillaNumber>
    {
        void Update(VillaNumber entity);
    }
}
