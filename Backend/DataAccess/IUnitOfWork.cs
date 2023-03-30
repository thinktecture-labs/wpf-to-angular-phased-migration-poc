using System.Threading;
using System.Threading.Tasks;

namespace Backend.DataAccess;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}