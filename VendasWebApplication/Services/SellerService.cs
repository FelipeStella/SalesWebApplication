using SalesWebApplication.Data;
using SalesWebApplication.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SalesWebApplication.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebApplication.Services
{
  public class SellerService
  {
    private readonly SalesWebApplicationContext _context;
    public SellerService(SalesWebApplicationContext context)
    {
      _context = context;
    }
    public async Task<List<Seller>> FindAllAsync()
    {
      return await _context.Seller.ToListAsync();
    }
    public async Task InsertAsync(Seller seller)
    {
      _context.Seller.Add(seller);
      await _context.SaveChangesAsync();
    }
    public async Task<Seller> FindByIdAsync(int Id)
    {
      return await _context.Seller.Include(s => s.Department).FirstOrDefaultAsync(s => s.Id == Id);
    }
    public async Task RemoveAsync(int id)
    {
      try
      {
        var seller = await _context.Seller.FindAsync(id);
        _context.Remove(seller);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException dbue)
      {
        throw new IntegrityException(dbue.Message);
      }

    }
    public async Task UpdateAsync(Seller seller)
    {
      bool hasAny = await _context.Seller.AnyAsync(s => s.Id == seller.Id);

      if (!hasAny) {
        throw new NotFoundException("Id Not Found");
      }

      try
      {
        _context.Update(seller);
        await _context.SaveChangesAsync();
      }
      catch(DbUpdateConcurrencyException e)
      {
        throw new DbConcurrencyException(e.Message);
      }
      
    }
  }
}
