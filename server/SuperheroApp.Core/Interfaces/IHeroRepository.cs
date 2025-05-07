using SuperheroApp.Core.Entities;

namespace SuperheroApp.Core.Interfaces;

public interface IHeroRepository
{
    Task<IEnumerable<Hero>> GetAllAsync();
    Task<Hero?> GetByIdAsync(int id);
    Task<Hero> AddAsync(Hero hero);
    Task UpdateAsync(Hero hero);
    Task DeleteAsync(int id);
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> ExistsByNameExceptIdAsync(string name, int id);
}