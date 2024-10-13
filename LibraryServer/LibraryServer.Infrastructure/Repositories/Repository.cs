﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using LibraryServer.Domain.Common.Models;

namespace LibraryServer.Domain.Abstactions;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _entities;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includedProperties)
    {
        IQueryable<T>? query = _entities.AsQueryable();

        foreach (var property in includedProperties)
        {
            query = query.Include(property);
        }

        var entity = await query.FirstOrDefaultAsync(e => e.Id == id);
        return entity;
    }

    public async Task<IEnumerable<T>> ListAllAsync()
    {
        var entities = await _entities.AsQueryable().ToListAsync();
        return entities;
    }

    public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? filter = null,
                                                params Expression<Func<T, object>>[] includedProperties)
    {
        IQueryable<T>? query = _entities.AsQueryable();
        foreach (var property in includedProperties)
        {
            query = query.Include(property);
        }

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        var entities = await query.ToListAsync();
        return entities;
    }

    public async Task<PaginatedListModel<T>> ListWithPaginationAsync(int pageNo, int pageSize,
                                                                     Expression<Func<T, bool>>? filter = null,
                                                                     params Expression<Func<T, object>>[] includedProperties)
    {
        var query = _entities.AsQueryable();

        foreach (var property in includedProperties)
        {
            query = query.Include(property);
        }

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        int count = await query.CountAsync();

        var entities = await query.OrderBy(e => e.Id)
                                  .Skip((pageNo - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

        var data = new PaginatedListModel<T>
        {
            Items = entities,
            CurrentPage = pageNo,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };

        return data;
    }

    public async Task<T> AddAsync(T entity)
    {
        var newEntity = await _dbContext.Set<T>().AddAsync(entity);
        return newEntity.Entity;
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
        var entity = await _entities.FirstOrDefaultAsync(filter);
        return entity;
    } 
}
