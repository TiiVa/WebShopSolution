using WebShop;

namespace WebShopSolution.DataAccess.RepositoryInterfaces;

public interface IRepository<TEntity, TId> where TEntity : IEntity<TId>
{
	Task <TEntity> GetByIdAsync(TId id);
	Task<IEnumerable<TEntity>> GetAllAsync();
	Task AddAsync(TEntity entity);
	Task UpdateAsync(TEntity entity, int id);
	Task DeleteAsync(TId id);
}