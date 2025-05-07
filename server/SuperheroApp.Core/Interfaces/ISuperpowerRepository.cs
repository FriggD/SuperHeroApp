using SuperheroApp.Core.Entities;

namespace SuperheroApp.Core.Interfaces;

public interface ISuperpowerRepository
{
    Task<IEnumerable<Superpower>> GetAllAsync();
    Task<Superpower?> GetByIdAsync(int id);
    Task<Superpower> AddAsync(Superpower superpower);
    Task UpdateAsync(Superpower superpower);
    Task DeleteAsync(int id);
}