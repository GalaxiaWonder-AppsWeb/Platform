using Microsoft.EntityFrameworkCore.Storage;
using Platform.API.Shared.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Unit of work implementation
/// </summary>
/// <remarks>
///     This class implements the basic operations for a unit of work.
///     It requires the context to be passed in the constructor.
/// </remarks>
/// <see cref="IUnitOfWork" />
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction is not null) return;
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction is null) return;
        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction is null) return;
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }
}
