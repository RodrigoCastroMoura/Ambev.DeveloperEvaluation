
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
    {
        await _context.Set<SaleItem>().AddAsync(saleItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return saleItem;
    }

    public async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<SaleItem>()
            .Include(si => si.Product)
            .FirstOrDefaultAsync(si => si.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<SaleItem>()
            .Include(si => si.Product)
            .Where(si => si.SaleId == saleId)
            .ToListAsync(cancellationToken);
    }

    public async Task<SaleItem> UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
    {
        _context.Set<SaleItem>().Update(saleItem);
        await _context.SaveChangesAsync(cancellationToken);
        return saleItem;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var saleItem = await GetByIdAsync(id, cancellationToken);
        if (saleItem == null) return false;

        _context.Set<SaleItem>().Remove(saleItem);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
