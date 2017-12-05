using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DockerSample.Data
{
    public class DataCollection<T> : IDataCollection<T>
    {
        private readonly IMongoCollection<T> collection;

        public DataCollection(IMongoCollection<T> collection)
        {
            this.collection = collection;
        }

        public async Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            var matching = new List<T>();
            using (IAsyncCursor<T> cursor = await collection.FindAsync(filter, cancellationToken: cancellationToken))
            {
                while (await cursor.MoveNextAsync(cancellationToken))
                    matching.AddRange(cursor.Current);
            }
            return matching;
        }
    }
}