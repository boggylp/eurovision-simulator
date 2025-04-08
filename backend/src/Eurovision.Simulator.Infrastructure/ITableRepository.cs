using System.Linq.Expressions;

namespace Eurovision.Simulator.Infrastructure;

public interface ITableRepository<T> where T : class
{
    Task<IReadOnlyCollection<T>> GetAll(CancellationToken cancellationToken);
    Task<T> Get(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);
}
