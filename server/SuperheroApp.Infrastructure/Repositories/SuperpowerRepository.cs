using Microsoft.EntityFrameworkCore;
using SuperheroApp.Core.Entities;
using SuperheroApp.Core.Interfaces;
using SuperheroApp.Infrastructure.Data;

namespace SuperheroApp.Infrastructure.Repositories
{
    public class SuperpowerRepository : ISuperpowerRepository
    {
        private readonly AppDbContext _context;

        public SuperpowerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Superpower>> GetAllAsync()
        {
            return await _context.Superpowers.ToListAsync();
        }

        public async Task<Superpower?> GetByIdAsync(int id)
        {
            return await _context.Superpowers.FindAsync(id);
        }

        public async Task<Superpower> AddAsync(Superpower superpower)
        {
            await _context.Superpowers.AddAsync(superpower);
            await _context.SaveChangesAsync();
            return superpower;
        }

        public async Task UpdateAsync(Superpower superpower)
        {
            _context.Superpowers.Update(superpower);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var superpower = await _context.Superpowers.FindAsync(id);
            if (superpower != null)
            {
                _context.Superpowers.Remove(superpower);
                await _context.SaveChangesAsync();
            }
        }
    }
}