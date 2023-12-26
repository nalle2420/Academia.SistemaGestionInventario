using Academia.SistemaGestionInventario.WApi.Infrastructure.GestionInventario;
using Farsiman.Domain.Core.Standard.Repositories;
using System.Linq.Expressions;

namespace Academia.SistemaGestionInventario.WApi.Infrastructure.Repository
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly GestionInventarioDbContext dbContext;

        public EntityRepository(GestionInventarioDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            dbContext.SaveChanges();

        }

        public IQueryable<TEntity> AsQueryable()
        {
            return dbContext.Set<TEntity>().AsQueryable();

        }

        public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> query)
        {
            return dbContext.Set<TEntity>().FirstOrDefault();
        }

        public List<TEntity> where(Expression<Func<TEntity, bool>> query)
        {
            return dbContext.Set<TEntity>().Where(query).ToList();

        }
    }
}
