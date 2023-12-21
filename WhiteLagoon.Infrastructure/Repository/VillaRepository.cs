using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void Update(Villa entity)
        {
            var villa = _context.Villas.Find(entity.Id);

            villa.Name = entity.Name;
            villa.Description = entity.Description;
            villa.Price = entity.Price;
            villa.Sqm = entity.Sqm;
            villa.Occupancy = entity.Occupancy;
            villa.ImageUrl = entity.ImageUrl;
            _context.SaveChanges();
        }
    }
}
