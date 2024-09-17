﻿using LibraryServer.DataAccess.Data;
using LibraryServer.DataAccess.Entities.Abstractions;
using LibraryServer.DataAccess.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryServer.DataAccess.Repositories;

internal class Repository<T> : IRepository<T> where T : Entity
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _entities;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includedProperties)
    {
        IQueryable<T>? query = _entities.AsQueryable();
        if (includedProperties.Any())
        {
            foreach (var property in includedProperties)
            {
                query = query.Include(property);
            }
        }

        return await query.ElementAtAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _entities.AsQueryable().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter, 
                                            params Expression<Func<T, object>>[] includedProperties)
    {
        IQueryable<T>? query = _entities.AsQueryable();
        if (includedProperties.Any())
        {
            foreach (var property in includedProperties)
            {
                query = query.Include(property);
            }
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _entities.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
    {
        return await _entities.FirstOrDefaultAsync(filter);
    } 
}
