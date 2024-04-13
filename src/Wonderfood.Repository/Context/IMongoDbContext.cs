using MongoDB.Driver;

namespace Wonderfood.Repository.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoDatabase GetDatabase();
    }
}