using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DockerSample.Data
{
    public interface IDataCollection<T>
    {
        Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default(CancellationToken));
    }
}