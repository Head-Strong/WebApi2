using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;

namespace Repository.Implementation
{
    public class Repository<TEnity> : IRepository<TEnity> where TEnity : class
    {
        private readonly DbContext _dbContext;
        private readonly IAutoMapperConfigMapper _autoMapperConfigMapper;

        protected Repository(DbContext dbContext, IAutoMapperConfigMapper autoMapperConfigMapper)
        {
            _dbContext = dbContext;
            _autoMapperConfigMapper = autoMapperConfigMapper;
        }

        public IEnumerable<TEnity> GetEntities()
        {
            return _dbContext.Set<TEnity>().ToList();
        }

        public TEnity Add(TEnity entity)
        {
            var addedEntity = _dbContext.Set<TEnity>().Add(entity);

            return addedEntity;
        }

        public void Remove(TEnity entity)
        {
            _dbContext.Set<TEnity>().Remove(entity);
        }

        public TEnity GetEntity(int id)
        {
            return _dbContext.Set<TEnity>().Find(id);
        }

        public void AddRange(IEnumerable<TEnity> entities)
        {
            _dbContext.Set<TEnity>().AddRange(entities);
        }

        public void RemoveRange(IEnumerable<TEnity> entities)
        {
            _dbContext.Set<TEnity>().RemoveRange(entities);
        }
    }
}