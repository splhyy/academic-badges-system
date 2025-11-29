using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly InMemoryDbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(InMemoryDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> ObterPorIdAsync(Guid id) => await _dbSet.FindAsync(id);
    
    public virtual async Task<IEnumerable<T>> ObterTodosAsync() => await _dbSet.ToListAsync();
    
    public virtual async Task AdicionarAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public virtual async Task AtualizarAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public virtual async Task RemoverAsync(Guid id)
    {
        var entity = await ObterPorIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    
    public virtual async Task<bool> ExisteAsync(Guid id) => await _dbSet.FindAsync(id) != null;
}