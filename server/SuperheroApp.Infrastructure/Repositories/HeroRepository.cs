using Microsoft.EntityFrameworkCore;
using SuperheroApp.Core.Entities;
using SuperheroApp.Core.Interfaces;
using SuperheroApp.Infrastructure.Data;

namespace SuperheroApp.Infrastructure.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly AppDbContext _context;

        public HeroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hero>> GetAllAsync()
        {
            return await _context.Heroes
                .Include(h => h.HeroSuperpowers)
                .ThenInclude(hs => hs.Superpower)
                .ToListAsync();
        }

        public async Task<Hero?> GetByIdAsync(int id)
        {
            return await _context.Heroes
                .Include(h => h.HeroSuperpowers)
                .ThenInclude(hs => hs.Superpower)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Hero> AddAsync(Hero hero)
        {
            await _context.Heroes.AddAsync(hero);
            await _context.SaveChangesAsync();
            return hero;
        }

        public async Task UpdateAsync(Hero hero)
        {
            _context.Heroes.Update(hero);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero != null)
            {
                _context.Heroes.Remove(hero);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Heroes.AnyAsync(h => h.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> ExistsByNameExceptIdAsync(string name, int id)
        {
            return await _context.Heroes.AnyAsync(h => h.Name.ToLower() == name.ToLower() && h.Id != id);
        }
    }
}