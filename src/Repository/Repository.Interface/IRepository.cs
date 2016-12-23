using System.Collections.Generic;

namespace Repository.Interface
{
    public interface IRepository<TEnity> where TEnity : class
    {
        IEnumerable<TEnity> GetEntities();
        TEnity GetEntity(int id);

        TEnity Add(TEnity entity);
        void AddRange(IEnumerable<TEnity> entities);

        void Remove(TEnity entity);
        void RemoveRange(IEnumerable<TEnity> entities);
    }
}
