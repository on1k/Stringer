using Microsoft.EntityFrameworkCore;
using Stringer.Data.Domain.Models;

namespace Stringer.Data.Db.Repository.StringEntities;

public class StringEntityRepository : IStringEntityRepository, IDisposable, IAsyncDisposable
{
    private DataContext _context;
    public StringEntityRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<StringEntity?>> GetStringEntities(string ownerId)
    {
        return await _context.StringEntities.Where(t => t.OwnerId == ownerId).ToListAsync();
    }

    public async Task<StringEntity?> GetStringEntity(string id)
    {
        return await _context.StringEntities.FindAsync(id);
    }

    public async Task<StringEntity> CreateStringEntity(string text, string ownerId)
    {
        var newStringEntity = new StringEntity { OwnerId = ownerId, Text = text };
        await _context.StringEntities.AddAsync(newStringEntity);
        await _context.SaveChangesAsync();
        return newStringEntity;
    }

    public async Task UpdateStringEntity(StringEntity stringEntity)
    {
        _context.Entry<StringEntity>(stringEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStringEntity(string id)
    {
        var existStringEntity = await _context.StringEntities.FindAsync(id);
        _context.StringEntities.Remove(existStringEntity);
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}