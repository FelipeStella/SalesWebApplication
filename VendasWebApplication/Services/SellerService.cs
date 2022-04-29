using SalesWebApplication.Data;
using SalesWebApplication.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SalesWebApplication.Services
{
  public class SellerService
  {
    private readonly SalesWebApplicationContext _context;
    public SellerService(SalesWebApplicationContext context)
    {
      _context = context;
    }
    public IEnumerable<Seller> FindAll()
    {
      return _context.Seller.ToList();
    }
    public void Insert(Seller seller)
    {
      _context.Seller.Add(seller);
      _context.SaveChanges();
    }
    public Seller FindById(int Id)
    {
      return _context.Seller.Include(s => s.Department).FirstOrDefault(s => s.Id == Id);
    }
    public void Remove(int id)
    {
      var seller = _context.Seller.Find(id);
      _context.Remove(seller);
    }
  }
}
