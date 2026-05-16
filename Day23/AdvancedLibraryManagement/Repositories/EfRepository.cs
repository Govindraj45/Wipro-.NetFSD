using AdvancedLibraryManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace AdvancedLibraryManagement.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
    protected readonly LibraryDbContext Db;

    public EfRepository(LibraryDbContext db)
    {
        Db = db;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await Db.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Db.Set<T>().FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await Db.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        Db.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        Db.Set<T>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await Db.SaveChangesAsync();
    }
}
