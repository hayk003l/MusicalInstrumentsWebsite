using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext) => RepositoryContext = repositoryContext;

        public IQueryable<T> FindAll(bool trackingChanges) =>
            trackingChanges ? RepositoryContext.Set<T>() 
            : RepositoryContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackingChanges) =>
            trackingChanges ? RepositoryContext.Set<T>().Where(expression) 
            : RepositoryContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => RepositoryContext.Add(entity);

        public void Update(T entity) => RepositoryContext.Update(entity);

        public void Delete(T entity) => RepositoryContext.Remove(entity);

    }
}
