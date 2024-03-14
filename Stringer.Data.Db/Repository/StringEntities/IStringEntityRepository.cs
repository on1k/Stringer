using Stringer.Data.Domain.Models;

namespace Stringer.Data.Db.Repository.StringEntities;

public interface IStringEntityRepository
{
    Task<IEnumerable<StringEntity?>> GetStringEntities(string ownerId);
    Task<StringEntity?> GetStringEntity(string id);
    Task<StringEntity> CreateStringEntity(string text, string ownerId);
    Task UpdateStringEntity(StringEntity stringEntity);
    Task DeleteStringEntity(string id);
}